using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Food2Weight.DataAccess;
using Food2Weight.DataAccess.Entities;
using Food2Weight.Models;

namespace Food2Weight.Services.Repository
{
    public class RepositoryService : IRepositoryService
    {
        private readonly Database database;
        private List<FoodEntity> foodEntities;
        private List<WeightEntity> weightEntities;
        private List<FoodRecordEntity> foodRecordEntities;

        public RepositoryService()
        {
            var specialFolder = Environment.SpecialFolder.Personal;
            var folderPath = Environment.GetFolderPath(specialFolder);
            var dbPath = Path.Combine(folderPath, "database.db3");
            database = new Database(dbPath);
        }

        public async Task AddWeight(double weight, DateTime at)
        {
            var weightEntity = new WeightEntity {Value = weight, At = at};
            await database.Save(weightEntity);
            weightEntities = await database.GetWeights();
        }

        public async Task UpdateWeight(WeightModel model)
        {
            var weightEntity = new WeightEntity { Id = model.Id, Value = model.Value, At = model.At };
            await database.Save(weightEntity);
            weightEntities = await database.GetWeights();
        }

        public async Task DeleteWeight(int id)
        {
            var weightEntity = weightEntities.Single(w => w.Id == id);
            await database.Delete(weightEntity);
            weightEntities = await database.GetWeights();
        }

        public async Task<List<WeightModel>> GetWeights()
        {
            if (weightEntities == null) weightEntities = await database.GetWeights();
            return weightEntities
                .Select(entity => new WeightModel(entity.Id, entity.Value, entity.At))
                .OrderBy(w => w.At)
                .ToList();
        }

        public async Task AddFood(string name, DateTime at)
        {
            if (foodEntities == null) foodEntities = await database.GetFood();
            var foodEntity = foodEntities.SingleOrDefault(fm => fm.Name == name);
            if (foodEntity == null)
            {
                foodEntity = new FoodEntity {Name = name};
                await database.Save(foodEntity);
                foodEntities = await database.GetFood();
                foodEntity = foodEntities.Single(fm => fm.Name == name);
            }

            var foodRecordEntity = new FoodRecordEntity {FoodId = foodEntity.Id, At = at};
            await database.Save(foodRecordEntity);
            foodRecordEntities = await database.GetFoodRecords();
        }

        public async Task<List<FoodModel>> GetFood()
        {
            if (foodEntities == null) foodEntities = await database.GetFood();
            var foodRecordModels = await GetFoodRecords();
            return foodEntities.Select(e =>
            {
                var foodModel = new FoodModel(e.Id, e.Name);
                foodModel.Records.AddRange(foodRecordModels.Where(frm => frm.FoodId == foodModel.Id));
                return foodModel;
            }).ToList();
        }

        public async Task<List<FoodRecordModel>> GetFoodRecords()
        {
            if (foodEntities == null) foodEntities = await database.GetFood();
            if (foodRecordEntities == null) foodRecordEntities = await database.GetFoodRecords();
            if (weightEntities == null) weightEntities = await database.GetWeights();

            var result = new List<FoodRecordModel>();
            if (weightEntities.Any())
            {
                for (var i = 0; i < weightEntities.Count - 1; i++)
                {
                    var we1 = weightEntities[i];
                    var we2 = weightEntities[i + 1];
                    var weightChange = we2.Value - we1.Value;
                    var records = foodRecordEntities.Where(fre => fre.At >= we1.At && fre.At < we2.At).ToList();
                    var weightRecordChange = records.Any() ? weightChange / records.Count : 0;
                    foreach (var fre in records)
                    {
                        var foodEntity = foodEntities.Single(fe => fe.Id == fre.FoodId);
                        var model = new FoodRecordModel(fre.Id, fre.FoodId, foodEntity.Name, fre.At, weightRecordChange);
                        result.Add(model);
                    }
                }

                var lastWeightEntity = weightEntities.Last();
                var notMeasuredRecords = foodRecordEntities.Where(fre => fre.At >= lastWeightEntity.At).ToList();
                if (notMeasuredRecords.Any())
                {
                    foreach (var fre in notMeasuredRecords)
                    {
                        var foodEntity = foodEntities.Single(fm => fm.Id == fre.FoodId);
                        var weightChange = result.Where(r => r.FoodId == fre.FoodId).Sum(r => r.WeightChange);
                        var model = new FoodRecordModel(fre.Id, fre.FoodId, foodEntity.Name, fre.At, weightChange);
                        result.Add(model);
                    }
                }
            }

            return result;
        }
    }
}