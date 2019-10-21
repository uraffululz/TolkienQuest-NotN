using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableEncounterReader : MonoBehaviour {

	public EncounterScriptable myEncounterScriptable;

	[SerializeField] GameObject mapSceneManager;

	[SerializeField] Text encounterTimeText;
	[SerializeField] Text encounterXPText;

	[SerializeField] GameObject merchantUI;



	void Awake () {
		//SelectScriptable();
    }


    void Update() {
        
    }


	public void ChooseLocationBGEncounter (int whichTileEncounterVariable) {
		int currentEncounterIndex = 0;

		switch (whichTileEncounterVariable) {
			case 1:  currentEncounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex1;
				break;
			case 2: currentEncounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex2;
				break;
			case 3: currentEncounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex3;
				break;
			case 4: currentEncounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex4;
				break;
			default: print("NOPE YOU JACKED UP!");
				break;
		}
		DetermineEncounterBGFromLocation(whichTileEncounterVariable, currentEncounterIndex);
	}


	public void DetermineEncounterBGFromLocation (int encounterButtonClicked, int encounterIndex) {
		//If the player is able to explore the area
		//if (encounterButtonClicked == 1 && MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().actionRequiresRoll) {
		//	int exploreSuccessRoll = Random.Range(2, 13);

		//	if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().addSkillToAction == MapTileScriptable.actionSkill.General) {
		//		exploreSuccessRoll += CharacterManager.mySkillGeneralTotal;
		//		print("Adding General skill bonus");
		//	}
		//	else if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().addSkillToAction == MapTileScriptable.actionSkill.Trickery) {
		//		exploreSuccessRoll += CharacterManager.mySkillTrickeryTotal;
		//		print("Adding Trickery skill bonus");
		//	}
		//	else if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().addSkillToAction == MapTileScriptable.actionSkill.Perception) {
		//		exploreSuccessRoll += CharacterManager.mySkillPerceptionTotal;
		//		print("Adding Perception skill bonus");
		//	}

		//	print("Explore Roll: " + exploreSuccessRoll);

		//	//If the player succeeds in exploring the area
		//	if (exploreSuccessRoll >= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().actionMinLimit) {
		//		encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().actionIndex;
		//	}
		//}


		//If the next MapSceneManager 's next currentLocation is chosen from the "Encounter Buttons" assigned by the 
		if (encounterButtonClicked == 1 && MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().exploreHasMultipleRanges) {
			int exploreRoll = Random.Range(2, 13);
			bool rolledWithinRanges = false;

			if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().addSkillToAction == MapTileScriptable.actionSkill.General) {
				exploreRoll += CharacterManager.mySkillGeneralTotal;
				print("Adding General skill bonus");
			}
			else if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().addSkillToAction == MapTileScriptable.actionSkill.Trickery) {
				exploreRoll += CharacterManager.mySkillTrickeryTotal;
				print("Adding Trickery skill bonus");
			}
			else if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().addSkillToAction == MapTileScriptable.actionSkill.Perception) {
				exploreRoll += CharacterManager.mySkillPerceptionTotal;
				print("Adding Perception skill bonus");
			}

			//print("Explore roll: " + exploreRoll);

			if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().howManyRanges > 0) {
				//print(MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().exploreRange1.end);
				if (exploreRoll >= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().exploreRange1.start &&
					exploreRoll <= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().exploreRange1.end) {
					rolledWithinRanges = true;
					encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().range1FurtherIndex;
				}
			}

			if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().howManyRanges > 1) {
				if (exploreRoll >= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().exploreRange2.start &&
					exploreRoll <= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().exploreRange2.end) {
					rolledWithinRanges = true;
					encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().range2FurtherIndex;
				}
			}

			if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().howManyRanges > 2) {
				if (exploreRoll >= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().exploreRange3.start &&
					exploreRoll <= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().exploreRange3.end) {
					rolledWithinRanges = true;
					encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().range3FurtherIndex;
				}
			}

			if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().howManyRanges > 3) {
				if (exploreRoll >= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().exploreRange4.start &&
					exploreRoll <= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().exploreRange4.end) {
					rolledWithinRanges = true;
					encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().range4FurtherIndex;
				}
			}

//TOMMAYBEDO Display the currently-rolled encounter's description (e.g. "Roll 2-5: Move on", etc.)

			if (rolledWithinRanges == false) {
				//If the number chosen doesn't belong to any of the other "Ranges"
				if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().rangeFailMeansMoveOn) {
					//If the player is meant to move on when rolling out of range
					print("Failed to roll within exploration ranges. Moving on");
					if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().canGetLost) {
						//If the player is then meant to move in a random direction
						int randomRoll = Random.Range(2, 13);
						randomRoll += CharacterManager.mySkillGeneralTotal;

						if (/*randomRoll >= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().randomDirectionChoiceMin &&*/
							randomRoll <= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().randomDirectionChoiceMax) {
							print("You are lost. Moving on in a random direction");
							mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = true;
							mapSceneManager.GetComponent<MapSceneManager>().MoveOn();
						}
						else {
							print("Moving on in the direction of your choice");
							mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = false;
							mapSceneManager.GetComponent<MapSceneManager>().MoveOn();
						}
					}
					else {
						mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = false;
						mapSceneManager.GetComponent<MapSceneManager>().MoveOn();

					}
				}
			}
		}

		//TOMAYBEDO If this doesn't work, just change moveToSpecificTile to a public bool and create string specificTileIndex.
		//if (MapSceneManager.currentEncounter.myEncounterScriptable.moveToSpecificTile != "") {

		//}

		if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().timeLocked) {

			if (CharacterManager.daysTaken <= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().daysLimit2) {
				if (CharacterManager.currentDayTimeTaken <= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().timeLimit2) {
					encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().limit2Index;
				}
				else {
					encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().overTimeIndex;
				}
			}

			if (CharacterManager.daysTaken == MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().daysLimit1) {
				if (CharacterManager.currentDayTimeTaken <= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().timeLimit1) {
					encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().limit1Index;
				}
			}

			if (CharacterManager.daysTaken < MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().daysLimit1) {
				encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().limit1Index;
			}
			else if (CharacterManager.daysTaken > MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().daysLimit2) {
				encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().overTimeIndex;
			}
		}

		if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().previousLocationsConsidered) {
			print(MapSceneManager.previousLocation);
			print(MapSceneManager.currentLocation);

			bool previousLocationConsidered = false;

			foreach (string location in MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().locationsConsidered) {
				if (MapSceneManager.previousLocation.GetComponent<ScriptableMapTileReader>().myLocationID == location) {
					previousLocationConsidered = true;
				}
			}

			if (previousLocationConsidered) {
				encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().locationBasedEncounter;
			}
			else {
				encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().alternateLocationEncounter;
			}
		}

//TODO These are for TESTING PURPOSES ONLY
		//Directly assign the Encounter Index, to bring it up "for inspection"
		Debug.Log("In case you were wondering, you're directly assigning the Encounter Index right here!");
		encounterIndex = 220;




		print("Encounter index: " + encounterIndex);

		UpdateEncounter(encounterIndex);

		if (MapSceneManager.currentEncounter.myEncounterScriptable.moveToSpecificTile != "") {
			foreach (GameObject tile in mapSceneManager.GetComponent<MapSceneManager>().mapTiles) {
				if (tile.GetComponent<ScriptableMapTileReader>().myLocationID ==
				MapSceneManager.currentEncounter.myEncounterScriptable.moveToSpecificTile) {

					//Update player GameObject location
					//TODO Animate/Lerp the player GameObject to the new MapSceneManager.currentLocation.
					mapSceneManager.GetComponent<MapSceneManager>().player.transform.position = tile.transform.position + (Vector3.back * .3f);

					//Update MapSceneManager.currentLocation
					MapSceneManager.previousLocation = MapSceneManager.currentLocation;
					MapSceneManager.currentLocation = tile;

					//Initialize tile's LocationText
					mapSceneManager.GetComponent<MapSceneManager>().UpdateLocationBG();
				}
			}
		}

//TOREFACTOR Merge this with the above ^ if-statement by passing in the relevant "Tile" to all further functionality
		if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().changesPlayerLocation) {
			foreach (GameObject tile in mapSceneManager.GetComponent<MapSceneManager>().mapTiles) {
				if (tile.GetComponent<ScriptableMapTileReader>().myLocationID ==
				MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().changedLocationID) {

	//Update player GameObject location
//TODO Animate/Lerp the player GameObject to the new MapSceneManager.currentLocation.
					mapSceneManager.GetComponent<MapSceneManager>().player.transform.position = tile.transform.position + (Vector3.back * .3f);

					//Update MapSceneManager.currentLocation
					MapSceneManager.previousLocation = MapSceneManager.currentLocation;
					MapSceneManager.currentLocation = tile;
				}
			}
		}
	}


	public void ChooseEncounterBGEncounter (int whichEncounterButtonSelected) {
		int currentEncounterIndex = 0;
		if (whichEncounterButtonSelected == 1) {
			currentEncounterIndex = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter1Index;
		}
		else if (whichEncounterButtonSelected == 2) {
			currentEncounterIndex = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter2Index;
		}
		else if (whichEncounterButtonSelected == 3) {
			currentEncounterIndex = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter3Index;
		}
		else if (whichEncounterButtonSelected == 4) {
			currentEncounterIndex = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter4Index;
		}

		DetermineEncounterBGFromEncounter(whichEncounterButtonSelected, currentEncounterIndex);
	}


	void DetermineEncounterBGFromEncounter (int furtherEncounterButton, int furtherEncounterIndex) {


		UpdateEncounter(furtherEncounterIndex);
	}


	void UpdateEncounter (int encounterIndex) {
		if (encounterIndex >= 100 && encounterIndex < 500) {
			myEncounterScriptable = mapSceneManager.GetComponent<EncounterManager>().encounterTextScriptables[encounterIndex];
			mapSceneManager.GetComponent<MapSceneManager>().encounterTextBG.GetComponent<ScriptableEncounterReader>().myEncounterScriptable = mapSceneManager.GetComponent<EncounterManager>().encounterTextScriptables[encounterIndex];

			print(myEncounterScriptable.name);
			
			MapSceneManager.currentEncounter = mapSceneManager.GetComponent<MapSceneManager>().encounterTextBG.GetComponent<ScriptableEncounterReader>();

			GetComponentInChildren<Text>().text = myEncounterScriptable.encounterText[0];
			encounterTimeText.text = "Time: " + myEncounterScriptable.timeTaken.ToString();
			encounterXPText.text = "Experience: " + myEncounterScriptable.XPGained.ToString();

			if (myEncounterScriptable.isMerchant) {
				OpenMerchantUI();
			}

			mapSceneManager.GetComponent<MapSceneManager>().UpdateEncounterBG();
		}
		else {
			Debug.Log("Unable to retrieve encounter " + encounterIndex + " because it doesn't exist.");
		}
	}

	void OpenMerchantUI () {
		merchantUI.GetComponent<Animator>().SetBool("SlideIn", true);

	}
}
