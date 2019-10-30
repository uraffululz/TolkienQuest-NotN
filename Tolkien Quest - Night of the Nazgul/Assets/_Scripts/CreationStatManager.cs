using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationStatManager : MonoBehaviour {

	[SerializeField] GameObject nameEditorBG;
	[SerializeField] GameObject nameInputBox;
	[SerializeField] GameObject characterNameText;

	[SerializeField] GameObject statsEditorBG;
	int randomStat1;
	int randomStat2;
	int randomStat3;
	[SerializeField] GameObject randomRolledStatText1;
	[SerializeField] GameObject randomRolledStatText2;
	[SerializeField] GameObject randomRolledStatText3;
	[SerializeField] Image randomRolledParent1;
	[SerializeField] Image randomRolledParent2;
	[SerializeField] Image randomRolledParent3;
	[SerializeField] Image assignedStrength;
	[SerializeField] Image assignedAgility;
	[SerializeField] Image assignedIntelligence;
	[SerializeField] GameObject strengthStatValue;
	[SerializeField] GameObject agilityStatValue;
	[SerializeField] GameObject intelligenceStatValue;

	[SerializeField] Text enduranceText;

	[SerializeField] Text[] strengthBonusTexts;
	[SerializeField] Text[] agilityBonusTexts;
	[SerializeField] Text[] intelligenceBonusTexts;


	void Update() {
		if (Input.GetKey(KeyCode.X) && statsEditorBG.GetComponent<Animator>().GetBool("slideTopUIOpen") == true) {
//TODO REPLACE THIS WITH THE DRAG-AND-DROP FUNCTIONALITY
			randomRolledStatText1.transform.SetParent(assignedStrength.transform, true);
			randomRolledStatText2.transform.SetParent(assignedAgility.transform, true);
			randomRolledStatText3.transform.SetParent(assignedIntelligence.transform, true);

			randomRolledStatText1.transform.position = assignedStrength.transform.position;
			randomRolledStatText2.transform.position = assignedAgility.transform.position;
			randomRolledStatText3.transform.position = assignedIntelligence.transform.position;
		}

		//Drag-and-drop

	}


	public void NameInputUISet() {
		//Animate the "Name Editor BG UI" to slide CLOSED
		GetComponent<CreationSceneManager>().CloseNewUIWindow(nameEditorBG);

		//Apply the character's name via the NameInputField UI GameObject
		CharacterManager.myName = nameInputBox.GetComponentInChildren<Text>().text;
		characterNameText.GetComponent<Text>().text = CharacterManager.myName;
		CharacterManager.nameSet = true;

		//Determine if the character sheet is complete, and the player is allowed to continue to the game
		GetComponent<CreationSceneManager>().AllowContinue();
	}


	public void RollNewRandomStatNumbers() {
		//Roll 3 random numbers
		randomStat1 = Random.Range(2, 13);
		randomStat2 = Random.Range(2, 13);
		randomStat3 = Random.Range(2, 13);

		//Apply the random numbers to the StatEditorBG "stat text" objects
		randomRolledStatText1.GetComponent<Text>().text = randomStat1.ToString();
		randomRolledStatText2.GetComponent<Text>().text = randomStat2.ToString();
		randomRolledStatText3.GetComponent<Text>().text = randomStat3.ToString();

		//Parent and position the "stat texts" to their original positions
			//(really only needed if they are rolled, applied, then re-rolled)
		randomRolledStatText1.transform.SetParent(randomRolledParent1.transform, true);
		randomRolledStatText2.transform.SetParent(randomRolledParent2.transform, true);
		randomRolledStatText3.transform.SetParent(randomRolledParent3.transform, true);
		randomRolledStatText1.transform.position = randomRolledParent1.transform.position;
		randomRolledStatText2.transform.position = randomRolledParent2.transform.position;
		randomRolledStatText3.transform.position = randomRolledParent3.transform.position;
	}


	public void StatsInputUISet() {
		if (assignedStrength.GetComponentInChildren<Text>() != null &&
		assignedAgility.GetComponentInChildren<Text>() != null &&
		assignedIntelligence.GetComponentInChildren<Text>() != null) {
			//If all of the player's stats are given an assigned "stat text", allow them to confirm and assign the numbers
			//Assign the player's stats
			AssignStatsToCharMan(assignedStrength, CharacterManager.myStatStrength);
			AssignStatsToCharMan(assignedAgility, CharacterManager.myStatAgility);
			AssignStatsToCharMan(assignedIntelligence, CharacterManager.myStatIntelligence);

			//Apply the character's stats to their relevant UI text objects on the character sheet
			strengthStatValue.GetComponent<Text>().text = CharacterManager.myStatStrength.ToString();
			agilityStatValue.GetComponent<Text>().text = CharacterManager.myStatAgility.ToString();
			intelligenceStatValue.GetComponent<Text>().text = CharacterManager.myStatIntelligence.ToString();

			//Apply the character's "stat bonuses" to the static CharacterManager script
			CharacterManager.mySkillMeleeOBStatBonus = CharacterManager.myStatStrengthBonus;
			CharacterManager.mySkillMissileOBStatBonus = CharacterManager.myStatAgilityBonus;
			CharacterManager.mySkillDBStatBonus = CharacterManager.myStatAgilityBonus;
			CharacterManager.mySkillRunningStatBonus = CharacterManager.myStatAgilityBonus;
			CharacterManager.mySkillGeneralStatBonus = CharacterManager.myStatAgilityBonus;
			CharacterManager.mySkillTrickeryStatBonus = CharacterManager.myStatIntelligenceBonus;
			CharacterManager.mySkillPerceptionStatBonus = CharacterManager.myStatIntelligenceBonus;
			CharacterManager.mySkillMagicalStatBonus = CharacterManager.myStatIntelligenceBonus;

			//Calculate the player's Endurance stat
			CalculateEndurance();

			//Confirm that all of the player's stats are assigned
			CharacterManager.allStatsSet = true;

			//Calculate and apply the character's combined "stat bonus" and "skill bonus" variables to their "skill totals"
			GetComponent<CreationSceneManager>().TotalAllSkills();

			//Close the StatEditorBG UI window
			GetComponent<CreationSceneManager>().CloseNewUIWindow(statsEditorBG);
		}
		else {
			Debug.Log("Some stat value not assigned");
		}
	}

	private void AssignStatsToCharMan(Image myAssignedStatBG, int charManStat) {
		//Determine which number to assign to the relevant stat
		if (myAssignedStatBG.transform.GetChild(0) == randomRolledStatText1.transform) {
			charManStat = randomStat1;
		}
		else if (myAssignedStatBG.transform.GetChild(0) == randomRolledStatText2.transform) {
			charManStat = randomStat2;
		}
		else if (myAssignedStatBG.transform.GetChild(0) == randomRolledStatText3.transform) {
			charManStat = randomStat3;
		}
		else {
			Debug.Log("I'm a little fucked up OVER HERE");
		}

		//Apply stat number based on relevant stat, and determine "stat bonus"
		if (myAssignedStatBG == assignedStrength) {
			CharacterManager.myStatStrength = charManStat;
			int myStatBonus = ConfigureStatBonus(CharacterManager.myStatStrength, CharacterManager.myStatStrengthBonus, strengthBonusTexts);
			CharacterManager.myStatStrengthBonus = myStatBonus;
		}
		else if (myAssignedStatBG == assignedAgility) {
			CharacterManager.myStatAgility = charManStat;
			int myStatBonus = ConfigureStatBonus(CharacterManager.myStatAgility, CharacterManager.myStatAgilityBonus, agilityBonusTexts);
			CharacterManager.myStatAgilityBonus = myStatBonus;
		}
		else if (myAssignedStatBG == assignedIntelligence) {
			CharacterManager.myStatIntelligence = charManStat;
			int myStatBonus = ConfigureStatBonus(CharacterManager.myStatIntelligence, CharacterManager.myStatIntelligenceBonus, intelligenceBonusTexts);
			CharacterManager.myStatIntelligenceBonus = myStatBonus;
		}
	}


	int ConfigureStatBonus(int thisStatValue, int thisStatBonus, Text[] thisStatBonusTextObjectArray) {
		//Determine the "stat bonus" of the relevant stat
		if (thisStatValue <= 4) { //If thisStatValue = 2-4
			thisStatBonus = -1;
		}
		else if (thisStatValue >= 5 && thisStatValue <=8) { //If thisStatValue = 5-8
			thisStatBonus = 0;
		}
		else if (thisStatValue >= 9 && thisStatValue <=10) { //If thisStatValue = 9-10
			thisStatBonus = 1;
		}
		else if (thisStatValue >= 11) { //If thisStatValue = 11-12
			thisStatBonus = 2;
		}
		else {
			Debug.Log("You fucked up THIS STAT");
		}

		//Apply the "stat bonus" (as text) to each of the relevant stat's "stat bonus" text fields on the character sheet
		foreach (Text statBonusText in thisStatBonusTextObjectArray) {
			statBonusText.text = thisStatBonus.ToString();
		}

		return thisStatBonus;
	}


	void CalculateEndurance() {
		//Calculate the player's endurance and apply the value (as text) to the EnduranceTotal UI text object
		CharacterManager.enduranceTotal = 20 + (2 * CharacterManager.myStatStrength);
		enduranceText.text = CharacterManager.enduranceTotal.ToString();
	}


	public void CancelBackToMainScene() {
		//Clear the CharacterManager skill "stat bonuses" variables

		//Reset the player's stats
		CharacterManager.myStatStrength = 0;
		CharacterManager.myStatAgility = 0;
		CharacterManager.myStatIntelligence = 0;

		//Clear the "random number texts"
		randomRolledStatText1.GetComponent<Text>().text = "";
		randomRolledStatText2.GetComponent<Text>().text = "";
		randomRolledStatText3.GetComponent<Text>().text = "";

		//Re-parent and position the "random number texts" to their "Unassigned" image objects
		randomRolledStatText1.transform.SetParent(randomRolledParent1.transform, true);
		randomRolledStatText2.transform.SetParent(randomRolledParent2.transform, true);
		randomRolledStatText3.transform.SetParent(randomRolledParent3.transform, true);
		randomRolledStatText1.transform.position = randomRolledParent1.transform.position;
		randomRolledStatText2.transform.position = randomRolledParent2.transform.position;
		randomRolledStatText3.transform.position = randomRolledParent3.transform.position;

		//Clear the "stat texts" on the character sheet
		strengthStatValue.GetComponent<Text>().text = "";
		agilityStatValue.GetComponent<Text>().text = "";
		intelligenceStatValue.GetComponent<Text>().text = "";

//TODO Do I still need these if I'm resetting all the text fields as "blank" anyway? TEST by commenting them out temporarily
		//ConfigureStatBonus(CharacterManager.myStatStrength, CharacterManager.myStatStrengthBonus, strengthBonusTexts);
		//ConfigureStatBonus(CharacterManager.myStatAgility, CharacterManager.myStatAgilityBonus, agilityBonusTexts);
		//ConfigureStatBonus(CharacterManager.myStatIntelligence, CharacterManager.myStatIntelligenceBonus, intelligenceBonusTexts);

		//Clear each stat's "bonus text" UI objects
		foreach (Text strBonusText in strengthBonusTexts) {
			strBonusText.text = "";
		}
		foreach (Text agiBonusText in agilityBonusTexts) {
			agiBonusText.text = "";
		}
		foreach (Text intBonusText in intelligenceBonusTexts) {
			intBonusText.text = "";
		}

		//Close the StatEditorBG UI window
		GetComponent<CreationSceneManager>().CloseNewUIWindow(statsEditorBG);
	}
	
}
