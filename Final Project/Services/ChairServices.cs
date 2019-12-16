using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    class ChairServices
    {
        public string GetChairMaterialType(Kit setOfFurniture)
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
                return FurnitureServices.GetSizeOrMaterial(FurnitureServices.chairMaterialTypes);
            }
        }

        public int GetNumberOfChairs(Kit setOfFurniture)
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

        public IEnumerable<Chair> GetChairsOfDiffPrices(string material, List<Chair> chairs)
        {
            var chairsOfEachSeperatePrice = ChairFilterBy(material, chairs)
                                              .GroupBy(m => m.Price)
                                              .Select(g => g.First())
                                              .ToList();
            return chairsOfEachSeperatePrice;
        }

        public IEnumerable<Chair> GetChairsOfOneTypeAtOnePrice(string material, List<Chair> chairs, int setOfChairs)
        {
            IEnumerable<Chair> chairOfOneTypeAndOnePrice = CountChairsOfOneTypeMaxPrice(material, chairs);
            foreach (Chair x in GetChairsOfDiffPrices(material, chairs))
            {
                chairOfOneTypeAndOnePrice = from prc in ChairFilterBy(material, chairs)
                                            where prc.Price == x.Price
                                            select prc;
                if (chairOfOneTypeAndOnePrice.Count() >= setOfChairs)
                {
                    return chairOfOneTypeAndOnePrice;
                }
            }
            return chairOfOneTypeAndOnePrice;
        }

        public IEnumerable<Chair> CountChairsOfOneTypeMaxPrice(string material, List<Chair> chairs)
        {
            var chairsPricedList = from prc in ChairFilterBy(material, chairs)
                                   where prc.Price == ChairFilterBy(material, chairs).Max(x => x.Price)
                                   select prc;
            return chairsPricedList;
        }



        public IEnumerable<Chair> ChairFilterBy(string whichType, List<Chair> chairs)
        {
            var FilteredList = from chr in chairs
                               where chr.Material.ToLower().Equals(whichType.ToLower())
                               select chr;
            return FilteredList;
        }

        public double GetPriceOfChairsInSet(Kit setOfFurniture)
        {
            double cost = 0;
            foreach (Chair x in setOfFurniture.GetList())
            {
                cost += x.Price;
            }
            return cost;
        }
    }
}
