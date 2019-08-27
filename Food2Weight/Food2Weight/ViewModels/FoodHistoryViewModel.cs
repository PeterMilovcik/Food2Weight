using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Food2Weight.Models;

namespace Food2Weight.ViewModels
{
    public class FoodHistoryViewModel : ViewModel
    {
        public FoodHistoryViewModel()
        {
            Items = new ObservableCollection<Model>();
        }

        public ObservableCollection<Model> Items { get; }

        public override async Task Initialize(object parameter)
        {
            Items.Clear();
            var foodRecords = await RepositoryService.GetFoodRecords();
            var orderedFoodRecords = foodRecords.OrderByDescending(fr => fr.At);
            var groupedRecordsByDate = orderedFoodRecords.GroupBy(fr => fr.At.Date);
            var today = DateTime.Now.Date;
            var yesterday = today.AddDays(-1);
            foreach (var groupedRecords in groupedRecordsByDate)
            {
                var date = groupedRecords.Key;
                var dateFormatted = date.ToString("dddd, dd MMMM yyyy");
                if (date == today) dateFormatted = "Today";
                if (date == yesterday) dateFormatted = "Yesterday";
                var model = new Model {Date = dateFormatted};
                var records = groupedRecords.AsEnumerable().OrderBy(r => r.At);
                model.AddRange(records);
                if (records.Any())
                {
                    model.WeightChange = records.Sum(r => r.WeightChange).ToString("N1");
                }
                Items.Add(model);
            }
        }

        public class Model : List<FoodRecordModel>
        {
            public string Date { get; set; }

            public List<FoodRecordModel> Records => this;

            public string WeightChange { get; set; }
        }
    }
}