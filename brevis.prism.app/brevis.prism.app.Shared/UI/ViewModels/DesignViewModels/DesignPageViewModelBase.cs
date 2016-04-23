using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace brevis.prism.app.UI.ViewModels.DesignViewModels
{
    public class DesignPageViewModelBase
    {
        public string PageTitle
        {
            get { return "PageTitle"; }
        }

        public string PageSubTitle
        {
            get { return "PageSubTitle"; }
        }

        public Visibility FlyoutVisibility
        {
            get { return Visibility.Collapsed; }
        }
    }
}
