using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Food2Weight.ViewModels
{
    public class AddWeightViewModel : WeightViewModel
    {
        public override async Task Initialize(object parameter)
        {
            try
            {
                IsBusy = true;
                await InitializeCurrentWeight();
                InitializeDateTime();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task InitializeCurrentWeight()
        {
            var weights = await RepositoryService.GetWeights();
            var currentWeight = weights.LastOrDefault();
            if (currentWeight != null)
            {
                Weight = currentWeight.Value;
            }
        }

        private void InitializeDateTime()
        {
            var now = DateTime.Now;
            Date = now.Date;
            Time = now.TimeOfDay;
        }

        protected override async Task Submit()
        {
            await RepositoryService.AddWeight(Weight, Date.Add(Time));
            MessagingCenter.Send(this, Messages.WeightsUpdated);
            await NavigationService.GoBackAsync();
        }
    }
}