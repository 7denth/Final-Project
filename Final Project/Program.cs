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
            List<IFurniture> furniture = FurnitureServices.ReadFurnitureFromFileIntoList(path);
            //List<Kit> setsOfFurniture = new List<Kit>();
            var tables = furniture.Where(item => item is Table)
                .Select(item => (Table)item)
                .ToList();
            var chairs = furniture.Where(item => item is Chair)
                .Select(item => (Chair)item)
                .ToList();

            Kit firstSetOfFurniture = new Kit();
            Kit secondSetOfFurniture2 = new Kit();
            FurnitureServices furnitureService1 = new FurnitureServices();
            FurnitureServices furnitureService2 = new FurnitureServices();
            furnitureService1.CreateNewKit(firstSetOfFurniture, tables, chairs);
            furnitureService2.CreateNewKit(secondSetOfFurniture2, tables, chairs);

            Console.ReadKey();
        }
    }
}
