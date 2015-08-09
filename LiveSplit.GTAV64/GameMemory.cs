using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.GTAV64
{
    class GameMemory
    {
        public const int SLEEP_TIME = 15;

        public event EventHandler OnLoadStart;
        public event EventHandler OnLoadEnd;
        public event EventHandler OnGameExit;

        private Task _thread;
        private CancellationTokenSource _cancelSource;
        private SynchronizationContext _uiThread;

        private DeepPointer _isLoadingPtr;

        public GameMemory()
        {
            _isLoadingPtr = new DeepPointer(0x2153C30);
            // _isLoadingPtr = new DeepPointer(0x2154090);
        }

        public void StartMonitoring()
        {
            if (_thread != null && _thread.Status == TaskStatus.Running)
            {
                throw new InvalidOperationException();
            }
            if (!(SynchronizationContext.Current is WindowsFormsSynchronizationContext))
            {
                throw new InvalidOperationException("SynchronizationContext.Current is not a UI thread.");
            }

            _uiThread = SynchronizationContext.Current;
            _cancelSource = new CancellationTokenSource();
            _thread = Task.Factory.StartNew(MemoryReadThread);
        }

        public void Stop()
        {
            if (_cancelSource == null || _thread == null || _thread.Status != TaskStatus.Running)
            {
                return;
            }

            _cancelSource.Cancel();
            _thread.Wait();
        }
        void MemoryReadThread()
        {
            Trace.WriteLine("[NoLoads] MemoryReadThread");

            while (!_cancelSource.IsCancellationRequested)
            {
                try
                {
                    Trace.WriteLine("[NoLoads] Waiting for GTA5.exe...");

                    Process game;
                    while ((game = GetGameProcess()) == null)
                    {
                        Thread.Sleep(250);
                        if (_cancelSource.IsCancellationRequested)
                        {
                            return;
                        }
                    }

                    Trace.WriteLine("[NoLoads] Got GTA5.exe!");

                    uint frameCounter = 0;
                    bool prevIsLoading = false;
                    bool firstUpdate = true;

                    while (!game.HasExited)
                    {
                        bool isLoading;
                        _isLoadingPtr.Deref(game, out isLoading);

                        if (isLoading != prevIsLoading || firstUpdate)
                        {
                            Trace.WriteLine($"[NoLoads] Load {(isLoading ? "start" : "end")} - {frameCounter}");
                            if (isLoading)
                            {
                                FireEvent(OnLoadStart);
                            }
                            else
                            {
                                FireEvent(OnLoadEnd);
                            }
                            firstUpdate = false;
                        }

                        frameCounter++;
                        prevIsLoading = isLoading;

                        Thread.Sleep(SLEEP_TIME);

                        if (_cancelSource.IsCancellationRequested)
                        {
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                    Thread.Sleep(1000);
                }

                FireEvent(OnGameExit);
            }
        }

        void FireEvent(Delegate handler, params object[] args) => _uiThread.Post(d => handler?.DynamicInvoke(args), null);

        void FireEvent(EventHandler handler) => FireEvent(handler, this, EventArgs.Empty);

        Process GetGameProcess()
        {
            Process game = Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "GTA5" && !p.HasExited);
            if (game == null)
                return null;

            return game;
        }
    }
}
