using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Food2Weight.Models;

namespace Food2Weight.ViewModels
{
    public class FoodListViewModel : ViewModel
    {
        private ObservableCollection<Model> items;

        public ObservableCollection<Model> Items
        {
            get => items;
            set
            {
                if (Equals(items, value)) return;
                items = value;
                OnPropertyChanged();
            }
        }

        public override async Task Initialize(object parameter)
        {
            var food = await RepositoryService.GetFood();
            var groupedFoodByFirstLetter = food.GroupBy(f => f.Name.First());
            var models = new List<Model>();
            foreach (var foodModels in groupedFoodByFirstLetter.OrderBy(g => g.Key))
            {
                var model = new Model(char.ToUpper(foodModels.Key));
                model.AddRange(foodModels.AsEnumerable().OrderBy(fm => fm.Name));
                models.Add(model);
            }
            Items = new ObservableCollection<Model>(models);
        }

        public class Model : List<FoodModel>
        {
            public Model(char firstLetter)
            {
                FirstLetter = firstLetter;
            }

            public char FirstLetter { get; }

            public List<FoodModel> Items => this;
        }
    }
}