using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Food2Weight.Models;

namespace Food2Weight.Services.Repository
{
    public interface IRepositoryService
    {
        Task AddWeight(double weight, DateTime at);
        Task UpdateWeight(WeightModel weightModel);
        Task DeleteWeight(int id);
        Task<List<WeightModel>> GetWeights();
        Task AddFood(string name, DateTime at);
        Task<List<FoodModel>> GetFood();
        Task<List<FoodRecordModel>> GetFoodRecords();
    }
}