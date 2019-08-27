using SQLite;

namespace Food2Weight.DataAccess.Entities
{
    public class FoodEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}