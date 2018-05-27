using PropertyChanged;

namespace HReception.UI.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class PageModelBase : FreshMvvm.FreshBasePageModel
    {
        public bool IsBusy { get; set; }
    }
}
