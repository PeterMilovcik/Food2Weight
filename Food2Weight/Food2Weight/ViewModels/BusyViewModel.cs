namespace Food2Weight.ViewModels
{
    public class BusyViewModel : ViewModel
    {
        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (value == isBusy) return;
                isBusy = value;
                OnPropertyChanged();
            }
        }
    }
}