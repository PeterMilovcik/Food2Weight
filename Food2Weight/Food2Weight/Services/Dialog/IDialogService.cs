using System.Threading.Tasks;

namespace Food2Weight.Services.Dialog
{
    public interface IDialogService
    {
        Task ShowAlert(string message, string title, string buttonLabel);
        Task<bool> ShowConfirmation(string message, string title);
    }
}