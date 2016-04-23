using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Resources;
using brevis.prism.app.Contracts.ApplicationServices;

namespace brevis.prism.app.Business.ApplicationServices
{
    public class NotificationService : INotificationService
    {
        private ResourceLoader _resourceLoader;

        public NotificationService(ResourceLoader resourceLoader)
        {
            if (resourceLoader == null) throw new ArgumentNullException("resourceLoader");
            _resourceLoader = resourceLoader;
        }

        public void SendLocalTileNotification(string content)
        {
            throw new NotImplementedException();
        }
    }
}
