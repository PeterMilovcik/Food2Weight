using System.Threading.Tasks;
using Xamarin.Forms;

namespace Food2Weight.ViewModels
{
    public class SetGoalWeightViewModel : WeightViewModel
    {
        public override Task Initialize(object parameter)
        {
            try
            {
                IsBusy = true;
                Weight = PreferenceService.GoalWeight;
                return base.Initialize(parameter);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected  override async Task Submit()
        {
            PreferenceService.GoalWeight = Weight;
            MessagingCenter.Send(this, Messages.GoalWeightUpdated);
            await NavigationService.GoBackAsync();
        }
    }
}