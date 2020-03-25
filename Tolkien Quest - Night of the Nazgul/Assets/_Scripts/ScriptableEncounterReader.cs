using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableEncounterReader : MonoBehaviour {

	public EncounterScriptable myEncounterScriptable;

	[SerializeField] GameObject mapSceneManager;
	[SerializeField] GameObject mapSceneInventoryManager;

	public bool movesTwoSpaces;

	//[SerializeField] Text encounterTimeText;
	//[SerializeField] Text encounterXPText;

	[SerializeField] GameObject merchantUI;

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

		mapSceneManager.GetComponent<MapSceneManager>().CloseLocationUI();
	}


	public void DetermineEncounterBGFromLocation (int encounterButtonClicked, int encounterIndex) {
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

						if (randomRoll <= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().randomDirectionChoiceMax) {
							print("You are lost. Moving on in a random direction");
							mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = true;
							mapSceneManager.GetComponent<MapSceneManager>().MoveOn(false);
						}
						else {
							print("Moving on in the direction of your choice");
							mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = false;
							mapSceneManager.GetComponent<MapSceneManager>().MoveOn(false);
						}
					}
					else {
						mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = false;
						mapSceneManager.GetComponent<MapSceneManager>().MoveOn(false);

					}
				}
			}
		}

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

		////TODO These are for TESTING PURPOSES ONLY
		//		//Directly assign the Encounter Index, to bring it up "for inspection"
		Debug.Log("<b>In case you were wondering, you're directly assigning the Encounter Index right here!</b>");
		encounterIndex = 222//304//385//336//216//319//204//452//204//385//222//15
			;

		print("Encounter index: " + encounterIndex);

		UpdateEncounter(encounterIndex);

//TOREFACTOR Merge this with the above ^ if-statement by passing in the relevant "Tile" to all further functionality
//Actually, it's basically the same as DetermineEncounterFromBGEncounter's "moveToSpecificTile" process. Maybe they can be merged?
		if (MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().changesPlayerLocation) {
			mapSceneManager.GetComponent<MapSceneManager>().MoveToSpecificLocation(MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().changedLocationID);
			print("You should be in space " + MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().changedLocationID);
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
			//Unique Rule for Encounters 319 & 267
			if (MapSceneManager.currentEncounter.myEncounterScriptable.canJumpInRiver) {
				//Just 267
				if (MapSceneManager.currentEncounter.myEncounterScriptable.jumpInRiverRequiresRoll) {
					int jumpRoll = Random.Range(2, 13);
					jumpRoll += CharacterManager.mySkillGeneralTotal;
					print("JumpRoll: " + jumpRoll);

					if (jumpRoll >= 9) {
						currentEncounterIndex = 290;
					}
					else {
						currentEncounterIndex = 295;
					}
				}
				else {
					currentEncounterIndex = 290;
				}
			}
			else{
				currentEncounterIndex = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter4Index;
			}
		}

		DetermineEncounterBGFromEncounter(whichEncounterButtonSelected, currentEncounterIndex);
	}


	void DetermineEncounterBGFromEncounter (int furtherEncounterButton, int furtherEncounterIndex) {
		if (MapSceneManager.currentEncounter.myEncounterScriptable != null) {
			if (furtherEncounterButton == 1 && MapSceneManager.currentEncounter.myEncounterScriptable.exploreHasMultipleRanges) {
			int exploreRoll = Random.Range(2, 13);
			bool rolledWithinRanges = false;

			if (MapSceneManager.currentEncounter.myEncounterScriptable.actionAddsSkill == EncounterScriptable.actionSkill.General) {
				exploreRoll += CharacterManager.mySkillGeneralTotal;
				print("Adding General skill bonus");
			}
			else if (MapSceneManager.currentEncounter.myEncounterScriptable.actionAddsSkill == EncounterScriptable.actionSkill.Trickery) {
				exploreRoll += CharacterManager.mySkillTrickeryTotal;
				print("Adding Trickery skill bonus");
			}
			else if (MapSceneManager.currentEncounter.myEncounterScriptable.actionAddsSkill == EncounterScriptable.actionSkill.Perception) {
				exploreRoll += CharacterManager.mySkillPerceptionTotal;
				print("Adding Perception skill bonus");
			}
//TODO Finish this list, adding the other relevant skill totals

			print("Explore roll: " + exploreRoll);

			if (MapSceneManager.currentEncounter.myEncounterScriptable.howManyRanges > 0) {
				MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange1 = new RangeInt(MapSceneManager.currentEncounter.myEncounterScriptable.range1Min, MapSceneManager.currentEncounter.myEncounterScriptable.range1Length);

				if (exploreRoll >= MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange1.start &&
					exploreRoll <= MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange1.end) {
					rolledWithinRanges = true;
					furtherEncounterIndex = MapSceneManager.currentEncounter.myEncounterScriptable.range1FurtherIndex;
					print("You rolled between" + MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange1.start + " - " +
						MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange1.end);

						//The Player's attempt to RUN AWAY FAILS
						if (MapSceneManager.currentEncounter.myEncounterScriptable.hasRunAwayRange) {
							print("You failed to Run Away!");
//INITIATE COMBAT AND OPEN CombatBG
//The Enemy attacks first
//CombatManager.whoAttacksFirst = enemy;


//IF COMBAT HAS ALREADY BEEN INITIATED (i.e. mapScenemanager.currentEncounter.isInCombat == true)
//Keep the player in the current combat bout (with the current enemy's current stats), and let the enemy attack immediately after the player fails to run away

						}
				}
			}

			if (MapSceneManager.currentEncounter.myEncounterScriptable.howManyRanges > 1) {
				MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange2 = new RangeInt(MapSceneManager.currentEncounter.myEncounterScriptable.range2Min, MapSceneManager.currentEncounter.myEncounterScriptable.range2Length);
				if (exploreRoll >= MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange2.start &&
					exploreRoll <= MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange2.end) {
						rolledWithinRanges = true;
						furtherEncounterIndex = MapSceneManager.currentEncounter.myEncounterScriptable.range2FurtherIndex;
						print("You rolled between" + MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange2.start + " - " +
								MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange2.end);

						//The Player's SNEAK ATTACK succeeds, allowing them extra damage for the first attack in the following combat
						if (MapSceneManager.currentEncounter.myEncounterScriptable.hasSneakAttackRange) {
//INITIATE COMBAT AND OPEN CombatBG
//The Player's FIRST ATTACK adds his/her Trickery bonus to their OB stat
							print("Sneak attack successful");
							CharacterManager.sneakAttackBonus = CharacterManager.mySkillTrickeryTotal;
//During their first attack, apply the sneakAttackBonus stat to the Player's various OB stat bonuses
//(In the CombatManager script) Each round after attacking, set sneakAttackBonus back to 0 if it isn't already

						}


						//For now this only applies to Encounter 354
						if (myEncounterScriptable.exploreRangeDeterminesItems) {
						InventoryManager.silverCarried += 5;
						print("You stole 5 silver from the troll. Current Silver: " + InventoryManager.silverCarried);
					}
				}
			}

			if (MapSceneManager.currentEncounter.myEncounterScriptable.howManyRanges > 2) {
				MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange3 = new RangeInt(MapSceneManager.currentEncounter.myEncounterScriptable.range3Min, MapSceneManager.currentEncounter.myEncounterScriptable.range3Length);
				if (exploreRoll >= MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange3.start &&
					exploreRoll <= MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange3.end) {
					rolledWithinRanges = true;
					furtherEncounterIndex = MapSceneManager.currentEncounter.myEncounterScriptable.range3FurtherIndex;
					print("You rolled between" + MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange3.start + " - " +
							MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange3.end);
				}
			}

			if (MapSceneManager.currentEncounter.myEncounterScriptable.howManyRanges > 3) {
				MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange4 = new RangeInt(MapSceneManager.currentEncounter.myEncounterScriptable.range4Min, MapSceneManager.currentEncounter.myEncounterScriptable.range4Length);
				if (exploreRoll >= MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange4.start &&
					exploreRoll <= MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange4.end) {
					rolledWithinRanges = true;
					furtherEncounterIndex = MapSceneManager.currentEncounter.myEncounterScriptable.range4FurtherIndex;
					print("You rolled between" + MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange4.start + " - " +
							MapSceneManager.currentEncounter.myEncounterScriptable.exploreRange4.end);
				}
			}

			//TOMMAYBEDO Display the currently-rolled encounter's description (e.g. "Roll 2-5: Move on", etc.)

			if (rolledWithinRanges == false) {
				//If the number chosen doesn't belong to any of the other "Ranges"
				if (MapSceneManager.currentEncounter.myEncounterScriptable.rangeFailMeansMoveOn) {
					//If the player is meant to move on when rolling out of range
					print("Failed to roll within exploration ranges. Moving on");
					if (MapSceneManager.currentEncounter.myEncounterScriptable.canGetLost) {
						//	//If the player is then meant to move in a random direction
						int randomRoll = Random.Range(2, 13);
						randomRoll += CharacterManager.mySkillGeneralTotal;

						print("Lost Chance Roll: " + randomRoll);

						if (/*randomRoll >= MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().randomDirectionChoiceMin &&*/
							randomRoll <= MapSceneManager.currentEncounter.myEncounterScriptable.randomDirectionChoiceMax) {
							print("You are lost. Moving on in a random direction");
							mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = true;
							mapSceneManager.GetComponent<MapSceneManager>().MoveOn(false);
						}
						else {
							print("Moving on in the direction of your choice");
							mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = false;
							mapSceneManager.GetComponent<MapSceneManager>().MoveOn(false);
						}
					}
					else {
						mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = false;
						mapSceneManager.GetComponent<MapSceneManager>().MoveOn(false);
					}
				}

				if (MapSceneManager.currentEncounter.myEncounterScriptable.rangeFailMeansGameOver) {
					print("Failed to roll within exploration ranges. You are dead");

					mapSceneManager.GetComponent<MapSceneManager>().GameOver();
				}
			}
		}
		
		
		MapSceneManager.currentEncounter.movesTwoSpaces = MapSceneManager.currentEncounter.myEncounterScriptable.movesOnTwoSpaces;

		UpdateEncounter(furtherEncounterIndex);
		}
	}


	public void UpdateEncounter (int encounterIndex) {
		//THIS is the place to test variables on the EncounterScriptable
		if (encounterIndex >= 100 && encounterIndex < 500) {
			myEncounterScriptable = mapSceneManager.GetComponent<EncounterManager>().encounterTextScriptables[encounterIndex];
			mapSceneManager.GetComponent<MapSceneManager>().encounterTextBG.GetComponent<ScriptableEncounterReader>().myEncounterScriptable = mapSceneManager.GetComponent<EncounterManager>().encounterTextScriptables[encounterIndex];

			print(myEncounterScriptable.name);

			MapSceneManager.currentEncounter = mapSceneManager.GetComponent<MapSceneManager>().encounterTextBG.GetComponent<ScriptableEncounterReader>();
			//MapSceneManager.currentEncounter.movesTwoSpaces = myEncounterScriptable.movesOnTwoSpaces;

			if (MapSceneManager.currentEncounter.myEncounterScriptable.canMoveOn) {
				//print("You can move on");
				//mapSceneManager.GetComponent<MapSceneManager>().moveOnEncounterButton.SetActive(true);
				mapSceneManager.GetComponent<MapSceneManager>().moveOnEncounterButton.GetComponentInChildren<Text>().text = "Move On";

				if (MapSceneManager.currentEncounter.myEncounterScriptable.moveOnRandomly) {
					mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = true;
					mapSceneManager.GetComponent<MapSceneManager>().moveOnEncounterButton.GetComponentInChildren<Text>().text = "Move On " + "\n" + "(Random)";
				}

				//"Lost" movement rules
				if (MapSceneManager.currentEncounter.myEncounterScriptable.rollHowToMoveOnMax != 0) {
					int moveRoll = Random.Range(2, 13);
					moveRoll += CharacterManager.mySkillPerceptionTotal;

					print("Lost Roll: " + moveRoll);

					if (moveRoll <= MapSceneManager.currentEncounter.myEncounterScriptable.rollHowToMoveOnMax) {
						print("Moving in a random direction");
						mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = true;
					}
					else {
						print("Moving in your specified direction");
						mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = false;
					}
				}
			}
			else {
				mapSceneManager.GetComponent<MapSceneManager>().moveOnEncounterButton.SetActive(false);
			}

			//Move the player/currentLocation to a SPECIFIC TILE, as directed by the currentEncounter
			if (MapSceneManager.currentEncounter.myEncounterScriptable.moveToSpecificTile != "") {
				mapSceneManager.GetComponent<MapSceneManager>().MoveToSpecificLocation(MapSceneManager.currentEncounter.myEncounterScriptable.moveToSpecificTile);
				print("You should be in space " + MapSceneManager.currentEncounter.myEncounterScriptable.moveToSpecificTile);

				//If the Location Text for the "specific tile" is meant to be read, rather than just moving on
				if (!MapSceneManager.currentEncounter.myEncounterScriptable.canMoveOn) {
//Open the LocationTextBG
					mapSceneManager.GetComponent<MapSceneManager>().currentLocationTextIndex = 0;
					mapSceneManager.GetComponent<MapSceneManager>().UpdateLocationBG();
				}

				if (MapSceneManager.currentEncounter.myEncounterScriptable.moveOnRandomly) {
					mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = true;
				}
//TODO Be sure to close the EncounterBG (and any other UI which might be in the way)
			}

			if (myEncounterScriptable.obtainsItems) {
				bool obtainedItems = false;
				List<ScriptableObject> itemsInList = new List<ScriptableObject>();
				List<int> itemQuantitiesList = new List<int>();

				//TODO Implement Ranges for obtaining random items.
				if (myEncounterScriptable.obtainedItemList.hasItemRanges) {
					int ObtainedObjectRoll = Random.Range(2, 13);

					if (ObtainedObjectRoll >= myEncounterScriptable.obtainedItemList.itemRange1Min && ObtainedObjectRoll <= myEncounterScriptable.obtainedItemList.itemRange1Max) {
						if (myEncounterScriptable.obtainedItemList.range1MultipleItems) {
							for (int i = 0; i < myEncounterScriptable.obtainedItemList.range1Items.Length; i++) {
								itemsInList.Add(myEncounterScriptable.obtainedItemList.range1Items[i]);
								itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.range1ItemQuantities[i]);
							}
						}
						else {
							itemsInList.Add(myEncounterScriptable.obtainedItemList.myItemList[0]);
							itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem1Quantity);
						}
						obtainedItems = true;
					}
					else if (ObtainedObjectRoll >= myEncounterScriptable.obtainedItemList.itemRange2Min && ObtainedObjectRoll <= myEncounterScriptable.obtainedItemList.itemRange2Max) {
						if (myEncounterScriptable.obtainedItemList.range2MultipleItems) {
							for (int i = 0; i < myEncounterScriptable.obtainedItemList.range2Items.Length; i++) {
								itemsInList.Add(myEncounterScriptable.obtainedItemList.range2Items[i]);
								itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.range2ItemQuantities[i]);
							}
						}
						else {
							itemsInList.Add(myEncounterScriptable.obtainedItemList.myItemList[1]);
							itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem2Quantity);
						}
						obtainedItems = true;
					}
					else if (ObtainedObjectRoll >= myEncounterScriptable.obtainedItemList.itemRange3Min && ObtainedObjectRoll <= myEncounterScriptable.obtainedItemList.itemRange3Max) {
						if (myEncounterScriptable.obtainedItemList.range3MultipleItems) {
							for (int i = 0; i < myEncounterScriptable.obtainedItemList.range3Items.Length; i++) {
								itemsInList.Add(myEncounterScriptable.obtainedItemList.range3Items[i]);
								itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.range3ItemQuantities[i]);
							}
						}
						else {
							itemsInList.Add(myEncounterScriptable.obtainedItemList.myItemList[2]);
							itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem3Quantity);
						}
						obtainedItems = true;
					}
					else if (ObtainedObjectRoll >= myEncounterScriptable.obtainedItemList.itemRange4Min && ObtainedObjectRoll <= myEncounterScriptable.obtainedItemList.itemRange4Max) {
						if (myEncounterScriptable.obtainedItemList.range4MultipleItems) {
							for (int i = 0; i < myEncounterScriptable.obtainedItemList.range4Items.Length; i++) {
								itemsInList.Add(myEncounterScriptable.obtainedItemList.range4Items[i]);
								itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.range4ItemQuantities[i]);
							}
						}
						else {
							itemsInList.Add(myEncounterScriptable.obtainedItemList.myItemList[3]);
							itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem4Quantity);
						}
						obtainedItems = true;
					}
					else {
						print("Rolled out of item ranges");
						if (myEncounterScriptable.obtainedItemList.outOfRangeMovesToEncounter) {
							UpdateEncounter(myEncounterScriptable.obtainedItemList.encounterIndex);
						}
					}
				}
				else {
					for (int i = 0; i < myEncounterScriptable.obtainedItemList.myItemList.Length; i++) {
						if (myEncounterScriptable.obtainedItemList.myItemList[i] != null) {
							itemsInList.Add(myEncounterScriptable.obtainedItemList.myItemList[i]);
							obtainedItems = true;
						}
					}
//TODO Make an array for these quantities, just like with "myEncounterScriptable.obtainedItemList.myItemList[i]", because this MIGHT NOT WORK
					itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem1Quantity);
					itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem2Quantity);
					itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem3Quantity);
					itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem4Quantity);

				}

				if (myEncounterScriptable.obtainedItemList.itemChecked != null) {
					bool itemFound = false;
					bool itemUsed = false;
					int itemQuantityTotal = 0;

					//Check for relevant item(s) in the player's inventory
					foreach (GameObject item in mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents) {
						if (item.transform.childCount != 0) {
							if (item.GetComponentInChildren<InventoryScriptableReader>().objectScript == myEncounterScriptable.obtainedItemList.itemChecked) {
								itemFound = true;
								print("Found the item you're looking for in your inventory");

								if (!itemUsed && myEncounterScriptable.obtainedItemList.useItem && !item.GetComponentInChildren<InventoryScriptableReader>().isInfinite) {
									item.GetComponentInChildren<InventoryScriptableReader>().itemQuantity--;
									item.GetComponentInChildren<InventoryScriptableReader>().InitializeItem();
									print ("Used the item. Item Quantity: " + item.GetComponentInChildren<InventoryScriptableReader>().itemQuantity);
									itemUsed = true;
								}

								itemQuantityTotal += item.GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
							}
							else {
								//print("Couldn't use the item. bool itemUsed = " + itemUsed + " || Encounter Uses Item = " + myEncounterScriptable.obtainedItemList.useItem);
							}
						}

						//Check the quantity
						if (itemFound && myEncounterScriptable.obtainedItemList.checksItemQuantityMax != 0) {
							if (itemQuantityTotal >= myEncounterScriptable.obtainedItemList.checksItemQuantityMin &&
								itemQuantityTotal <= myEncounterScriptable.obtainedItemList.checksItemQuantityMax) {
//FINISH THIS
							}
						}
					}

					if (itemFound) {
						if (myEncounterScriptable.obtainedItemList.hasItemMovesToEncounter != 0) {
							print("Item detected in inventory. Moving on");
							DelayEncounterUpdate(myEncounterScriptable.obtainedItemList.hasItemMovesToEncounter);
							//return;
						}
					}
					else {
						if (myEncounterScriptable.obtainedItemList.lacksItemMovesToEncounter != 0) {
							print("Item not detected in inventory. Moving on");
							DelayEncounterUpdate(myEncounterScriptable.obtainedItemList.lacksItemMovesToEncounter);
							//return;
						}
					}
					
					if (!itemUsed) {
						//print(CharacterManager.statusDiseased);
						if (myEncounterScriptable.obtainedItemList.lackingItemMeansDiseased) {
							CharacterManager.statusDiseased = true;
						}

						//Unique rule for encounter 385
						if (myEncounterScriptable.obtainedItemList.lackingItemMeansPoisoned) {
							print("Failed to heal your poisoning");
							DelayEncounterUpdate(357);
							//UpdateEncounter(357);
							//return;
						}
					}
				}

				if (myEncounterScriptable.obtainedItemList.gainDaggerIfYouHaveNoWeapon) {
					bool hasWeapon = false;
					foreach (GameObject itemParent in mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents) {
						if (itemParent.transform.childCount != 0) {
							if (itemParent.GetComponentInChildren<InventoryScriptableReader>().myObjectType == InventoryScriptableReader.objectType.weapon) {
								hasWeapon = true;
							}
						}
					}

					if (!hasWeapon) {
						itemsInList.Add(myEncounterScriptable.obtainedItemList.dagger);
						obtainedItems = true;
					}
				}

				if (obtainedItems) {
					mapSceneManager.GetComponent<MapSceneManager>().UpdateItemListBG(itemsInList, itemQuantitiesList);
				}

				if (myEncounterScriptable.loseWeaponsAndArmor) {
					foreach (GameObject itemParent in mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents) {
						if (itemParent.transform.childCount != 0) {
							if (itemParent.GetComponentInChildren<InventoryScriptableReader>().objectScript != myEncounterScriptable.keepSilverDagger) {
								if (itemParent.GetComponentInChildren<InventoryScriptableReader>().myObjectType == InventoryScriptableReader.objectType.weapon ||
									itemParent.GetComponentInChildren<InventoryScriptableReader>().myObjectType == InventoryScriptableReader.objectType.armor) {
									Destroy(itemParent.transform.GetChild(0));
									//Alternately, just DISABLE the object rather than DESTROYING it
									//itemParent.transform.GetChild(0).gameObject.SetActive(false);
								}
							}
						}
					}
				}
			}

			
			if (myEncounterScriptable.floatsDownstream) {
				string downstreamTileIndex = null;

				if (myEncounterScriptable.hasDownstreamRanges) {
					int downstreamRoll = Random.Range(2, 13);

					if (downstreamRoll <= 6) {
						if (myEncounterScriptable.landsOnOtherRiverSide) {
							if (myEncounterScriptable.lowerValueFloats1Space) {
								downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamOtherSideTile1ID;
							}
							else {
								downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamOtherSideTile2ID;
							}
						}
						else {
							if (myEncounterScriptable.lowerValueFloats1Space) {
								downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamSameSideTile1ID;
							}
							else {
								downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamSameSideTile2ID;
							}
						}
					}
					else {
						if (myEncounterScriptable.landsOnOtherRiverSide) {
							if (myEncounterScriptable.lowerValueFloats1Space) {
								downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamOtherSideTile2ID;
							}
							else {
								downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamOtherSideTile1ID;
							}
						}
						else {
							if (myEncounterScriptable.lowerValueFloats1Space) {
								downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamSameSideTile2ID;
							}
							else {
								downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamSameSideTile1ID;
							}
						}
					}
				}
				else {
					if (myEncounterScriptable.landsOnOtherRiverSide) {
						if (myEncounterScriptable.lowerValueFloats1Space) {
							downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamOtherSideTile1ID;
						}
						else {
							downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamOtherSideTile2ID;
						}
					}
					else {
						if (myEncounterScriptable.lowerValueFloats1Space) {
							downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamSameSideTile1ID;
						}
						else {
							downstreamTileIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().downstreamSameSideTile2ID;
						}
					}
				}

				if (downstreamTileIndex != null) {
					//mapSceneManager.GetComponent<MapSceneManager>().mapTiles[downstreamTileIndex].GetComponent<
					MapSceneManager.currentEncounter.GetComponent<ScriptableEncounterReader>().myEncounterScriptable.moveToSpecificTile = "Moving";
					//MapSceneManager.currentEncounter.myEncounterScriptable.moveToSpecificTile = "Moving downstream || " + downstreamTileIndex;
					mapSceneManager.GetComponent<MapSceneManager>().MoveToSpecificLocation(downstreamTileIndex);

				}
				else {
					print("Couldn't find a downstreamTile");
				}
			}

			if (myEncounterScriptable.isMerchant) {
				OpenMerchantUI();
			}

			if (myEncounterScriptable.damageAlteredAmount != 0) {
				if (myEncounterScriptable.dealsRandomDirectDamage) {
					mapSceneManager.GetComponent<MapSceneManager>().AlterDamage(Random.Range(2, 13));
				}
				else if (myEncounterScriptable.hasDamageRanges) {
					//Only applies to Encounter 118
					int damageRangeRoll = Random.Range(2, 13);
					print(damageRangeRoll);

					if (damageRangeRoll <= 4) {
						mapSceneManager.GetComponent<MapSceneManager>().AlterDamage(6);
					}
					else if (damageRangeRoll >= 5 && damageRangeRoll <= 7) {
						mapSceneManager.GetComponent<MapSceneManager>().AlterDamage(3);
					}
				}
				else if (myEncounterScriptable.deals50PercentDamage) {
					CharacterManager.damageTaken = CharacterManager.enduranceTotal / 2;
				}
				else {
					mapSceneManager.GetComponent<MapSceneManager>().AlterDamage(MapSceneManager.currentEncounter.myEncounterScriptable.damageAlteredAmount);
				}
			}

			if (CharacterManager.statusDiseased) {
				if (MapSceneManager.currentEncounter.myEncounterScriptable.curesDisease) {
					CharacterManager.statusDiseased = false;
					CharacterManager.diseaseTimer = 0;
				}
				else {
					CharacterManager.diseaseTimer += myEncounterScriptable.timeTaken;
					if (CharacterManager.diseaseTimer >= 60) {
						mapSceneManager.GetComponent<MapSceneManager>().AlterDamage(1);
					}
				}
			}

			if (MapSceneManager.currentEncounter.myEncounterScriptable.meetTom) {
				CharacterManager.metTom = true;
			}
			if (MapSceneManager.currentEncounter.myEncounterScriptable.meetGildor) {
				CharacterManager.metGildor = true;
			}

			//Unique Encounter Rule on Encounter 128
			if (MapSceneManager.currentEncounter.myEncounterScriptable.checkIfMetTom) {
				if (CharacterManager.metTom) {
					mapSceneManager.GetComponent<MapSceneManager>().MoveOn(false);
				}
				else {
					MapSceneManager.currentEncounter.DelayEncounterUpdate(122);
					//MapSceneManager.currentEncounter.UpdateEncounter(122);
				}
			}
			//Unique Encounter Rule on Encounter 304
			if (MapSceneManager.currentEncounter.myEncounterScriptable.checkIfMetGildor) {
				if (CharacterManager.metGildor) {
					mapSceneManager.GetComponent<MapSceneManager>().MoveOn(false);
					mapSceneManager.GetComponent<MapSceneManager>().CloseEncounterUI();
					return;
				}
				else {
					MapSceneManager.currentEncounter.DelayEncounterUpdate(283);
					//MapSceneManager.currentEncounter.UpdateEncounter(283);
				}
			}

			if (MapSceneManager.currentEncounter.myEncounterScriptable.autoWin) {
				mapSceneManager.GetComponent<MapSceneManager>().Win();
			}

			if (MapSceneManager.currentEncounter.myEncounterScriptable.autoGameOver) {
				mapSceneManager.GetComponent<MapSceneManager>().GameOver();

			}

			mapSceneManager.GetComponent<MapSceneManager>().UpdateEncounterBG();
			mapSceneManager.GetComponent<MapSceneManager>().OpenEncounterUI();

		}
		else {
			Debug.Log("Unable to retrieve encounter " + encounterIndex + " because it doesn't exist.");
		}
	}


	public void DelayEncounterUpdate(int nextEncounter) {
		MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter1Index = nextEncounter;
	}


	void OpenMerchantUI () {
		merchantUI.GetComponent<Animator>().SetBool("SlideIn", true);

	}
}
