using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Final_Project
{
    public class FurnitureServices
    {
        int setOfChairs = 0;
        string chairMaterial = "";
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

        public void FillInKit(Kit setOfFurniture, List<Table> tables, List<Chair> chairs)
        {
            TableServices tableServices = new TableServices();
            ChairServices chairServices = new ChairServices();
            int generalCounter = 0;
            int smallWoodTable = tableServices.CountTablesOfEachType("wood", "small", tables);
            int smallPlasticTable = tableServices.CountTablesOfEachType("plastic", "small", tables);
            int smallGlassTable = tableServices.CountTablesOfEachType("glass", "small", tables);
            int woodPricedChair = chairServices.GetChairsOfOneTypeAtOnePrice("wood", chairs, setOfChairs).Count();
            int plasticPricedChair = chairServices.GetChairsOfOneTypeAtOnePrice("plastic", chairs, setOfChairs).Count();
            Console.WriteLine("Lets build a set of furniture from what we have in stock.");
            setOfFurniture.Name = FurnitureServices.GetNameOfSet();

            if (tables.Count() == 0)
            {
                Console.WriteLine("There is no tables in stock. Unfortunatelly we can not produce any set of furniture.");
            }
            else
            {
                while (generalCounter == 0)
                {
                    tableServices.FindTable(setOfFurniture, tables);
                    setOfChairs = chairServices.GetNumberOfChairs(setOfFurniture);
                    chairMaterial = chairServices.GetChairMaterialType(setOfFurniture);
                    IEnumerable<Chair> chairsOfOneTypeAtOnePrice = chairServices.GetChairsOfOneTypeAtOnePrice(chairMaterial, chairs, setOfChairs);

                    if (chairsOfOneTypeAtOnePrice.Count() >= setOfChairs)
                    {
                        foreach (Chair y in chairsOfOneTypeAtOnePrice)
                        {
                            generalCounter++;
                            setOfFurniture.GetList().Add(y);
                            if (setOfFurniture.GetList().Count == setOfChairs)
                            {
                                int newCounter = 0;
                                foreach (var c in setOfFurniture.GetList())
                                {
                                    chairs.Remove(c);
                                }
                                foreach (Chair x in chairs)
                                {
                                    newCounter++;
                                }
                                tables.Remove(setOfFurniture.Table);
                                break;
                            }
                        }
                        Console.WriteLine($"\n{ShowResult(setOfFurniture)}");
                    }
                    else if ((chairsOfOneTypeAtOnePrice.Count() <= setOfChairs &&
                        (chairServices.GetChairsOfOneTypeAtOnePrice("wood", chairs, setOfChairs).Count() >= 2)) && (smallWoodTable > 0 || smallGlassTable > 0))
                    {
                        Console.WriteLine("Try other combinations.");
                    }
                    else if ((chairsOfOneTypeAtOnePrice.Count() <= setOfChairs &&
                        (chairServices.GetChairsOfOneTypeAtOnePrice("plastic", chairs, setOfChairs).Count() >= 2)) && (smallPlasticTable > 0 || smallGlassTable > 0))
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
        }

        public static string GetNameOfSet()
        {
            int counter = 0;
            Console.WriteLine("Please enter a name for furniture kit.");
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

        string ShowResult(Kit setOfFurniture)
        {
            ChairServices chairServices = new ChairServices();
            return $"\n{setOfFurniture.Name} Kit includes:\n{setOfFurniture.Table.Size} {setOfFurniture.Table.Material} " +
                $"{setOfFurniture.Table.GetType().Name.ToString()} at price of USD {setOfFurniture.Table.Price}\n" +
                $"and {setOfChairs} chairs at USD {chairServices.GetPriceOfChairsInSet(setOfFurniture) / setOfChairs} each.\n" +
                $"Total price for {setOfFurniture.Name} Kit is USD " +
                $"{setOfFurniture.Table.Price + chairServices.GetPriceOfChairsInSet(setOfFurniture)}.\n";
        }

        public string CreateAFileToWriteKitInto()
        {
            Console.WriteLine("Lets save this Kit into File. Please give a name to this file.");
            string pathToWriteTo = @"";
            while (pathToWriteTo.Equals(""))
            {
                pathToWriteTo = Console.ReadLine();
                if (!File.Exists(@"F:\\test\Final Project\" + pathToWriteTo + ".txt"))
                {
                    FileStream kitFile = File.Create(@"F:\\test\Final Project\" + pathToWriteTo + ".txt");
                    pathToWriteTo = @"F:\\test\Final Project\" + pathToWriteTo + ".txt";
                    kitFile.Close();
                }
                else
                {
                    Console.WriteLine("This name already exists, please choose another name.");
                }
            }
            return pathToWriteTo;
        }

        public void WriteKitIntoFile(Kit kitToSave, string path)
        {
            using (StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                int numberOfChairsInKit = ChairServices.CountChairsInKit(kitToSave);
                double totalCost = kitToSave.Table.Price + numberOfChairsInKit * kitToSave.sampleChairOutOfList().Price;
                writer.WriteLine($"Kit of furniture {kitToSave.Name} which includes: " +
                    $"{kitToSave.Table.Size} {kitToSave.Table.GetType().Name.ToString()} made out of {kitToSave.Table.Material}" +
                    $" and {numberOfChairsInKit} {kitToSave.sampleChairOutOfList().Material} " +
                    $"chairs is ready." +
                    $"\nTotal cost for the kit is USD {totalCost}.\n");
            }
        }
    }
}
