using System;
using System.Collections.Generic;
using System.Text;

namespace brevis.prism.app.Contracts.ApplicationServices
{
    public interface IAppSettingsService
    {
        string SerialNr { get; set; }
        string AppVersion { get; }
    }
}
