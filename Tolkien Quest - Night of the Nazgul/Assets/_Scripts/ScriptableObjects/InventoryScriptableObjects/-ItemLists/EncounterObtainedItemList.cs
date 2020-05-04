using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemListScriptable", menuName = "ScriptableObjects/EncounterObtainedItemListScriptable", order = 0)]
public class EncounterObtainedItemList : ScriptableObject {

	[SerializeField] string[] Notes;

	public int silverEarned;
	public int copperEarned;

	[Space]
	public ScriptableObject[] myItemList;
	//public int[] myItemQuantities;

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

	[Space]

	//range variables
	[Header("Range 1 Attributes", order = 0)]
	public int itemRange1Min; public int itemRange1Max;
	public bool range1MultipleItems;
	public ScriptableObject[] range1Items;
	public int[] range1ItemQuantities;
	public bool range1RecoversMoneyPouch;
	public bool range1RecoversDagger;
	//public bool range1ConfirmsObject;
	public InventoryItemScriptable range1ObjectToConfirm;
	public int range1ConfirmQuantity;

	[Space]
	[Header("Range 2 Attributes", order = 0)]
	public int itemRange2Min; public int itemRange2Max;
	public bool range2MultipleItems;
	public ScriptableObject[] range2Items;
	public int[] range2ItemQuantities;
	public bool range2RecoversMoneyPouch;
	public bool range2RecoversDagger;
	public bool range2RecoversRandomWeapon;
	//public bool range2ConfirmsObject;
	public InventoryItemScriptable range2ObjectToConfirm;
	public int range2ConfirmQuantity;
	public int obtainsArrowsInRange2;

	[Space]
	[Header("Range 3 Attributes", order = 0)]
	public int itemRange3Min; public int itemRange3Max;
	public bool range3MultipleItems;
	public ScriptableObject[] range3Items;
	public int[] range3ItemQuantities;
	//public bool range3RecoversMoneyPouch;
	//public bool range3RecoversDagger;


	[Space]
	[Header("Range 4 Attributes", order = 0)]
	public int itemRange4Min; public int itemRange4Max;
	public bool range4MultipleItems;
	public ScriptableObject[] range4Items;
	public int[] range4ItemQuantities;

	[Space]
	[Header("Out of Range Attributes", order = 0)]
	public bool outOfRangeMovesToEncounter;
	public int outOfRangeEncounterIndex;
	public bool outOfRangeRecoversAllEquipment;
	public bool outOfRangeRecoversMoneyPouch;

	[Space]

	//public int itemQuantity1;
	//public int itemQuantity2;
	//public int itemQuantity3;
	//public int itemQuantity4;

	//[Space]

	//public bool checksForSpecificItem;
	public ScriptableObject itemChecked;
	public bool quantityCheckIncludesZero;
	public int checksItemQuantityMin;
	public int checksItemQuantityMax;
	public ScriptableObject itemsGainedViaQuantity;
	public int NumberOfItemsGained;

	[Space]

	public bool useItem;
	public int hasItemMovesToEncounter;
	public int lacksItemMovesToEncounter;
	//public bool lackingItemMeansDiseased;
	//public bool lackingItemMeansPoisoned;

	[Space]

	public bool gainDaggerIfYouHaveNoWeapon;
	public InventoryWeaponScriptable dagger;
	public InventoryWeaponScriptable silverDagger;

}
