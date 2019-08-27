using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Food2Weight.Services.Dialog;
using Food2Weight.Services.Navigation;
using Food2Weight.Services.Repository;
using Xamarin.Forms;

namespace Food2Weight.ViewModels
{
    public class EditWeightHistoryViewModel : ViewModel
    {
        private ObservableCollection<Model> items;

        public EditWeightHistoryViewModel()
        {
            MessagingCenter.Subscribe<EditWeightRecordViewModel>(
                this,
                Messages.WeightsUpdated,
                async sender => await Initialize(null));
            MessagingCenter.Subscribe<Model>(
                this,
                Messages.WeightsUpdated,
                async sender => await Initialize(null));
        }

        public ObservableCollection<Model> Items
        {
            get => items;
            private set
            {
                if (Equals(items, value)) return;
                items = value;
                OnPropertyChanged();
            }
        }

        public override async Task Initialize(object parameter)
        {
            var weights = await RepositoryService.GetWeights();
            Items = new ObservableCollection<Model>(
                weights.OrderByDescending(w => w.At)
                    .Select(w =>
                    {
                        string at = w.At.ToString("dddd, dd MMMM yyyy, HH:mm");
                        return new Model(w.Id, at, w.Value, 
                            NavigationService, 
                            RepositoryService,
                            DialogService);
                    }));
        }

        public class Model
        {
            public int Id { get; }
            public string At { get; }
            public double Value { get; }

            public Model(int id, string at, double value, 
                INavigationService navigation, 
                IRepositoryService repository,
                IDialogService dialog)
            {
                Id = id;
                At = at;
                Value = value;
                EditCommand = new Command(
                    async ()=> await navigation.NavigateTo<EditWeightRecordViewModel>(Id));
                RemoveCommand = new Command(
                    async ()=>
                        {
                            if (await dialog.ShowConfirmation(
                                $"Do you want to delete weight record from {at}?", 
                                "Confirmation"))
                            {
                                await repository.DeleteWeight(Id);
                                MessagingCenter.Send(this, Messages.WeightsUpdated);
                            }
                        });
            }

            public Command EditCommand { get; }

            public Command RemoveCommand { get; }
        }
    }
}