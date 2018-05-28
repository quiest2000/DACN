using System;
using System.Threading.Tasks;
using FreshMvvm;
namespace HReception.UI.Utils.Extensions
{
    public static class MessageExtensions
    {
        public static Task<bool> ShowConfirmAsync(this FreshBasePageModel pageModel, string message)
        {
            return pageModel.CoreMethods.DisplayAlert("Confirm", message, "Ok", "Cancel");
        }
        public static Task ShowWarningAsync(this FreshBasePageModel pageModel, string message)
        {
            return pageModel.CoreMethods.DisplayAlert("Warning", message, "Ok");
        }
        public static Task ShowInfoAsync(this FreshBasePageModel pageModel, string message)
        {
            return pageModel.CoreMethods.DisplayAlert("Info", message, "Ok");
        }
    }
}
