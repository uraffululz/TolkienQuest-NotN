using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSceneManager : MonoBehaviour {

	public GameObject player;

	//public bool prologueComplete;

	public GameObject[] mapTiles;
	[SerializeField] List<GameObject> adjacentTiles;
//TODO Make a List<GameObject> for "tilesVisited", and use it to show the character's path across the map
	[SerializeField] List<GameObject> riversideTiles;
	[SerializeField] Material tileBaseMat;

	public static GameObject currentLocation;
	public static GameObject previousLocation;

	public static ScriptableEncounterReader currentEncounter;
	public static EncounterScriptable previousEncounterScriptable;

	[SerializeField] GameObject characterSheet;
	[SerializeField] GameObject characterSheetInventoryParent;

	[SerializeField] GameObject locationTextBG;
	[SerializeField] GameObject locationIndexText;
	[SerializeField] Text locationText;
	[SerializeField] Text locationTimeText;
	[SerializeField] Text locationXPText;
	public int currentLocationTextIndex;
	[SerializeField] GameObject locationEncounterButton1;
	[SerializeField] GameObject locationEncounterButton2;
	[SerializeField] GameObject locationEncounterButton3;

	public GameObject encounterTextBG;
	[SerializeField] GameObject encounterIndexText;
	[SerializeField] Text encounterText;
	[SerializeField] Text encounterTimeText;
	[SerializeField] Text encounterXPText;
	int currentEncounterTextIndex;
	[SerializeField] GameObject furtherEncounterButton1;
	[SerializeField] GameObject furtherEncounterButton2;
	[SerializeField] GameObject furtherEncounterButton3;
	[SerializeField] GameObject furtherEncounterButton4;
	public GameObject delayedEncounterButton;

	[SerializeField] GameObject itemListBG;
	//[SerializeField] EncounterObtainedItemList currentItemList;
	[SerializeField] Text itemHost;
	//[SerializeField] InventoryItemScriptable currentItemTypeItem;
	//[SerializeField] InventoryWeaponScriptable currentItemTypeWeapon;
	//[SerializeField] InventoryArmorScriptable currentItemTypeArmor;

	[SerializeField] GameObject inventoryBG;
	public GameObject inventoryParent;

	[SerializeField] GameObject MerchantUI;

	[SerializeField] GameObject progressLocationTextButton;
	[SerializeField] GameObject progressEncounterTextButton;
	[SerializeField] GameObject openMerchantUIButton;
	[SerializeField] GameObject restButton;
	[SerializeField] GameObject moveOnButton;
	public GameObject moveOnEncounterButton;
	public bool moveOnInRandomDirection;
	public bool moveOnToSpecificTile;

	[SerializeField] GameObject WinUIBG;

	[SerializeField] GameObject LoseUIBG;

	void Awake () {
		mapTiles = GameObject.FindGameObjectsWithTag("MapTile");
		adjacentTiles = new List<GameObject>();
	}

	void Start() {

//TODO For testing purposes, update this to whicever tile you need to start at
		currentLocation = GameObject.Find("16C");
		UpdateLocationBG();

		//currentEncounter.myEncounterScriptable = gameObject.GetComponent<EncounterManager>().encounterTextScriptables[298];


		//ArriveAtLocation(currentLocation);
		//MoveOn();

		OpenLocationUI();
		//OpenEncounterUI();
		//CloseEncounterUI();
	}


    void Update() {
		if (Input.GetMouseButtonDown(0)) {
			ChooseNewLocationTile();
		}

		if (Input.GetKeyDown(KeyCode.I)) {
			if (characterSheet.GetComponent<Animator>().GetBool("openInventory") == false) {
				OpenCharacterSheet();
			}
			else {
				CloseCharacterSheet();
			}
		}
	}


	void OpenCharacterSheet() {
		characterSheet.GetComponent<Animator>().SetBool("openInventory", true);
	}


	void CloseCharacterSheet() {
		characterSheet.GetComponent<Animator>().SetBool("openInventory", false);
		characterSheetInventoryParent.GetComponent<MapSceneInventoryManager>().LogInventory();
		inventoryParent.GetComponent<MapSceneInventoryManager>().InitializeInventory();

	}

	void ChooseNewLocationTile () {
		//Raycast to select a new currentLocation GameObject/player.position/Player LineRenderer trail
		Ray tileRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit tileRayHit;

		if (Physics.Raycast(tileRay, out tileRayHit)) {
			if (tileRayHit.collider.gameObject.tag == "MapTile") {
				//print("You clicked on a Map Tile");

				//The tileRay can only hit ADJACENT tiles (if allowed)
				if (adjacentTiles.Contains(tileRayHit.collider.gameObject)) {
					//print("You clicked on an ADJACENT map tile");

					if (tileRayHit.collider.gameObject.GetComponent<ScriptableMapTileReader>().allowedToClickMapTile) {
						UpdateLocation(tileRayHit.collider.gameObject);

						if (currentEncounter != null && currentEncounter.movesTwoSpaces) {
							//print("YOU CAN MOVE TWO FUCKING SPACES!");
							MoveOn(false);
							currentEncounter.movesTwoSpaces = false;
						}
						else {
							adjacentTiles.Clear();

							tileRayHit.collider.gameObject.GetComponent<ScriptableMapTileReader>().allowedToClickMapTile = false;
						}
					}
				}
			}
		}
	}


	public void MoveToSpecificLocation (string nextMapTile) {
		foreach (GameObject tile in mapTiles) {
			if (tile.GetComponent<ScriptableMapTileReader>().myLocationID == nextMapTile) {
				UpdateLocation(tile);
			}
		}

		//if (!currentEncounter.myEncounterScriptable.canMoveOn) {
		//	progressEncounterTextButton.SetActive(true);
		//}
		//else {
		//	currentEncounterTextIndex = 0;
		//	progressEncounterTextButton.SetActive(false);
		//}
	}


	void UpdateLocation (GameObject newLocationTile) {
		//Update player GameObject location
//TODO Animate/Lerp the player GameObject to the new MapSceneManager.currentLocation.
		player.transform.position = newLocationTile.transform.position + (Vector3.back * .3f);

		//Update MapSceneManager.currentLocation
		previousLocation = currentLocation;
		currentLocation = newLocationTile;
		//adjacentTiles.Clear();

		currentLocationTextIndex = 0;
		OpenLocationUI();
		UpdateLocationBG();
	}


	public void OpenLocationUI() {
		locationTextBG.GetComponent<Animator>().SetBool("SlideTopUIOpen", true);
	}


	public void CloseLocationUI() {
		locationTextBG.GetComponent<Animator>().SetBool("SlideTopUIOpen", false);
	}


	public void UpdateLocationBG () {
		locationIndexText.GetComponentInChildren<Text>().text = ("Location: " + currentLocation.GetComponent<ScriptableMapTileReader>().myLocationID.ToString());
		locationText.text = currentLocation.GetComponent<ScriptableMapTileReader>().locationText[currentLocationTextIndex];
		locationTimeText.text = "Time: " + currentLocation.GetComponent<ScriptableMapTileReader>().timeTaken.ToString();
		locationXPText.text = "Experience: " + currentLocation.GetComponent<ScriptableMapTileReader>().XPGained.ToString();

		if (currentEncounter != null) {
			//If movesTwoSpaces wasn't already false (most of the time it will be), do it now
			//currentEncounter.movesTwoSpaces = false;
		}

		if (currentLocationTextIndex >= currentLocation.GetComponent<ScriptableMapTileReader>().locationText.Length -1) {
			progressLocationTextButton.SetActive(false);

			//If the current location has an ENCOUNTER
			if (currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex1 == 0) {
				locationEncounterButton1.SetActive(false);
			}
			else {
				locationEncounterButton1.SetActive(true);
				locationEncounterButton1.GetComponentInChildren<Text>().text = currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionText1;
			}

			//If the current location has a SECOND ENCOUNTER
			if (currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex2 == 0) {
				locationEncounterButton2.SetActive(false);
			}
			else {
				locationEncounterButton2.SetActive(true);
				locationEncounterButton2.GetComponentInChildren<Text>().text = currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionText2;
			}

			//If the current location has a THIRD ENCOUNTER
			if (currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex3 == 0) {
				locationEncounterButton3.SetActive(false);
			}
			else {
				locationEncounterButton3.SetActive(true);
				locationEncounterButton3.GetComponentInChildren<Text>().text = currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionText3;
			}


			//print("Current settlement index checked: " + currentLocation.GetComponent<ScriptableMapTileReader>().mySettlementIndex);
			//If the current tile has a settlement and they have already been warned
			if (currentLocation.GetComponent<ScriptableMapTileReader>().mySettlementIndex != 0) {

				bool alreadyWarnedSettlement = false;
				switch (currentLocation.GetComponent<ScriptableMapTileReader>().mySettlementIndex) {
					case 1: //Bridgefields
						if (CharacterManager.warnedBridgefields) {
							print("Already warned Bridgefields");
							alreadyWarnedSettlement = true;
						}
						break;
					case 2: //Bywater
						if (CharacterManager.warnedBywater) {
							alreadyWarnedSettlement = true;
						}
						break;
					case 3: //Frogmorton
						if (CharacterManager.warnedFrogmorton) {
							alreadyWarnedSettlement = true;
						}
						break;
					case 4: //Hobbiton
						if (CharacterManager.warnedHobbiton) {
							alreadyWarnedSettlement = true;
						}
						break;
					case 5: //Marish
						if (CharacterManager.warnedMarish) {
							alreadyWarnedSettlement = true;
						}
						break;
					case 6: //Scary
						if (CharacterManager.warnedScary) {
							alreadyWarnedSettlement = true;
						}
						break;
					case 7: //Stock
						if (CharacterManager.warnedStock) {
							print ("Already warned Stock");
							alreadyWarnedSettlement = true;
						}
						break;
					case 8: //Tuckborough
						if (CharacterManager.warnedTuckborough) {
							alreadyWarnedSettlement = true;
						}
						break;
					case 9: //Whitfurrows
						if (CharacterManager.warnedWhitfurrows) {
							alreadyWarnedSettlement = true;
						}
						break;
					case 10: //Woodhall
						if (CharacterManager.warnedWoodhall) {
							alreadyWarnedSettlement = true;
						}
						break;
					default:
						Debug.Log("What settlement is this supposed to be?");
						break;
				}

				if (alreadyWarnedSettlement) {
					if (currentLocation.GetComponent<ScriptableMapTileReader>().myMerchant != null) {
						currentEncounter.myEncounterScriptable = GetComponent<EncounterManager>().encounterTextScriptables[464];
						UpdateEncounterBG();
						CloseLocationUI();
						OpenEncounterUI();
					}
					else {
						print("Already warned this settlement, but it doesn't have a merchant");
						currentEncounter.myEncounterScriptable = GetComponent<EncounterManager>().encounterTextScriptables[465];
						UpdateEncounterBG();
						CloseLocationUI();
						OpenEncounterUI();
					}
				}
			}

			//TOMAYBEDO Move this if-statement to a separate public function, to be used for the OnClick() for any "merchant encounter" buttons
			//If the current location has a MERCHANT
			if (currentLocation.GetComponent<ScriptableMapTileReader>().myTileScriptable.myMerchantScriptable != null) {
				EncounterMerchantScriptable myMerchant = currentLocation.GetComponent<ScriptableMapTileReader>().myMerchant;

				//Update MerchantBG display
				for (int i = 0; i < MerchantUI.GetComponent<MerchantUIManager>().itemNameTexts.Length; i++) {
					MerchantUI.GetComponent<MerchantUIManager>().itemNameTexts[i].text = myMerchant.itemsForSale[i];
					MerchantUI.GetComponent<MerchantUIManager>().itemCostTexts[i].text = myMerchant.itemPrices[i] + " " + myMerchant.coinType[i].ToString();
				}

				//Display the character's current funds on the MerchantBG
				MerchantUI.GetComponent<MerchantUIManager>().currentMoneyText.text = "Current Money: \n" +
																						"Silver: " + InventoryManager.silverCarried + "\n" +
																						"Copper: " + InventoryManager.copperCarried;

				//openMerchantUIButton.SetActive(true);
			}

			//If the current location has COMBAT

			//If the character can REST or MOVE ON from the current text
			if (CharacterManager.currentDayTimeTaken >= 780 && currentLocation.GetComponent<ScriptableMapTileReader>().canRestAtNight) {
				locationEncounterButton1.SetActive(false);
				locationEncounterButton2.SetActive(false);
				locationEncounterButton3.SetActive(false);
				
				restButton.SetActive(true);
				moveOnButton.SetActive(false);
			}
			else if (currentLocation.GetComponent<ScriptableMapTileReader>().myTileScriptable.canMoveOn) {
				restButton.SetActive(false);
				moveOnButton.SetActive(true);
			}
			else {
				restButton.SetActive(false);
				moveOnButton.SetActive(false);
			}


		}
		else {
			progressLocationTextButton.SetActive(true);
			moveOnButton.SetActive(false);
		}
	}


	public void OpenEncounterUI() {
		encounterTextBG.GetComponent<Animator>().SetBool("SlideBottomUIOpen", true);
	}


	public void CloseEncounterUI() {
		encounterTextBG.GetComponent<Animator>().SetBool("SlideBottomUIOpen", false);
	}


	public void UpdateEncounterBG () {
		encounterIndexText.GetComponentInChildren<Text>().text = ("Encounter: " + currentEncounter.myEncounterScriptable.encounterIndex.ToString());
		encounterText.text = currentEncounter.myEncounterScriptable.encounterText[currentEncounterTextIndex];
		encounterTimeText.text = "Time: " + currentEncounter.myEncounterScriptable.timeTaken.ToString();
		encounterXPText.text = "Experience: " + currentEncounter.myEncounterScriptable.XPGained.ToString();

		if (currentEncounterTextIndex >= currentEncounter.myEncounterScriptable.encounterText.Length - 1) {
			if (currentEncounter.myEncounterScriptable.moveToSpecificTile != "" && !currentEncounter.myEncounterScriptable.canMoveOn) {
				progressEncounterTextButton.SetActive(true);
			}
			else {
				currentEncounterTextIndex = 0;
				progressEncounterTextButton.SetActive(false);
			}

			//if (currentEncounter.myEncounterScriptable.obtainsItems) {
			//	OpenItemListUI();
			//}

			//If the player is allowed to MOVE ON from the current encounter
			if (currentEncounter.myEncounterScriptable.canMoveOn) {
				moveOnEncounterButton.SetActive(true);
			}

			//If the current location has an ENCOUNTER
			if (currentEncounter.myEncounterScriptable.furtherEncounter1Index == 0) {
				furtherEncounterButton1.SetActive(false);
			}
			else {
				furtherEncounterButton1.SetActive(true);
				furtherEncounterButton1.GetComponentInChildren<Text>().text = currentEncounter.myEncounterScriptable.furtherEncounter1Text;
			}

			//If the current location has a SECOND ENCOUNTER
			if (currentEncounter.myEncounterScriptable.furtherEncounter2Index == 0) {
				furtherEncounterButton2.SetActive(false);
			}
			else {
				furtherEncounterButton2.SetActive(true);
				furtherEncounterButton2.GetComponentInChildren<Text>().text = currentEncounter.myEncounterScriptable.furtherEncounter2Text;
			}

			//If the current location has a THIRD ENCOUNTER
			if (currentEncounter.myEncounterScriptable.furtherEncounter3Index == 0) {
				furtherEncounterButton3.SetActive(false);
			}
			else {
				furtherEncounterButton3.SetActive(true);
				furtherEncounterButton3.GetComponentInChildren<Text>().text = currentEncounter.myEncounterScriptable.furtherEncounter3Text;
			}
			//If the current location has a FOURTH ENCOUNTER
			if (currentEncounter.myEncounterScriptable.furtherEncounter4Index == 0) {
				furtherEncounterButton4.SetActive(false);
			}
			else {
				furtherEncounterButton4.SetActive(true);
				furtherEncounterButton4.GetComponentInChildren<Text>().text = currentEncounter.myEncounterScriptable.furtherEncounter4Text;
			}

			//If the current encounter has been delayed
			if (encounterTextBG.GetComponent<ScriptableEncounterReader>().delayedEncounterIndex != 0) {
				delayedEncounterButton.SetActive(true);

				furtherEncounterButton1.SetActive(false);
				furtherEncounterButton2.SetActive(false);
				furtherEncounterButton3.SetActive(false);
				furtherEncounterButton4.SetActive(false);
			}
			else {
				delayedEncounterButton.SetActive(false);
			}

			if (riversideTiles.Contains(currentLocation) && currentEncounter.myEncounterScriptable.canJumpInRiver) {
				furtherEncounterButton4.SetActive(true);
				furtherEncounterButton4.GetComponentInChildren<Text>().text = "Jump in the River";
			}
		}
		else {
			progressEncounterTextButton.SetActive(true);
			moveOnEncounterButton.SetActive(false);

			//Disable "Further Encounter" Buttons 1-4
			furtherEncounterButton1.SetActive(false);
			furtherEncounterButton2.SetActive(false);
			furtherEncounterButton3.SetActive(false);
			furtherEncounterButton4.SetActive(false);

			delayedEncounterButton.SetActive(false);

		}

		CloseLocationUI();
		//OpenEncounterUI();
	}


	public void UpdateItemListBG(List<ScriptableObject> myListedItems, List<int> myListedItemQuantities) {
		EncounterObtainedItemList currentItemList = currentEncounter.myEncounterScriptable.obtainedItemList;

		int additionalSilverEarned = 0;

		for (int i = 0; i < myListedItems.Count; i++) {
			
			Text newItemHost = Instantiate(itemHost, itemListBG.GetComponent<ItemListBGScript>().positionParentList[i].transform.position, Quaternion.identity,
			itemListBG.GetComponent<ItemListBGScript>().positionParentList[i].transform);

			newItemHost.GetComponent<InventoryScriptableReader>().objectScript = myListedItems[i];
			newItemHost.GetComponent<InventoryScriptableReader>().itemQuantity = myListedItemQuantities[i];
			newItemHost.GetComponent<InventoryScriptableReader>().SetMyObjectType();

			if (myListedItems[i].GetType() == typeof(InventoryItemScriptable)) {
				//print("This object is an ITEM");
				//currentItemTypeItem = currentItemList.myItemList[i] as InventoryItemScriptable;

				//currentItemTypeItem = currentItemList.myItem1;
				//currentItemList.myItem1.Equals(typeof(InventoryItemScriptable));

				/* override object.Equals

					public override bool Equals (object obj) {
						//       
						// See the full list of guidelines at
						//   http://go.microsoft.com/fwlink/?LinkID=85237  
						// and also the guidance for operator== at
						//   http://go.microsoft.com/fwlink/?LinkId=85238
						//

						if (obj == null || GetType() != obj.GetType()) {
							return false;
						}

						// TODO: write your implementation of Equals() here
						throw new System.NotImplementedException();
						return base.Equals(obj);
					}

					// override object.GetHashCode
					public override int GetHashCode () {
						// TODO: write your implementation of GetHashCode() here
						throw new System.NotImplementedException();
						return base.GetHashCode();
					}
				*/
				//newItemHost.text = newItemHost.GetComponent<InventoryScriptableReader>().itemScript.itemName;

				if (newItemHost.GetComponent<InventoryScriptableReader>().itemScript.isMoney) {
					additionalSilverEarned = newItemHost.GetComponent<InventoryScriptableReader>().itemScript.itemQuantity;
					Destroy(newItemHost);
				}
			}
			else if (myListedItems[i].GetType() == typeof(InventoryWeaponScriptable)) {
				//print("This object is a WEAPON");
				//currentItemTypeWeapon = currentItemList.myItemList[i] as InventoryWeaponScriptable;
				//newItemHost.text = newItemHost.GetComponent<InventoryScriptableReader>().weaponScript.itemName;
			}
			else if (myListedItems[i].GetType() == typeof(InventoryArmorScriptable)) {
				//print("This object is an ARMOR");
				//currentItemTypeArmor = currentItemList.myItemList[i] as InventoryArmorScriptable;
				//newItemHost.text = newItemHost.GetComponent<InventoryScriptableReader>().armorScript.itemName;
			}
		}

		InventoryManager.silverCarried += (currentItemList.silverEarned + additionalSilverEarned);
		itemListBG.GetComponent<ItemListBGScript>().silverText.text = ("Silver: " + (currentItemList.silverEarned + additionalSilverEarned).ToString());
		InventoryManager.copperCarried += currentItemList.copperEarned;
		itemListBG.GetComponent<ItemListBGScript>().copperText.text = ("Silver: " + currentItemList.copperEarned.ToString());

		inventoryParent.GetComponent<MapSceneInventoryManager>().pouchParent.text = ("Silver: " + InventoryManager.silverCarried + " | Copper: " + InventoryManager.copperCarried);

		print("Current Silver: " + InventoryManager.silverCarried + "|| " + "Current Copper: " + InventoryManager.copperCarried);

		//OpenItemListUI();
	}


	public void ClearItemListBG() {
		//When clicking the ItemListBG's "Done" button

		//Destroy any unwanted items
		foreach (Image itemParent in itemListBG.GetComponent<ItemListBGScript>().positionParentList) {
			if (itemParent.transform.childCount != 0) {
				//print("Item parent child count: " + itemParent.transform.childCount);
				for (int x = itemParent.transform.childCount-1; x >= 0 ; x--) {
					Destroy(itemParent.transform.GetChild(x).gameObject);
				}
			}
		}

//Recompile/Re-initialize the player's inventory (Arrays, TextBGs, etc.), based on which items are now in each slot
		inventoryParent.GetComponent<MapSceneInventoryManager>().LogInventory();

		CloseItemListUI();
	}


	public void OpenItemListUI () {
		inventoryBG.GetComponent<Animator>().SetBool("openInventory", true);
		itemListBG.GetComponent<Animator>().SetBool("openItemList", true);
		CloseLocationUI();
		CloseEncounterUI();
	}


	public void CloseItemListUI () {
		inventoryBG.GetComponent<Animator>().SetBool("openInventory", false);
		itemListBG.GetComponent<Animator>().SetBool("openItemList", false);
		OpenEncounterUI();

		characterSheetInventoryParent.GetComponent<MapSceneInventoryManager>().InitializeInventory();
	}

	//public void RecompileInventoryBG() {
	//	//for (int i = 0; i < inventoryParent.GetComponent<MapSceneInventoryManager>().inventoryParents.Length; i++) {
	//	//	if (inventoryParent.GetComponent<MapSceneInventoryManager>().inventoryParents[i].transform.childCount > 0) {
	//	//		Destroy(inventoryParent.GetComponent<MapSceneInventoryManager>().inventoryParents[i].transform.GetChild(0).gameObject);
	//	//	}
	//	//}

	//	inventoryParent.GetComponent<MapSceneInventoryManager>().InitializeInventory();
	//}


	public void CloseMerchantUI() {
		MerchantUI.GetComponent<Animator>().SetBool("SlideIn", false);
	}


	public void ProgressThroughLocationText () {
		currentLocationTextIndex++;
		//locationTextBG.GetComponentInChildren<Text>().text = currentLocation.GetComponent<ScriptableMapTileReader>().locationText[currentLocationTextIndex];
		UpdateLocationBG();
	}

	public void ProgressThroughEncounterText () {
		if (currentEncounterTextIndex >= currentEncounter.myEncounterScriptable.encounterText.Length -1) {
			if(currentEncounter.myEncounterScriptable.moveToSpecificTile != "" && !currentEncounter.myEncounterScriptable.canMoveOn) {
				Debug.Log("Closing EncounterTextBG and opening LocationTextBG");
				CloseEncounterUI();

				//Open LocationBG
				//locationTextBG.GetComponent<Animator>().SetBool("SlideTopUIOpen", true);

				if (currentEncounter.myEncounterScriptable.floatsDownstream) {
					Debug.Log("Resetting 'downstream floating' encounter");
					currentEncounter.myEncounterScriptable.moveToSpecificTile = "";
				}
			}
			currentEncounterTextIndex = 0;
		}
		else {
			currentEncounterTextIndex++;
			//encounterTextBG.GetComponentInChildren<Text>().text = currentEncounter.myEncounterScriptable.encounterText[currentEncounterTextIndex];
			UpdateEncounterBG();
		}
	}


	public void MoveOn (bool initiatedByButton) { //Not using this bool at the moment, BTW
		CloseEncounterUI();
		CloseLocationUI();

		//if (initiatedByButton) {
		//	currentEncounter.movesTwoSpaces = false;
		//}

		foreach (GameObject mapTile in mapTiles) {
			/*KEEP THIS*/ //adjacentTiles.Clear();
			/*KEEP if (currentLocation.GetComponent<Collider>().bounds.Intersects(mapTile.GetComponent<Collider>().bounds)) {*/
				adjacentTiles.Add(mapTile);
			/*KEEP}*/
			/*KEEP else {*/
				mapTile.GetComponent<ScriptableMapTileReader>().allowedToClickMapTile = false;
				mapTile.GetComponent<MeshRenderer>().material = tileBaseMat;
			/*KEEP}*/
		}

		//print(adjacentTiles.Count);

		if (moveOnInRandomDirection) {
			GameObject randomTile = adjacentTiles[Random.Range(0, adjacentTiles.Count)];
			randomTile.GetComponent<ScriptableMapTileReader>().allowedToClickMapTile = true;
			randomTile.GetComponent<MeshRenderer>().material.color = Color.red;

			CharacterManager.currentDayTimeTaken += (currentLocation.GetComponent<ScriptableMapTileReader>().timeTaken * 2);
			CharacterManager.totalTimeTaken += (currentLocation.GetComponent<ScriptableMapTileReader>().timeTaken * 2);
		}
		//else if (moveOnToSpecificTile) {
		//	//GameObject.chosenTile = adj
		//}
		else {
			foreach (GameObject tile in adjacentTiles) {
				tile.GetComponent<ScriptableMapTileReader>().allowedToClickMapTile = true;
				tile.GetComponent<MeshRenderer>().material.color = Color.red;
			}

			CharacterManager.currentDayTimeTaken += currentLocation.GetComponent<ScriptableMapTileReader>().timeTaken;
			CharacterManager.totalTimeTaken += currentLocation.GetComponent<ScriptableMapTileReader>().timeTaken;
		}

		CharacterManager.totalXP += currentLocation.GetComponent<ScriptableMapTileReader>().XPGained;

		print("Time Taken Today: " + CharacterManager.currentDayTimeTaken);
		//print("Total time: " + CharacterManager.totalTimeTaken);
		//print("Total XP: " + CharacterManager.totalXP);

		moveOnInRandomDirection = false;

		//TOMAYBEDO I might not need to reset all this crap
		//currentLocationTextIndex = 0;
		//progressLocationTextButton.SetActive(false);
		//openMerchantUIButton.SetActive(false);
		//moveOnButton.SetActive(false);

		currentLocation.GetComponent<ScriptableMapTileReader>().allowedToClickMapTile = false;
		currentLocation.GetComponent<MeshRenderer>().material = tileBaseMat;
	}



	public void Rest() {
		//Adjust ongoing "Total Time" variables in CharacterManager
		CharacterManager.daysTaken++;
		CharacterManager.currentDayTimeTaken = 0;

		//If the player has a "meal", they must eat it to regain health while resting
		//If not, they heal nothing and take 5 damage

		restButton.SetActive(false);
		moveOnButton.SetActive(true);

		UpdateLocationBG();
	}


	public void AlterDamage(int newDamage) {
		CharacterManager.damageTaken += newDamage;
		print("You took " + newDamage + " more damage.");

		if (CharacterManager.damageTaken >= CharacterManager.enduranceTotal) {
			if (MapSceneManager.currentEncounter.myEncounterScriptable.encounterIndex == 357) {
				//Proceed to Encounter 337
				print("The snake's poison killed you");
				currentEncounter.DelayEncounterUpdate(337);
				//currentEncounter.UpdateEncounter(337);
			}
			else {
				GameOver();
			}
		}
		else {
			if (MapSceneManager.currentEncounter.myEncounterScriptable.encounterIndex == 357) {
				int thisRoll = Random.Range(2, 13);
				if (thisRoll <= 8) {
					print("Taking 3 more poison damage");
					print("Current Damage Taken:" + CharacterManager.damageTaken + "/" + CharacterManager.enduranceTotal);
					currentEncounter.DelayEncounterUpdate(357);
					//currentEncounter.UpdateEncounter(357);
				}
				else {
					print ("Tom Bombadil saved your ass!");
					print("Current HP:" + CharacterManager.damageTaken + "/" + CharacterManager.enduranceTotal);
					currentEncounter.DelayEncounterUpdate(323);
					//currentEncounter.UpdateEncounter(323);
				}
			}
		}
		
		if (CharacterManager.damageTaken < 0) {
			print ("You are fully healed");

			CharacterManager.damageTaken = 0;
			//print("Damage taken: " + CharacterManager.damageTaken + "/ " + CharacterManager.enduranceTotal);

		}

	}


	public void Win () {
		WinUIBG.GetComponent<Animator>().SetBool("slideTopUIOpen", true);
//TODO Close all other "UI backgrounds" (except maybe for the one that led here)
//That is, if any Locations DO lead to a game over, which I don't think occurs. Just keep it in mind.

//TODO Display relevant stats (Time taken, XP earned, settlements warned, etc.)
		print("YOU WIN");
	}


	public void GameOver () {
		LoseUIBG.GetComponent<Animator>().SetBool("SlideBottomUIOpen", true);
//TODO Close all other "UI backgrounds" (except maybe for the one that led here)
//Display what fault the player made to lead them here ("You failed to..."/"You were defeated by..."/etc.)
		print("GAME OVER!");
	}
}
