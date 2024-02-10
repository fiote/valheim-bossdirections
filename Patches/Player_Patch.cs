﻿using HarmonyLib;
namespace BossDirections.Patches {

	[HarmonyPatch(typeof(Player), "UseHotbarItem")]

	public class Player_UseHotbarItem_Patch {
		[HarmonyPrefix]
		static bool Prefix(Player __instance, int index) {
			var item = __instance.GetInventory().GetItemAt(index - 1, 0);
			if (item == null) return true;

			var hoverObject = __instance.GetHoverObject();
			if (hoverObject == null) return true;
			
			var fireplaceObject = hoverObject.GetComponent<Fireplace>();
			var fireplaceParent = hoverObject.GetComponentInParent<Fireplace>();
			if (fireplaceObject == null && fireplaceParent == null) return false;

			var offered = BossDirections.TryOffer(item);
			if (offered) return false;

			return true;
		}
	}

}