﻿namespace EssentialsPlugin.ChatHandlers.AdminDelete
{
	using System.Collections.Generic;
	using System.Linq;
	using EssentialsPlugin.Utility;
	using Sandbox.Common.ObjectBuilders;
	using Sandbox.ModAPI;
	using SEModAPIInternal.API.Common;
	using SEModAPIInternal.API.Entity.Sector.SectorObject;
	using VRage.Game;
	using VRage.Game.Entity;
	using VRage.Game.ModAPI;
	using VRage.ModAPI;
    using VRage.Game.ObjectBuilders;

	public class HandleAdminDeleteCleanup : ChatHandlerBase
	{
		public override string GetHelp( )
		{
			return "This command allows you to scan for grids that are considered trash and delete them.  A trash item contains no function or terminal blocks.  Adding function or terminal to the command allows you to omit those search parameters.  Usage: /admin delete cleanup [functional] [terminal]";
		}
		public override string GetCommandText()
		{
			return "/admin delete cleanup";
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

		public override bool HandleCommand(ulong userId, string[] words)
		{
			HashSet<MyEntity> entitiesFound = CubeGrids.ScanCleanup(userId, words);

			foreach (MyEntity entity in entitiesFound)
			{
				CubeGridEntity removeEntity = new CubeGridEntity((MyObjectBuilder_CubeGrid)entity.GetObjectBuilder(), entity);
				removeEntity.Dispose();

				IMyCubeGrid grid = (IMyCubeGrid)entity;
				long ownerId = 0;
				string ownerName = "";
				if (grid.BigOwners.Count > 0)
				{
					ownerId = grid.BigOwners.First();
					ownerName = PlayerMap.Instance.GetPlayerItemFromPlayerId(ownerId).Name;
				}

				Log.Info("Cleanup", string.Format("Cleanup Removed Grid - Id: {0} Display: {1} OwnerId: {2} OwnerName: {3}", entity.EntityId, entity.DisplayName, ownerId, ownerName));
			}
			return true;
		}
	}
}
