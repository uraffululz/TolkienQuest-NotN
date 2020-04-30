using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableEncounterReader : MonoBehaviour {

	public EncounterScriptable myEncounterScriptable;

	[SerializeField] GameObject mapSceneManager;
	[SerializeField] GameObject mapSceneInventoryManager;
	[SerializeField] GameObject itemListInventoryManager;

	public int bonusToNextExplore = 0;

	public bool movesTwoSpaces;

	public bool onlyMovesToDelayedEncounter = false;

	//[SerializeField] Text encounterTimeText;
	//[SerializeField] Text encounterXPText;

	[SerializeField] GameObject merchantUI;

	[SerializeField] InventoryItemScriptable healingHerbScript;
	[SerializeField] InventoryWeaponScriptable arrowScript;

	public int delayedEncounterIndex = 0;


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
							mapSceneManager.GetComponent<MapSceneManager>().MoveOn(1);
						}
						else {
							print("Moving on in the direction of your choice");
							mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = false;
							mapSceneManager.GetComponent<MapSceneManager>().MoveOn(1);
						}
					}
					else {
						mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = false;
						mapSceneManager.GetComponent<MapSceneManager>().MoveOn(1);

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
		//Directly assign the Encounter Index, to bring it up "for inspection"
Debug.Log("<b>In case you were wondering, you're directly assigning the Encounter Index right here!</b>");
		encounterIndex = 253//207//219//316//154//263//172//356//355//278//176//222//298//164//268//165//146//118//361//364//385//445//128//354//421//466//151//110//354//222//336//216//319//452//204//158
			;

		//print("Encounter index: " + encounterIndex);

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

				if (bonusToNextExplore != 0) {
					exploreRoll += bonusToNextExplore;
					print("Added bonus. New Explore Roll: " + exploreRoll);
				}
				bonusToNextExplore = 0;


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

							//For now this only applies to Encounter 354
							if (myEncounterScriptable.exploreRangeDeterminesItems) {
								myEncounterScriptable.obtainedItemList.silverEarned = 0;
								print("You failed to steal from the troll. Current Silver: " + InventoryManager.silverCarried);
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
							//InventoryManager.silverCarried += 5;
							myEncounterScriptable.obtainedItemList.silverEarned = 5;
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
								mapSceneManager.GetComponent<MapSceneManager>().MoveOn(2);
							}
							else {
								print("Moving on in the direction of your choice");
								mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = false;
								mapSceneManager.GetComponent<MapSceneManager>().MoveOn(2);
							}
						}
						else {
							mapSceneManager.GetComponent<MapSceneManager>().moveOnInRandomDirection = false;
							mapSceneManager.GetComponent<MapSceneManager>().MoveOn(2);
						}
					}

					if (MapSceneManager.currentEncounter.myEncounterScriptable.rangeFailMeansGameOver) {
						print("Failed to roll within exploration ranges. You are dead");

						mapSceneManager.GetComponent<MapSceneManager>().GameOver();

	//TOMAYBEDO Have unique text for each encounter that leads to automatic death (357, which leads to 337), 364, the one where you drown, etc.)
					}
				}
			}
		
		
			MapSceneManager.currentEncounter.movesTwoSpaces = MapSceneManager.currentEncounter.myEncounterScriptable.movesOnTwoSpaces;

			UpdateEncounter(furtherEncounterIndex);
		}
	}

	//public void OverrideEncounterButton(InputField input) {
	//	UpdateEncounter(input.int);
	//}


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

			if (myEncounterScriptable.emptiesInventory) {//If a certain encounter EMPTIES THE PLAYER'S INVENTORY
				if (myEncounterScriptable.logsEmptiedInventory) {//If some or all of the inventory can be RECOVERED LATER
					mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().SaveInventory();
					//itemListInventoryManager.GetComponent<MapSceneInventoryManager>().SaveInventory(); 
					
					print("Your inventory has been saved for recovery");
				}

				mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().EmptyInventory();
				itemListInventoryManager.GetComponent<MapSceneInventoryManager>().EmptyInventory();

				InventoryManager.cloakWornScriptable = null;
				InventoryManager.armorWornScriptable = null;
				InventoryManager.daggerWornScriptable = null;
				InventoryManager.slot1Scriptable = null;
				InventoryManager.slot2Scriptable = null;
				InventoryManager.slot3Scriptable = null;
				InventoryManager.slot4Scriptable = null;
				InventoryManager.slot5Scriptable = null;
				InventoryManager.slot6Scriptable = null;
				InventoryManager.slot7Scriptable = null;
				InventoryManager.slot8Scriptable = null;
				InventoryManager.slot9Scriptable = null;
				InventoryManager.slot10Scriptable = null;
				InventoryManager.slot11Scriptable = null;
				InventoryManager.slot12Scriptable = null;

				InventoryManager.cloakQuantity = 0;
				InventoryManager.armorQuantity = 0;
				InventoryManager.daggerQuantity = 0;
				InventoryManager.slot1Quantity = 0;
				InventoryManager.slot2Quantity = 0;
				InventoryManager.slot3Quantity = 0;
				InventoryManager.slot4Quantity = 0;
				InventoryManager.slot5Quantity = 0;
				InventoryManager.slot6Quantity = 0;
				InventoryManager.slot7Quantity = 0;
				InventoryManager.slot8Quantity = 0;
				InventoryManager.slot9Quantity = 0;
				InventoryManager.slot10Quantity = 0;
				InventoryManager.slot11Quantity = 0;
				InventoryManager.slot12Quantity = 0;

				InventoryManager.arrowsCarried = 0;
				InventoryManager.silverCarried = 0;
				InventoryManager.copperCarried = 0;

				//print("Your inventory has been emptied");

				//if (!myEncounterScriptable.logsEmptiedInventory) {
					mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().LogInventory();
					itemListInventoryManager.GetComponent<MapSceneInventoryManager>().InitializeInventory();
				//}
			}

			if (myEncounterScriptable.loseWeaponsAndArmor) {
				foreach (GameObject itemParent in mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents) {
					if (itemParent.transform.childCount != 0) {
						if (itemParent.GetComponentInChildren<InventoryScriptableReader>().objectScript != myEncounterScriptable.keepSilverDagger) {
							if (itemParent.GetComponentInChildren<InventoryScriptableReader>().myObjectType == InventoryScriptableReader.objectType.weapon ||
								itemParent.GetComponentInChildren<InventoryScriptableReader>().myObjectType == InventoryScriptableReader.objectType.armor) {
								Destroy(itemParent.transform.GetChild(0).gameObject);
							}
						}
					}
				}
			}

			if (myEncounterScriptable.obtainsItems) {
				bool obtainedItems = false;
				int earnedSilver = 0;
				int earnedCopper = 0;
				
				//bool recoversInventory = false;
				bool recoversAll = false;
				bool recoversPouch = false;
				bool recoversDagger = false;
				bool recoversRandomWeapon = false;

				List<ScriptableObject> itemsInList = new List<ScriptableObject>();
				List<int> itemQuantitiesList = new List<int>();

				if (myEncounterScriptable.obtainedItemList.hasItemRanges) {
					int ObtainedObjectRoll = Random.Range(2, 13);

//ObtainedObjectRoll = 10;
					//print("Overriding Obtained object roll: " + ObtainedObjectRoll);

					//If the Obtained Item Roll is within the FIRST RANGE
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

						if (myEncounterScriptable.obtainedItemList.range1RecoversMoneyPouch) {
							recoversPouch = true;
						}

						if (myEncounterScriptable.obtainedItemList.range1RecoversDagger) {
							recoversDagger = true;
						}

						mapSceneManager.GetComponent<MapSceneManager>().disableMoveOnButton = false;
						mapSceneManager.GetComponent<MapSceneManager>().disableDelayedEncounterButton = true;
					}
					//If the Obtained Item Roll is within the SECOND RANGE
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
							if (myEncounterScriptable.obtainedItemList.obtainsArrowsInRange2 != 0) {
								InventoryManager.arrowsCarried += myEncounterScriptable.obtainedItemList.obtainsArrowsInRange2;
								print("Arrows carried: " + InventoryManager.arrowsCarried);
							}
						}
						obtainedItems = true;

						if (myEncounterScriptable.obtainedItemList.range2RecoversMoneyPouch) {
							recoversPouch = true;
						}

						if (myEncounterScriptable.obtainedItemList.range2RecoversDagger) {
							recoversDagger = true;
						}

						if (myEncounterScriptable.obtainedItemList.range2RecoversRandomWeapon) {
							recoversRandomWeapon = true;
						}

						mapSceneManager.GetComponent<MapSceneManager>().disableMoveOnButton = false;
						mapSceneManager.GetComponent<MapSceneManager>().disableDelayedEncounterButton = true;
					}
					//If the Obtained Item Roll is within the THIRD RANGE
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

						//if (myEncounterScriptable.obtainedItemList.range3RecoversMoneyPouch) {
						//	recoversPouch = true;
						//}

						//if (myEncounterScriptable.obtainedItemList.range3RecoversDagger) {
						//	recoversDagger = true;
						//}

						mapSceneManager.GetComponent<MapSceneManager>().disableMoveOnButton = false;
						mapSceneManager.GetComponent<MapSceneManager>().disableDelayedEncounterButton = true;
					}
					//If the Obtained Item Roll is within the FOURTH RANGE
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
						mapSceneManager.GetComponent<MapSceneManager>().disableMoveOnButton = false;
						mapSceneManager.GetComponent<MapSceneManager>().disableDelayedEncounterButton = true;
					}
					//If the Obtained Item Roll is OUT OF RANGE
					else {
						print("Rolled out of item ranges");
						if (myEncounterScriptable.obtainedItemList.outOfRangeMovesToEncounter) {
//TOMAYBEDOHERE Clear itemsInList and itemQuantitiesList
//TOPROBABLYDO Switch this to DelayEncounter instead
							DelayEncounterUpdate(myEncounterScriptable.obtainedItemList.outOfRangeEncounterIndex, true);
							mapSceneManager.GetComponent<MapSceneManager>().disableMoveOnButton = true;
							//mapSceneManager.GetComponent<MapSceneManager>().disableDelayedEncounterButton = false;

							//onlyMovesToDelayedEncounter = true;
						}

						if (myEncounterScriptable.obtainedItemList.outOfRangeRecoversMoneyPouch) {
							recoversPouch = true;
						}

						if (myEncounterScriptable.obtainedItemList.outOfRangeRecoversAllEquipment) {
							recoversAll = true;
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
					if (myEncounterScriptable.obtainedItemList.myItem1Quantity != 0) {
						itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem1Quantity);
					}
					if (myEncounterScriptable.obtainedItemList.myItem2Quantity != 0) {
						itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem2Quantity);
					}
					if (myEncounterScriptable.obtainedItemList.myItem3Quantity != 0) {
						itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem3Quantity);
					}
					if (myEncounterScriptable.obtainedItemList.myItem4Quantity != 0) {
						itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.myItem4Quantity);
					}


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
									mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().UseItem(item);
									itemUsed = true;
									print("Used the " + item.GetComponentInChildren<InventoryScriptableReader>().objectName);
								}

								itemQuantityTotal += item.GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
							}
							else {
								//print("Couldn't use the item. bool itemUsed = " + itemUsed + " || Encounter Uses Item = " + myEncounterScriptable.obtainedItemList.useItem);
							}
						}
					}

					//Check the quantity
					if (!itemFound && myEncounterScriptable.obtainedItemList.quantityCheckIncludesZero || itemFound && myEncounterScriptable.obtainedItemList.checksItemQuantityMax != 0) {
						if (itemQuantityTotal >= myEncounterScriptable.obtainedItemList.checksItemQuantityMin &&
							itemQuantityTotal <= myEncounterScriptable.obtainedItemList.checksItemQuantityMax) {
							//FINISH THIS
							itemsInList.Add(myEncounterScriptable.obtainedItemList.itemsGainedViaQuantity);
							itemQuantitiesList.Add(myEncounterScriptable.obtainedItemList.NumberOfItemsGained);

							print("Adding item to ObtainedItemList based on quantity");

							obtainedItems = true;
						}
					}

					if (itemFound) {
						if (myEncounterScriptable.obtainedItemList.hasItemMovesToEncounter != 0) {
							print("Item detected in inventory. Moving on");
							DelayEncounterUpdate(myEncounterScriptable.obtainedItemList.hasItemMovesToEncounter, false);
							//return;
						}
					}
					else {
						if (myEncounterScriptable.obtainedItemList.lacksItemMovesToEncounter != 0) {
							print("Item not detected in inventory. Moving on");
							DelayEncounterUpdate(myEncounterScriptable.obtainedItemList.lacksItemMovesToEncounter, false);
							//mapSceneManager.GetComponent<MapSceneManager>().disableDelayedEncounterButton = true;

							//return;
						}
					}
					
					//if (itemUsed) {
					//	//if (myEncounterScriptable.obtainedItemList.lackingItemMeansPoisoned) {
					//	//	print("You healed the poison");
					//	//	//DelayEncounterUpdate(469, false);
					//	//	//UpdateEncounter(469);
					//	//	//return;
					//	//}

					//	if (myEncounterScriptable.obtainedItemList.lackingItemMeansDiseased) {
					//		print("You immediately used a healing herb to cure disease");
					//		DelayEncounterUpdate(471, false);
					//	}
					//}
					//else {
					//	if (myEncounterScriptable.obtainedItemList.lackingItemMeansDiseased) {
					//		CharacterManager.statusDiseased = true;
					//		DelayEncounterUpdate(472, false);
					//	}

					//	////Unique rule for encounter 385
					//	//if (myEncounterScriptable.obtainedItemList.lackingItemMeansPoisoned) {
					//	//	print("Failed to heal your poisoning");
					//	//	//DelayEncounterUpdate(357, false);
					//	//	//UpdateEncounter(357);
					//	//	//return;
					//	//}
					//}
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
						itemQuantitiesList.Add(1);
						obtainedItems = true;
					}
				}

				if (recoversAll || recoversPouch || recoversDagger || recoversRandomWeapon) {
					if (recoversAll) {
						print("ALL EQUIPMENT should be recovered");
					}
					if (recoversPouch) {
						print("ALL MONEY should be recovered");
					}
					if (recoversDagger) {
						print("A DAGGER should be recovered");
					}
					if (recoversRandomWeapon) {
						print("A RANDOM WEAPON should be recovered");
					}

					mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().RecoverSavedInventory(recoversAll, recoversPouch, recoversDagger, recoversRandomWeapon);
					//itemListInventoryManager.GetComponent<MapSceneInventoryManager>().RecoverSavedInventory(recoversAll, recoversPouch, recoversDagger, recoversRandomWeapon);

					//itemListInventoryManager.GetComponent<MapSceneInventoryManager>().InitializeInventory();
				}

				earnedSilver += myEncounterScriptable.obtainedItemList.silverEarned;
				earnedCopper += myEncounterScriptable.obtainedItemList.copperEarned;

				if (obtainedItems || earnedSilver > 0 || earnedCopper > 0) {
					mapSceneManager.GetComponent<MapSceneManager>().UpdateItemListBG(itemsInList, itemQuantitiesList);
					mapSceneManager.GetComponent<MapSceneManager>().OpenItemListUI();
				}
			}

			if (myEncounterScriptable.mealScript != null) {
				foreach (GameObject itemParent in mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents) {
					if (itemParent.transform.childCount != 0) {
						if (itemParent.transform.GetChild(0).GetComponent<InventoryScriptableReader>().objectScript == myEncounterScriptable.mealScript) {
							itemParent.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity = (int)(itemParent.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity * myEncounterScriptable.loseMeals);
						}
					}
				}

				mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().LogInventory();
			}

			if (myEncounterScriptable.costsCopper != 0) {
//TODO What if the player doesn't have enough money?
				MerchantUIManager.SpendPlayerMoney(EncounterMerchantScriptable.coinTypes.copper, myEncounterScriptable.costsCopper);
				mapSceneInventoryManager.GetComponent<MapSceneInventoryManager>().LogInventory();
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
					print("Damage range roll: " + damageRangeRoll);

					if (damageRangeRoll <= 4) {
						mapSceneManager.GetComponent<MapSceneManager>().AlterDamage(6);
					}
					else if (damageRangeRoll >= 5 && damageRangeRoll <= 7) {
						mapSceneManager.GetComponent<MapSceneManager>().AlterDamage(3);
					}
				}
				else if (myEncounterScriptable.deals50PercentDamage) {
					CharacterManager.damageTaken = CharacterManager.enduranceTotal / 2;
					mapSceneManager.GetComponent<MapSceneManager>().UpdateHealthBar();
				}
				else {
					mapSceneManager.GetComponent<MapSceneManager>().AlterDamage(MapSceneManager.currentEncounter.myEncounterScriptable.damageAlteredAmount);
				}
			}

			if (MapSceneManager.currentEncounter.myEncounterScriptable.curesDisease) {
				CharacterManager.statusDiseased = false;
				CharacterManager.diseaseTimer = 0;
				mapSceneManager.GetComponent<MapSceneManager>().diseaseIcon.gameObject.SetActive(false);
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
					MapSceneManager.currentEncounter.UpdateEncounter(467);
					//DelayEncounterUpdate(467);
				}
				else {
					MapSceneManager.currentEncounter.DelayEncounterUpdate(122, false);
					//MapSceneManager.currentEncounter.UpdateEncounter(122);
				}
			}
			//Unique Encounter Rule on Encounter 304
			if (MapSceneManager.currentEncounter.myEncounterScriptable.checkIfMetGildor) {
				if (CharacterManager.metGildor) {
					MapSceneManager.currentEncounter.UpdateEncounter(468);
					//DelayEncounterUpdate(468);
				}
				else {
					MapSceneManager.currentEncounter.DelayEncounterUpdate(283, false);
					//MapSceneManager.currentEncounter.UpdateEncounter(283);
				}
			}

			if (MapSceneManager.currentEncounter.myEncounterScriptable.isTimeLocked) {
				if (CharacterManager.daysTaken <= MapSceneManager.currentEncounter.myEncounterScriptable.dayLimit2) {
					if (CharacterManager.daysTaken <= MapSceneManager.currentEncounter.myEncounterScriptable.dayLimit1) {
						if (CharacterManager.currentDayTimeTaken <= MapSceneManager.currentEncounter.myEncounterScriptable.timeLimit1) {
							DelayEncounterUpdate(MapSceneManager.currentEncounter.myEncounterScriptable.limit1Index, false);
						}
						else {
							DelayEncounterUpdate(MapSceneManager.currentEncounter.myEncounterScriptable.limit2Index, false);
						}
					}
					else if (CharacterManager.currentDayTimeTaken <= MapSceneManager.currentEncounter.myEncounterScriptable.timeLimit2) {
						DelayEncounterUpdate(MapSceneManager.currentEncounter.myEncounterScriptable.limit2Index, false);
					}
					else {
						DelayEncounterUpdate(MapSceneManager.currentEncounter.myEncounterScriptable.overTimeIndex, false);
					}
				}
				else {
					DelayEncounterUpdate(MapSceneManager.currentEncounter.myEncounterScriptable.overTimeIndex, false);
				}
			}

			if (MapSceneManager.currentEncounter.myEncounterScriptable.warnedSettlement != 0) {
				print("You warned settlement " + MapSceneManager.currentEncounter.myEncounterScriptable.warnedSettlement);

				switch (MapSceneManager.currentEncounter.myEncounterScriptable.warnedSettlement) {
					case 1: //Bridgefields
						CharacterManager.warnedBridgefields = true;
						CharacterManager.warnedBridgefieldsTime = CharacterManager.daysTaken + " days, " + CharacterManager.currentDayTimeTaken + " minutes";
						break;
					case 2: //Bywater
						CharacterManager.warnedBywater = true;
						CharacterManager.warnedBywaterTime = CharacterManager.daysTaken + " days, " + CharacterManager.currentDayTimeTaken + " minutes";
						break;
					case 3: //Frogmorton
						CharacterManager.warnedFrogmorton = true;
						CharacterManager.warnedFrogmortonTime = CharacterManager.daysTaken + " days, " + CharacterManager.currentDayTimeTaken + " minutes";
						break;
					case 4: //Hobbiton
						CharacterManager.warnedHobbiton = true;
						CharacterManager.warnedHobbitonTime = CharacterManager.daysTaken + " days, " + CharacterManager.currentDayTimeTaken + " minutes";
						break;
					case 5: //Marish
						CharacterManager.warnedMarish = true;
						CharacterManager.warnedMarishTime = CharacterManager.daysTaken + " days, " + CharacterManager.currentDayTimeTaken + " minutes";
						break;
					case 6: //Scary
						CharacterManager.warnedScary = true;
						CharacterManager.warnedScaryTime = CharacterManager.daysTaken + " days, " + CharacterManager.currentDayTimeTaken + " minutes";
						break;
					case 7: //Stock
						CharacterManager.warnedStock = true;
						CharacterManager.warnedStockTime = CharacterManager.daysTaken + " days, " + CharacterManager.currentDayTimeTaken + " minutes";
						break;
					case 8: //Tuckborough
						CharacterManager.warnedTuckborough = true;
						CharacterManager.warnedTuckboroughTime = CharacterManager.daysTaken + " days, " + CharacterManager.currentDayTimeTaken + " minutes";
							break;
					case 9: //Whitfurrows
						CharacterManager.warnedWhitfurrows = true;
						CharacterManager.warnedWhitfurrowsTime = CharacterManager.daysTaken + " days, " + CharacterManager.currentDayTimeTaken + " minutes";
						break;
					case 10: //Woodhall
						CharacterManager.warnedWoodhall = true;
						CharacterManager.warnedWoodhallTime = CharacterManager.daysTaken + " days, " + CharacterManager.currentDayTimeTaken + " minutes";
						break;
					
					default: Debug.Log("You warned a settlement, but which one?");
						break;
				}
			}


			if (MapSceneManager.currentEncounter.myEncounterScriptable.addsBonusToNextExplore != 0) {
				bonusToNextExplore = MapSceneManager.currentEncounter.myEncounterScriptable.addsBonusToNextExplore;
				print("Adding bonus to next explore roll: " + bonusToNextExplore);
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


	public void DelayEncounterUpdate(int nextEncounter, bool disableDelayedButton) {
		//MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter1Index = nextEncounter;
		//mapSceneManager.GetComponent<MapSceneManager>().delayedEncounterButton.GetComponent<Button>().onClick
		delayedEncounterIndex = nextEncounter;
		print("Delayed encounter: " + delayedEncounterIndex);
		mapSceneManager.GetComponent<MapSceneManager>().disableDelayedEncounterButton = disableDelayedButton;

	}


	public void GoToDelayedEncounter() {
		int delayedIndex = delayedEncounterIndex;
		delayedEncounterIndex = 0;
		//mapSceneManager.GetComponent<MapSceneManager>().disableDelayedEncounterButton = true;
		UpdateEncounter(delayedIndex);
	}


	void OpenMerchantUI () {
		merchantUI.GetComponent<Animator>().SetBool("SlideIn", true);

	}
	
}
