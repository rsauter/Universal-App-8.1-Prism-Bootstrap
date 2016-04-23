using brevis.prism.app.Business.ApplicationServices;
using brevis.prism.app.Contracts.ApplicationServices;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
#if WINDOWS_APP
using Windows.UI.ApplicationSettings;
#endif
using Windows.UI.Xaml.Media.Animation;


namespace brevis.prism.app
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : MvvmAppBase
    {
        #region Fields
#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif

        public IEventAggregator EventAggregator { get; set; }
        private readonly IUnityContainer _container = new UnityContainer();

        #endregion

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Debug.WriteLine(string.Format("{0:dd.MM.yyyy HH:mm:ss:fff} - Start App()", DateTime.Now));

            this.InitializeComponent();

            //this.Suspending += this.OnSuspending;

            Debug.WriteLine(string.Format("{0:dd.MM.yyyy HH:mm:ss:fff} - Finished App()", DateTime.Now));
        }

        protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                // Here we would load the application's resources.
                await this.LoadAppResources();
            }

            this.NavigationService.Navigate("Main", null);

        }

        private async Task LoadAppResources()
        {
            Debug.WriteLine(string.Format("{0:dd.MM.yyyy HH:mm:ss:fff} - Start LoadAppResources()", DateTime.Now));

            EventAggregator = new EventAggregator();

            _container.RegisterInstance<INavigationService>(NavigationService);
            _container.RegisterInstance<ISessionStateService>(SessionStateService);
            _container.RegisterInstance<IEventAggregator>(EventAggregator);
            _container.RegisterInstance<IResourceLoader>(
                new ResourceLoaderAdapter(new ResourceLoader()));

            _container.RegisterType<IAlertMessageService, AlertMessageService>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IAppSettingsService, AppSettingsService>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<INotificationService, NotificationService>(
                new ContainerControlledLifetimeManager());

            // register repositories
            

            // register application services
           


            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewModelTypeName = string.Format(
                    CultureInfo.InvariantCulture,
                    "brevis.prism.app.UI.ViewModels.{0}ViewModel",
                    viewType.Name);
                var viewModelType = Type.GetType(viewModelTypeName);

                if (viewModelType == null)
                {
                    throw new NullReferenceException("viewModelType");
                }

                return viewModelType;
            });

            Debug.WriteLine(string.Format("{0:dd.MM.yyyy HH:mm:ss:fff} - Finish LoadAppResources()", DateTime.Now));
        }


        protected override object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        protected override void OnRegisterKnownTypesForSerialization()
        {
            //SessionStateService.RegisterKnownType(typeof(irgendwas));
        }

#if WINDOWS_APP
        protected override IList<SettingsCommand> GetSettingsCommands()
        {
            var settingsCommands = new List<SettingsCommand>();
            var resourceLoader = _container.Resolve<IResourceLoader>();

            settingsCommands.Add(new SettingsCommand(
                Guid.NewGuid().ToString(),
                resourceLoader.GetString("SettingsCharmAboutButtonLabel"),
                (c) => NavigationService.Navigate("About", null)));

            settingsCommands.Add(new SettingsCommand(
                Guid.NewGuid().ToString(),
                resourceLoader.GetString("SettingsCharmPrivacyButtonLabel"),
                (c) => NavigationService.Navigate("Privacy", null)));

            return settingsCommands;
        }
#endif

    }
}
