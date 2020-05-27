using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSceneCharacterSheetManager : MonoBehaviour {

	[SerializeField] Text characterNameText;
	[SerializeField] Text characterRaceText;

	[Header("Stat Texts")]
	[SerializeField] Text statStrValueText;
	[SerializeField] Text statStrBonusText;
	[SerializeField] Text statAgiValueText;
	[SerializeField] Text statAgiBonusText;
	[SerializeField] Text statIntValueText;
	[SerializeField] Text statIntBonusText;
	[SerializeField] Text enduranceTotalText;

	[Header("Skill Texts")]
	[SerializeField] Text meleeOBSkillBonusText;
	[SerializeField] Text meleeOBStatBonusText;
	[SerializeField] Text meleeOBSpecialBonusText;
	[SerializeField] Text meleeOBTotalBonusText;

	[SerializeField] Text missileOBSkillBonusText;
	[SerializeField] Text missileOBStatBonusText;
	[SerializeField] Text missileOBSpecialBonusText;
	[SerializeField] Text missileOBTotalBonusText;

	//[SerializeField] Text DBSkillBonusText;
	[SerializeField] Text DBStatBonusText;
	[SerializeField] Text DBSpecialBonusText;
	[SerializeField] Text DBTotalBonusText;

	//[SerializeField] Text runningSkillBonusText;
	[SerializeField] Text runningStatBonusText;
	[SerializeField] Text runningSpecialBonusText;
	[SerializeField] Text runningTotalBonusText;

	[SerializeField] Text generalSkillBonusText;
	[SerializeField] Text generalStatBonusText;
	[SerializeField] Text generalSpecialBonusText;
	[SerializeField] Text generalTotalBonusText;

	[SerializeField] Text trickerySkillBonusText;
	[SerializeField] Text trickeryStatBonusText;
	[SerializeField] Text trickerySpecialBonusText;
	[SerializeField] Text trickeryTotalBonusText;

	[SerializeField] Text perceptionSkillBonusText;
	[SerializeField] Text perceptionStatBonusText;
	[SerializeField] Text perceptionSpecialBonusText;
	[SerializeField] Text perceptionTotalBonusText;

	[SerializeField] Text magicalSkillBonusText;
	[SerializeField] Text magicalStatBonusText;
	[SerializeField] Text magicalSpecialBonusText;
	[SerializeField] Text magicalTotalBonusText;

	[Header("Spell Slot Texts")]
	[SerializeField] Text spellSlot1Text;
	[SerializeField] Text spellSlot2Text;
	[SerializeField] Text spellSlot3Text;
	[SerializeField] Text spellSlot4Text;
	[SerializeField] Text spellSlot5Text;
	[SerializeField] Text spellSlot6Text;
	[SerializeField] Text spellSlot7Text;

	[Header("Misc. Stat Texts")]
	[SerializeField] Text damageTakenText;
	[SerializeField] Text timeTakenMinutesText;
	[SerializeField] Text timeTakenDaysText;
	[SerializeField] Text XPText;



	void Start () {
        CharacterSheetInitialization();
    }


	void Update() {
        
    }


	void CharacterSheetInitialization() {
		characterNameText.text = CharacterManager.myName;
		characterRaceText.text = "Race: <size=30>" + CharacterManager.myRace.ToString() + "</size>";

		statStrValueText.text = CharacterManager.myStatStrength.ToString();
		statStrBonusText.text = CharacterManager.myStatStrengthBonus.ToString();
		statAgiValueText.text = CharacterManager.myStatAgility.ToString();
		statAgiBonusText.text = CharacterManager.myStatAgilityBonus.ToString();
		statIntValueText.text = CharacterManager.myStatIntelligence.ToString();
		statIntBonusText.text = CharacterManager.myStatIntelligenceBonus.ToString();
		enduranceTotalText.text = CharacterManager.enduranceTotal.ToString();

		meleeOBSkillBonusText.text = CharacterManager.mySkillMeleeOBSkillBonus.ToString();
		meleeOBStatBonusText.text = CharacterManager.mySkillMeleeOBStatBonus.ToString();
		meleeOBSpecialBonusText.text = CharacterManager.mySkillMeleeOBSpecialBonuses.ToString();
		meleeOBTotalBonusText.text = CharacterManager.mySkillMeleeOBTotal.ToString();

		missileOBSkillBonusText.text = CharacterManager.mySkillMissileOBSkillBonus.ToString();
		missileOBStatBonusText.text = CharacterManager.mySkillMissileOBStatBonus.ToString();
		missileOBSpecialBonusText.text = CharacterManager.mySkillMissileOBSpecialBonuses.ToString();
		missileOBTotalBonusText.text = CharacterManager.mySkillMissileOBTotal.ToString();

		DBStatBonusText.text = CharacterManager.mySkillDBStatBonus.ToString();
		DBSpecialBonusText.text = CharacterManager.mySkillDBSpecialBonuses.ToString();
		DBTotalBonusText.text = CharacterManager.mySkillDBTotal.ToString(); 
		
		runningStatBonusText.text = CharacterManager.mySkillRunningStatBonus.ToString();
		runningSpecialBonusText.text = CharacterManager.mySkillRunningSpecialBonuses.ToString();
		runningTotalBonusText.text = CharacterManager.mySkillRunningTotal.ToString(); 
		
		generalSkillBonusText.text = CharacterManager.mySkillGeneralSkillBonus.ToString();
		generalStatBonusText.text = CharacterManager.mySkillGeneralStatBonus.ToString();
		generalSpecialBonusText.text = CharacterManager.mySkillGeneralSpecialBonuses.ToString();
		generalTotalBonusText.text = CharacterManager.mySkillGeneralTotal.ToString(); 
		
		trickerySkillBonusText.text = CharacterManager.mySkillTrickerySkillBonus.ToString();
		trickeryStatBonusText.text = CharacterManager.mySkillTrickeryStatBonus.ToString();
		trickerySpecialBonusText.text = CharacterManager.mySkillTrickerySpecialBonuses.ToString();
		trickeryTotalBonusText.text = CharacterManager.mySkillTrickeryTotal.ToString(); 
		
		perceptionSkillBonusText.text = CharacterManager.mySkillPerceptionSkillBonus.ToString();
		perceptionStatBonusText.text = CharacterManager.mySkillPerceptionStatBonus.ToString();
		perceptionSpecialBonusText.text = CharacterManager.mySkillPerceptionSpecialBonuses.ToString();
		perceptionTotalBonusText.text = CharacterManager.mySkillPerceptionTotal.ToString(); 
		
		magicalSkillBonusText.text = CharacterManager.mySkillMagicalSkillBonus.ToString();
		magicalStatBonusText.text = CharacterManager.mySkillMagicalStatBonus.ToString();
		magicalSpecialBonusText.text = CharacterManager.mySkillMagicalSpecialBonuses.ToString();
		magicalTotalBonusText.text = CharacterManager.mySkillMagicalTotal.ToString();

		if (CharacterManager.spellScriptables[0] != null) {
			spellSlot1Text.GetComponent<ScriptableSpellReader>().mySpellScriptable = CharacterManager.spellScriptables[0];
			spellSlot1Text.GetComponent<ScriptableSpellReader>().InitializeSpell();
			spellSlot1Text.text = spellSlot1Text.GetComponent<ScriptableSpellReader>().mySpellScriptable.mySpellName;
		}
		else {
			spellSlot1Text.text = "";
		}

		if (CharacterManager.spellScriptables[1] != null) {
			spellSlot2Text.GetComponent<ScriptableSpellReader>().mySpellScriptable = CharacterManager.spellScriptables[1];
			spellSlot2Text.GetComponent<ScriptableSpellReader>().InitializeSpell();
			spellSlot2Text.text = spellSlot2Text.GetComponent<ScriptableSpellReader>().mySpellScriptable.mySpellName;
		}
		else {
			spellSlot2Text.text = "";
		}

		if (CharacterManager.spellScriptables[2] != null) {
			spellSlot3Text.GetComponent<ScriptableSpellReader>().mySpellScriptable = CharacterManager.spellScriptables[2];
			spellSlot3Text.GetComponent<ScriptableSpellReader>().InitializeSpell();
			spellSlot3Text.text = spellSlot3Text.GetComponent<ScriptableSpellReader>().mySpellScriptable.mySpellName;
		}
		else {
			spellSlot3Text.text = "";
		}

		if (CharacterManager.spellScriptables[3] != null) {
			spellSlot4Text.GetComponent<ScriptableSpellReader>().mySpellScriptable = CharacterManager.spellScriptables[3];
			spellSlot4Text.GetComponent<ScriptableSpellReader>().InitializeSpell();
			spellSlot4Text.text = spellSlot4Text.GetComponent<ScriptableSpellReader>().mySpellScriptable.mySpellName;
		}
		else {
			spellSlot4Text.text = "";
		}

		if (CharacterManager.spellScriptables[4] != null) {
			spellSlot5Text.GetComponent<ScriptableSpellReader>().mySpellScriptable = CharacterManager.spellScriptables[4];
			spellSlot5Text.GetComponent<ScriptableSpellReader>().InitializeSpell();
			spellSlot5Text.text = spellSlot5Text.GetComponent<ScriptableSpellReader>().mySpellScriptable.mySpellName;
		}
		else {
			spellSlot5Text.text = "";
		}

		if (CharacterManager.spellScriptables[5] != null) {
			spellSlot6Text.GetComponent<ScriptableSpellReader>().mySpellScriptable = CharacterManager.spellScriptables[5];
			spellSlot6Text.GetComponent<ScriptableSpellReader>().InitializeSpell();
			spellSlot6Text.text = spellSlot6Text.GetComponent<ScriptableSpellReader>().mySpellScriptable.mySpellName;
		}
		else {
			spellSlot6Text.text = "";
		}

		if (CharacterManager.spellScriptables[6] != null) {
			spellSlot7Text.GetComponent<ScriptableSpellReader>().mySpellScriptable = CharacterManager.spellScriptables[6];
			spellSlot7Text.GetComponent<ScriptableSpellReader>().InitializeSpell();
			spellSlot7Text.text = spellSlot7Text.GetComponent<ScriptableSpellReader>().mySpellScriptable.mySpellName;
		}
		else {
			spellSlot7Text.text = "";
		}

		damageTakenText.text = CharacterManager.damageTaken + " / " + CharacterManager.enduranceTotal;

		timeTakenMinutesText.text = CharacterManager.currentDayTimeTaken.ToString();
		timeTakenDaysText.text = CharacterManager.daysTaken.ToString();

		XPText.text = CharacterManager.totalXP.ToString();

	}


	public void UpdateDamageText() {
		damageTakenText.text = CharacterManager.damageTaken + " / " + CharacterManager.enduranceTotal;
	}


	public void UpdateTimeText() {
		timeTakenMinutesText.text = CharacterManager.currentDayTimeTaken.ToString();
		timeTakenDaysText.text = CharacterManager.daysTaken.ToString();
	}


	public void UpdateXPText() {
		XPText.text = CharacterManager.totalXP.ToString();
	}


	public void UpdateGeneralSkillTexts() {
		//print(CharacterManager.mySkillGeneralTotal);
		CharacterManager.mySkillGeneralTotal = CharacterManager.mySkillGeneralStatBonus + CharacterManager.mySkillGeneralSkillBonus + CharacterManager.mySkillGeneralSpecialBonuses;
		//print(CharacterManager.mySkillGeneralTotal);

		generalSkillBonusText.text = CharacterManager.mySkillGeneralSkillBonus.ToString();
		generalStatBonusText.text = CharacterManager.mySkillGeneralStatBonus.ToString();
		generalSpecialBonusText.text = CharacterManager.mySkillGeneralSpecialBonuses.ToString();
		generalTotalBonusText.text = CharacterManager.mySkillGeneralTotal.ToString();
	}


	public void UpdateRunningSkillTexts() {
		CharacterManager.mySkillRunningTotal = CharacterManager.mySkillRunningStatBonus + CharacterManager.mySkillRunningSpecialBonuses;

		runningStatBonusText.text = CharacterManager.mySkillRunningStatBonus.ToString();
		runningSpecialBonusText.text = CharacterManager.mySkillRunningSpecialBonuses.ToString();
		runningTotalBonusText.text = CharacterManager.mySkillRunningTotal.ToString();
	}


	public void UpdateTrickerySkillTexts() {
		CharacterManager.mySkillTrickeryTotal = CharacterManager.mySkillTrickeryStatBonus + CharacterManager.mySkillTrickerySkillBonus + CharacterManager.mySkillTrickerySpecialBonuses;

		trickerySkillBonusText.text = CharacterManager.mySkillTrickerySkillBonus.ToString();
		trickeryStatBonusText.text = CharacterManager.mySkillTrickeryStatBonus.ToString();
		trickerySpecialBonusText.text = CharacterManager.mySkillTrickerySpecialBonuses.ToString();
		trickeryTotalBonusText.text = CharacterManager.mySkillTrickeryTotal.ToString();
	}
}
