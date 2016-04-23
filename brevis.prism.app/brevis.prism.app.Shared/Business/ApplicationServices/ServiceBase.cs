using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using brevis.prism.app.Contracts.ApplicationServices;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.StoreApps.Interfaces;

namespace brevis.prism.app.Business.ApplicationServices
{
    public class ServiceBase
    {
        private IAppSettingsService _settingsService;
        private IAlertMessageService _alertService;
        private IResourceLoader _resourceLoader;

        public IAppSettingsService AppSettingsService { get { return _settingsService; } }
        public IAlertMessageService AlertMessageService { get { return _alertService; } }
        public IResourceLoader ResourceLoader { get { return _resourceLoader; } }

        public ServiceBase(
            IAppSettingsService appSettingsService,
            IAlertMessageService alertMessageService,
            IResourceLoader resourceLoader)
        {
            if (appSettingsService == null) throw new ArgumentNullException("appSettingsService");
            if (alertMessageService == null) throw new ArgumentNullException("alertMessageService");
            if (resourceLoader == null) throw new ArgumentNullException("resourceLoader");
            _settingsService = appSettingsService;
            _alertService = alertMessageService;
            _resourceLoader = resourceLoader;
        }

        public async Task HandleSecurityExceptionAsync(SecurityException sex)
        {
            Debug.WriteLine("SecurityException: {0}", sex.Message);
        }
        public async Task HandleHttpRequestExceptionAsync(HttpRequestException hrex)
        {
            Debug.WriteLine("HttpRequestException: {0}", hrex.Message);
        }

        public async Task HandleExceptionAsync(Exception ex)
        {
            Debug.WriteLine("Exception: {0}", ex.Message);
        }
            
    }
}
