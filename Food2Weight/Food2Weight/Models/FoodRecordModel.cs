using System;

namespace Food2Weight.Models
{
    public class FoodRecordModel
    {
        public FoodRecordModel(int id, int foodId, string name, DateTime at, double weightChange)
        {
            Id = id;
            FoodId = foodId;
            Name = name;
            At = at;
            WeightChange = weightChange;
        }

        public int Id { get; }

        public int FoodId { get; }

        public string Name { get; }

        public DateTime At { get; }

        public double WeightChange { get; }
    }
}