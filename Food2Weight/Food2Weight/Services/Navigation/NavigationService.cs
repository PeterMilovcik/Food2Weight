using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Food2Weight.ViewModels;
using Xamarin.Forms;

namespace Food2Weight.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        public ViewModel PreviousPageViewModel
        {
            get
            {
                var navigationStack = Shell.Current.Navigation.NavigationStack;
                if (navigationStack.Count <= 1)
                    throw new InvalidOperationException("There is no previous page in navigation stack.");
                var previousPage = navigationStack[navigationStack.Count - 2];
                return previousPage.BindingContext as ViewModel;
            }
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public Task NavigateTo<TViewModel>(bool animated = true) where TViewModel : ViewModel =>
            InternalNavigateToAsync(typeof(TViewModel), null, animated);

        public Task NavigateTo<TViewModel>(object parameter, bool animated = true) where TViewModel : ViewModel =>
            InternalNavigateToAsync(typeof(TViewModel), parameter, animated);

        public Task RemoveLastFromBackStackAsync()
        {
            var lastPage = Shell.Current.Navigation.NavigationStack.Last();
            Shell.Current.Navigation.RemovePage(lastPage);
            return Task.CompletedTask;
        }

        public Task RemoveBackStackAsync()
        {
            for (int i = 0; i < Shell.Current.Navigation.NavigationStack.Count - 1; i++)
            {
                var page = Shell.Current.Navigation.NavigationStack[i];
                Shell.Current.Navigation.RemovePage(page);
            }

            return Task.FromResult(true);
        }

        public async Task GoBackAsync(bool animated = true) => await Shell.Current.Navigation.PopAsync(animated);

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter, bool animated)
        {
            var page = CreatePage(viewModelType);
            await Shell.Current.Navigation.PushAsync(page, animated);
            if (page.BindingContext is ViewModel viewModel)
            {
                await viewModel.Initialize(parameter);
            }
        }

        private Page CreatePage(Type viewModelType)
        {
            var pageType = GetPageTypeFor(viewModelType);
            if (pageType == null)
                throw new NotSupportedException($"Cannot locate page type for {viewModelType}");
            return Activator.CreateInstance(pageType) as Page;
        }

        private Type GetPageTypeFor(Type viewModelType)
        {
            var viewName = viewModelType.FullName?.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = $"{viewName}, {viewModelAssemblyName}";
            return Type.GetType(viewAssemblyName);
        }
    }

}