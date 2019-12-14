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
            ReadFileIntoList(path, furniture);
            foreach (IFurniture x in furniture )
            {
                Console.WriteLine($"{x.Material} costs {x.Price}");
            }




            Console.ReadKey();
        }

        static void ReadFileIntoList(string path, List<IFurniture> list)
        {
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string material = "";
                        string size = "";
                        double price = 0;
                        string[] materialTypes = { "wood", "plastic", "glass" };
                        string[] sizeVerieties = { "big", "medium", "small" };
                        string pattern = @"[0-9]+,[0-9]{2}";
                        foreach (Match same in Regex.Matches(line, @"table", RegexOptions.IgnoreCase))
                        {
                            for (int i = 0; i < materialTypes.Length; i++)
                            {
                                foreach (Match match in Regex.Matches(line, materialTypes[i], RegexOptions.IgnoreCase))
                                {
                                    material = match.Value;
                                }
                            }
                            for (int i = 0; i < sizeVerieties.Length; i++)
                            {
                                foreach (Match match in Regex.Matches(line, sizeVerieties[i], RegexOptions.IgnoreCase))
                                {
                                    size = match.Value;
                                }
                            }
                            foreach (Match match in Regex.Matches(line, pattern))
                            {
                                price = Convert.ToDouble(match.Value);
                            }
                            list.Add(new Table(material, size, price));
                        }
                        foreach (Match same in Regex.Matches(line, @"chair", RegexOptions.IgnoreCase))
                        {
                            for (int i = 0; i < materialTypes.Length; i++)
                            {
                                foreach (Match match in Regex.Matches(line, materialTypes[i], RegexOptions.IgnoreCase))
                                {
                                    material = match.Value;
                                }
                            }
                            foreach (Match match in Regex.Matches(line, pattern))
                            {
                                price = Convert.ToDouble(match.Value);
                            }
                            list.Add(new Chair(material, price));
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("The file with new supplies is not found.");
            }
        }



    }
}
