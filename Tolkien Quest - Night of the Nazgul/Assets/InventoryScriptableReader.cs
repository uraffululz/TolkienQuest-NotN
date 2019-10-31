using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScriptableReader : MonoBehaviour {

	public ScriptableObject objectScript;
	public InventoryItemScriptable itemScript;
	public InventoryWeaponScriptable weaponScript;
	public InventoryArmorScriptable armorScript;

	public int itemQuantity;



	public void SetMyObjectType () {
		if (objectScript != null) {
			if (objectScript.GetType() == typeof(InventoryItemScriptable)) {
				itemScript = objectScript as InventoryItemScriptable;
			}
			else if (objectScript.GetType() == typeof(InventoryWeaponScriptable)) {
				weaponScript = objectScript as InventoryWeaponScriptable;
			}
			else if (objectScript.GetType() == typeof(InventoryArmorScriptable)) {
				armorScript = objectScript as InventoryArmorScriptable;
			}
		}
    }
}
