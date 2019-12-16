using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Final_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"F:\\test\Final Project\Furniture.txt";
            int generalCounter = 0;
            List<IFurniture> furniture = Helpers.Helpers.ReadFurnitureFromFileIntoList(path);
            //List<Kit> setsOfFurniture = new List<Kit>();
            var tables = furniture.Where(item => item is Table)
                .Select(item => (Table)item)
                .ToList();
            var chairs = furniture.Where(item => item is Chair)
                .Select(item => (Chair)item)
                .ToList();

            int setOfChairs = 0;
            string chairMaterial = "";
            int smallWoodTable = CountTablesOfEachType("wood", "small");
            int smallPlasticTable = CountTablesOfEachType("plastic", "small");
            int smallGlassTable = CountTablesOfEachType("glass", "small");
            int woodPricedChair = GetChairsOfOneTypeAtOnePrice("wood").Count();
            int plasticPricedChair = GetChairsOfOneTypeAtOnePrice("plastic").Count();

            Console.WriteLine("Lets build a set of furniture from what we have in stock.");
            Kit setOfFurniture = new Kit(Helpers.Helpers.GetNameOfSet());

            if (HowManyTables() == 0)
            {
                Console.WriteLine("There is no tables in stock. Unfortunatelly we can not produce any set of furniture.");
            }
            else
            {
                while (generalCounter == 0)
                {
                    FindTable();
                    setOfChairs = GetNumberOfChairs();
                    chairMaterial = GetChairMaterialType();
                    IEnumerable<Chair> chairsOfOneTypeAtOnePrice = GetChairsOfOneTypeAtOnePrice(chairMaterial);

                    if (chairsOfOneTypeAtOnePrice.Count() >= setOfChairs)
                    {
                        foreach (Chair y in chairsOfOneTypeAtOnePrice)
                        {
                            generalCounter++;
                            setOfFurniture.GetList().Add(y);
                            if (setOfFurniture.GetList().Count == setOfChairs)
                            {
                                break;
                            }
                        }
                        Console.WriteLine($"\n{ShowResult()}");
                    }
                    else if ((chairsOfOneTypeAtOnePrice.Count() <= setOfChairs &&
                        (GetChairsOfOneTypeAtOnePrice("wood").Count() >= 2)) && (smallWoodTable > 0 || smallGlassTable > 0))
                    {
                        Console.WriteLine("Try other combinations.");
                    }
                    else if ((chairsOfOneTypeAtOnePrice.Count() <= setOfChairs &&
                        (GetChairsOfOneTypeAtOnePrice("plastic").Count() >= 2)) && (smallPlasticTable > 0 || smallGlassTable > 0))
                    {
                        Console.WriteLine("Try other combinations.");
                    }
                    else if (woodPricedChair < 2 || (woodPricedChair < 2 && (smallWoodTable < 1 || smallGlassTable < 1)) ||
                        plasticPricedChair < 2 || (plasticPricedChair < 2 && (smallPlasticTable < 1 || smallGlassTable < 1)))
                    {
                        generalCounter--;
                        Console.WriteLine("There is not enough chairs to make any set.");
                    }
                }
            }


            IEnumerable<Chair> GetChairsOfDiffPrices(string material)
            {
                var chairsOfEachSeperatePrice = ChairFilterBy(material)
                                                  .GroupBy(m => m.Price)
                                                  .Select(g => g.First())
                                                  .ToList();
                return chairsOfEachSeperatePrice;
            }

            IEnumerable<Chair> GetChairsOfOneTypeAtOnePrice(string material)
            {
                IEnumerable<Chair> chairOfOneTypeAndOnePrice = CountChairsOfOneTypeMaxPrice(chairMaterial);
                foreach (Chair x in GetChairsOfDiffPrices(material))
                {
                    chairOfOneTypeAndOnePrice = from prc in ChairFilterBy(material)
                                       where prc.Price == x.Price
                                       select prc;
                    if (chairOfOneTypeAndOnePrice.Count() >= setOfChairs)
                    {
                        return chairOfOneTypeAndOnePrice;
                    }
                }
                return chairOfOneTypeAndOnePrice;
            }

            IEnumerable<Chair> CountChairsOfOneTypeMaxPrice(string material)
            {
                var chairsPricedList = from prc in ChairFilterBy(material)
                                       where prc.Price == ChairFilterBy(material).Max(x => x.Price)
                                       select prc;
                return chairsPricedList;
            }






            IEnumerable<Table> TableFilterBy(string material, string size)
            {
                var intermediateFilteredList = from chr in tables
                                               where chr.Material.ToLower().Equals(material.ToLower())
                                               select chr;
                var filteredList = from chr in intermediateFilteredList
                                   where chr.Size.ToLower().Equals(size.ToLower())
                                   select chr;
                return filteredList;
            }

            int CountTablesOfEachType(string whichFilter, string whichType)
            {
                int counterForTables = 0;
                foreach (Table z in TableFilterBy(whichFilter, whichType))
                {
                    counterForTables++;
                }
                return counterForTables;
            }

            IEnumerable<Chair> ChairFilterBy(string whichType)
            {
                    var FilteredList = from chr in chairs
                                   where chr.Material.ToLower().Equals(whichType.ToLower())
                                   select chr;
                    return FilteredList;
            }

            int newCounter = 0;
            foreach (var c in setOfFurniture.GetList())
            {
                chairs.Remove(c);
            }
            foreach (Chair x in chairs)
            {
                newCounter++;
            }

            double GetPriceOfChairsInSet()
            {
                double cost = 0;
                foreach (Chair x in setOfFurniture.GetList())
                {
                    cost += x.Price;
                }
                return cost;
            }





            int HowManyTables()
            {
                int result = 0;
                foreach (Table x in tables)
                {
                    result++;
                }
                return result;
            }

            void FindTable()
            {
                int tableCounter = 0;
                while (tableCounter == 0)
                {
                    Console.WriteLine("Now please type in a size of table you want to have in your set.");
                    string sizeOfTable = Helpers.Helpers.GetSizeOrMaterial(Helpers.Helpers.sizeVerieties);

                    Console.WriteLine("And we should define a material from which a table is made.");
                    string materialOfTable = Helpers.Helpers.GetSizeOrMaterial(Helpers.Helpers.materialTypes);

                    var expensiveTable = TableFilterBy(materialOfTable, sizeOfTable)
                        .Where(p => p.Price == TableFilterBy(materialOfTable, sizeOfTable).Max(mp => mp.Price))
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

            string GetChairMaterialType()
            {
                if (setOfFurniture.Table.Material.ToLower().Equals("wood"))
                {
                    return "wood";
                }
                else if (setOfFurniture.Table.Material.ToLower().Equals("plastic"))
                {
                    return "plastic";
                }
                else
                {
                    Console.WriteLine("For glass table please choose either wood or plastic chairs.");
                    return Helpers.Helpers.GetSizeOrMaterial(Helpers.Helpers.chairMaterialTypes);
                }
            }

            int GetNumberOfChairs()
            {
                if (setOfFurniture.Table.Size.ToLower().Equals("big"))
                {
                    return 6;
                }
                else if (setOfFurniture.Table.Size.ToLower().Equals("medium"))
                {
                    return 4;
                }
                else
                {
                    return 2;
                }
            }

            string ShowResult()
            {
                return $"{setOfFurniture.Name} Kit includes:\n{setOfFurniture.Table.Size} {setOfFurniture.Table.Material} " +
                    $"{setOfFurniture.Table.GetType().Name.ToString()} at price of USD {setOfFurniture.Table.Price}\n" +
                    $"and {setOfChairs} chairs at USD {GetPriceOfChairsInSet() / setOfChairs} each.\n" +
                    $"Total price for {setOfFurniture.Name} Kit is USD {setOfFurniture.Table.Price + GetPriceOfChairsInSet()}.";
            }

            ShowChairs();
            void ShowChairs()
            {
                foreach (Chair x in setOfFurniture.GetList())
                {
                    Console.Write($"{x.Material} at {x.Price}\n");
                }
            }

            Console.ReadKey();
        }
    }
}
