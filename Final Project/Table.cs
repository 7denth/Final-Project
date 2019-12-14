using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    class Table : IFurniture
    {
        string material;
        string size;
        double price;
        public string Material 
        { 
            get
            {
                return material;
            } 
        }
        public string Size 
        {
            get 
            {
                return size;
            }
        }
        public double Price 
        {
            get
            {
                return price;
            }
        }
        public Table(string material, string size, double price)
        {
            this.material = material;
            this.size = size;
            this.price = price;
        }
    }
}
