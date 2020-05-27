using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSceneInventoryManager : MonoBehaviour {

	[SerializeField] MapSceneManager mapSceneManager;
	[SerializeField] GameObject itemHost;
	[SerializeField] GameObject itemListInventoryManager;

	public Text pouchParent;
	public GameObject[] inventoryParents;
	//public GameObject[] itemHosts;
	public List<ScriptableObject> savedInventoryScripts;
	public List<int> savedInventoryQuantities;
	public int savedSilver;
	public int savedCopper;

	public ScriptableObject[] inventoryScriptables;

	[Space]

	[SerializeField] InventoryWeaponScriptable silverDagger;
	[SerializeField] InventoryWeaponScriptable dagger;

	[Space]

	[SerializeField] InventoryItemScriptable healingHerbScript;
	[SerializeField] GameObject herbParent;

	[Space]

	[SerializeField] Text charSheetArrowText;
	
	
	void Start() {
		InitializeInventory();
	}


	public void InitializeInventory () {
		inventoryScriptables = new ScriptableObject[15] {InventoryManager.cloakWornScriptable, InventoryManager.armorWornScriptable, InventoryManager.daggerWornScriptable,
			InventoryManager.slot1Scriptable, InventoryManager.slot2Scriptable, InventoryManager.slot3Scriptable, InventoryManager.slot4Scriptable, InventoryManager.slot5Scriptable,
			InventoryManager.slot6Scriptable, InventoryManager.slot7Scriptable, InventoryManager.slot8Scriptable, InventoryManager.slot9Scriptable, InventoryManager.slot10Scriptable,
			InventoryManager.slot11Scriptable, InventoryManager.slot12Scriptable};

		for (int i = 0; i < inventoryParents.Length; i++) {
			if (inventoryScriptables[i] != null) {
				if (inventoryParents[i].transform.childCount == 0) {
					GameObject newItemHost = Instantiate(itemHost, inventoryParents[i].transform.position, Quaternion.identity, inventoryParents[i].transform);

					newItemHost.GetComponent<InventoryScriptableReader>().objectScript = inventoryScriptables[i];
					//newItemHost.GetComponent<InventoryScriptableReader>().itemQuantity = ;
					newItemHost.GetComponent<Text>().text = newItemHost.GetComponent<InventoryScriptableReader>().objectScript.name;
					newItemHost.GetComponent<InventoryScriptableReader>().SetMyObjectType();

					GameObject objectInInventory = inventoryParents[i];
					if (objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().myObjectType == InventoryScriptableReader.objectType.item) {
						switch (i) {
							case 0:
								if (InventoryManager.cloakQuantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.cloakQuantity;
								}
								break;
							case 1:
								if (InventoryManager.armorQuantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.armorQuantity;
								}
								break;
							case 2:
								if (InventoryManager.daggerQuantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.daggerQuantity;
								}
								break;
							case 3:
								if (InventoryManager.slot1Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot1Quantity;
								}
								break;
							case 4:
								if (InventoryManager.slot2Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot2Quantity;
								}
								break;
							case 5:
								if (InventoryManager.slot3Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot3Quantity;
								}
								break;
							case 6:
								if (InventoryManager.slot4Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot4Quantity;
								}
								break;
							case 7:
								if (InventoryManager.slot5Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot5Quantity;
								}
								break;
							case 8:
								if (InventoryManager.slot6Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot6Quantity;
								}
								break;
							case 9:
								if (InventoryManager.slot7Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot7Quantity;
								}
								break;
							case 10:
								if (InventoryManager.slot8Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot8Quantity;
								}
								break;
							case 11:
								if (InventoryManager.slot9Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot9Quantity;
								}
								break;
							case 12:
								if (InventoryManager.slot10Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot10Quantity;
								}
								break;
							case 13:
								if (InventoryManager.slot11Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot11Quantity;
								}
								break;
							case 14:
								if (InventoryManager.slot12Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot12Quantity;
								}
								break;
							default:
								print("Something fucked up here.");
								break;
						}

						objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().InitializeItem();

					}
					//print("Populated inventory");
				}
				else {
					GameObject objectInInventory = inventoryParents[i];
					if (objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().myObjectType == InventoryScriptableReader.objectType.item) {
						switch (i) {
							case 0:
								if (InventoryManager.cloakQuantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.cloakQuantity;
								}
								break;
							case 1:
								if (InventoryManager.armorQuantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.armorQuantity;
								}
								break;
							case 2:
								if (InventoryManager.daggerQuantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.daggerQuantity;
								}
								break;
							case 3:
								if (InventoryManager.slot1Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot1Quantity;
								}
								break;
							case 4:
								if (InventoryManager.slot2Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot2Quantity;
								}
								break;
							case 5:
								if (InventoryManager.slot3Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot3Quantity;
								}
								break;
							case 6:
								if (InventoryManager.slot4Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot4Quantity;
								}
								break;
							case 7:
								if (InventoryManager.slot5Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot5Quantity;
								}
								break;
							case 8:
								if (InventoryManager.slot6Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot6Quantity;
								}
								break;
							case 9:
								if (InventoryManager.slot7Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot7Quantity;
								}
								break;
							case 10:
								if (InventoryManager.slot8Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot8Quantity;
								}
								break;
							case 11:
								if (InventoryManager.slot9Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot9Quantity;
								}
								break;
							case 12:
								if (InventoryManager.slot10Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot10Quantity;
								}
								break;
							case 13:
								if (InventoryManager.slot11Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot11Quantity;
								}
								break;
							case 14:
								if (InventoryManager.slot12Quantity != 0) {
									objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = InventoryManager.slot12Quantity;
								}
								break;
							default:
								print("Something fucked up here.");
								break;
						}

						objectInInventory.transform.GetChild(0).GetComponent<InventoryScriptableReader>().InitializeItem();
					}
				}
			}
			else {//If inventoryScriptables[i] == null
				if (inventoryParents[i].transform.childCount > 0) {
					Destroy(inventoryParents[i].transform.GetChild(0).gameObject);
				}
			}
		}

		pouchParent.text = ("Silver: " + InventoryManager.silverCarried + " | Copper: " + InventoryManager.copperCarried);
		charSheetArrowText.text = ("Arrows: " + InventoryManager.arrowsCarried.ToString());
	}


	public void LogInventory() {
		for (int i = 0; i < inventoryParents.Length; i++) {
			if (inventoryParents[i].transform.childCount != 0) {
				inventoryParents[i].transform.GetChild(0).GetComponent<InventoryScriptableReader>().fromTakeOneList = false;
				inventoryScriptables[i] = inventoryParents[i].GetComponentInChildren<InventoryScriptableReader>().objectScript;

				switch (i) {
					case 0:
						InventoryManager.cloakQuantity = inventoryParents[0].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 1:
						InventoryManager.armorQuantity = inventoryParents[1].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 2:
						InventoryManager.daggerQuantity = inventoryParents[2].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 3:
						InventoryManager.slot1Quantity = inventoryParents[3].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 4:
						InventoryManager.slot2Quantity = inventoryParents[4].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 5:
						InventoryManager.slot3Quantity = inventoryParents[5].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 6:
						InventoryManager.slot4Quantity = inventoryParents[6].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 7:
						InventoryManager.slot5Quantity = inventoryParents[7].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 8:
						InventoryManager.slot6Quantity = inventoryParents[8].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 9:
						InventoryManager.slot7Quantity = inventoryParents[9].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 10:
						InventoryManager.slot8Quantity = inventoryParents[10].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 11:
						InventoryManager.slot9Quantity = inventoryParents[11].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 12:
						InventoryManager.slot10Quantity = inventoryParents[12].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 13:
						InventoryManager.slot11Quantity = inventoryParents[13].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 14:
						InventoryManager.slot12Quantity = inventoryParents[14].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					default:
						print("Something fucked up here.");
						break;
				}
			}
			else {
				inventoryScriptables[i] = null;

				switch (i) {
					case 0:
						InventoryManager.cloakQuantity = 0;
						break;
					case 1:
						InventoryManager.armorQuantity = 0;
						break;
					case 2:
						InventoryManager.daggerQuantity = 0;
						break;
					case 3:
						InventoryManager.slot1Quantity = 0;
						break;
					case 4:
						InventoryManager.slot2Quantity = 0;
						break;
					case 5:
						InventoryManager.slot3Quantity = 0;
						break;
					case 6:
						InventoryManager.slot4Quantity = 0;
						break;
					case 7:
						InventoryManager.slot5Quantity = 0;
						break;
					case 8:
						InventoryManager.slot6Quantity = 0;
						break;
					case 9:
						InventoryManager.slot7Quantity = 0;
						break;
					case 10:
						InventoryManager.slot8Quantity = 0;
						break;
					case 11:
						InventoryManager.slot9Quantity = 0;
						break;
					case 12:
						InventoryManager.slot10Quantity = 0;
						break;
					case 13:
						InventoryManager.slot11Quantity = 0;
						break;
					case 14:
						InventoryManager.slot12Quantity = 0;
						break;
					default:
						print("Something fucked up here.");
						break;
				}
			}
		}

		InventoryManager.cloakWornScriptable = inventoryScriptables[0];
		InventoryManager.armorWornScriptable = inventoryScriptables[1];
		InventoryManager.daggerWornScriptable = inventoryScriptables[2];
		InventoryManager.slot1Scriptable = inventoryScriptables[3];
		InventoryManager.slot2Scriptable = inventoryScriptables[4];
		InventoryManager.slot3Scriptable = inventoryScriptables[5];
		InventoryManager.slot4Scriptable = inventoryScriptables[6];
		InventoryManager.slot5Scriptable = inventoryScriptables[7];
		InventoryManager.slot6Scriptable = inventoryScriptables[8];
		InventoryManager.slot7Scriptable = inventoryScriptables[9];
		InventoryManager.slot8Scriptable = inventoryScriptables[10];
		InventoryManager.slot9Scriptable = inventoryScriptables[11];
		InventoryManager.slot10Scriptable = inventoryScriptables[12];
		InventoryManager.slot11Scriptable = inventoryScriptables[13];
		InventoryManager.slot12Scriptable = inventoryScriptables[14];

				//print("Item Quantities: " + InventoryManager.cloakQuantity + ", " +
				//InventoryManager.armorQuantity + ", " +
				//InventoryManager.daggerQuantity + ", " +
				//InventoryManager.slot1Quantity + ", " +
				//InventoryManager.slot2Quantity + ", " +
				//InventoryManager.slot3Quantity + ", " +
				//InventoryManager.slot4Quantity + ", " +
				//InventoryManager.slot5Quantity + ", " +
				//InventoryManager.slot6Quantity + ", " +
				//InventoryManager.slot7Quantity + ", " +
				//InventoryManager.slot8Quantity + ", " +
				//InventoryManager.slot9Quantity + ", " +
				//InventoryManager.slot10Quantity + ", " +
				//InventoryManager.slot11Quantity + ", " +
				//InventoryManager.slot12Quantity);

		InitializeInventory();
	}


	public void EmptyInventory() {
		//Saving Inventory
		print("Saving inventory for recovery");
		savedInventoryScripts = new List<ScriptableObject>();
		savedInventoryQuantities = new List<int>();

		for (int i = 0; i < inventoryParents.Length; i++) {
			if (inventoryParents[i].transform.childCount != 0) {
				savedInventoryScripts.Add(inventoryParents[i].transform.GetChild(0).GetComponent<InventoryScriptableReader>().objectScript);
				savedInventoryQuantities.Add(inventoryParents[i].transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity);
			}
		}

		savedSilver = InventoryManager.silverCarried;
		savedCopper = InventoryManager.copperCarried;

		//Emptying Inventory
		foreach (GameObject itemParent in inventoryParents) {
			if (itemParent.transform.childCount != 0) {
				Destroy(itemParent.transform.GetChild(0).gameObject);
			}
		}

		inventoryScriptables = new ScriptableObject[15] {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null};
	}


//Incorporated into EmptyInventory()
	//public void SaveInventory() {
	//	print("Saving inventory for recovery");
	//	savedInventoryScripts = new List<ScriptableObject>();
	//	savedInventoryQuantities = new List<int>();

	//	for (int i = 0; i < inventoryParents.Length; i++) {
	//		if (inventoryParents[i].transform.childCount != 0) {
	//			savedInventoryScripts.Add(inventoryParents[i].transform.GetChild(0).GetComponent<InventoryScriptableReader>().objectScript);
	//			savedInventoryQuantities.Add(inventoryParents[i].transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity);
	//		}
	//	}

	//	savedSilver = InventoryManager.silverCarried;
	//	savedCopper = InventoryManager.copperCarried;
	//}


	public void RecoverSavedInventory(bool recoverAll, bool recoverPouch, bool recoverDagger, bool recoversRandomWeapon, InventoryItemScriptable confirmsItemInInventory, int quantityToConfirm) {
		List<ScriptableObject> recoveryScriptList = new List<ScriptableObject>();
		List<int> recoveryQuantitiesList = new List<int>();

		if (recoverAll) {
//TODO Currently this only allows the player to have 12 items in their inventory before things (probably) get fucky (because of the cloak/armor/dagger slots).
//Is that a problem?
			for (int i = 0; i < savedInventoryScripts.Count; i++) {
				recoveryScriptList.Add(savedInventoryScripts[i]);
				recoveryQuantitiesList.Add(savedInventoryQuantities[i]);
			}
		}

		if (recoverPouch) {
			InventoryManager.silverCarried = savedSilver;
			InventoryManager.copperCarried = savedCopper;
		}


		if (recoverDagger) {
			bool foundSilverDagger = false;
			bool foundDagger = false;

			//Search through the player's saved inventory for their SILVER DAGGER. If it can't be found, look for a REGULAR DAGGER. If not that, then give them nothing
			for (int i = 0; i < savedInventoryScripts.Count; i++) {
				if (savedInventoryScripts[i] == silverDagger) {
					recoveryScriptList.Add(savedInventoryScripts[i]);
					recoveryQuantitiesList.Add(savedInventoryQuantities[i]);

					savedInventoryScripts.RemoveAt(i);
					savedInventoryQuantities.RemoveAt(i);

					foundSilverDagger = true;
					break;
				}
			}

			if (!foundSilverDagger) {
				for (int i = 0; i < savedInventoryScripts.Count; i++) {
					if (savedInventoryScripts[i] == dagger) {
						recoveryScriptList.Add(savedInventoryScripts[i]);
						recoveryQuantitiesList.Add(1);

						savedInventoryScripts.RemoveAt(i);
						savedInventoryQuantities.RemoveAt(i);

						foundDagger = true;
						break;
					}
				}
			}

			if (foundSilverDagger) {
				print("You found the SILVER DAGGER from your previous inventory");
			}
			else if (foundDagger) {
				print("You found a DAGGER from your previous inventory");
			}
			else {
				print("You had NO DAGGERS in your previous inventory");
			}
		}

		if (recoversRandomWeapon) {
			//Search through the player's saved inventory for any weapon OTHER THAN A DAGGER.
			print("You recovered a RANDOM WEAPON from your PREVIOUS INVENTORY");
			List<ScriptableObject> randomWeaponScriptList = new List<ScriptableObject>();

			for (int i = 0; i < savedInventoryScripts.Count; i++) {
				if (savedInventoryScripts[i].GetType() == typeof(InventoryWeaponScriptable)) {
					randomWeaponScriptList.Add(savedInventoryScripts[i]);
				}
			}

			if (randomWeaponScriptList.Count > 0) {
				int randomWeaponChosen = Random.Range(0, randomWeaponScriptList.Count);

				recoveryScriptList.Add(randomWeaponScriptList[randomWeaponChosen]);
				recoveryQuantitiesList.Add(1);
			}
			else {
				print("NO RANDOM WEAPONS saved in previous inventory");
			}
		}

		if (confirmsItemInInventory != null) {

			int objectsTotal = 0;

			for (int i = 0; i < savedInventoryScripts.Count; i++) {
				if (savedInventoryScripts[i] as InventoryItemScriptable == confirmsItemInInventory) {
					InventoryItemScriptable itemConfirmed = savedInventoryScripts[i] as InventoryItemScriptable;
					itemConfirmed.itemQuantity = savedInventoryQuantities[i];
					objectsTotal = itemConfirmed.itemQuantity;

					print("Object quantity: " + itemConfirmed.itemQuantity);
					print("Object total: " + objectsTotal);
				}
			}

			if (objectsTotal == 0) {
				print("Didn't find object to recover in inventory");
			}
			else if (objectsTotal >= quantityToConfirm) {
				recoveryScriptList.Add(confirmsItemInInventory);
				recoveryQuantitiesList.Add(quantityToConfirm);

				print("You recovered " + quantityToConfirm + " " + confirmsItemInInventory);
			}
			else {
				recoveryScriptList.Add(confirmsItemInInventory);
				recoveryQuantitiesList.Add(objectsTotal);

				print("Couldn't recover " + quantityToConfirm + " " + confirmsItemInInventory + ". Can only recover " + objectsTotal);
			}
		}

		//Wait to respawn newItemHosts until the list has been fully compiled HERE
		for (int i = 0; i < recoveryScriptList.Count; i++) {
			GameObject newItemHost = Instantiate(itemHost, inventoryParents[3 + i].transform.position, Quaternion.identity, inventoryParents[3 + i].transform);

			newItemHost.GetComponent<InventoryScriptableReader>().objectScript = recoveryScriptList[i];
			newItemHost.GetComponent<InventoryScriptableReader>().itemQuantity = recoveryQuantitiesList[i];

			newItemHost.GetComponent<Text>().text = newItemHost.GetComponent<InventoryScriptableReader>().objectScript.name + " x " +
				newItemHost.GetComponent<InventoryScriptableReader>().itemQuantity.ToString();

			newItemHost.GetComponent<InventoryScriptableReader>().SetMyObjectType();
		}

		LogInventory();
		itemListInventoryManager.GetComponent<MapSceneInventoryManager>().InitializeInventory();
		//print("Your inventory has been partially/fully recovered and logged");
		mapSceneManager.OpenCharacterSheet();
	}


	public void UseItem (GameObject itemParent) {
		itemParent.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity--;
		//int UsedItemParentIndex = mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents.IndexOf(item);
		//int itemNewQuantity = item.GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
		//item.GetComponentInChildren<InventoryScriptableReader>().itemQuantity = itemNewQuantity;
		//print(item.GetComponentInChildren<InventoryScriptableReader>().itemQuantity);
		itemParent.transform.GetChild(0).GetComponent<InventoryScriptableReader>().InitializeItem();
		/*GetComponent<MapSceneInventoryManager>().*/LogInventory();
		print("Used the " + itemParent.GetComponentInChildren<InventoryScriptableReader>().objectName + ". Item Quantity: " + itemParent.GetComponentInChildren<InventoryScriptableReader>().itemQuantity);
		//mapSceneManager.GetComponent<MapSceneManager>().UpdateItemListBG();
		itemListInventoryManager.GetComponent<MapSceneInventoryManager>().InitializeInventory();
		//mapSceneManager.GetComponent<MapSceneManager>().RecompileInventoryBG();
	}

	public bool InventoryHoldsHealingHerb () {
		bool hasHerb = false;

		foreach (GameObject item in GetComponent<MapSceneInventoryManager>().inventoryParents) {
			if (item.transform.childCount != 0) {
				if (item.GetComponentInChildren<InventoryScriptableReader>().objectScript == healingHerbScript) {
					print("Found you some herb, maaaaaan");
					hasHerb = true;
					herbParent = item;
				}
			}
		}

		if (hasHerb) {
			return true;
		}
		else {
			herbParent = null;
			return false;
		}
	}

	public void UseHealingHerb () {
		if (herbParent != null) {
			UseItem(herbParent);
			CharacterManager.statusDiseased = false;
			CharacterManager.diseaseTimer = 0;
			mapSceneManager.diseaseIcon.gameObject.SetActive(false);
		}
	}
}
