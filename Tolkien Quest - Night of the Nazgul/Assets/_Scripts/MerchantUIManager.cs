using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MerchantUIManager : MonoBehaviour {

	public Text[] itemNameTexts;
	public Text[] itemCostTexts;
	public Button[] purchaseButtons;

	public Text currentMoneyText;


	void Start() {
        
    }


    void Update() {
        
    }


	public void PurchaseItem (Button purchaseButtonClicked) {
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

		if (myCoinType == EncounterMerchantScriptable.coinTypes.silver) {
			if (InventoryManager.silverCarried >= itemCost) {
				InventoryManager.silverCarried -= itemCost;
			}
			else {
				print("You don't have enough silver to purchase this item.");
			}

			//print("This item costs " + itemCost + " " + myCoinType);
		}
		else if (myCoinType == EncounterMerchantScriptable.coinTypes.copper) {
			if (InventoryManager.copperCarried >= itemCost) {
				InventoryManager.copperCarried -= itemCost;
			}
			else if (InventoryManager.copperCarried < itemCost && InventoryManager.silverCarried > 0) {
				InventoryManager.silverCarried--;
				InventoryManager.copperCarried += 100;
				InventoryManager.copperCarried -= itemCost;
			}
			else {
				print("You don't have enough copper to purchase this item.");
			}

			//print ("This item costs " + itemCost + " " + myCoinType);
		}

		currentMoneyText.text = "Current Money: \n" +
									"Silver: " + InventoryManager.silverCarried + "\n" +
									"Copper: " + InventoryManager.copperCarried;
	}
}
