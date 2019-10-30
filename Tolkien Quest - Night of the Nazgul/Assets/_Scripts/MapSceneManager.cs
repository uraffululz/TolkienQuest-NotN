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
		currentLocation = GameObject.Find("13B");
		UpdateLocationBG();

		//currentEncounter.myEncounterScriptable = gameObject.GetComponent<EncounterManager>().encounterTextScriptables[298];


		//ArriveAtLocation(currentLocation);
		//MoveOn();
	}


    void Update() {
		if (Input.GetMouseButtonDown(0)) {
			ChooseNewLocationTile();
		}
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
		UpdateLocationBG();
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

		if (currentLocationTextIndex == currentLocation.GetComponent<ScriptableMapTileReader>().locationText.Length -1) {
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


	public void UpdateEncounterBG () {
		encounterIndexText.GetComponentInChildren<Text>().text = ("Encounter: " + currentEncounter.myEncounterScriptable.encounterIndex.ToString());
		encounterText.text = currentEncounter.myEncounterScriptable.encounterText[currentEncounterTextIndex];
		encounterTimeText.text = "Time: " + currentEncounter.myEncounterScriptable.timeTaken.ToString();
		encounterXPText.text = "Experience: " + currentEncounter.myEncounterScriptable.XPGained.ToString();

		if (currentEncounterTextIndex == currentEncounter.myEncounterScriptable.encounterText.Length - 1) {
			currentEncounterTextIndex = 0;
			progressEncounterTextButton.SetActive(false);

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
			if (riversideTiles.Contains(currentLocation) && currentEncounter.myEncounterScriptable.canJumpInRiver) {
				furtherEncounterButton4.SetActive(true);
				furtherEncounterButton4.GetComponentInChildren<Text>().text = "Jump in the River";
			}






		}
		else {
			progressEncounterTextButton.SetActive(true);
			moveOnEncounterButton.SetActive(false);
		}
	}


	public void ProgressThroughLocationText () {
		currentLocationTextIndex++;
		//locationTextBG.GetComponentInChildren<Text>().text = currentLocation.GetComponent<ScriptableMapTileReader>().locationText[currentLocationTextIndex];
		UpdateLocationBG();
	}

	public void ProgressThroughEncounterText () {
		currentEncounterTextIndex++;
		//encounterTextBG.GetComponentInChildren<Text>().text = currentEncounter.myEncounterScriptable.encounterText[currentEncounterTextIndex];
		UpdateEncounterBG();
	}


	public void MoveOn (bool initiatedByButton) { //Not using this bool at the moment, BTW
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

		if (CharacterManager.damageTaken >= CharacterManager.enduranceTotal) {
			if (MapSceneManager.currentEncounter.myEncounterScriptable.encounterIndex == 357) {
				//Proceed to Encounter 337
				print ("<b>You should proceed to Encounter 337</b>");
			}
			else {
				GameOver();
			}
		}
		else if (CharacterManager.damageTaken < 0) {
			print ("You are fully healed");
			CharacterManager.damageTaken = 0;
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
