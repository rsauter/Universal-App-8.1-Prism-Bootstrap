using System;
using System.Collections.Generic;
using System.Text;

namespace brevis.prism.app.Contracts.ApplicationServices
{
    interface INotificationService
    {
        void SendLocalTileNotification(string content);
    }
}
