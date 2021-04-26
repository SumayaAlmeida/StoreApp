using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreApp
{
    public partial class FormStoreItemsReport : Form
    {
        string reportMessage;

        public FormStoreItemsReport(string reportMessage)
        {
            InitializeComponent();
            this.reportMessage = reportMessage;
        }

        public FormStoreItemsReport()
        {
            InitializeComponent();
        }

        private void FormStoreItemsReport_Load(object sender, EventArgs e)
        {
            textBoxReport.Text = "Item                  Department           Best Before     Quantity     Price\r\n" +
                                 "------------------------------------------------------------------------------\r\n" +
                reportMessage;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}