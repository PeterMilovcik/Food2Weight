using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Food2Weight.ViewModels
{
    public class WeightHistoryViewModel : ViewModel
    {
        private ObservableCollection<Model> items;

        public WeightHistoryViewModel()
        {
            Items = new ObservableCollection<Model>();
            ShowEditWeightHistoryCommand = new Command(async () => await ShowEditWeightHistory());
        }

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

        public Command ShowEditWeightHistoryCommand { get; }

        public override async Task Initialize(object parameter)
        {
            Items.Clear();
            var weights = await RepositoryService.GetWeights();
            foreach (var weight in weights)
            {
                Items.Add(new Model(weight.At, weight.Value));
            }
        }

        private async Task ShowEditWeightHistory() => 
            await NavigationService.NavigateTo<EditWeightHistoryViewModel>();

        public class Model
        {
            public Model(DateTime at, double value)
            {
                At = at;
                Value = value;
            }

            public DateTime At { get; }

            public double Value { get; }
        }
    }
}