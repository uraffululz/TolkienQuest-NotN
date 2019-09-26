using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationSpellManager : MonoBehaviour {

	public Text[] characterSheetSpellTexts;

	public Button[] spellSlots;
	[SerializeField] GameObject lastSpellSlotClicked;

	[SerializeField] string chosenSpell;
	[SerializeField] SpellScriptable mySpellScriptable;

	int skillPointsAssignedToSpells;

	[SerializeField] GameObject spellChoiceBG;
	[SerializeField] Button[] spellChoiceSelections;
	[SerializeField] Text spellDescriptionText;


	public void IncreaseSpellSlots () {
		if (CharacterManager.skillPointsAvailable > 0) {
			skillPointsAssignedToSpells++;

			for (int i = 0; i < spellSlots.Length; i++) {
				if (i < skillPointsAssignedToSpells * 2) {
					spellSlots[i].interactable = true;
				}
				else {
					spellSlots[i].interactable = false;
				}
			}

			CharacterManager.skillPointsAvailable--;
			GetComponent<CreationSkillManager>().skillPointsRemainingText.text = ("Skill Points: " + CharacterManager.skillPointsAvailable);
		}
	}


	public void DecreaseSpellSlots () {
		if (CharacterManager.skillPointsAvailable < GetComponent<CreationSkillManager>().maxSkillPointsAvailable) {
			if (skillPointsAssignedToSpells > 0) {
				skillPointsAssignedToSpells--;

				for (int i = 0; i < spellSlots.Length; i++) {
					if (i < skillPointsAssignedToSpells * 2) {
						spellSlots[i].interactable = true;
					}
					else {
						spellSlots[i].interactable = false;
					}
				}

				CharacterManager.skillPointsAvailable++;
				GetComponent<CreationSkillManager>().skillPointsRemainingText.text = ("Skill Points: " + CharacterManager.skillPointsAvailable);
			}
		}
	}


	public void AlterSpellSlot (GameObject clickedButton) {
		//When an active spell slot is chosen, and opens the SpellChoiceBG
		lastSpellSlotClicked = clickedButton;

		/*TODO Maybe use the following commented lines to change the color of any buttons which match any spells already learned
		*TOMAYBEALSODO Modify any spells restricted by the character's Race to change color
		*I don't quite want to make them INACTIVE (so that the player can still read the spell's description),
		*but I do still want them to be unable to "confirm", which will take a bit of code in the ConfirmChosenSpell() method
		*/

		foreach (Button spellChoiceButton in spellChoiceSelections) {
			if (CharacterManager.myRace == CharacterManager.Races.Hobbit && spellChoiceButton.GetComponent<ScriptableSpellReader>().noHobbits) {
				spellChoiceButton.interactable = false;
			}
			else if (CharacterManager.myRace == CharacterManager.Races.Dwarf && spellChoiceButton.GetComponent<ScriptableSpellReader>().noDwarves) {
				spellChoiceButton.interactable = false;
			}
		}

		spellDescriptionText.text = "";
	}


	public void SpellChosen (GameObject spellClicked) {
	//	//During character creation, when a "Spell Button" from the SpellChoiceBG is clicked
		chosenSpell = spellClicked.GetComponentInChildren<Text>().text;
		mySpellScriptable = spellClicked.GetComponent<ScriptableSpellReader>().mySpellScriptable;

		DisplaySpellDescription(spellClicked);
	}


	public void ConfirmChosenSpell () {
		bool alreadyKnowsSpell = false;

		foreach (Button spellChoice in spellChoiceSelections) {
			spellChoice.interactable = true;
		}

		foreach (Button slot in spellSlots) {
			if (slot.GetComponentInChildren<Text>().text == chosenSpell) {
				alreadyKnowsSpell = true;
			}
		}

		//Debug.Log(alreadyKnowsSpell);

		if (!alreadyKnowsSpell) {
			ScriptableSpellReader spellReader = lastSpellSlotClicked.GetComponent<ScriptableSpellReader>();
			spellReader.mySpellScriptable = mySpellScriptable;
			spellReader.InitializeSpell();
			lastSpellSlotClicked.GetComponentInChildren<Text>().text = spellReader.spellName;

			GetComponent<CreationSceneManager>().CloseNewUIWindow(spellChoiceBG);
		}
		else {
			if (alreadyKnowsSpell) {
				GetComponent<CreationSceneManager>().CloseNewUIWindow(spellChoiceBG);
			}
		}
	}


	public void ConfirmActiveSpellSlots() {
//TODO Before changing the text of the character sheet's Spell Slots, make sure that all skill points are assigned
//and all Spell Slots are assigned a spell
//TOMAYBEDO, when confirming the Spell Slots, organize them alphabetically OR by index (spell number)
		CharacterManager.allSpellsSet = true;

		for (int ss = 0; ss < spellSlots.Length; ss++) {
			if (spellSlots[ss].interactable) {
				//Sync up each "Spell Text" on the character sheet with the relevant assigned "Spell Slot" on the SkillEditorBG
				characterSheetSpellTexts[ss].text = spellSlots[ss].GetComponent<ScriptableSpellReader>().spellName;
				CharacterManager.assignedSpells[ss] = spellSlots[ss].GetComponent<ScriptableSpellReader>().spellName;

				//Make sure that a spell is assigned to the slot. Otherwise, do not allow the player to close the SkillEditorBG
				if (spellSlots[ss].GetComponent<ScriptableSpellReader>().thisSpell == ScriptableSpellReader.whichSpell.nothing ||
					spellSlots[ss].GetComponentInChildren<Text>().text == "") {
					CharacterManager.allSpellsSet = false;
					characterSheetSpellTexts[ss].text = "";
					CharacterManager.assignedSpells[ss] = "";
				}
			}
			else {
				//characterSheetSpellTexts[ss].text = spellSlots[ss].GetComponentInChildren<Text>().text;
				characterSheetSpellTexts[ss].text = "";
				CharacterManager.assignedSpells[ss] = "";
			}

			//Debug.Log(CharacterManager.assignedSpells[ss]);
		}

		GetComponent<CreationSkillManager>().ConfirmSkillValues();
	}


	void DisplaySpellDescription (GameObject spellButton) {
		ScriptableSpellReader mySpellScriptableReader = spellButton.GetComponent<ScriptableSpellReader>();
		
		spellDescriptionText.text = mySpellScriptableReader.spellName + "\n \n" +
					"Player Damage: " + mySpellScriptableReader.playerDamage + "\n \n" +
					"Spell Description: \n" + mySpellScriptableReader.spellDescriptionFromBook;                                                                                            
	}
}