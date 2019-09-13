using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationSkillManager : MonoBehaviour {

	[SerializeField] GameObject skillsEditorBG;

	public int maxSkillPointsAvailable = 6;

	public Text skillPointsRemainingText;

	//Text Variables on the Skill Editor BG
	[SerializeField] Text MeleeOBSkillValueText;
	[SerializeField] Text MissileOBSkillValueText;
	[SerializeField] Text GeneralSkillValueText;
	[SerializeField] Text TrickerySkillValueText;
	[SerializeField] Text PerceptionSkillValueText;
	[SerializeField] Text MagicalSkillValueText;

	//Text Variables on the Character Sheet
	[SerializeField] Text MeleeOBSkillBonusText;
	[SerializeField] Text MissileOBSkillBonusText;
	[SerializeField] Text GeneralSkillBonusText;
	[SerializeField] Text TrickerySkillBonusText;
	[SerializeField] Text PerceptionSkillBonusText;
	[SerializeField] Text MagicalSkillBonusText;


	void Start() {
		SetupInitialSkillValues();
		CharacterManager.skillPointsAvailable = maxSkillPointsAvailable;
    }


	void SetupInitialSkillValues() {
		//Set the character's "skill bonuses" to their base values
		CharacterManager.mySkillMeleeOBSkillBonus = -2;
		CharacterManager.mySkillMissileOBSkillBonus = -2;
		CharacterManager.mySkillGeneralSkillBonus = -2;
		CharacterManager.mySkillTrickerySkillBonus = -2;
		CharacterManager.mySkillPerceptionSkillBonus = -2;
		CharacterManager.mySkillMagicalSkillBonus = -2;
	}


	public void IncreaseOrDecreaseSkill(string whichSkill) {
		switch (whichSkill) {
			case "MeleeOB+":
				//Debug.Log("Increasing MeleeOB skill");
				CharacterManager.mySkillMeleeOBSkillBonus = IncreasedSkillValue(CharacterManager.mySkillMeleeOBSkillBonus);
				MeleeOBSkillValueText.text = CharacterManager.mySkillMeleeOBSkillBonus.ToString();
				MeleeOBSkillBonusText.text = CharacterManager.mySkillMeleeOBSkillBonus.ToString();
				break;

			case "MeleeOB-":
				//Debug.Log("Decreasing MeleeOB skill");
				CharacterManager.mySkillMeleeOBSkillBonus = DecreasedSkillValue(CharacterManager.mySkillMeleeOBSkillBonus);
				MeleeOBSkillValueText.text = CharacterManager.mySkillMeleeOBSkillBonus.ToString();
				MeleeOBSkillBonusText.text = CharacterManager.mySkillMeleeOBSkillBonus.ToString();
				break;

			case "MissileOB+":
				CharacterManager.mySkillMissileOBSkillBonus = IncreasedSkillValue(CharacterManager.mySkillMissileOBSkillBonus);
				MissileOBSkillValueText.text = CharacterManager.mySkillMissileOBSkillBonus.ToString();
				MissileOBSkillBonusText.text = CharacterManager.mySkillMissileOBSkillBonus.ToString();
				break;

			case "MissileOB-":
				//Debug.Log("Decreasing MissileOB skill");
				CharacterManager.mySkillMissileOBSkillBonus = DecreasedSkillValue(CharacterManager.mySkillMissileOBSkillBonus);
				MissileOBSkillValueText.text = CharacterManager.mySkillMissileOBSkillBonus.ToString();
				MissileOBSkillBonusText.text = CharacterManager.mySkillMissileOBSkillBonus.ToString();
				break;

			case "General+":
				CharacterManager.mySkillGeneralSkillBonus = IncreasedSkillValue(CharacterManager.mySkillGeneralSkillBonus);
				GeneralSkillValueText.text = CharacterManager.mySkillGeneralSkillBonus.ToString();
				GeneralSkillBonusText.text = CharacterManager.mySkillGeneralSkillBonus.ToString();
				break;

			case "General-":
				CharacterManager.mySkillGeneralSkillBonus = DecreasedSkillValue(CharacterManager.mySkillGeneralSkillBonus);
				GeneralSkillValueText.text = CharacterManager.mySkillGeneralSkillBonus.ToString();
				GeneralSkillBonusText.text = CharacterManager.mySkillGeneralSkillBonus.ToString();
				break;

			case "Trickery+":
				CharacterManager.mySkillTrickerySkillBonus = IncreasedSkillValue(CharacterManager.mySkillTrickerySkillBonus);
				TrickerySkillValueText.text = CharacterManager.mySkillTrickerySkillBonus.ToString();
				TrickerySkillBonusText.text = CharacterManager.mySkillTrickerySkillBonus.ToString();
				break;

			case "Trickery-":
				CharacterManager.mySkillTrickerySkillBonus = DecreasedSkillValue(CharacterManager.mySkillTrickerySkillBonus);
				TrickerySkillValueText.text = CharacterManager.mySkillTrickerySkillBonus.ToString();
				TrickerySkillBonusText.text = CharacterManager.mySkillTrickerySkillBonus.ToString();
				break;

			case "Perception+":
				CharacterManager.mySkillPerceptionSkillBonus = IncreasedSkillValue(CharacterManager.mySkillPerceptionSkillBonus);
				PerceptionSkillValueText.text = CharacterManager.mySkillPerceptionSkillBonus.ToString();
				PerceptionSkillBonusText.text = CharacterManager.mySkillPerceptionSkillBonus.ToString();
				break;

			case "Perception-":
				CharacterManager.mySkillPerceptionSkillBonus = DecreasedSkillValue(CharacterManager.mySkillPerceptionSkillBonus);
				PerceptionSkillValueText.text = CharacterManager.mySkillPerceptionSkillBonus.ToString();
				PerceptionSkillBonusText.text = CharacterManager.mySkillPerceptionSkillBonus.ToString();
				break;

			case "Magical+":
				CharacterManager.mySkillMagicalSkillBonus = IncreasedSkillValue(CharacterManager.mySkillMagicalSkillBonus);
				MagicalSkillValueText.text = CharacterManager.mySkillMagicalSkillBonus.ToString();
				MagicalSkillBonusText.text = CharacterManager.mySkillMagicalSkillBonus.ToString();
				break;

			case "Magical-":
				CharacterManager.mySkillMagicalSkillBonus = DecreasedSkillValue(CharacterManager.mySkillMagicalSkillBonus);
				MagicalSkillValueText.text = CharacterManager.mySkillMagicalSkillBonus.ToString();
				MagicalSkillBonusText.text = CharacterManager.mySkillMagicalSkillBonus.ToString();
				break;

			default:
				Debug.Log("The string variable on my button's OnClick() function is wrong");
				break;
		}
	}


	int IncreasedSkillValue (int mySkillBonus) {
		if (CharacterManager.skillPointsAvailable > 0) {
			if (mySkillBonus == -2) {
				mySkillBonus = 1;
			}
			else if (mySkillBonus > 0) {
				mySkillBonus++;
			}

			CharacterManager.skillPointsAvailable--;
			skillPointsRemainingText.text = ("Skill Points: " + CharacterManager.skillPointsAvailable);
		}
		else {
			Debug.Log("I've used up all my skill points");
		}

		return mySkillBonus;
	}


	int DecreasedSkillValue (int mySkillBonus) {
		if (mySkillBonus == -2) {
			Debug.Log("My skill is already too low! I can't decrease it any farther");
		}
		else if (CharacterManager.skillPointsAvailable < maxSkillPointsAvailable) {
			if (mySkillBonus == 1) {
				mySkillBonus = -2;
			}
			else if (mySkillBonus > 1) {
				mySkillBonus--;
			}

			CharacterManager.skillPointsAvailable++;
			skillPointsRemainingText.text = ("Skill Points: " + CharacterManager.skillPointsAvailable);
		}
		else {
			Debug.Log("I can't give you any more skill points");
		}

		return mySkillBonus;
	}


	public void ConfirmSkillValues() {
		if (CharacterManager.skillPointsAvailable == 0) {
			MeleeOBSkillBonusText.text = CharacterManager.mySkillMeleeOBSkillBonus.ToString();
			MissileOBSkillBonusText.text = CharacterManager.mySkillMissileOBSkillBonus.ToString();
			GeneralSkillBonusText.text = CharacterManager.mySkillGeneralSkillBonus.ToString();
			TrickerySkillBonusText.text = CharacterManager.mySkillTrickerySkillBonus.ToString();
			PerceptionSkillBonusText.text = CharacterManager.mySkillPerceptionSkillBonus.ToString();
			MagicalSkillBonusText.text = CharacterManager.mySkillMagicalSkillBonus.ToString();

			CharacterManager.allSkillsSet = true;

			GetComponent<CreationSceneManager>().TotalAllSkills();
			GetComponent<CreationSceneManager>().CloseNewUIWindow(skillsEditorBG);
		}
		else {
			Debug.Log("You still have skill points to assign");
		}
	}


	public void CancelSkillValues() {
		GetComponent<CreationSceneManager>().CloseNewUIWindow(skillsEditorBG);
		
	}

}
