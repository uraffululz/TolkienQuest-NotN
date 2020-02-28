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
	public int myItem1Quantity;
	public ScriptableObject myItem2;
	public int myItem2Quantity;
	public ScriptableObject myItem3;
	public int myItem3Quantity;
	public ScriptableObject myItem4;
	public int myItem4Quantity;

	public bool hasItemRanges;
	public enum addSkillToRange {None, Trickery};
	public addSkillToRange addedSkill;
	//range variables
	public int itemRange1Min; public int itemRange1Max;
	public bool range1MultipleItems;
	public ScriptableObject[] range1Items;
	public int[] range1ItemQuantities;
	[Space]
	public int itemRange2Min; public int itemRange2Max;
	public bool range2MultipleItems;
	public ScriptableObject[] range2Items;
	public int[] range2ItemQuantities;
	[Space]
	public int itemRange3Min; public int itemRange3Max;
	public bool range3MultipleItems;
	public ScriptableObject[] range3Items;
	public int[] range3ItemQuantities;
	[Space]
	public int itemRange4Min; public int itemRange4Max;
	public bool range4MultipleItems;
	public ScriptableObject[] range4Items;
	public int[] range4ItemQuantities;
	[Space]
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
	public int NumberOfItemsGained;
	public bool useItem;
	public int hasItemMovesToEncounter;
	public int lacksItemMovesToEncounter;
	public bool lackingItemMeansDiseased;
	public bool lackingItemMeansPoisoned;

	[Space]

	public bool gainDaggerIfYouHaveNoWeapon;
	public InventoryWeaponScriptable dagger;
}
