using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Food2Weight.ViewModels
{
    public class MainViewModel : BusyViewModel
    {
        private string floatingActionButtonText;
        private bool isActionMenuVisible;
        private bool isCurrentWeightVisible;
        private bool isAddFoodButtonVisible;
        private bool isAddButtonVisible;
        private string actionMenuText;
        private string currentWeight;
        private string goalWeight;

        public MainViewModel()
        {
            FloatingActionButtonText = "+";
            ToggleActionMenuVisibilityCommand = new Command(ToggleActionMenuVisibility);
            AddFoodCommand = new Command(async () => await AddFood());
            AddWeightCommand = new Command(async () => await AddWeight());
            ShowWeightHistoryCommand = new Command(async () => await ShowWeightHistory());
            SetGoalWeightCommand = new Command(async () => await SetGoalWeight());
            MessagingCenter.Subscribe<AddWeightViewModel>(
                this, 
                Messages.WeightsUpdated,
                async sender => await Initialize(null));
            MessagingCenter.Subscribe<EditWeightRecordViewModel>(
                this,
                Messages.WeightsUpdated,
                async sender => await Initialize(null));
            MessagingCenter.Subscribe<SetGoalWeightViewModel>(
                this,
                Messages.GoalWeightUpdated,
                sender => UpdateGoalWeight());
        }

        public override async Task Initialize(object parameter)
        {
            var weights = await RepositoryService.GetWeights();
            var lastWeight = weights.LastOrDefault();
            if (lastWeight != null)
            {
                IsActionMenuVisible = false;
                IsAddButtonVisible = true;
                IsAddFoodButtonVisible = true;
                IsCurrentWeightVisible = true;
                CurrentWeight = $"{lastWeight.Value:N1}";
                ActionMenuText = "Add weight or food";
            }
            else
            {
                IsActionMenuVisible = true;
                IsAddButtonVisible = false;
                IsAddFoodButtonVisible = false;
                IsCurrentWeightVisible = false;
                CurrentWeight = string.Empty;
                ActionMenuText = "Add your weight";
            }

            UpdateGoalWeight();
        }

        public string ActionMenuText
        {
            get => actionMenuText;
            set
            {
                if (Equals(actionMenuText, value)) return;
                actionMenuText = value;
                OnPropertyChanged();
            }
        }

        public bool IsActionMenuVisible
        {
            get => isActionMenuVisible;
            set
            {
                if (value == isActionMenuVisible) return;
                isActionMenuVisible = value;
                OnPropertyChanged();
                FloatingActionButtonText = isActionMenuVisible ? "x" : "+";
            }
        }

        public string FloatingActionButtonText
        {
            get => floatingActionButtonText;
            set
            {
                if (value == floatingActionButtonText) return;
                floatingActionButtonText = value;
                OnPropertyChanged();
            }
        }

        public Command ToggleActionMenuVisibilityCommand { get; }

        public Command AddFoodCommand { get; }

        public Command AddWeightCommand { get; }

        public Command ShowWeightHistoryCommand { get; }

        public Command SetGoalWeightCommand { get; }

        public bool IsCurrentWeightVisible
        {
            get => isCurrentWeightVisible;
            set
            {
                if (value == isCurrentWeightVisible) return;
                isCurrentWeightVisible = value;
                OnPropertyChanged();
            }
        }

        public string CurrentWeight
        {
            get => currentWeight;
            set
            {
                if (Equals(currentWeight, value)) return;
                currentWeight = value;
                OnPropertyChanged();
            }
        }

        public string GoalWeight
        {
            get => goalWeight;
            set
            {
                if (Equals(goalWeight, value)) return;
                goalWeight = value;
                OnPropertyChanged();
            }
        }

        public bool IsAddFoodButtonVisible
        {
            get => isAddFoodButtonVisible;
            set
            {
                if (Equals(isAddFoodButtonVisible, value)) return;
                isAddFoodButtonVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsAddButtonVisible
        {
            get => isAddButtonVisible;
            set
            {
                if (Equals(IsAddButtonVisible, value)) return;
                isAddButtonVisible = value;
                OnPropertyChanged();
            }
        }

        private void UpdateGoalWeight()
        {
            double gw = PreferenceService.GoalWeight;
            GoalWeight = gw > 0 ? gw.ToString("N1") : "?";
        }

        private async Task AddWeight()
        {
            ToggleActionMenuVisibility();
            await NavigationService.NavigateTo<AddWeightViewModel>();
        }

        private async Task AddFood()
        {
            ToggleActionMenuVisibility();
            await NavigationService.NavigateTo<AddFoodViewModel>();
        }

        private void ToggleActionMenuVisibility() => IsActionMenuVisible = !IsActionMenuVisible;

        private async Task ShowWeightHistory()
        {
            IsBusy = true;
            try
            {
                await NavigationService.NavigateTo<WeightHistoryViewModel>();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SetGoalWeight()
        {
            IsBusy = true;
            try
            {
                await NavigationService.NavigateTo<SetGoalWeightViewModel>();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
