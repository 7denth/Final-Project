using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Final_Project.Helpers
{
    public class Helpers
    {
        static public string[] furnitureTypes = { "Table", "Chair" };
        static public string[] materialTypes = { "wood", "plastic", "glass" };
        static public string[] sizeVerieties = { "big", "medium", "small" };

        public static void ReadFurnitureFromFileIntoList(string path, List<IFurniture> list)
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
                        string pattern = @"[0-9]+,[0-9]{2}";
                        for (int j = 0; j < furnitureTypes.Length; j++)
                        {
                            foreach (Match same in Regex.Matches(line, furnitureTypes[j], RegexOptions.IgnoreCase))
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
                                if (furnitureTypes[j].Equals("Table"))
                                {
                                    list.Add(new Table(material, size, price));
                                }
                                else if (furnitureTypes[j].Equals("Chair"))
                                {
                                    list.Add(new Chair(material, price));
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("The file with new supplies is not found.");
            }
        }

        public static float GetDistribution()
        {
            int counter = 0;
            float distribution = 0;
            while (counter == 0)
            {
                try
                {
                    counter++;
                    distribution = float.Parse(Console.ReadLine());
                    if (distribution > 100 || distribution < 0)
                    {
                        throw new IndexOutOfRangeException("Value of distribution should be withing 0 to 100 range.");
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    counter--;
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    counter--;
                    Console.WriteLine(ex.Message);
                }
            }
            return distribution;
        }
    }
}
