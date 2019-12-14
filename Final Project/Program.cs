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
            float distributionForBigTables;
            float distributionForMiddleTables;
            float distributionForSmallTables;
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
            foreach (IFurniture x in furniture)
            {
                Console.WriteLine($"{x.Material} costs {x.Price}");
            }

            Console.ReadKey();
        }


    }
}
