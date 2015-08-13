using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.GTAV64
{
    public partial class Settings : Form
    {
        public bool CheckUpdates { get; set; }
        public string SERVER_IP { get; set; }
        public int Port { get; set; }

        public const string CONFIG_FILE_NAME = "LiveSplit.GTAV64.cfg";

        Form1 _mainWindow;

        public Settings(Form1 parent)
        {
            InitializeComponent();
            this.FormClosing += Form_FormClosing;
            this.Icon = Properties.Resources.Splr_gtav;

            _mainWindow = parent;

            CheckUpdates = true;
            SERVER_IP = "127.0.0.1";
            Port = 16834;

            LoadSettings();
            CheckArguments();

            this.chkCheckUpdates.DataBindings.Add("Checked", this, "CheckUpdates", false, DataSourceUpdateMode.OnPropertyChanged);
            this.numPort.DataBindings.Add("Value", this, "Port", false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtAddress.DataBindings.Add("Text", this, "SERVER_IP", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        void CheckArguments()
        {
            for (int i = 0; i < Program.args.Length; i++)
            {
                string arg = Program.args[i];

                if (arg == "-port" && i + 1 < Program.args.Length)
                {
                    int port;
                    if (int.TryParse(Program.args[i + 1], out port))
                        Port = port;
                }
                else if (arg == "-ip" && i + 1 < Program.args.Length)
                {
                    string ip = Program.args[i + 1];
                    SERVER_IP = ip;
                }
                else if (arg == "-livesplit") // arg used when launching via the LiveSplit component
                {
                    CheckUpdates = false;
                    this.chkCheckUpdates.Visible = false;
                }
            }
            SaveSettings();
        }

        void SaveSettings()
        {
            var doc = new XmlDocument();

            doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));

            XmlElement settingsElem = doc.CreateElement("Settings");
            settingsElem.SetAttribute("version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3));
            doc.AppendChild(settingsElem);

            XmlNode generalNode = doc.CreateElement("General");
            settingsElem.AppendChild(generalNode);

            generalNode.AppendChild(ToElement(doc, "CheckUpdates", CheckUpdates));

            XmlNode liveSplitServerNode = doc.CreateElement("LiveSplitServer");
            settingsElem.AppendChild(liveSplitServerNode);

            liveSplitServerNode.AppendChild(ToElement(doc, "Port", Port));
            liveSplitServerNode.AppendChild(ToElement(doc, "Address", SERVER_IP));

            try
            {
                doc.Save(Application.StartupPath + "\\" + CONFIG_FILE_NAME);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(_mainWindow, "Access to the config file was denied when trying to save settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadSettings()
        {
            var doc = new XmlDocument();

            if (File.Exists(Application.StartupPath + "\\" + CONFIG_FILE_NAME))
            {
                doc.Load(Application.StartupPath + "\\" + CONFIG_FILE_NAME);

                Version settingsVersion = new Version(doc["Settings"].GetAttribute("version"));

                XmlNode generalNode = doc["Settings"]["General"];

                CheckUpdates = ParseBool(generalNode, "CheckUpdates", true);

                XmlNode liveSplitServerNode = doc["Settings"]["LiveSplitServer"];

                Port = int.Parse(liveSplitServerNode["Port"].InnerText);
                SERVER_IP = liveSplitServerNode["Address"].InnerText;
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
                e.Cancel = true;
            SaveSettings();
            this.Hide();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadSettings();
            this.Hide();
        }

        static XmlElement ToElement<T>(XmlDocument document, string name, T value)
        {
            XmlElement str = document.CreateElement(name);
            str.InnerText = value.ToString();
            return str;
        }

        static bool ParseBool(XmlNode settings, string setting, bool default_ = false)
        {
            bool val;
            return settings[setting] != null ?
                (Boolean.TryParse(settings[setting].InnerText, out val) ? val : default_)
                : default_;
        }
    }
}
