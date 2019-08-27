using System.Collections.Generic;
using System.Threading.Tasks;
using Food2Weight.DataAccess.Entities;
using SQLite;

namespace Food2Weight.DataAccess
{
    public class Database
    {
        private readonly SQLiteAsyncConnection connection;

        public Database(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<WeightEntity>().Wait();
            connection.CreateTableAsync<FoodEntity>().Wait();
            connection.CreateTableAsync<FoodRecordEntity>().Wait();
        }

        public async Task Save(WeightEntity entity)
        {
            if (entity.Id == 0)
            {
                await connection.InsertAsync(entity);
            }
            else
            {
                await connection.UpdateAsync(entity);
            }
        }

        public async Task Delete(WeightEntity weightEntity) => 
            await connection.DeleteAsync(weightEntity);

        public Task<List<WeightEntity>> GetWeights() 
            => connection.Table<WeightEntity>().OrderBy(w => w.At).ToListAsync();

        public async Task Save(FoodRecordEntity entity)
        {
            if (entity.Id == 0)
            {
                await connection.InsertAsync(entity);
            }
            else
            {
                await connection.UpdateAsync(entity);
            }
        }

        public Task<List<FoodRecordEntity>> GetFoodRecords() => connection.Table<FoodRecordEntity>().ToListAsync();

        public async Task Save(FoodEntity entity)
        {
            if (entity.Id == 0)
            {
                await connection.InsertAsync(entity);
            }
            else
            {
                await connection.UpdateAsync(entity);
            }
        }

        public Task<List<FoodEntity>> GetFood() => connection.Table<FoodEntity>().ToListAsync();
    }
}