using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableEncounterReader : MonoBehaviour {

	[SerializeField] EncounterScriptable myEncounterScriptable;

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
		if (whichTileEncounterVariable == 1) {
			currentEncounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex1;
		}
		else if (whichTileEncounterVariable == 2) {
			currentEncounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex2;
		}
		else if (whichTileEncounterVariable == 3) {
			currentEncounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex3;
		}

		//print(currentEncounterIndex.ToString());

//TODO  Create and update the encounter's TIME variable display on a Text child of this Gameobject (the EncounterBG)
//Same with the Experience Points
//Then do the same to the LocationBG

		UpdateEncounterBG(whichTileEncounterVariable, currentEncounterIndex);
	}


	public void UpdateEncounterBG (int encounterButtonClicked, int encounterIndex) {
		//If the player is able to explore the area
		if (encounterButtonClicked == 1 && MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().actionRequiresRoll) {
			int exploreSuccessRoll = Random.Range(2, 13);

			if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().addSkillToAction == MapTileScriptable.actionSkill.General) {
				exploreSuccessRoll += CharacterManager.mySkillGeneralTotal;
				print("Adding General skill bonus");
			}
			else if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().addSkillToAction == MapTileScriptable.actionSkill.Trickery) {
				exploreSuccessRoll += CharacterManager.mySkillTrickeryTotal;
				print("Adding Trickery skill bonus");
			}
			else if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().addSkillToAction == MapTileScriptable.actionSkill.Perception) {
				exploreSuccessRoll += CharacterManager.mySkillPerceptionTotal;
				print("Adding Perception skill bonus");
			}

			print("Explore Roll: " + exploreSuccessRoll);

			//If the player succeeds in exploring the area
			if (exploreSuccessRoll >= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().actionMinLimit) {
				encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().actionIndex;
			}
		}

		if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().exploreHasMultipleRanges) {
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

			print("Explore roll: " + exploreRoll);

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

			if (rolledWithinRanges == false) {
				//If the number chosen doesn't belong to any of the other "Ranges"
				if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().rangeFailMeansMoveOn) {
					//If the player is meant to move on when rolling out of range
					print("Failed to roll within exploration ranges. Moving on");
					if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().moveRandomDirection) {
						//If the player is then meant to move in a random direction
						int randomRoll = Random.Range(2, 13);
						randomRoll += CharacterManager.mySkillGeneralTotal;

						if (randomRoll >= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().randomDirectionChoiceMin &&
							randomRoll <= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().randomDirectionChoiceMax) {
							print("Moving on in a random direction");
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

		print(encounterIndex);

		if (encounterIndex != 0 && encounterIndex != 1 && encounterIndex >= 100 && encounterIndex <= 454) {
			myEncounterScriptable = mapSceneManager.GetComponent<EncounterManager>().encounterTextScriptables[encounterIndex];

			GetComponentInChildren<Text>().text = myEncounterScriptable.encounterText[0];

			encounterTimeText.text = "Time: " + myEncounterScriptable.timeTaken.ToString();

			encounterXPText.text = "Experience: " + myEncounterScriptable.XPGained.ToString();

			//Re-calculate the player's TOTAL TIME TAKEN & TOTAL XP EARNED
			CharacterManager.totalTimeTaken += myEncounterScriptable.timeTaken;
			CharacterManager.totalXP += myEncounterScriptable.XPGained;

			//print("Total time: " + CharacterManager.totalTimeTaken);
			//print("Total XP: " + CharacterManager.totalXP);

			if (myEncounterScriptable.isMerchant) {
				OpenMerchantUI();
			}
		}
		else {
			Debug.Log("Unable to retrieve encounter " + encounterIndex + " because it doesn't exist.");
		}
	}


	void OpenMerchantUI () {
		merchantUI.GetComponent<Animator>().SetBool("SlideIn", true);

	}
}
