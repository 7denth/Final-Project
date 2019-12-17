using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    class TableServices
    {
        public void FindTable(Kit setOfFurniture, List<Table> tables)
        {
            int tableCounter = 0;
            while (tableCounter == 0)
            {
                Console.WriteLine("Now please type in a size of table you want to have in your set.");
                string sizeOfTable = FurnitureServices.GetSizeOrMaterial(FurnitureServices.sizeVerieties);

                Console.WriteLine("And we should define a material from which a table is made.");
                string materialOfTable = FurnitureServices.GetSizeOrMaterial(FurnitureServices.materialTypes);

                var expensiveTable = TableFilterBy(materialOfTable, sizeOfTable, tables)
                    .Where(p => p.Price == TableFilterBy(materialOfTable, sizeOfTable, tables).Max(mp => mp.Price))
                    .FirstOrDefault();

                if (expensiveTable != null)
                {
                    tableCounter++;
                    setOfFurniture.Table = expensiveTable;
                    tables.Remove(expensiveTable);
                }
                else if (expensiveTable == null)
                {
                    Console.WriteLine($"We don't have {sizeOfTable} {materialOfTable} Tables in stock, " +
                        $"please choose another size and/or material.");
                }
            }
        }

        public IEnumerable<Table> TableFilterBy(string material, string size, List<Table> tables)
        {
            var intermediateFilteredList = from chr in tables
                                           where chr.Material.ToLower().Equals(material.ToLower())
                                           select chr;
            var filteredList = from chr in intermediateFilteredList
                               where chr.Size.ToLower().Equals(size.ToLower())
                               select chr;
            return filteredList;
        }

        public int CountTablesOfEachType(string whichFilter, string whichType, List<Table> tables)
        {
            int counterForTables = 0;
            foreach (Table z in TableFilterBy(whichFilter, whichType, tables))
            {
                counterForTables++;
            }
            return counterForTables;
        }
    }
}
