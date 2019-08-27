using System;
using SQLite;

namespace Food2Weight.DataAccess.Entities
{
    public class WeightEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime At { get; set; }
    }
}