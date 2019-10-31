using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemListScriptable", menuName = "ScriptableObjects/EncounterObtainedItemListScriptable", order = 0)]
public class EncounterObtainedItemList : ScriptableObject {

	public int silverEarned;
	public int copperEarned;

	[Space]
	public ScriptableObject[] myItemList;

	public ScriptableObject myItem1;
	public ScriptableObject myItem2;
	public ScriptableObject myItem3;
	public ScriptableObject myItem4;

	[Space]

	public int itemQuantity1;
	public int itemQuantity2;
	public int itemQuantity3;
	public int itemQuantity4;

	[Space]

	public bool gainDaggerIfYouHaveNoWeapon;
	public InventoryWeaponScriptable dagger;
}
