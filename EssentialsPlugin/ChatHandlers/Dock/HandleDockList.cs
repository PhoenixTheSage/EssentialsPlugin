﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

using EssentialsPlugin.Utility;

using Sandbox.ModAPI;
using Sandbox.Common.ObjectBuilders;

using VRageMath;

using SEModAPIInternal.API.Entity;
using SEModAPIInternal.API.Entity.Sector.SectorObject;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid.CubeBlock;
using SEModAPIInternal.API.Common;

namespace EssentialsPlugin.ChatHandlers
{
	public class HandleDockList : ChatHandlerBase
	{
		public override string GetHelp()
		{
			return "This command lists if there are ships in a docking zone.  Usage: /dock list <dockingZoneName>";
		}

		public override string GetCommandText()
		{
			return "/dock list";
		}

		public override bool IsAdminCommand()
		{
			return false;
		}

		public override bool AllowedInConsole()
		{
			return false;
		}

		public override bool HandleCommand(ulong userId, string[] words)
		{
			if (!PluginSettings.Instance.DockingEnabled)
				return false;

			if (words.Length < 1)
			{
				Communication.SendPrivateInformation(userId, GetHelp());
				return true;
			}

			String pylonName = String.Join(" ", words);
			if (PlayerMap.Instance.GetPlayerIdsFromSteamId(userId).Count < 1)
			{
				Communication.SendPrivateInformation(userId, string.Format("Unable to find player Id: {0}", userId));
				return true;
			}

			long playerId = PlayerMap.Instance.GetPlayerIdsFromSteamId(userId).First();

			Dictionary<String, List<IMyCubeBlock>> testList;
			List<IMyCubeBlock> beaconList;
			DockingZone.FindByName(pylonName, out testList, out beaconList);

			if (beaconList.Count == 4)
			{
				foreach (IMyCubeBlock entity in beaconList)
				{
					if (!Entity.CheckOwnership(entity, playerId))
					{
						Communication.SendPrivateInformation(userId, string.Format("You do not have permission to use '{0}'.  You must either own all the beacons or they must be shared with faction.", pylonName));
						return true;
					}
				}

				IMyCubeBlock e = beaconList.First();
				IMyCubeGrid parent = (IMyCubeGrid)e.Parent;

				long[] beaconListIds = beaconList.Select(p => p.EntityId).ToArray();
				long ownerId = beaconList.First().OwnerId;
				DockingItem dockingItem = Docking.Instance.Find(d => d.PlayerId == ownerId && d.TargetEntityId == parent.EntityId && d.DockingBeaconIds.Intersect(beaconListIds).Count() == 4);

				if (dockingItem != null)
				{
					Communication.SendPrivateInformation(userId, string.Format("You have a ship docked in docking zone '{0}'.", pylonName));
					return true;
				}
				else
				{
					Communication.SendPrivateInformation(userId, string.Format("No ships are docked in docking zone '{0}'.", pylonName));
				}
			}
			else if (beaconList.Count > 4)
			{
				Communication.SendPrivateInformation(userId, string.Format("Too many beacons with the name or another zone with the name '{0}'.  Place only 4 beacons to create a zone or try a different zone name.", pylonName));
			}
			else
			{
				Communication.SendPrivateInformation(userId, string.Format("Can not locate docking zone '{0}'", pylonName));
			}

			return true;
		}
	}
}
