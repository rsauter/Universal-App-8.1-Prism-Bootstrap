using brevis.prism.app.UI.ViewModels;
using Microsoft.Practices.Prism.StoreApps;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace brevis.prism.app.Views
{
    public class PageBase : VisualStateAwarePage
    {
        public PageViewModelBase PageViewModel { get; set; }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Window.Current.SizeChanged += Current_SizeChanged;
            var viewModel = this.DataContext as INotifyPropertyChanged;

            PageViewModel = viewModel as PageViewModelBase;

#if WINDOWS_PHONE_APP
            VisualStateManager.GoToState(this, "Tiny", false);
#else
            VisualStateManager.GoToState(this, "Wide", false);
#endif

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (e.Size.Width < 700)
            {
                VisualStateManager.GoToState(this, "Tiny", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "Wide", false);
            }
        }
    }
}
