using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Food2Weight.ViewModels
{
    public class AddFoodViewModel : ViewModel
    {
        private DateTime date;
        private TimeSpan time;
        private ObservableCollection<Model> items;
        private string searchText;
        private ObservableCollection<Model> allItems;

        public AddFoodViewModel()
        {
            Items = new ObservableCollection<Model>();
            AddFoodCommand = new Command(AddFood);
            SubmitCommand = new Command(async () => await Submit());
            SearchTextChangedCommand = new Command(SearchTextChanged);
        }

        public DateTime Date
        {
            get => date;
            set
            {
                if (Equals(date, value)) return;
                date = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Time
        {
            get => time;
            set
            {
                if (Equals(time, value)) return;
                time = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                if (Equals(searchText, value)) return;
                searchText = value;
                OnPropertyChanged();
            }
        }

        public Command SearchTextChangedCommand { get; }

        public Command AddFoodCommand { get; }

        public Command SubmitCommand { get; }

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
            try
            {
                var now = DateTime.Now;
                Date = now.Date;
                Time = now.TimeOfDay;
                var food = await RepositoryService.GetFood();
                var orderedFood = food.OrderByDescending(f => f.Records.Count);
                allItems = new ObservableCollection<Model>(
                    orderedFood.Select(
                        f => new Model(f.Id, f.Name, f.WeightChange.ToString("N1"), 0)));
                Items = allItems;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void AddFood()
        {
            if (string.IsNullOrWhiteSpace(SearchText)) return;
            allItems.Add(new Model(0, SearchText, string.Empty, 1));
            Items = allItems;
            SearchText = string.Empty;
        }

        private async Task Submit()
        {
            var at = Date.Add(Time);
            var added = Items.Where(i => i.Count > 0).ToList();
            foreach (var model in added)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    await RepositoryService.AddFood(model.Name, at);
                }
            }

            if (added.Any())
            {
                await NavigationService.NavigateTo<FoodHistoryViewModel>();
            }
            else
            {
                await NavigationService.GoBackAsync();
            }
        }

        private void SearchTextChanged()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Items = allItems;
            }
            else
            {
                Items = new ObservableCollection<Model>(
                    allItems.Where(i => i.Name.Contains(SearchText)));
            }
        }

        public class Model : Observable
        {
            private int count;

            public Model(int id, string name, string weightChange, int count)
            {
                Id = id;
                Name = name;
                WeightChange = weightChange;
                Count = count;
                IncreaseCountCommand = new Command(() => Count++);
                DecreaseCountCommand = new Command(() => Count--);
            }

            public int Id { get; }

            public string Name { get; }

            public string WeightChange { get; }

            public int Count
            {
                get => count;
                set
                {
                    if (Equals(count, value)) return;
                    if (value < 0) return;
                    count = value;
                    OnPropertyChanged();
                }
            }

            public Command IncreaseCountCommand { get; }

            public Command DecreaseCountCommand { get; }
        }
    }
}
