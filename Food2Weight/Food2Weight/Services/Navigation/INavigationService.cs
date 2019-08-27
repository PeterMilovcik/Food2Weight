using System.Threading.Tasks;
using Food2Weight.ViewModels;

namespace Food2Weight.Services.Navigation
{
    public interface INavigationService
    {
        ViewModel PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task NavigateTo<TViewModel>(bool animated = true) where TViewModel : ViewModel;
        Task NavigateTo<TViewModel>(object parameter, bool animated = true) where TViewModel : ViewModel;
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
        Task GoBackAsync(bool animated = true);
    }

}