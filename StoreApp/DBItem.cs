using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreApp
{
    class DBItem
    {

        public MySqlDataReader GetItems(MySqlConnection connection)
        {
            string sql = "select Item, Department, BestBefore, Quantity, Price, image from items where Item is not null;";

            MySqlDataReader reader = null;

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Exception found: " + ex.Message);
            }
            return reader;
        }

        public void Update(MySqlConnection connection, string item, string department, string bestBefore, int quantity, double price)
        {
            string sql = "UPDATE items SET Item = @item, Department = @department, BestBefore = @bestBefore, Quantity = @quantity, " +
            "Price = @price, Image = @image WHERE Item = @item;";

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@item", item);
            cmd.Parameters.AddWithValue("@department", department);
            cmd.Parameters.AddWithValue("@bestBefore", bestBefore);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            
            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

        public void Insert(MySqlConnection connection, string item, string department, string bestBefore, int quantity, double price, string image)
        {
            /*string sql = "INSERT INTO items VALUES (item = @item, department = @department, bestBefore = @bestBefore, quantity = @quantity, " +
            "RentalPerDay = @price, Available = @available);";*/

            string sql = "INSERT INTO items VALUES (@item, @department, @bestBefore, @quantity, @price, @image);";

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@item", item);
            cmd.Parameters.AddWithValue("@department", department);
            cmd.Parameters.AddWithValue("@bestBefore", bestBefore);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@image", image);


            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Delete(MySqlConnection connection, string item)
        {
            string sql = "DELETE FROM items WHERE item = @item;";

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@item", item);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }
    }
}
