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
            string pathToReadFrom = @"F:\\test\Final Project\Furniture.txt";
            List<IFurniture> furniture = FurnitureServices.ReadFurnitureFromFileIntoList(pathToReadFrom);
            List<Kit> kitsOfFurniture = new List<Kit>();
            var tables = furniture.Where(item => item is Table)
                .Select(item => (Table)item)
                .ToList();
            var chairs = furniture.Where(item => item is Chair)
                .Select(item => (Chair)item)
                .ToList();

            Kit firstKitOfFurniture = new Kit();
            FurnitureServices furnitureService1 = new FurnitureServices();
            furnitureService1.FillInKit(firstKitOfFurniture, tables, chairs);
            furnitureService1.WriteKitIntoFile(firstKitOfFurniture, furnitureService1.CreateAFileToWriteKitInto());
            Kit secondKitOfFurniture = new Kit();
            FurnitureServices furnitureService2 = new FurnitureServices();
            furnitureService2.FillInKit(secondKitOfFurniture, tables, chairs);                   
            furnitureService1.WriteKitIntoFile(secondKitOfFurniture, furnitureService2.CreateAFileToWriteKitInto());
            kitsOfFurniture.Add(firstKitOfFurniture);
            kitsOfFurniture.Add(secondKitOfFurniture);

            Console.ReadKey();
        }
    }
}
