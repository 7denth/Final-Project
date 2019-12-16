using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    public class Kit
    {
        string name;
        public string Name 
        { 
            get 
            { 
                return name; 
            } 
            set
            {
                name = value;
            }
        }

        public Kit() { }
        public Kit(string name)
        {
            this.name = name;
        }

        public Table Table { get; set; }

        List<Chair> chairsInTheKit = new List<Chair>();
        public List<Chair> GetList()
        {
            return chairsInTheKit;
        }
    }
}
