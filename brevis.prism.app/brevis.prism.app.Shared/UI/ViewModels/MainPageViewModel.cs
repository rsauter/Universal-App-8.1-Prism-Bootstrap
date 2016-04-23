using Microsoft.Practices.Prism.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using brevis.prism.app.Contracts.ApplicationServices;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.StoreApps.Interfaces;

namespace brevis.prism.app.UI.ViewModels
{
    public class MainPageViewModel : PageViewModelBase
    {
        public MainPageViewModel(
         INavigationService navigationService,
         IAlertMessageService alertMessageService,
         IResourceLoader resourceLoader,
         IAppSettingsService appSettingsService)
         : base(navigationService, alertMessageService, resourceLoader, appSettingsService)
        {
            PageTitle = ResourceLoader.GetString("PageTitleMain");
        }
    }
}
