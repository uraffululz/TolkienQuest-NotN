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

	public bool hasItemRanges;
	//range variables
	public int itemRange1Min; public int itemRange1Max;
	public bool range1MultipleItems;
	public ScriptableObject[] range1Items;
	public int itemRange2Min; public int itemRange2Max;
	public bool range2MultipleItems;
	public ScriptableObject[] range2Items;
	public int itemRange3Min; public int itemRange3Max;
	public bool range3MultipleItems;
	public ScriptableObject[] range3Items;
	public int itemRange4Min; public int itemRange4Max;
	public bool range4MultipleItems;
	public ScriptableObject[] range4Items;
	public bool outOfRangeMovesToEncounter;
	public int encounterIndex;

	[Space]

	//public int itemQuantity1;
	//public int itemQuantity2;
	//public int itemQuantity3;
	//public int itemQuantity4;

	//[Space]

	//public bool checksForSpecificItem;
	public ScriptableObject itemChecked;
	public int checksItemQuantityMin;
	public int checksItemQuantityMax;
	public ScriptableObject itemsGainedViaQuantity;
	public bool useItem;

	[Space]

	public bool gainDaggerIfYouHaveNoWeapon;
	public InventoryWeaponScriptable dagger;
}
