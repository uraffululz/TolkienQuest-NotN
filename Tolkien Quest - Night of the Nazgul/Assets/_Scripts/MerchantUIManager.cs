using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MerchantUIManager : MonoBehaviour {

	[SerializeField] GameObject mapSceneManager;
	[SerializeField] EncounterMerchantScriptable merchantScript;
	[SerializeField] GameObject inventoryManager;

	//[SerializeField] ScriptableObject[] objectScripts;
	public Text[] itemNameTexts;
	public Text[] itemCostTexts;

	public Button[] purchaseButtons;

	public Text currentMoneyText;

	GameObject emptySlotParent = null;

	[SerializeField] GameObject itemHostPrefab;


	void Start() {
        
    }


    void Update() {
        
    }


	public void PurchaseItem (Button purchaseButtonClicked) {
		merchantScript = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().myMerchant;
		//int itemIndex = Array.FindIndex<Button>(purchaseButtons, purchaseButtons => purchaseButtonClicked);
		int itemIndex = 0;

//TOMAYBEDO This probably isn't very efficient (why not?). Try using System.Array or maybe even just a List<>
		for (int i = 0; i < purchaseButtons.Length; i++) {
			if (purchaseButtonClicked == purchaseButtons[i]) {
				itemIndex = i;
			}
		}

		int itemCost = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().myMerchant.itemPrices[itemIndex];
		EncounterMerchantScriptable.coinTypes myCoinType = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().myMerchant.coinType[itemIndex];

		//Debug.Log(itemCost + myCoinType.ToString());

		AddObjectToInventory(merchantScript.itemScripts[itemIndex], merchantScript.objectQuantities[itemIndex], myCoinType, itemCost);

		currentMoneyText.text = "Current Money: \n" +
									"Silver: " + InventoryManager.silverCarried + "\n" +
									"Copper: " + InventoryManager.copperCarried;
	}


	void AddObjectToInventory(ScriptableObject purchasedObject, int purchasedQuantity, EncounterMerchantScriptable.coinTypes coinTypeUsed, int howManyCoins) {
		bool itemSlotFound = false;

		for (int i = 3; i < inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryScriptables.Length; i++) {
			//Compare purchased object with items in inventory, until a match is found
			if (!itemSlotFound && inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryScriptables[i] != null && purchasedObject == inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryScriptables[i]) {
				Debug.Log("Purchased item matches object in inventory");
				//If the items are stackable
				if (purchasedObject.GetType() == typeof(InventoryItemScriptable)) {
					InventoryItemScriptable purchasedItem = (InventoryItemScriptable)purchasedObject;
					GameObject matchedItemHost = inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents[i].transform.GetChild(0).gameObject;
					if (purchasedItem.isStackable) {
						Debug.Log("Purchased object stacks with object in inventory");

						matchedItemHost.GetComponent<InventoryScriptableReader>().itemQuantity += purchasedQuantity;
						print(matchedItemHost.GetComponent<InventoryScriptableReader>().itemQuantity);
						matchedItemHost.GetComponent<Text>().text = (matchedItemHost.GetComponent<InventoryScriptableReader>().objectName + " x " +
																	matchedItemHost.GetComponent<InventoryScriptableReader>().itemQuantity);

						//inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryScriptables[i] = matchedItem;
						inventoryManager.GetComponent<MapSceneInventoryManager>().LogInventory();

						SpendPlayerMoney(coinTypeUsed, howManyCoins);

						itemSlotFound = true;
						break;
					}
					//else {
					//	Debug.Log("Objects do not stack. Finding empty slot");
					//	FindEmptyInventorySlot(purchasedObject, coinTypeUsed, howManyCoins);
					//}
				}
				
				//itemMatch = true;
			}
			//else {
			//	print("Purchased object didn't find a match");
			//	Debug.Log("itemSlotFound: " + itemSlotFound.ToString());
			//	if (inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryScriptables[i] != null) {
			//		Debug.Log("There is an item here: " + inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryScriptables[i]);
			//		if (purchasedObject == inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryScriptables[i]) {
			//			Debug.Log("The items definitely match: " + purchasedObject.name + "::" + inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryScriptables[i].name);
			//		}
			//	}
			//}
		}

		if (!itemSlotFound) {
			Debug.Log("Objects do not match/stack. Finding empty slot");
			FindEmptyInventorySlot(purchasedObject, purchasedQuantity, coinTypeUsed, howManyCoins);
		}
	}


	void FindEmptyInventorySlot(ScriptableObject objectPurchased, int quantityPurchased, EncounterMerchantScriptable.coinTypes coinUsed, int objectPrice) {
		bool foundEmptyParent = false;

		for (int ip = 3; ip < inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents.Length; ip++) {
			//GameObject Objectparent = inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents[ip];

			if (!foundEmptyParent && inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents[ip].transform.childCount == 0) {
				//Spawn a new ItemHost and populate it with the Scriptable for the purchased item
				Debug.Log("Spawning new ItemHost with purchased object and placing in inventory");

				emptySlotParent = inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents[ip];

				foundEmptyParent = true;
				break;
			}
		}

		if (foundEmptyParent/* && emptySlotParent != null*/) {
			GameObject newHost = Instantiate(itemHostPrefab, emptySlotParent.transform.position, Quaternion.identity, emptySlotParent.transform);
			newHost.GetComponent<InventoryScriptableReader>().objectScript = objectPurchased;
			newHost.GetComponent<InventoryScriptableReader>().itemQuantity = quantityPurchased;
			newHost.GetComponent<InventoryScriptableReader>().SetMyObjectType();

			SpendPlayerMoney(coinUsed, objectPrice);

			inventoryManager.GetComponent<MapSceneInventoryManager>().LogInventory();
		}
		else {
			Debug.Log("Couldn't find an empty slot to place the purchased object");
		}

		emptySlotParent = null;
	}


	public static void SpendPlayerMoney(EncounterMerchantScriptable.coinTypes coin, int price) {
		if (coin == EncounterMerchantScriptable.coinTypes.silver) {
			if (InventoryManager.silverCarried >= price) {
				InventoryManager.silverCarried -= price;
			}
			else {
				print("You don't have enough silver to purchase this item.");
			}
		}
		else if (coin == EncounterMerchantScriptable.coinTypes.copper) {
			if (InventoryManager.copperCarried >= price) {
				InventoryManager.copperCarried -= price;
			}
			else if (InventoryManager.copperCarried < price && InventoryManager.silverCarried > 0) {
				InventoryManager.silverCarried--;
				InventoryManager.copperCarried += 100;
				InventoryManager.copperCarried -= price;
			}
			else {
				print("You don't have enough copper to purchase this item.");
			}
		}
		else {
			Debug.Log("I can't identify which type of coin was used");
		}
	}
}
