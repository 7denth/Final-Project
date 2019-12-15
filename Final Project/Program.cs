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
            //Console.WriteLine("What is an expected distribution of sets of different sizes?\n" +
            //    "In other words, how many times family, middle, and small size sets are bought per hundred sold sets?\n");
            //int counter = 0;
            //float distributionForBigTables = 0;
            //float distributionForMiddleTables = 0;
            //float distributionForSmallTables = 0;
            //while (counter == 0)
            //{
            //    try
            //    {
            //        counter++;
            //        Console.WriteLine("Please enter the expected value for family size sets?");
            //        distributionForBigTables = Helpers.Helpers.GetDistribution();
            //        Console.WriteLine("What is the expected value for middle size sets?");
            //        distributionForMiddleTables = Helpers.Helpers.GetDistribution();
            //        if (distributionForBigTables + distributionForMiddleTables > 100)
            //        {
            //            throw new IndexOutOfRangeException("The sum of destributions can not be greater then 100.");
            //        }
            //        distributionForSmallTables = 100 - distributionForBigTables - distributionForMiddleTables;
            //    }
            //    catch (IndexOutOfRangeException ex)
            //    {
            //        counter--;
            //        Console.WriteLine(ex.Message);
            //    }
            //}
            List<IFurniture> furniture = Helpers.Helpers.ReadFurnitureFromFileIntoList(path);
            var tables = furniture.Where(item => item is Table)
                .Select(item => (Table)item)
                .ToList();
            var chairs = furniture.Where(item => item is Chair)
                .Select(item => (Chair)item)
                .ToList();

            Console.WriteLine("Lets build a set of furniture from what we have in stock.");
            Kit setOfFurniture = new Kit(Helpers.Helpers.GetNameOfSet());

            Console.WriteLine("Now please type in a size of table you want to have in your set");
            string sizeOfTable = Helpers.Helpers.GetSizeOrMaterial(Helpers.Helpers.sizeVerieties);

            Console.WriteLine("And we should define a material from which a table is made.");
            string materialOfTable = Helpers.Helpers.GetSizeOrMaterial(Helpers.Helpers.materialTypes);

            var expensiveTable = TableFilterBy(materialOfTable, sizeOfTable)
                .Where(p => p.Price == TableFilterBy(materialOfTable, sizeOfTable).Max(mp => mp.Price))
                .FirstOrDefault();

            if (expensiveTable != null)
            {
                setOfFurniture.Table = expensiveTable;
                tables.Remove(expensiveTable);
            }
            else if ()
            {

            }








            int bigTableWood = CountTablesOfEachType("wood", "big");
            int mediumTableWood = CountTablesOfEachType("wood", "medium");
            int smallTableWood = CountTablesOfEachType("wood", "small");
            int bigTablePlastic = CountTablesOfEachType("plastic", "big");
            int mediumTablePlastic = CountTablesOfEachType("plastic", "medium");
            int smallTablePlastic = CountTablesOfEachType("plastic", "small");
            int bigTableGlass = CountTablesOfEachType("glass", "big");
            int mediumTableGlass = CountTablesOfEachType("glass", "medium");
            int smallTableGlass = CountTablesOfEachType("glass", "small");
            int bigTables = bigTableWood + bigTablePlastic + bigTableGlass;
            int mediumTables = mediumTableWood + mediumTablePlastic + mediumTableGlass;
            int smallTables = smallTableWood + smallTablePlastic + smallTableGlass;
            int woodTables = bigTableWood + mediumTableWood + smallTableWood;
            int plasticTables = bigTablePlastic + mediumTablePlastic + smallTablePlastic;
            int glassTables = bigTableGlass + mediumTableGlass + smallTableGlass;
            int woodchair = CountChairsOfEachType("wood");
            int plasticchair = CountChairsOfEachType("plastic");
            int vacantSpacesForChairsInSets = bigTables * 6 + mediumTables * 4 + smallTables * 2;
            int amountOfTables = bigTables + mediumTables + smallTables;

            //int FindAmntOfTablesBasedOnDistributon(int tablesSize, float distribution )
            //{
            //    return (int)(tablesSize * (distribution / 100));
            //}

            //int rqrdBigTables = FindAmntOfTablesBasedOnDistributon(bigTables, distributionForBigTables);
            //int rqrdMediumTables = FindAmntOfTablesBasedOnDistributon(mediumTables, distributionForMiddleTables);
            //int rqrdSmallTables = amountOfTables - rqrdBigTables - rqrdMediumTables;
            //Console.WriteLine($"{rqrdBigTables}, {rqrdMediumTables}, {rqrdSmallTables}: Total: {amountOfTables}");

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
            int CountChairsOfEachType(string whichType)
            {
                int counterForChairs = 0;
                foreach (Chair z in ChairFilterBy(whichType))
                {
                    counterForChairs++;
                }
                return counterForChairs;
            }


            var woodChairsPricedList = from prc in ChairFilterBy("wood")
                                       where prc.Price == ChairFilterBy("wood").Max(x => x.Price)
                                       select prc;

            int bigSet = 6;
            if (woodChairsPricedList.Count() >= bigSet)
            {
                foreach (Chair y in woodChairsPricedList)
                {
                    setOfFurniture.GetList().Add(y);
                    if (setOfFurniture.GetList().Count == bigSet)
                    {
                        break;
                    }
                }
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
            Console.WriteLine(woodchair + plasticchair);
            Console.WriteLine(newCounter);

            void ShowChairs()
            {
                foreach (Chair x in setOfFurniture.GetList())
                {
                    Console.Write($"{x.Material} at {x.Price}\n");
                }
            }

            Console.WriteLine($"{setOfFurniture.Name} Kit includes:\n{setOfFurniture.Table.Size} {setOfFurniture.Table.Material} " +
                $"{setOfFurniture.Table.GetType().Name.ToString()} at price of {setOfFurniture.Table.Price}\n" +
                $"and 6 Chairs:");
            ShowChairs();

            Console.ReadKey();
        }
    }
}
