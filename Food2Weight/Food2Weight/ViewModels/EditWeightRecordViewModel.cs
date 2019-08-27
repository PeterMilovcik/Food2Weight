using System.Linq;
using System.Threading.Tasks;
using Food2Weight.Models;
using Xamarin.Forms;

namespace Food2Weight.ViewModels
{
    public class EditWeightRecordViewModel : WeightViewModel
    {
        private WeightModel model;

        public override async Task Initialize(object parameter)
        {
            try
            {
                IsBusy = true;
                if (parameter == null) return;
                var id = (int)parameter;
                var weights = await RepositoryService.GetWeights();
                model = weights.Single(w => w.Id == id);
                Weight = model.Value;
                Date = model.At.Date;
                Time = model.At.TimeOfDay;
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override async Task Submit()
        {
            var newModel = new WeightModel(model.Id, Weight, Date.Add(Time));
            await RepositoryService.UpdateWeight(newModel);
            MessagingCenter.Send(this, Messages.WeightsUpdated);
            await NavigationService.GoBackAsync();
        }
    }
}