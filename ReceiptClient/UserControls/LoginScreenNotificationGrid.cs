using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReceiptClient.ViewModels;

namespace ReceiptClient.UserControls
{
    public partial class LoginScreenNotificationGrid : UserControl
    {
        private LoginScreenNotificationManager notificationGridManager = new LoginScreenNotificationManager();

        public LoginScreenNotificationGrid()
        {
            InitializeComponent();
            c1FlexGrid1.DataSource = notificationGridManager.Dt;
            c1FlexGrid1.ExtendLastCol = true;
            this.c1FlexGrid1.Rows.Fixed = 0;
            this.c1FlexGrid1.Cols.Fixed = 0;
            // Set WordWrap to true for the cell style to enable multi-line text
            c1FlexGrid1.Cols[1].AllowResizing = true; // Adjust column width if needed

            // Adjust row height to accommodate multi-line text
            c1FlexGrid1.AutoSizeRows();
            //c1FlexGrid1.AutoSizeCols(c1FlexGrid1.Cols.Fixed, c1FlexGrid1.Cols.Count - 1)
        }
    }
}
