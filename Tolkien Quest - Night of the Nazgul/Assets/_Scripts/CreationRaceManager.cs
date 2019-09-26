using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationRaceManager : MonoBehaviour {

	[SerializeField] GameObject raceChoiceBG;

	ScriptableRaceReader myRaceScriptableReader;

	string selectedRace;

	[SerializeField] Text charSheetRaceText;
	[SerializeField] Text raceDescriptionText;

	[SerializeField] Text meleeOBSpecialBonusText;
	[SerializeField] Text missileOBSpecialBonusText;
	[SerializeField] Text DBSpecialBonusText;
	[SerializeField] Text runningSpecialBonusText;
	[SerializeField] Text generalSpecialBonusText;
	[SerializeField] Text trickerySpecialBonusText;
	[SerializeField] Text perceptionSpecialBonusText;
	[SerializeField] Text magicalSpecialBonusText;


	public void RaceChosen (GameObject raceClicked) {
		//Apply the chosen race's attributes to the character
		selectedRace = raceClicked.GetComponentInChildren<Text>().text;
		myRaceScriptableReader = raceClicked.GetComponent<ScriptableRaceReader>();

		//Display the current chosen race's description (from the book)
		DisplayRaceDescription(raceClicked);
	}


	public void ConfirmChosenRace () {
		//Apply the currently chosen race's attributes/skill bonuses to the CharacterManager script
		charSheetRaceText.text = "Race: " + selectedRace;

		CreationSpellManager mySpellManager = GetComponent<CreationSpellManager>();

		switch (selectedRace) {
			case "Default":
				CharacterManager.myRace = CharacterManager.Races.Default;
				break;
			case "Dwarf":
				CharacterManager.myRace = CharacterManager.Races.Dwarf;
				for (int slotIndex = 0; slotIndex < mySpellManager.spellSlots.Length; slotIndex++) {
					Button spellSlot = mySpellManager.spellSlots[slotIndex];
					if (spellSlot.GetComponent<ScriptableSpellReader>().noDwarves) {
						spellSlot.GetComponent<ScriptableSpellReader>().ClearSpellSlotScriptable();
						
						spellSlot.GetComponentInChildren<Text>().text = "";

						mySpellManager.characterSheetSpellTexts[slotIndex].text = "";

						CharacterManager.assignedSpells[slotIndex] = "";

						CharacterManager.allSpellsSet = false;
					}
				}
				break;
			case "Elf":
				CharacterManager.myRace = CharacterManager.Races.Elf;
				break;
			case "Hobbit":
				CharacterManager.myRace = CharacterManager.Races.Hobbit;

				//foreach (Button spellSlot in mySpellManager.spellSlots) {
				//	spellSlot.GetComponentInChildren<Text>().text = "";
				//	CharacterManager.allSpellsSet = false;
				//}
				//foreach (Text spellText in mySpellManager.characterSheetSpellTexts) {
				//	spellText.text = "";
				//}

				for (int slotIndex = 0; slotIndex < mySpellManager.spellSlots.Length; slotIndex++) {
					Button spellSlot = mySpellManager.spellSlots[slotIndex];
					if (spellSlot.GetComponent<ScriptableSpellReader>().noHobbits) {
						spellSlot.GetComponent<ScriptableSpellReader>().ClearSpellSlotScriptable();
						
						spellSlot.GetComponentInChildren<Text>().text = "";

						mySpellManager.characterSheetSpellTexts[slotIndex].text = "";

						CharacterManager.assignedSpells[slotIndex] = "";

						CharacterManager.allSpellsSet = false;
					}
				}
					break;
			case "Human":
				CharacterManager.myRace = CharacterManager.Races.Human;
				break;
			default:
				CharacterManager.myRace = CharacterManager.Races.Default;
				break;
		}

		//Debug.Log(CharacterManager.myRace);

		//Apply the character's "persistent racial SPECIAL skill bonuses"
		CharacterManager.mySkillMeleeOBSpecialBonuses = myRaceScriptableReader.myMeleeOBAdjust;
		CharacterManager.mySkillRunningSpecialBonuses = myRaceScriptableReader.myRunningAdjust;
		CharacterManager.mySkillGeneralSpecialBonuses = myRaceScriptableReader.myGeneralAdjust;
		CharacterManager.mySkillTrickerySpecialBonuses = myRaceScriptableReader.myTrickeryAdjust;

		//Display the character's "persistent racial SPECIAL skill bonuses"
		meleeOBSpecialBonusText.text = CharacterManager.mySkillMeleeOBSpecialBonuses.ToString();
		missileOBSpecialBonusText.text = CharacterManager.mySkillMissileOBSpecialBonuses.ToString();
		DBSpecialBonusText.text = CharacterManager.mySkillDBSpecialBonuses.ToString();
		runningSpecialBonusText.text = CharacterManager.mySkillRunningSpecialBonuses.ToString();
		generalSpecialBonusText.text = CharacterManager.mySkillGeneralSpecialBonuses.ToString();
		trickerySpecialBonusText.text = CharacterManager.mySkillTrickerySpecialBonuses.ToString();
		perceptionSpecialBonusText.text = CharacterManager.mySkillPerceptionSpecialBonuses.ToString();
		magicalSpecialBonusText.text = CharacterManager.mySkillMagicalSpecialBonuses.ToString();

		CharacterManager.raceSet = true;

		GetComponent<CreationSkillManager>().ConfirmSkillValues();
		GetComponent<CreationSceneManager>().CloseNewUIWindow(raceChoiceBG);
	}


	public void DisplayRaceDescription (GameObject raceButton) {
		raceDescriptionText.text = myRaceScriptableReader.raceName + "\n \n" +
			myRaceScriptableReader.raceDescriptionFromBook;
	}
}
