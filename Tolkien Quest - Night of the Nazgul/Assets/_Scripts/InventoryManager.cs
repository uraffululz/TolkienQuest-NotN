using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryManager {

	public static int silverCarried;
	public static int copperCarried;
	public static int arrowsCarried;

	public static GameObject daggerWorn;
	public static GameObject armorWorn;
	public static GameObject cloakWorn;

	public static GameObject slot1Item;
	public static GameObject slot2Item;
	public static GameObject slot3Item;
	public static GameObject slot4Item;
	public static GameObject slot5Item;
	public static GameObject slot6Item;
	public static GameObject slot7Item;
	public static GameObject slot8Item;
	public static GameObject slot9Item;
	public static GameObject slot10Item;
	public static GameObject slot11Item;
	public static GameObject slot12Item;
	public static GameObject[] inventoryItems = new GameObject[15] {cloakWorn, armorWorn, daggerWorn, slot1Item, slot2Item, slot3Item,
		slot4Item, slot5Item, slot6Item, slot7Item, slot8Item, slot9Item, slot10Item, slot11Item, slot12Item};

	[Space]

	public static ScriptableObject daggerWornScriptable;
	public static ScriptableObject armorWornScriptable;
	public static ScriptableObject cloakWornScriptable;
	public static ScriptableObject slot1Scriptable;
	public static ScriptableObject slot2Scriptable;
	public static ScriptableObject slot3Scriptable;
	public static ScriptableObject slot4Scriptable;
	public static ScriptableObject slot5Scriptable;
	public static ScriptableObject slot6Scriptable;
	public static ScriptableObject slot7Scriptable;
	public static ScriptableObject slot8Scriptable;
	public static ScriptableObject slot9Scriptable;
	public static ScriptableObject slot10Scriptable;
	public static ScriptableObject slot11Scriptable;
	public static ScriptableObject slot12Scriptable;
	public static ScriptableObject[] inventoryItemScriptables = new ScriptableObject[] {cloakWornScriptable, armorWornScriptable, daggerWornScriptable, slot11Scriptable, slot2Scriptable,
		slot3Scriptable, slot4Scriptable, slot5Scriptable, slot6Scriptable, slot7Scriptable, slot8Scriptable, slot9Scriptable, slot10Scriptable, slot11Scriptable, slot12Scriptable};
	


	//public static GameObject startingItemTaken;
}
