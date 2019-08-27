using System.Threading.Tasks;
using Food2Weight.Services.Dialog;
using Food2Weight.Services.Navigation;
using Food2Weight.Services.Preference;
using Food2Weight.Services.Repository;

namespace Food2Weight.ViewModels
{
    public class ViewModel : Observable
    {
        public ViewModel()
        {
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            RepositoryService = ViewModelLocator.Resolve<IRepositoryService>();
            PreferenceService = ViewModelLocator.Resolve<IPreferenceService>();
        }

        protected INavigationService NavigationService { get; }
        protected IDialogService DialogService { get; }
        protected IRepositoryService RepositoryService { get; }
        protected IPreferenceService PreferenceService { get; }

        public virtual Task Initialize(object parameter) => Task.CompletedTask;
    }
}