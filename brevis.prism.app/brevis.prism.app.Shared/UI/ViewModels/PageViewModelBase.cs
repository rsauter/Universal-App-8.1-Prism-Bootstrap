using brevis.prism.app.Contracts.ApplicationServices;
using brevis.prism.app.Contracts.Exceptions;
using brevis.prism.app.UI.ViewModels.DataViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace brevis.prism.app.UI.ViewModels
{
    public class PageViewModelBase : ViewModel
    {
        #region Fields

        private readonly INavigationService _navigationService;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IAppSettingsService _appSettingsService;
        private readonly IResourceLoader _resourceLoader;
        private readonly BusinessObjectDataViewModelMapper _viewModelMapper;

        private string _pageTitle;
        private string _pageSubTitle;
        private string _visualState;
        private bool _loadingData;
        private Visibility _flyoutVisiblity;
        private Visibility _refreshDataButtonVisibility;
        private Visibility _pageContentVisibility;
        List<DelegateCommand> _refreshCommands;

        #endregion

        #region Properties

        public string VisualState
        {
            get { return _visualState; }
            set { SetProperty(ref _visualState, value); }
        }

        public string PageTitle
        {
            get { return _pageTitle; }
            set { SetProperty(ref _pageTitle, value); }
        }

        public string PageSubTitle
        {
            get { return _pageSubTitle; }
            set { SetProperty(ref _pageSubTitle, value); }
        }

        public bool LoadingData
        {
            get { return _loadingData; }
            set
            {
                SetProperty(ref _loadingData, value);
                SetRefreshDataButtonVisibility();
                SetPageContentVisibility();
            }
        }

        public Visibility FlyoutVisibility
        {
            get { return _flyoutVisiblity; }
            set { SetProperty(ref _flyoutVisiblity, value); }
        }

        public Visibility RefreshDataButtonVisibility
        {
            get { return _refreshDataButtonVisibility; }
            set { SetProperty(ref _refreshDataButtonVisibility, value); }
        }

        public Visibility PageContentVisibility
        {
            get { return _pageContentVisibility; }
            set { SetProperty(ref _pageContentVisibility, value); }
        }

        public Visibility BackButtonVisibility
        {
            get
            {
#if WINDOWS_PHONE_APP
                return Visibility.Collapsed;
#endif
#if WINDOWS_APP
                return (BackCommand != null) ? Visibility.Visible : Visibility.Collapsed;
#endif
            }
        }

        public INavigationService NavigationService { get { return _navigationService; } }
        public IAlertMessageService AlertMessageService { get { return _alertMessageService; } }
        public IAppSettingsService AppSettingsService { get { return _appSettingsService; } }
        public IResourceLoader ResourceLoader { get { return _resourceLoader; } }
        public BusinessObjectDataViewModelMapper Mapper { get { return _viewModelMapper; } }



        #endregion

        #region Commands / ActionFunctions

        public DelegateCommand BackCommand { get; set; }
        public DelegateCommand NavigateHomeCommand { get; set; }
        public DelegateCommand NavigateSettingsCommand { get; set; }
        public DelegateCommand NavigateLinksCommand { get; set; }
        public DelegateCommand NavigatePrivacyCommand { get; set; }
        public DelegateCommand NavigateAboutCommand { get; set; }
        public DelegateCommand NavigateGamesCommand { get; set; }
        public DelegateCommand NavigatePlayersCommand { get; set; }
        public DelegateCommand ToggleFlyoutCommand { get; private set; }

        public List<DelegateCommand> RefreshCommands
        {
            get { return _refreshCommands; }
            set
            {
                SetProperty(ref _refreshCommands, value);
                SetRefreshDataButtonVisibility();
            }
        }

        #endregion

        public PageViewModelBase(INavigationService navigationService,
            IAlertMessageService alertMessageService,
            IResourceLoader resourceLoader,
            IAppSettingsService appSettingsService)
        {
            _navigationService = navigationService;
            _alertMessageService = alertMessageService;
            _resourceLoader = resourceLoader;
            _appSettingsService = appSettingsService;
            _viewModelMapper = new BusinessObjectDataViewModelMapper(resourceLoader);


            NavigateHomeCommand = new DelegateCommand(NavigateToMain);
            ToggleFlyoutCommand = new DelegateCommand(ToggleFlyout);

            FlyoutVisibility = Visibility.Collapsed;

            InitActionFunctions();


#if WINDOWS_APP
            if (NavigationService.CanGoBack())
            {
                // default back command
                BackCommand = new DelegateCommand(() => NavigationService.GoBack());
            }
#endif
        }



        #region Public Methods

        public void ClearUnhandledException(Exception ex)
        {
            _alertMessageService.ShowAsync(
                _resourceLoader.GetString("ExceptionHandlingUnhandledAlert"),
                _resourceLoader.GetString("ExceptionHandlingAlertTitle"));
        }

        public void ClearHandledException(BusinessException ex)
        {
            _alertMessageService.ShowAsync(
                ex.Message,
                _resourceLoader.GetString("ExceptionHandlingAlertTitle"));

        }

        #endregion

        #region Helpers

        private void InitActionFunctions()
        {
            NavigateHomeCommand = new DelegateCommand(() => NavigationService.Navigate("Main", null));
            NavigateAboutCommand = new DelegateCommand(() => NavigationService.Navigate("About", null));
            NavigatePrivacyCommand = new DelegateCommand(() => NavigationService.Navigate("Privacy", null));
            NavigateSettingsCommand = new DelegateCommand(() => NavigationService.Navigate("Settings", null));
            NavigateGamesCommand = new DelegateCommand(() => NavigationService.Navigate("Games", null));
            NavigatePlayersCommand = new DelegateCommand(() => NavigationService.Navigate("Players", null));
            NavigateLinksCommand = new DelegateCommand(() => NavigationService.Navigate("Links", null));
        }

        private void NavigateToMain()
        {
            NavigationService.Navigate("Main", null);
        }

        private void ToggleFlyout()
        {
            FlyoutVisibility = (FlyoutVisibility == Visibility.Visible)
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        private void SetRefreshDataButtonVisibility()
        {
            if (RefreshCommands.Count > 0 && !LoadingData)
            {
                RefreshDataButtonVisibility = Visibility.Visible;
            }
            else
            {
                RefreshDataButtonVisibility = Visibility.Collapsed;
            }
        }

        private void SetPageContentVisibility()
        {
            if (!LoadingData)
            {
                PageContentVisibility = Visibility.Visible;
            }
            else
            {
                PageContentVisibility = Visibility.Collapsed;
            }
        }

        #endregion
    }
}
