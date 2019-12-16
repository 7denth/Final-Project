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
        static public string[] chairMaterialTypes = { "wood", "plastic" };

        public static List<IFurniture> ReadFurnitureFromFileIntoList(string path)
        {
            List<IFurniture> list = new List<IFurniture>();
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
                return list;
            }
            else
            {
                Console.WriteLine("The file with new supplies is not found.");
                return list;
            }
        }

        public static string GetNameOfSet()
        {
            int counter = 0;
            Console.WriteLine("Please enter a name for futere set.");
            string nameOfSet = "";
            while (counter == 0)
            {
                try
                {
                    string temp = Console.ReadLine();
                    Regex pattern = new Regex(@"[a-zA-Z]");
                    if (pattern.IsMatch(temp))
                    {
                        counter++;
                        nameOfSet = temp.First().ToString().ToUpper() + temp.Substring(1).ToLower();
                    }
                    else
                    {
                        throw new ArgumentException("Please enter better name.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return nameOfSet;
        }

        public static string GetSizeOrMaterial(string[] arrayOfSizeOrMaterial)
        {
            int getCounter = 0;
            string sizeOrMaterial = "";
            while (getCounter == 0)
            {
                string sizeMat = Console.ReadLine();
                foreach (string s in arrayOfSizeOrMaterial)
                {
                    if (sizeMat.ToLower().Equals(s))
                    {
                        getCounter++;
                        sizeOrMaterial = sizeMat.First().ToString().ToUpper() + sizeMat.Substring(1).ToLower();
                    }
                }
                if (sizeOrMaterial.Equals(""))
                {
                    if (arrayOfSizeOrMaterial[0].Equals("big"))
                    {
                        Console.WriteLine("Wrong size. Possible sizes: big, medium, or small.");
                    }
                    else
                    {
                        Console.WriteLine("Wrong material. Possible materials: wood and plastic. " +
                            "Only in case of table it can be glass.");
                    }
                }
            }
            return sizeOrMaterial;
        }



    }
}
