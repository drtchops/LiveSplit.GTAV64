using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.GTAV64
{
    public partial class Form1 : Form
    {
        GameMemory _gameMemory;

        TcpClient _client;
        Process _launcher;

        Task _connectionTask;
        Task _connectionCheckTask;
        SynchronizationContext _uiThread;

        Settings _settings;

        public Form1()
        {
            InitializeComponent();
            Version currentVer = Assembly.GetExecutingAssembly().GetName().Version;
            labelVersion.Text = $"{currentVer.Major}.{currentVer.Minor}.{currentVer.Build}";
            this.Disposed += Dispose;
            this.Icon = Properties.Resources.Splr_gtav;

            CheckArguments();

            _settings = new Settings(this);

            _uiThread = SynchronizationContext.Current;
            if (_settings.CheckUpdates)
                Task.Factory.StartNew(CheckUpdate);
            _connectionTask = Task.Factory.StartNew(TryToConnect);
            _connectionCheckTask = Task.Factory.StartNew(CheckClientConnection);

            _gameMemory = new GameMemory();
            _gameMemory.OnLoadStart += _gameMemory_OnLoadStart;
            _gameMemory.OnLoadEnd += _gameMemory_OnLoadEnd;
            _gameMemory.OnGameExit += _gameMemory_OnGameExit;
            _gameMemory.StartMonitoring();
        }

        private void _gameMemory_OnGameExit(object sender, EventArgs e)
        {
            _gameMemory_OnLoadStart(this, EventArgs.Empty);
            lIsLoadingValue.Text = "-    ";
            lIsLoadingValue.ForeColor = SystemColors.ControlText;
        }

        private void _gameMemory_OnLoadStart(object sender, EventArgs e)
        {
            SetIsLoadingLabel(true);
            SendCommand("pausegametime");
        }

        private void _gameMemory_OnLoadEnd(object sender, EventArgs e)
        {
            SetIsLoadingLabel(false);
            SendCommand("unpausegametime");
        }

        void SetIsLoadingLabel(bool value)
        {
            if (value)
            {
                lIsLoadingValue.Text = "yes";
                lIsLoadingValue.ForeColor = Color.Green;
            }
            else
            {
                lIsLoadingValue.Text = "no ";
                lIsLoadingValue.ForeColor = Color.Red;
            }
        }

        void CheckArguments()
        {
            for (int i = 0; i < Program.args.Length; i++)
            {
                string arg = Program.args[i];

                if (arg == "-launcherid" && i + 1 < Program.args.Length)
                {
                    int id;
                    if (int.TryParse(Program.args[i + 1], out id))
                    {
                        try
                        {
                            _launcher = Process.GetProcessById(id);
                        }
                        catch (ArgumentException) { }
                    }
                }
                else if (arg == "-nogui")
                {
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.ShowInTaskbar = false;
                    this.Load += (s, e) =>
                    {
                        this.Size = new Size(0, 0);
                    };
                }
            }
        }

        void SendCommand(string str)
        {
            if (_client != null)
            {
                try
                {
                    byte[] bytes = ASCIIEncoding.ASCII.GetBytes(str + "\r\n");
                    Trace.WriteLine("Sending: " + str);
                    _client.GetStream().Write(bytes, 0, bytes.Length);
                }
                catch (Exception) { }
            }
        }

        void TryToConnect()
        {
            _uiThread.Send(d =>
            {
                this.SetLinkStatus(false);
            }, null);

            while (_client == null)
            {
                if (_launcher != null && _launcher.HasExited)
                    Application.Exit();

                try
                {
                    _client = new TcpClient(_settings.SERVER_IP, _settings.Port);
                }
                catch (SocketException) { }
                Thread.Sleep(500);
            }

            _uiThread.Send(d =>
            {
                this.SetLinkStatus(true);
            }, null);
        }

        void CheckClientConnection()
        {
            while (true)
            {
                if (_client != null && _connectionTask.IsCompleted)
                {
                    try
                    {
                        if (_client.GetStream().Read(new byte[] { 0 }, 0, 1) == 0)
                            throw new System.IO.IOException("Couldn't read from the stream.");
                    }
                    catch (Exception)
                    {
                        if (_client != null)
                        {
                            _client.Close();
                            _client = null;
                        }
                        _connectionTask = Task.Factory.StartNew(TryToConnect);
                    }
                }
                Thread.Sleep(500);
            }
        }

        public void SetLinkStatus(bool status)
        {
            if (status)
                this.pictureBox1.Image = Properties.Resources.tick;
            else
                this.pictureBox1.Image = Properties.Resources.cross;
        }

        void CheckUpdate()
        {
            Version lastVer = new Version(0, 0, 0, 0);
            try
            {
                lastVer = Updater.Check();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }

            if (lastVer > Assembly.GetExecutingAssembly().GetName().Version)
            {
                DialogResult result = MessageBox.Show(this, "A new version is available.\nDo you want to update?", lastVer + " update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    Process.Start("https://github.com/DrTChops/LiveSplit.GTAV64/releases/latest");
                }
            }
        }

        void Dispose(Object sender, EventArgs e)
        {
            if (_gameMemory != null)
            {
                _gameMemory.Stop();
            }

            if (_client != null)
            {
                SendCommand("unpausegametime");
                _client.Close();
            }

            if (_settings != null)
                _settings.Dispose();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = this.DesktopLocation.X + (this.Width - _settings.Width) / 2;
            var y = this.DesktopLocation.Y + (this.Height - _settings.Height) / 2 - 10;
            _settings.SetDesktopLocation(x > 0 ? x : 0, y > 0 ? y : 0);
            _settings.ShowDialog(this);
        }

        private void gitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/DrTChops/LiveSplit.GTAV64");
        }
    }
}
