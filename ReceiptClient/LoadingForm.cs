using System;
using System.Windows.Forms;

namespace ReceiptClient
{
    public partial class LoadingForm : Form
    {
        public event System.EventHandler OnCancel;

        public LoadingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnCancel?.Invoke(this, new EventArgs());
            Close();
        }
    }
}
