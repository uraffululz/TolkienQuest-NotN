using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSceneManager : MonoBehaviour {

	[SerializeField] GameObject player;

	//public bool prologueComplete;

	[SerializeField] GameObject[] mapTiles;
	[SerializeField] List<GameObject> adjacentTiles;
//TODO Make a List<GameObject> for "tilesVisited", and use it to show the character's path across the map
	[SerializeField] Material tileBaseMat;

	public static GameObject currentLocation;
	public static ScriptableEncounterReader currentEncounter;
	bool allowedToClickMapTile = true;

	[SerializeField] GameObject locationTextBG;
	int currentLocationTextIndex;
	[SerializeField] GameObject locationEncounterButton1;
	[SerializeField] GameObject locationEncounterButton2;
	[SerializeField] GameObject locationEncounterButton3;

	[SerializeField] GameObject MerchantUI;

	[SerializeField] GameObject progressLocationTextButton;
	[SerializeField] GameObject openMerchantUIButton;
	[SerializeField] GameObject moveOnButton;

 
	void Awake () {
		mapTiles = GameObject.FindGameObjectsWithTag("MapTile");
		adjacentTiles = new List<GameObject>();
	}

	void Start() {
		currentLocation = GameObject.Find("1D");
        //ArriveAtLocation(currentLocation);
    }


    void Update() {
		if (Input.GetMouseButtonDown(0)) {
			ChooseNewLocationTile();
		}
	}

	void ChooseNewLocationTile () {
		Ray tileRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit tileRayHit;

		if (Physics.Raycast(tileRay, out tileRayHit) && allowedToClickMapTile) {
			if (tileRayHit.collider.gameObject.tag == "MapTile") {
				print("You clicked on a Map Tile");

				//TODO Make sure the tileRay can only hit ADJACENT tiles
				if (adjacentTiles.Contains(tileRayHit.collider.gameObject)) {
					print("You clicked on an ADJACENT map tile");
					currentLocation = tileRayHit.collider.gameObject;
					player.transform.position = currentLocation.transform.position + (Vector3.back * .3f);
					adjacentTiles.Clear();

					allowedToClickMapTile = false;
					currentLocationTextIndex = 0;
					UpdateLocationBG();
				}
			}
		}
	}

	public void UpdateLocationBG () {
		locationTextBG.GetComponentInChildren<Text>().text = currentLocation.GetComponent<ScriptableMapTileReader>().locationText[currentLocationTextIndex];
		if (currentLocationTextIndex == currentLocation.GetComponent<ScriptableMapTileReader>().locationText.Length -1) {
			progressLocationTextButton.SetActive(false);

			//If the current location has an ENCOUNTER
			if (currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex1 == 0) {
				locationEncounterButton1.SetActive(false);
			}
			else {
				locationEncounterButton1.SetActive(true);
				locationEncounterButton1.GetComponentInChildren<Text>().text = currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionText1;
				//locationEncounterButton1.GetComponent<Button>().onClick.
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

				openMerchantUIButton.SetActive(true);
			}

			//If the current location has COMBAT

			//If the character can MOVE ON from the current text
			if (currentLocation.GetComponent<ScriptableMapTileReader>().myTileScriptable.canMoveOn) {
				moveOnButton.SetActive(true);
			}


		}
		else {
			progressLocationTextButton.SetActive(true);
		}
	}


	public void ProgressThroughLocationText () {
		currentLocationTextIndex++;
		locationTextBG.GetComponentInChildren<Text>().text = currentLocation.GetComponent<ScriptableMapTileReader>().locationText[currentLocationTextIndex];
		UpdateLocationBG();
	}


	public void ResetLocationTextBG() {
		//When the character reaches a new location, reset the various UI backgrounds/buttons/etc. to the new Location
		
	}


	public void DisplayEncounterText (int thisEncounter) {
	
	}


	public void MoveOn () {
		allowedToClickMapTile = true;

		foreach (GameObject mapTile in mapTiles) {
			//adjacentTiles.Clear();
			if (currentLocation.GetComponent<Collider>().bounds.Intersects(mapTile.GetComponent<Collider>().bounds)) {
				adjacentTiles.Add(mapTile);
				mapTile.GetComponent<MeshRenderer>().material.color = Color.red;
			}
			else {
				mapTile.GetComponent<MeshRenderer>().material = tileBaseMat;
			}
		}

//TOMAYBEDO I might not need to reset all this crap
		//currentLocationTextIndex = 0;
		progressLocationTextButton.SetActive(false);
		openMerchantUIButton.SetActive(false);
		moveOnButton.SetActive(false);
	}
}
