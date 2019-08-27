using System.Threading.Tasks;
using Acr.UserDialogs;

namespace Food2Weight.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public Task ShowAlert(string message, string title, string buttonLabel) => 
            UserDialogs.Instance.AlertAsync(message, title, buttonLabel);

        public Task<bool> ShowConfirmation(string message, string title) => 
            UserDialogs.Instance.ConfirmAsync(message, title, "Yes", "No");
    }
}