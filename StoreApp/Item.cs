using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    class Item
    {
        string name;

        string department;

        string bestBefore;

        int quantity;

        double price;

        string image = "C:\\Users\\Sumaya\\source\\repos\\StoreApp\\Images\\default.jpg;";

        public Item()
        {
        }

        public Item(string name, string department, string bestBefore, int quantity, double price)
        {
            this.name = name;
            this.department = department;
            this.bestBefore = bestBefore;
            this.quantity = quantity;
            this.price = price;
        }

        public Item(string name, string department, string bestBefore, int quantity, double price, string image) : this(name, department, bestBefore, quantity, price)
        {
            this.image = image;
        }

        public string Name { get => name; set => name = value; }
        public string Department { get => department; set => department = value; }
        public string BestBefore { get => bestBefore; set => bestBefore = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Price { get => price; set => price = value; }
        public string Image { get => image; set => image = value; }

        public override string ToString()
        {
            return $"{Name.PadRight(22, ' ')}{Department.PadRight(22, ' ')}{BestBefore.PadRight(16, ' ')} " +
                $"{Quantity.ToString().PadRight(11, ' ')}€{Price.ToString()} \r\n";
        }
    }
        
}
