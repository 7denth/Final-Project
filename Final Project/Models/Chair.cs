﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    public class Chair : IFurniture
    {
        string material;
        double price;

        public Chair(string material, double price)
        {
            this.material = material;
            this.price = price;
        }

        public string Material 
        {
            get
            {
                return material;
            }
        }
        public double Price 
        {
            get
            {
                return price;
            }
        }
    }
}
