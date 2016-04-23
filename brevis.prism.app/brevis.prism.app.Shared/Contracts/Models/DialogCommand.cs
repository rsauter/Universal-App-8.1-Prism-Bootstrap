using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Resources;

namespace brevis.prism.app.Contracts.Models
{
    public class DialogCommand
    {
        public object Id { get; set; }
        public string Label { get; set; }
        public Action Invoked { get; set; }

        public static IEnumerable<DialogCommand> GetConfirmDialogCommands(Action okAction, Action cancelAction)
        {
            var commands = new List<DialogCommand>();
            commands.Add(new DialogCommand
            {
                Id = Guid.NewGuid(),
                Label = ResourceLoader.GetForCurrentView().GetString("ConfirmOkButtonLabel"),
                Invoked = okAction
            });
            commands.Add(new DialogCommand
            {
                Id = Guid.NewGuid(),
                Label = ResourceLoader.GetForCurrentView().GetString("ConfirmCancelButtonLabel"),
                Invoked = cancelAction
            });

            return commands;
        }
    }
}
