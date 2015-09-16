using System;
using System.Collections.Generic;
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
        private List<int> _ignorePIDs;

        private enum ExpectedDllSizes
        {
            RGSC3722 = 70718464,
            RGSC3934 = 70944768,
            Steam3934 = 71725056,
            RGSC4631 = 70275072
        }

        public GameMemory()
        {
            _ignorePIDs = new List<int>();
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
            Process game = Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "GTA5" && !p.HasExited && !_ignorePIDs.Contains(p.Id));
            if (game == null)
                return null;

            if (game.MainModule.ModuleMemorySize == (int)ExpectedDllSizes.RGSC3722)
                _isLoadingPtr = new DeepPointer(0x2153C30);

            else if (game.MainModule.ModuleMemorySize == (int)ExpectedDllSizes.RGSC3934)
                _isLoadingPtr = new DeepPointer(0x21C94C0);

            else if (game.MainModule.ModuleMemorySize == (int)ExpectedDllSizes.Steam3934)
                _isLoadingPtr = new DeepPointer(0x21CC740);

            else if (game.MainModule.ModuleMemorySize == (int)ExpectedDllSizes.RGSC4631)
                _isLoadingPtr = new DeepPointer(0x21D3DB0);

            else
            {
                MessageBox.Show("Unexpected game version. Steam 393.4 or RGSC 463.1/393.4/372.2 is currently required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ignorePIDs.Add(game.Id);
                return null;
            }

            return game;
        }
    }
}
