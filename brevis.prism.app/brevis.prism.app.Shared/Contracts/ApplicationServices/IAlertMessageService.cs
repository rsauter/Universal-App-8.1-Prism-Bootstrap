using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using brevis.prism.app.Contracts.Models;

namespace brevis.prism.app.Contracts.ApplicationServices
{
    public interface IAlertMessageService
    {
        Task ShowAsync(string message, string title);

        Task ShowAsync(string message, string title, IEnumerable<DialogCommand> dialogCommands);
    }
}
