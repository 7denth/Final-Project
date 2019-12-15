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
            List<IFurniture> furniture = new List<IFurniture>();
            int counter = 0;
            float distributionForBigTables = 0;
            float distributionForMiddleTables = 0;
            float distributionForSmallTables = 0;
            Console.WriteLine("What is an expected distribution of sets of different sizes?\n" +
                "In other words, how many times family, middle, and small size sets are bought per hundred sold sets?\n");
            while (counter == 0)
            {
                try
                {
                    counter++;
                    Console.WriteLine("Please enter the expected value for family size sets?");
                    distributionForBigTables = Helpers.Helpers.GetDistribution();
                    Console.WriteLine("What is the expected value for middle size sets?");
                    distributionForMiddleTables = Helpers.Helpers.GetDistribution();
                    if (distributionForBigTables + distributionForMiddleTables > 100)
                    {
                        throw new IndexOutOfRangeException("The sum of destributions can not be greater then 100.");
                    }
                    distributionForSmallTables = 100 - distributionForBigTables - distributionForMiddleTables;
                }
                catch (IndexOutOfRangeException ex)
                {
                    counter--;
                    Console.WriteLine(ex.Message);
                }
            }

            Helpers.Helpers.ReadFurnitureFromFileIntoList(path, furniture);

            var tables = furniture.Where(item => item is Table)
                .Select(item => (Table)item)
                .ToList();
            var chairs = furniture.Where(item => item is Chair)
                .Select(item => (Chair)item)
                .ToList();

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

            int FindAmntOfTablesBasedOnDistributon(int tablesSize, float distribution )
            {
                return (int)(tablesSize * (distribution / 100));
            }

            int rqrdBigTables = FindAmntOfTablesBasedOnDistributon(bigTables, distributionForBigTables);
            int rqrdMediumTables = FindAmntOfTablesBasedOnDistributon(mediumTables, distributionForMiddleTables);
            int rqrdSmallTables = amountOfTables - rqrdBigTables - rqrdMediumTables;
            //Console.WriteLine($"{rqrdBigTables}, {rqrdMediumTables}, {rqrdSmallTables}: Total: {amountOfTables}");

            Kit setOfFurniture = new Kit("Royal Style");
            IEnumerable<Table> TableFilterBy(string whichFilter, string whichType)
            {
                var intermediateFilteredList = from chr in tables
                                               where chr.Material.ToLower().Equals(whichFilter.ToLower())
                                               select chr;
                var FilteredList = from chr in intermediateFilteredList
                                   where chr.Size.ToLower().Equals(whichType.ToLower())
                                   select chr;
                return FilteredList;
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
            var TablesPriced = from prc in TableFilterBy("plastic", "medium")
                               where prc.Price == TableFilterBy("plastic", "medium").Max(x => x.Price)
                               select prc;
            foreach (Table x in TablesPriced)
            {
                setOfFurniture.Table = x;
            }
            var woodChairsPricedList = from prc in ChairFilterBy("plastic")
                                       where prc.Price == ChairFilterBy("plastic").Max(x => x.Price)
                                       select prc;

            int spcounter = 0;
            foreach (Chair y in woodChairsPricedList)
            {
                spcounter++;
                setOfFurniture.GetList().Add(y);
                if (spcounter == 6)
                {
                    break;
                }
            }

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
