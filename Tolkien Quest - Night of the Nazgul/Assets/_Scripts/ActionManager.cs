using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour {

	[SerializeField] int actionGoToEncounter;
	[SerializeField] bool actionMoveOnRandomly;

	[Space]

	[SerializeField] MapSceneManager mapSceneManager;
	[SerializeField] MapSceneCharacterSheetManager mSCSM;
	[SerializeField] CombatManager combatManager;
	[SerializeField] ItemListBGScript itemListScript;

	[Space]

	[SerializeField] GameObject runAwayButton;
	[SerializeField] GameObject runPastButton;
	[SerializeField] GameObject sneakAttackButton;
	[SerializeField] GameObject sneakAwayButton;
	[SerializeField] GameObject sneakPastButton;
	[SerializeField] GameObject stealTakeButton;

	[SerializeField] GameObject encounterSpellUI;
	[SerializeField] GameObject calmButton;
	[SerializeField] GameObject telekinesisButton;

	[Space]

	[SerializeField] GameObject actionResultUI;
	[SerializeField] Text actionResultText;
	[SerializeField] GameObject actionResultProceedButton;
	[SerializeField] GameObject actionResultMoveOnButton;

	[SerializeField] Text overallActionText;
	
	

//Sneak Away, Sneak Past, Sneak Attack, & Steal & Take are ONLY AVAILABLE WHEN THE TEXT INDICATES THE ENEMY IS UNAWARE OF THE PLAYER
//Run Past and Sneak Past are ONLY AVAILABLE WHEN PLAYING ADVANCED RULES

	public void InitializeActionBG() {
		bool playerHasRelevantSpells = false;

		runAwayButton.SetActive(false);
		runPastButton.SetActive(false);
		sneakAttackButton.SetActive(false);
		sneakAwayButton.SetActive(false);
		sneakPastButton.SetActive(false);
		stealTakeButton.SetActive(false);

		actionResultProceedButton.SetActive(false);
		actionResultMoveOnButton.SetActive(false);

		actionMoveOnRandomly = false;

		//if (MapSceneManager.currentEncounter.myEncounterScriptable.canRunAwayPast) {
			runAwayButton.SetActive(true);
			runPastButton.SetActive(true);
		//}

		if (MapSceneManager.currentEncounter.myEncounterScriptable.enemyUnaware) {
			sneakAttackButton.SetActive(true);
			sneakAwayButton.SetActive(true);
			sneakPastButton.SetActive(true);
			stealTakeButton.SetActive(true);

			for (int s = 0; s < CharacterManager.spellScriptables.Length; s++) {
				if (CharacterManager.spellScriptables[s] != null) {
					if (CharacterManager.spellScriptables[s].spellIndex == 14) {//If the player knows the Telekinesis spell
						playerHasRelevantSpells = true;
						//encounterSpellUI.SetActive(true);
						telekinesisButton.SetActive(true);
					}
				}
			}
		}
		else {
			//If the enemy is aware of the player
			if (MapSceneManager.currentEncounter.myEncounterScriptable.enemyAwareAndHostile) {
				for (int s = 0; s < CharacterManager.spellScriptables.Length; s++) {
					if (CharacterManager.spellScriptables[s] != null) {
						if (CharacterManager.spellScriptables[s].spellIndex == 2) { //If the player knows the Calm spell
							playerHasRelevantSpells = true;
							CombatScriptable fightEncounterScript = mapSceneManager.gameObject.GetComponent<EncounterManager>().encounterTextScriptables[MapSceneManager.currentEncounter.myEncounterScriptable.fightEncounter].combatScript;
							if (fightEncounterScript != null) {
								if (fightEncounterScript.enemyCount == 1) {
									//encounterSpellUI.SetActive(true);
									calmButton.SetActive(true);
								}
								else {
									//encounterSpellUI.SetActive(false);
									calmButton.SetActive(false);

									Debug.Log("\nYou can't cast your Calm spell!\nThere are too many enemies!");
								}
							}
							else {
								//encounterSpellUI.SetActive(false);
								calmButton.SetActive(false);

								Debug.LogError("Couldn't find a fight script for casting Calm!", this.gameObject);
								Debug.Break();
							}
						}
					}
				}
			}
		}

		if (playerHasRelevantSpells) {
			encounterSpellUI.SetActive(true);
		}
		else {
			//KEEP
			//encounterSpellUI.SetActive(false);
		}
	}


	public void AttemptRunAwayAction() {
		overallActionText.text += "\nYou attempted to Run Away from the enemy...";

		//Pick a number and add Running bonus
		int runAwayRoll = Random.Range(2, 13) + CharacterManager.mySkillRunningTotal;

		if (CharacterManager.speedSpellActive) {
			runAwayRoll += 2;
			CharacterManager.speedSpellActive = false;
			CharacterManager.mySkillRunningSpecialBonuses -= 2;
			mSCSM.UpdateRunningSkillTexts();
		}

		if (runAwayRoll > 7) {
			//Follow the text instructions or Move On
			if (MapSceneManager.currentEncounter.myEncounterScriptable.runAwayPastEncounter != 0) {
				actionGoToEncounter = MapSceneManager.currentEncounter.myEncounterScriptable.runAwayPastEncounter;
				actionResultText.text = "You successfully ran away!";
				overallActionText.text += "\nYou successfully ran away!";

				actionResultProceedButton.SetActive(true);
			}
			else {
				//ADVANCED RULE: Move On Randomly instead
					//mapSceneManager.moveOnInRandomDirection = true;

				actionResultMoveOnButton.SetActive(true);
			}
		}
		else {
			//Otherwise, you must FIGHT and you are SURPRISED
			actionGoToEncounter = MapSceneManager.currentEncounter.myEncounterScriptable.fightEncounter;
			actionResultText.text = "You failed to run away!\nYou alerted the enemy!";
			overallActionText.text += "\nYou failed to run away!\nYou alerted the enemy!";

			actionResultProceedButton.SetActive(true);

			CombatManager.playerSurprised = true;
		}

		actionResultUI.SetActive(true);
		mapSceneManager.CloseEncounterUI();
	}


	public void AttemptRunPast() {
		overallActionText.text += "\nYou attempted to Run Past the enemy...";

		//ADVANCED RULES ONLY
		//Pick a number, add Running bonus, and subtract 2
		int runPastRoll = Random.Range(2, 13) + CharacterManager.mySkillRunningTotal - 2;

		if (CharacterManager.speedSpellActive) {
			runPastRoll += 2;
			CharacterManager.speedSpellActive = false;
			CharacterManager.mySkillRunningSpecialBonuses -= 2;
			mSCSM.UpdateRunningSkillTexts();
		}

		if (runPastRoll > 7) {
			//Follow the text instructions or Move On
			if (MapSceneManager.currentEncounter.myEncounterScriptable.runAwayPastEncounter != 0) {
				actionGoToEncounter = MapSceneManager.currentEncounter.myEncounterScriptable.runAwayPastEncounter;
				actionResultText.text = "You successfully ran past the enemy!";
				overallActionText.text += "\nYou successfully ran past the enemy!";

				actionResultProceedButton.SetActive(true);
			}
			else {
				actionResultMoveOnButton.SetActive(true);
			}
		}
		else {
			//Otherwise, you must FIGHT and you are SURPRISED
			actionGoToEncounter = MapSceneManager.currentEncounter.myEncounterScriptable.fightEncounter;
			actionResultText.text = "You failed to run past!\nYou alerted the enemy!";
			overallActionText.text += "\nYou failed to run past!\nYou alerted the enemy!";

			actionResultProceedButton.SetActive(true);

			CombatManager.playerSurprised = true;
		}

		actionResultUI.SetActive(true);
		mapSceneManager.CloseEncounterUI();
	}


	public void AttemptSneakAttack() {
		overallActionText.text += "\nYou attempted to Sneak Attack the enemy...";
		//You must fight
		actionGoToEncounter = MapSceneManager.currentEncounter.myEncounterScriptable.fightEncounter;

		//Pick a number and add Trickery
		int sneakAttackRoll = Random.Range(2, 13) + CharacterManager.mySkillTrickeryTotal;

		if (CharacterManager.camoSpellActive) {
			overallActionText.text += "\nYour Camouflage spell adds +2 to your attempt!";
			CharacterManager.mySkillTrickerySpecialBonuses -= 2;
			CharacterManager.camoSpellActive = false;
			
			mSCSM.UpdateTrickerySkillTexts();
		}

		//If the result is more than 7, you are successful
		if (sneakAttackRoll > 7) {
			CombatManager.sneakAttackSuccessful = true;
			actionResultText.text = "Your sneak attack was successful!";
			overallActionText.text += "\nYour sneak attack was successful!";
		}
		else {
			actionResultText.text = "You failed to sneak attack the enemy!";
			overallActionText.text += "\nYou failed to sneak attack the enemy!";
		}

		actionResultProceedButton.SetActive(true);

		actionResultUI.SetActive(true);
		mapSceneManager.CloseEncounterUI();
	}


	public void AttemptSneakAway() {
		overallActionText.text += "\nYou attempted to Sneak Away from the enemy...";
		//Pick a number and add your Trickery
		int sneakAwayRoll = Random.Range(2, 13) + CharacterManager.mySkillTrickeryTotal;

		if (CharacterManager.camoSpellActive) {
			overallActionText.text += "\nYour Camouflage spell adds +2 to your attempt!";
			CharacterManager.mySkillTrickerySpecialBonuses -= 2;
			CharacterManager.camoSpellActive = false;

			mSCSM.UpdateTrickerySkillTexts();
		}
		
		//If the result is more than 7, MOVE ON
		if (sneakAwayRoll > 7) {
			//ADVANCED RULE: Move On Randomly instead
				//mapSceneManager.moveOnInRandomDirection = true;

			actionResultText.text = "You successfully sneaked away!";
			overallActionText.text += "\nYou successfully sneaked away!";
			actionResultMoveOnButton.SetActive(true);
		}
		else {
			//Otherwise, you must fight
			actionGoToEncounter = MapSceneManager.currentEncounter.myEncounterScriptable.fightEncounter;
			actionResultText.text = "You failed to sneak away!\nYou must fight!";
			overallActionText.text += "\nYou failed to sneak away!\nYou must fight!";
			actionResultProceedButton.SetActive(true);
		}

		actionResultUI.SetActive(true);
		mapSceneManager.CloseEncounterUI();
	}


	public void AttemptSneakPast() {
		overallActionText.text += "\nYou attempted to Sneak Past the enemy...";

		//ADVANCED RULES ONLY
		//Pick a number, add Trickery bonus, and subtract 2
		int sneakPastRoll = Random.Range(2, 13) + CharacterManager.mySkillTrickeryTotal - 2;

		if (CharacterManager.camoSpellActive) {
			overallActionText.text += "\nYour Camouflage spell adds +2 to your attempt!";
			CharacterManager.mySkillTrickerySpecialBonuses -= 2;
			CharacterManager.camoSpellActive = false;

			mSCSM.UpdateTrickerySkillTexts();
		}

		//If the result is more than 7, MOVE ON
		if (sneakPastRoll > 7) {
			actionResultText.text = "You successfully sneaked past the enemy!";
			overallActionText.text += "\nYou successfully sneaked past the enemy!";
			actionResultMoveOnButton.SetActive(true);
		}
		else {
			//Otherwise, you must fight
			actionGoToEncounter = MapSceneManager.currentEncounter.myEncounterScriptable.fightEncounter;
			actionResultText.text = "You failed to sneak past the enemy!\nYou must fight!";
			overallActionText.text += "\nYou failed to sneak past the enemy!\nYou must fight!";
			actionResultProceedButton.SetActive(true);
		}

		actionResultUI.SetActive(true);
		mapSceneManager.CloseEncounterUI();
	}


	public void AttemptStealAndTake() {
		overallActionText.text += "\nYou attempted to Steal from the enemy...";

		//Pick a number and add Trickery
		int stealRoll = Random.Range(2, 13) + CharacterManager.mySkillTrickeryTotal;

		if (CharacterManager.camoSpellActive) {
			overallActionText.text += "\nYour Camouflage spell adds +2 to your attempt!";
			CharacterManager.mySkillTrickerySpecialBonuses -= 2;
			CharacterManager.camoSpellActive = false;

			mSCSM.UpdateTrickerySkillTexts();
		}

		if (stealRoll > 8) {
			//If the result is more than 8, you have "outsmarted" your opponent
			//Read the text indicated (?), only taking ONE ITEM
			//The enemy is still alive
			itemListScript.canOnlyTakeOneItem = true;
			actionGoToEncounter = MapSceneManager.currentEncounter.myEncounterScriptable.skipFightToEncounter;
			actionResultText.text = "You succeeded in stealing from the enemy!\nYou may take ONE of the following items.\nIgnore the part of the following text that says you defeated the enemy. It is still very much alive, should you choose to return.";
			overallActionText.text += "\nYou succeeded in stealing from the enemy!";

			if (!mapSceneManager.gameObject.GetComponent<EncounterManager>().encounterTextScriptables[MapSceneManager.currentEncounter.myEncounterScriptable.skipFightToEncounter].obtainsItems) {
				itemListScript.canOnlyTakeOneItem = false;
			}
		}
		else {
			//If the result is 8 or less, you must fight the enemy and you are surprised
			actionGoToEncounter = MapSceneManager.currentEncounter.myEncounterScriptable.fightEncounter;
			actionResultText.text = "You were spotted stealing from the enemy!\nYou must fight!";
			overallActionText.text += "\nYou were spotted stealing from the enemy!\nYou must fight!";

			CombatManager.playerSurprised = true;
		}

		actionResultProceedButton.SetActive(true);

		actionResultUI.SetActive(true);
		mapSceneManager.CloseEncounterUI();
	}


	public void ConfirmActionGoToEncounter () {
		MapSceneManager.currentEncounter.UpdateEncounter(actionGoToEncounter);

		DisableAllActionUI();
		actionGoToEncounter = 0;
		actionMoveOnRandomly = false;
	}


	public void ConfirmActionMoveOn() {
		if (actionMoveOnRandomly) {
			mapSceneManager.moveOnInRandomDirection = true;
		}

		mapSceneManager.MoveOn(2);

		DisableAllActionUI();
		actionGoToEncounter = 0;
		actionMoveOnRandomly = false;
	}


	void DisableAllActionUI() {
		actionResultUI.SetActive(false);
		actionResultProceedButton.SetActive(false);
		actionResultMoveOnButton.SetActive(false);

		encounterSpellUI.SetActive(false);
		telekinesisButton.SetActive(false);

		mapSceneManager.DisableActionBG();
	}
}
