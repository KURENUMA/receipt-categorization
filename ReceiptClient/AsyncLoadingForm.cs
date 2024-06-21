

using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReceiptClient
{
    
    public partial class AsyncLoadingForm : Form
    {
        public Exception exception { get; set; }
        public delegate void IsWaiter();
        Task run;
        IsWaiter callback;
        public AsyncLoadingForm(IsWaiter callback)
        {
            this.callback = callback;
            InitializeComponent();
           
        }

        

        private void AsyncLoadingForm_Load(object sender, EventArgs e)
        {
            run = Task.Run(() => {
                try { 
                 callback.Invoke();
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    exception= ex;
                    MessageBox.Show(ex.Message);
                    DialogResult = DialogResult.Abort;
                }
            
            });
           
           
        }
    }
}
