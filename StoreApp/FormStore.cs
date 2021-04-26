using MySql.Data.MySqlClient;
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
    public partial class FormStore : Form
    {
        List<Item> list = new List<Item>();

        DBConnection db = null;

        DBItem dbItem = null;

        MySqlDataReader reader = null;

        int count = 1;

        FormStoreItemsReport reportWindow;

        internal void DisplayItem(Item item)
        {
            
            textBoxName.Text = item.Name;
            textBoxDepartment.Text = item.Department;
            textBoxBestBefore.Text = item.BestBefore;
            textBoxQuantity.Text = item.Quantity.ToString(); 
            textBoxPrice.Text = item.Price.ToString();
            
            textBoxCount.Text = count.ToString() + " of " + list.Count();

            try { pictureBox1.Image = Image.FromFile(@item.Image); }
            catch (Exception e)
            {
                pictureBox1.Image = Image.FromFile("C:\\Users\\Sumaya\\source\\repos\\StoreApp\\StoreApp\\Images\\default.jpg");
            }

        }

        internal void UpdateList(Item item)
        {
            item.Name = textBoxName.Text;
            item.Department = textBoxDepartment.Text;
            item.BestBefore = textBoxBestBefore.Text;
            item.Quantity = int.Parse(textBoxQuantity.Text);
            item.Price = Double.Parse(textBoxPrice.Text);
        }

        public FormStore()
        {
            InitializeComponent();
        }

        private void FormStore_Load(object sender, EventArgs e)

        {
            this.db = new DBConnection("localhost", "store", "csharp", "password");


            if (db.Connect())
            {
                MessageBox.Show("Connected to MySql Server");
            }

            dbItem = new DBItem();

            reader = dbItem.GetItems(this.db.Connection);


            while (reader.Read())
            {
                Item item = new Item();

                item.Name = reader.GetString(0);
                item.Department = reader.GetString(1);
                item.BestBefore = reader.GetString(2);
                item.Quantity = reader.GetInt32(3);
                item.Price = reader.GetDouble(4);
                item.Image = reader.GetString(5);


                list.Add(item);
            }//WHILE

            DisplayItem(list[count - 1]);
               

            reader.Close();

        }//LOAD

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            count = 1;
            DisplayItem(list[count - 1]);
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            count = list.Count();
            DisplayItem(list[count - 1]);

        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            count++;

            if (count <= list.Count())
            {
                DisplayItem(list[count - 1]);
            }
            else
            {
                count = list.Count();
                DisplayItem(list[count - 1]);
            }

        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            count--;

            if (count >= 1)
            {
                DisplayItem(list[count - 1]);
            }
            else
            {
                count = 1;
                DisplayItem(list[count - 1]);
            }

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            UpdateList(list[count - 1]);
            DBItem dbItem = new DBItem();
            dbItem.Update(this.db.Connection, list[count - 1].Name, list[count - 1].Department, list[count - 1].BestBefore, list[count - 1].Quantity,
                list[count - 1].Price);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DBItem dbItem = new DBItem();

            dbItem.Delete(this.db.Connection, list[count - 1].Name);

            list.RemoveAt(count - 1);
            count--;
            DisplayItem(list[count - 1]);

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            textBoxName.Text = "";
            textBoxDepartment.Text = "";
            textBoxBestBefore.Text = "";
            textBoxQuantity.Text = "";
            textBoxPrice.Text = "";

            buttonSaveItem.Visible = true;
            textBoxPath.Visible = true;
            labelPath.Visible = true;
              

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DisplayItem(list[count - 1]);
            buttonSaveItem.Visible = false;
            labelPath.Visible = false;
            textBoxPath.Visible = false;
        }

        private void buttonSaveItem_Click(object sender, EventArgs e)
        {
            Item item = new Item(textBoxName.Text, textBoxDepartment.Text, textBoxBestBefore.Text, int.Parse(textBoxQuantity.Text), Double.Parse(textBoxPrice.Text), textBoxPath.Text);

            list.Add(item);

            DBItem dbItem = new DBItem();


            dbItem.Insert(this.db.Connection, item.Name, item.Department, item.BestBefore, item.Quantity, item.Price, item.Image);

            DisplayItem(item);

            buttonSaveItem.Visible = false;
            labelPath.Visible = false;
            textBoxPath.Visible = false;

        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            string reportMessage = "";
            foreach(Item i in list)
            {
                reportMessage += i.ToString();
            }
            reportWindow = new FormStoreItemsReport(reportMessage);
            reportWindow.ShowDialog();
        }
    }//CLASS


}//NAMESPACE
