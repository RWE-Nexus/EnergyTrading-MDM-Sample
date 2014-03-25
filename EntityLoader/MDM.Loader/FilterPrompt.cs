namespace MDM.Loader
{
    using System;
    using System.Windows.Forms;

    public partial class FilterPrompt : Form
    {
        public FilterPrompt()
        {
            InitializeComponent();
        }

        public string Filter()
        {
            return textBox1.Text;
        }

        private void FilterPrompt_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}