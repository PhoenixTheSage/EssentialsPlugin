﻿namespace EssentialsPlugin.ChatHandlers.Admin
{
    using Sandbox.Definitions;
    using Utility;
    public class HandleAdminTest : ChatHandlerBase
	{

    public override string GetHelp()
		{
			return "For testing.";
		}

		public override string GetCommandText()
		{
			return "/admin test";
		}

        public override Communication.ServerDialogItem GetHelpDialog( )
        {
            Communication.ServerDialogItem DialogItem = new Communication.ServerDialogItem( );
            DialogItem.title = "Help";
            DialogItem.header = "";
            DialogItem.content = GetHelp( );
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

        public override bool HandleCommand( ulong userId, string[ ] words )
        {
            //CargoShips.SpawnCargoShip( );
            foreach ( var material in MyDefinitionManager.Static.GetTransparentMaterialDefinitions( ) )
            {
                Essentials.Log.Info( material.Texture );
            }
            foreach (var material in MyDefinitionManager.Static.GetTransparentMaterialDefinitions())
            {
                Essentials.Log.Info(material.ToString);
            }
            return true;
        }

	}

}

