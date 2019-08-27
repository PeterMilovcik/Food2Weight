using System;
using SQLite;

namespace Food2Weight.DataAccess.Entities
{
    public class FoodRecordEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int FoodId { get; set; }

        public DateTime At { get; set; }
    }
}