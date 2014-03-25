namespace MDM.Loader
{
    using System;
    using System.Configuration;
    using System.Threading;
    using System.Windows.Forms;

    using EnergyTrading.Logging;

    using MDM.Loader.Views;
    using MDM.Sync.Loaders;

    public partial class FormMain : Form, IMainFormView
    {
        private DataCache cache;

        private ILogger logger;

        private LoaderProcessor processor;

        public FormMain()
        {
            InitializeComponent();
        }

        private ILogger Logger
        {
            get
            {
                return logger ?? (logger = LoggerFactory.GetLogger(typeof(FormMain)));
            }
        }

        private int Workers
        {
            get
            {
                var w = 0;
                return int.TryParse(this.textBoxWorkers.Text, out w) ? w : 1;
            }
        }

        public void AppendLogText(string message)
        {
            AppendText(LogTextBox, message);
        }

        public void SetStatusMesage(string message)
        {
            AssignText(StatusMessage, message);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitialiseLoader();
        }

        private void AddWork(Loader loader)
        {
            if (loader == null)
            {
                return;
            }

            if (!processor.Running)
            {
                processor.WorkerCount = Workers;
                processor.Start();
            }

            processor.AddWork(loader);
        }

        private void AppendText(TextBox value, string message)
        {
            if (value.InvokeRequired)
            {
                value.Invoke((MethodInvoker)(() => value.AppendText(Environment.NewLine + message)));
            }
            else
            {
                value.AppendText(Environment.NewLine + message);
            }
        }

        private void AssignText(Label value, string message)
        {
            if (value.InvokeRequired)
            {
                value.Invoke((MethodInvoker)(() => value.Text = message));
            }
            else
            {
                value.Text = message;
            }
        }

        private void EntityBrowseButton_Click(object sender, EventArgs e)
        {
            if (EntityComboBox.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "First select an entity", 
                    "Entity Browse", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                return;
            }

            openFileDialog1.Title = "MDM - " + EntityComboBox.SelectedText;
            if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.EntityFileTextBox.Text = this.openFileDialog1.FileName;

            try
            {
                new MDMLoaderFactory().Create((string)EntityComboBox.SelectedItem, EntityFileTextBox.Text, false);
            }
            catch (Exception ex)
            {
                this.Logger.ErrorFormat("Error loading file. {0}", ex.Message);
                MessageBox.Show(
                    string.Format("Error loading file. Correct format?\r\n{0}", ex), 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Exclamation);
            }
        }

        private void EntityImportButton_Click(object sender, EventArgs e)
        {
            var loader = new MDMLoaderFactory().Create(
                (string)EntityComboBox.SelectedItem, 
                EntityFileTextBox.Text, 
                chkCandidateData.Checked);

            if (loader == null)
            {
                this.Logger.ErrorFormat("Unable to create MDM loader for {0} entity", this.EntityComboBox.SelectedItem);
            }
            else
            {
                AddWork(loader);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            processor.Stop();
        }

        private void InitialiseLoader()
        {
            textBoxBaseUri.Text = ConfigurationManager.AppSettings["MdmUri"];

            processor = new LoaderProcessor { WorkerCount = 1 };
            cache = new DataCache(processor);
        }

        private void StopProcessor()
        {
            if (processor.Running)
            {
                // Put this on a separate thread so that the UI continues to respond.
                var worker = new Thread(processor.Stop);
                worker.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.StopProcessor();
        }
    }
}