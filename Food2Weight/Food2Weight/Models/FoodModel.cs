using System.Collections.Generic;
using System.Linq;

namespace Food2Weight.Models
{
    public class FoodModel
    {
        public FoodModel(int id, string name)
        {
            Id = id;
            Name = name;
            Records = new List<FoodRecordModel>();
        }

        public int Id { get; }

        public string Name { get; }

        public List<FoodRecordModel> Records { get; }

        public double WeightChange => Records.Sum(r => r.WeightChange);
    }
}