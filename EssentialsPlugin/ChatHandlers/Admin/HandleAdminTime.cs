namespace EssentialsPlugin.ChatHandlers.Admin
{
    using System;
    using System.Linq;
    using EssentialsPlugin.Utility;
    using Sandbox.Common;
    using VRage.Game;

    class HandleAdminTime : ChatHandlerBase
    {
        public override string GetHelp()
        {
            return "Sets global time of day.  Usage: /admin time <hh:mm>";
        }

        public override string GetCommandText()
        {
            return "/admin time";
        }

        public override Communication.ServerDialogItem GetHelpDialog()
        {
            Communication.ServerDialogItem DialogItem = new Communication.ServerDialogItem();
            DialogItem.title = "Help";
            DialogItem.header = "Admin Time of Day";
            DialogItem.content = "Sets global time of day.|" +
                "Usage: /admin time <hh:mm>|";
            DialogItem.buttonText = "close";
            return DialogItem;
        }

        public override bool IsAdminCommand()
        {
            return true;
        }

        public override bool AllowedInConsole()
        {
            return true;
        }

        public override bool HandleCommand(ulong userId, string[] words)
        {

            Wrapper.GameAction( () =>
                {
                    GC.Collect();
                    Communication.SendPublicInformation(string.Format("New time of day: {0}",  ));
                }
            );

            return true;
        }
    }
}
