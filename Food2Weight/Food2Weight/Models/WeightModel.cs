using System;

namespace Food2Weight.Models
{
    public class WeightModel
    {
        public WeightModel(int id, double value, DateTime at)
        {
            Id = id;
            Value = value;
            At = at;
        }

        public int Id { get; }
        public double Value { get; }
        public DateTime At { get; set; }
    }
}