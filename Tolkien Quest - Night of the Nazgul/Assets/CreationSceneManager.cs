using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationSceneManager : MonoBehaviour {

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
	}


	public void CloseNewUIWindow (GameObject newUIWindow) {
		//Animate the relevant UI window to slide CLOSED
		newUIWindow.GetComponent<Animator>().SetBool("slideTopUIOpen", false);
	}


	public void TotalAllSkills () {
		if (CharacterManager.allStatsSet && CharacterManager.allSkillsSet) {
			CharacterManager.mySkillMeleeOBTotal = CharacterManager.mySkillMeleeOBStatBonus + CharacterManager.mySkillMeleeOBSkillBonus;
			CharacterManager.mySkillMissileOBTotal = CharacterManager.mySkillMissileOBStatBonus + CharacterManager.mySkillMissileOBSkillBonus;
			CharacterManager.mySkillDBTotal = CharacterManager.myStatAgilityBonus;
			CharacterManager.mySkillRunningTotal = CharacterManager.myStatAgilityBonus;
			CharacterManager.mySkillGeneralTotal = CharacterManager.mySkillGeneralStatBonus + CharacterManager.mySkillGeneralSkillBonus;
			CharacterManager.mySkillTrickeryTotal = CharacterManager.mySkillTrickeryStatBonus + CharacterManager.mySkillTrickerySkillBonus;
			CharacterManager.mySkillPerceptionTotal = CharacterManager.mySkillPerceptionStatBonus + CharacterManager.mySkillPerceptionSkillBonus;
			CharacterManager.mySkillMagicalTotal = CharacterManager.mySkillMagicalStatBonus + CharacterManager.mySkillMagicalSkillBonus;

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
		if (CharacterManager.nameSet && CharacterManager.allStatsSet && CharacterManager.allSkillsSet) {
//TODO Are there any other parameters to determine if the player is allowed to continue? All spell slots assigned, etc.?
			continueButton.interactable = true;
		}
	}


	public void ConfirmCharacterComplete () {
		Debug.Log("Character creation complete. Continuing to the game's Prologue");
		
	}

}
