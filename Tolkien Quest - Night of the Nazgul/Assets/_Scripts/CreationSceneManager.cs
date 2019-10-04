using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreationSceneManager : MonoBehaviour {

	[SerializeField] Button setNameButton;
	[SerializeField] Button setStatsButton;
	[SerializeField] Button setRaceButton;
	[SerializeField] Button setSkillsButton;


	[SerializeField] Button continueButton;

	[SerializeField] Text MeleeOBSkillTotalText;
	[SerializeField] Text MissileOBSkillTotalText;
	[SerializeField] Text DBSkillTotalText;
	[SerializeField] Text RunningSkillTotalText;
	[SerializeField] Text GeneralSkillTotalText;
	[SerializeField] Text TrickerySkillTotalText;
	[SerializeField] Text PerceptionSkillTotalText;
	[SerializeField] Text MagicalSkillTotalText;


	void Start () {
        
    }


    void Update () {
        
    }


	public void OpenNewUIWindow (GameObject newUIWindow) {
		//Animate the relevant UI window to slide OPEN
		newUIWindow.GetComponent<Animator>().SetBool("slideTopUIOpen", true);

		setNameButton.interactable = false;
		setStatsButton.interactable = false;
		setRaceButton.interactable = false;
		setSkillsButton.interactable = false;

		continueButton.interactable = false;
	}


	public void CloseNewUIWindow (GameObject newUIWindow) {
		//Animate the relevant UI window to slide CLOSED
		newUIWindow.GetComponent<Animator>().SetBool("slideTopUIOpen", false);

		setNameButton.interactable = true;
		setStatsButton.interactable = true;
		setRaceButton.interactable = true;
		setSkillsButton.interactable = true;
	}


	public void TotalAllSkills () {                                                                                                                                        
		if (CharacterManager.raceSet && CharacterManager.allStatsSet && CharacterManager.allSkillsSet) {
			CharacterManager.mySkillMeleeOBTotal = CharacterManager.mySkillMeleeOBStatBonus + CharacterManager.mySkillMeleeOBSkillBonus + CharacterManager.mySkillMeleeOBSpecialBonuses;
			CharacterManager.mySkillMissileOBTotal = CharacterManager.mySkillMissileOBStatBonus + CharacterManager.mySkillMissileOBSkillBonus + CharacterManager.mySkillMissileOBSpecialBonuses;
			CharacterManager.mySkillDBTotal = CharacterManager.myStatAgilityBonus + CharacterManager.mySkillDBSpecialBonuses;
			CharacterManager.mySkillRunningTotal = CharacterManager.myStatAgilityBonus + CharacterManager.mySkillRunningSpecialBonuses;
			CharacterManager.mySkillGeneralTotal = CharacterManager.mySkillGeneralStatBonus + CharacterManager.mySkillGeneralSkillBonus + CharacterManager.mySkillGeneralSpecialBonuses;
			CharacterManager.mySkillTrickeryTotal = CharacterManager.mySkillTrickeryStatBonus + CharacterManager.mySkillTrickerySkillBonus + CharacterManager.mySkillTrickerySpecialBonuses;
			CharacterManager.mySkillPerceptionTotal = CharacterManager.mySkillPerceptionStatBonus + CharacterManager.mySkillPerceptionSkillBonus + CharacterManager.mySkillPerceptionSpecialBonuses;
			CharacterManager.mySkillMagicalTotal = CharacterManager.mySkillMagicalStatBonus + CharacterManager.mySkillMagicalSkillBonus + CharacterManager.mySkillMagicalSpecialBonuses;

			//Debug.Log("MeleeOB stat bonus = " + CharacterManager.mySkillMeleeOBStatBonus + " | Skill Bonus = " + +CharacterManager.mySkillMeleeOBSkillBonus);

			MeleeOBSkillTotalText.text = CharacterManager.mySkillMeleeOBTotal.ToString();
			MissileOBSkillTotalText.text = CharacterManager.mySkillMissileOBTotal.ToString();
			DBSkillTotalText.text = CharacterManager.mySkillDBTotal.ToString();
			RunningSkillTotalText.text = CharacterManager.mySkillRunningTotal.ToString();
			GeneralSkillTotalText.text = CharacterManager.mySkillGeneralTotal.ToString();
			TrickerySkillTotalText.text = CharacterManager.mySkillTrickeryTotal.ToString();
			PerceptionSkillTotalText.text = CharacterManager.mySkillPerceptionTotal.ToString();
			MagicalSkillTotalText.text = CharacterManager.mySkillMagicalTotal.ToString();

			//Determine if the character sheet is complete, and the player is allowed to continue to the game
			AllowContinue();
		}
	}


	public void AllowContinue () {
		if (CharacterManager.nameSet && CharacterManager.raceSet &&
			CharacterManager.allStatsSet && CharacterManager.allSkillsSet && CharacterManager.allSpellsSet) {
			//If the player has assigned all of their character's attributes, allow them to continue to the main game

			continueButton.interactable = true;
		}
		else {
			continueButton.interactable = false;
		}
	}


	public void ConfirmCharacterComplete () {
		//When the playerr clicks the "Continue" button to move on to the main game
		Debug.Log("Character creation complete. Continuing to the game's Prologue");

		SceneManager.LoadScene("PrologueScene");
	}

}
