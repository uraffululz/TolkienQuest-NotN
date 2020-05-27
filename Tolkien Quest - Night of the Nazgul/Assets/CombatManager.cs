using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {

	[SerializeField] CombatScriptable currentCombat;

	[Space]

	[SerializeField] int combatRound;
	public static bool sneakAttackSuccessful;
	//public int bonusToFirstAttack;
	public static bool playerSurprised;
	public static bool playerDamagedInLastCombat = true;

	[Space]

	public static bool shieldSpellActive = false;
	public static bool strengthSpellActive = false;
	public static bool charmedAnimal = false;
	[SerializeField] string charmedAnimalName;
	[SerializeField] int charmedAnimalOB;
	[SerializeField] int charmedAnimalDB;
	[SerializeField] int charmedAnimalEPTotal;
	[SerializeField] int charmedAnimalEPCurrent;
	[SerializeField] GameObject charmedAnimalUIParent;
	[SerializeField] Text charmedAnimalNameText;
	[SerializeField] Text charmedAnimalOBText;
	[SerializeField] Text charmedAnimalDBText;
	[SerializeField] Image charmedAnimalEPBar;
	[SerializeField] Text charmedAnimalEPText;
	[SerializeField] GameObject confirmUseCharmedAnimalUI;
	//[SerializeField] GameObject animalAttackButton;

	[Space]

	public int activeEnemy;
	bool enemyTargetingCharmedAnimal;
	//GameObject activeEnemyParent;
	//Text activeEnemyNameText;
	//Image activeEnemyEPBar;
	//int activeEnemyEPMax;
	//int activeEnemyEPRemaining;
	//Text activeEnemyEPText;
	//Text activeEnemyOBText;
	//Text activeEnemyDBText;

	[Space]

	[SerializeField] MapSceneManager mapSceneManager;
	[SerializeField] MapSceneCharacterSheetManager mSCSM;
	[SerializeField] MapSceneInventoryManager mapSceneInventoryManager;
	//[SerializeField] GameObject combatUI;
	[SerializeField] GameObject encounterBG;
	[SerializeField] GameObject encounterSpellBG;

	[Space]

	//UI Stuff
	[SerializeField] GameObject playerAttackButton;
	[SerializeField] GameObject playerSpellButton;
	[SerializeField] GameObject playerRunButton;
	[SerializeField] GameObject playDeadButton;
	[SerializeField] GameObject weaponChoiceUI;
	[SerializeField] GameObject spellChoiceUI;
	[SerializeField] Text combatActionText;
	//Weapon Choice Buttons
	[SerializeField] GameObject weaponChoiceButtonMagicSword;
	[SerializeField] GameObject weaponChoiceButtonMagicBow;
	[SerializeField] GameObject weaponChoiceButtonOrcSlayingSword;
	[SerializeField] GameObject weaponChoiceButtonWarhammer;
	[SerializeField] GameObject weaponChoiceButtonBattleaxe;
	[SerializeField] GameObject weaponChoiceButtonSword;
	[SerializeField] GameObject weaponChoiceButtonDagger;
	[SerializeField] GameObject weaponChoiceButtonMace;
	[SerializeField] GameObject weaponChoiceButtonClub;
	[SerializeField] GameObject weaponChoiceButtonStaff;
	[SerializeField] GameObject weaponChoiceButtonSpear;
	[SerializeField] GameObject weaponChoiceButtonTwoHandedSword;
	[SerializeField] GameObject weaponChoiceButtonBow;
	[SerializeField] GameObject weaponChoiceButtonBareHanded;

	[Space]

	[SerializeField] GameObject charmAnimalButton;
	[SerializeField] GameObject fireBoltButton;
	[SerializeField] GameObject shieldButton;
	[SerializeField] GameObject strengthButton;
	[SerializeField] SpellScriptable charmAnimalScript;
	[SerializeField] SpellScriptable fireBoltScript;
	[SerializeField] SpellScriptable shieldScript;
	[SerializeField] SpellScriptable strengthScript;

	//Win-Lose UI
	[SerializeField] GameObject combatWinUI;
	[SerializeField] GameObject outOfRangeWinText;
	[SerializeField] GameObject combatLoseUI;
	[SerializeField] GameObject combatRunUI;

	[Space]

	[Header("Enemy Stat & UI Arrays")]
	public CombatScriptable.enemyType[] enemyTypes;
	[SerializeField] GameObject[] enemyParents;
	[SerializeField] Text[] enemyNameTexts;
	[SerializeField] Image[] enemyEPBars;
	[SerializeField] Text[] enemyEPTexts;
	[SerializeField] Text[] enemyOBTexts;
	[SerializeField] Text[] enemyDBTexts;
	[SerializeField] Image[] enemyDefeatedOverlays;

	[Space]

	[SerializeField] int[] enemiesEPMax;
	[SerializeField] int[] enemiesEPRemaining;
	[SerializeField] bool[] enemiesDefeated;


	void Start () {
        
    }


    void Update() {
		if (Input.GetKeyDown(KeyCode.T)) {
			combatActionText.text += ("\n-next line");
		}
    }


	public void StartCombat(CombatScriptable combatScript) {
		encounterSpellBG.SetActive(false);

		currentCombat = combatScript;
		combatRound = 1;
		shieldSpellActive = false;
		strengthSpellActive = false;

		//if (charmedAnimal) {
		//	SetCharmedAnimal();
		//}
		//else {
		//	charmedAnimalUIParent.SetActive(false);
		//}
		////charmedAnimal = false;

		playerDamagedInLastCombat = false;

		combatActionText.text += ("\n" + "<color=#008000ff>Beginning Combat Encounter</color>");
		outOfRangeWinText.SetActive(false);

		if (currentCombat.playerCannotSneakAttack) {
			sneakAttackSuccessful = false;
		}

		if (sneakAttackSuccessful) {
			//On the Combat Action Text, display whether the player gets their sneak attack bonus
			combatActionText.text += "\n<color=#0000ffff>Your successful sneak attack provides a +" + CharacterManager.mySkillTrickeryTotal + " bonus to hit with your first attack.</color>";
		}

		if (currentCombat.firstAttackBonus != 0) {
			//On the Combat Action Text, display whether the encounter provides a bonus to the player's first attack
			combatActionText.text += "\n<color=#0000ffff>This encounter provides a +" + currentCombat.firstAttackBonus + " bonus to hit with your first attack.</color>";
		}

		if (currentCombat.penaltyToRun != 0) {
			combatActionText.text += "\n<color=#ff0000ff>This encounter suffers a -" + currentCombat.penaltyToRun + " penalty to any attempts to run.</color>";
		}

		if (currentCombat.playerSurprised) {
			playerSurprised = true;
		}

		if (currentCombat.canPlayDead) {
			playDeadButton.SetActive(true);
		}
		else {
			playDeadButton.SetActive(false);
		}

		for (int i = 0; i < enemiesDefeated.Length; i++) {
			enemiesDefeated[i] = false;
		}

		if (currentCombat.hasRandomEnemy) {
			int randomEnemyRoll = Random.Range(2, 13);
			print("Random Enemy Roll: " + randomEnemyRoll);

			if (randomEnemyRoll <= currentCombat.enemy1RangeMax) {
				currentCombat.enemyTypes[0] = currentCombat.randomEnemyTypes[0];
				enemyTypes[1] = currentCombat.randomEnemyTypes[0];
				currentCombat.enemyNames[0] = currentCombat.randomEnemyNames[0];
				enemyNameTexts[1].text = currentCombat.enemyNames[0];
				currentCombat.enemyOB[0] = currentCombat.randomEnemyOB[0];
				currentCombat.enemyDB[0] = currentCombat.randomEnemyDB[0];
				currentCombat.enemyEP[0] = currentCombat.randomEnemyEP[0];
				enemiesEPMax[1] = currentCombat.enemyEP[0];
				enemiesEPRemaining[1] = enemiesEPMax[1];
				enemyEPBars[1].fillAmount = 1;
				enemyEPTexts[1].text = "EP: " + enemiesEPRemaining[1] + " / " + enemiesEPMax[1];
				enemyOBTexts[1].text = "OB: " + currentCombat.randomEnemyOB[0].ToString();
				enemyDBTexts[1].text = "DB: " + currentCombat.randomEnemyDB[0].ToString();
			}
			else if (randomEnemyRoll > currentCombat.enemy1RangeMax && randomEnemyRoll <= currentCombat.enemy2RangeMax) {
				currentCombat.enemyTypes[0] = currentCombat.randomEnemyTypes[1];
				enemyTypes[1] = currentCombat.randomEnemyTypes[1];
				currentCombat.enemyNames[0] = currentCombat.randomEnemyNames[1]; 
				enemyNameTexts[1].text = currentCombat.enemyNames[0];
				currentCombat.enemyOB[0] = currentCombat.randomEnemyOB[1];
				currentCombat.enemyDB[0] = currentCombat.randomEnemyDB[1];
				currentCombat.enemyEP[0] = currentCombat.randomEnemyEP[1];
				enemiesEPMax[1] = currentCombat.enemyEP[0];
				enemiesEPRemaining[1] = enemiesEPMax[1];
				enemyEPBars[1].fillAmount = 1;
				enemyEPTexts[1].text = "EP: " + enemiesEPRemaining[1] + " / " + enemiesEPMax[1];
				enemyOBTexts[1].text = "OB: " + currentCombat.enemyOB[0].ToString();
				enemyDBTexts[1].text = "DB: " + currentCombat.enemyDB[0].ToString();
			}
			else if (randomEnemyRoll > currentCombat.enemy2RangeMax && randomEnemyRoll <= currentCombat.enemy3RangeMax) {
				currentCombat.enemyTypes[0] = currentCombat.randomEnemyTypes[2];
				enemyTypes[1] = currentCombat.randomEnemyTypes[2];
				currentCombat.enemyNames[0] = currentCombat.randomEnemyNames[2];
				enemyNameTexts[1].text = currentCombat.enemyNames[0];
				currentCombat.enemyOB[0] = currentCombat.randomEnemyOB[2];
				currentCombat.enemyDB[0] = currentCombat.randomEnemyDB[2];
				currentCombat.enemyEP[0] = currentCombat.randomEnemyEP[2];
				enemiesEPMax[1] = currentCombat.enemyEP[0];
				enemiesEPRemaining[1] = enemiesEPMax[1];
				enemyEPBars[1].fillAmount = 1;
				enemyEPTexts[1].text = "EP: " + enemiesEPRemaining[1] + " / " + enemiesEPMax[1];
				enemyOBTexts[1].text = "OB: " + currentCombat.enemyOB[0].ToString();
				enemyDBTexts[1].text = "DB: " + currentCombat.enemyDB[0].ToString();
			}
			else if (currentCombat.enemy4RangeMax != 0 && randomEnemyRoll > currentCombat.enemy3RangeMax && randomEnemyRoll <= currentCombat.enemy4RangeMax) {
				currentCombat.enemyTypes[0] = currentCombat.randomEnemyTypes[3];
				enemyTypes[1] = currentCombat.randomEnemyTypes[3];
				currentCombat.enemyNames[0] = currentCombat.randomEnemyNames[3];
				enemyNameTexts[1].text = currentCombat.enemyNames[0];
				currentCombat.enemyOB[0] = currentCombat.randomEnemyOB[3];
				currentCombat.enemyDB[0] = currentCombat.randomEnemyDB[3];
				currentCombat.enemyEP[0] = currentCombat.randomEnemyEP[3];
				enemiesEPMax[1] = currentCombat.enemyEP[0];
				enemiesEPRemaining[1] = enemiesEPMax[1];
				enemyEPBars[1].fillAmount = 1;
				enemyEPTexts[1].text = "EP: " + enemiesEPRemaining[1] + " / " + enemiesEPMax[1];
				enemyOBTexts[1].text = "OB: " + currentCombat.enemyOB[0].ToString();
				enemyDBTexts[1].text = "DB: " + currentCombat.enemyDB[0].ToString();
			}
			else {
				//Open the CombatWinUI and proceed to the combat's "Out of range encounter", which is really always just the "Win encounter" anyway
				OutsmartEnemy(currentCombat.outOfEnemyRangeDescription);
				
			}
		}
		else {
			enemyTypes[1] = currentCombat.enemyTypes[0];
			enemyNameTexts[1].text = currentCombat.enemyNames[0];
			enemiesEPMax[1] = currentCombat.enemyEP[0];
			enemiesEPRemaining[1] = enemiesEPMax[1];
			enemyEPBars[1].fillAmount = 1;
			enemyEPTexts[1].text = "EP: " + enemiesEPRemaining[1] + " / " + enemiesEPMax[1];
			enemyOBTexts[1].text = "OB: " + currentCombat.enemyOB[0].ToString();
			enemyDBTexts[1].text = "DB: " + currentCombat.enemyDB[0].ToString();
		}
		

		if (currentCombat.enemyTypes.Length > 1 && currentCombat.enemyTypes[1] != CombatScriptable.enemyType.None) {
			enemyTypes[2] = currentCombat.enemyTypes[1];
			enemyParents[2].SetActive(true);
			enemyNameTexts[2].text = currentCombat.enemyNames[1];
			enemiesEPMax[2] = currentCombat.enemyEP[1];
			enemiesEPRemaining[2] = enemiesEPMax[2];
			enemyEPBars[2].fillAmount = 1;
			enemyEPTexts[2].text = "EP: " + enemiesEPRemaining[2] + " / " + enemiesEPMax[2];
			enemyOBTexts[2].text = "OB: " + currentCombat.enemyOB[1].ToString();
			enemyDBTexts[2].text = "DB: " + currentCombat.enemyDB[1].ToString();

			//enemy2Parent.SetActive(false);
		}
		else {
			enemyParents[2].SetActive(false);
			enemiesDefeated[2] = true;
		}

		if (currentCombat.enemyTypes.Length > 2 && currentCombat.enemyTypes[2] != CombatScriptable.enemyType.None) {
			enemyTypes[3] = currentCombat.enemyTypes[2];
			enemyParents[3].SetActive(true);
			enemyNameTexts[3].text = currentCombat.enemyNames[2];
			enemiesEPMax[3] = currentCombat.enemyEP[2];
			enemiesEPRemaining[3] = enemiesEPMax[3];
			enemyEPBars[3].fillAmount = 1;
			enemyEPTexts[3].text = "EP: " + enemiesEPRemaining[3] + " / " + enemiesEPMax[3];
			enemyOBTexts[3].text = "OB: " + currentCombat.enemyOB[2].ToString();
			enemyDBTexts[3].text = "DB: " + currentCombat.enemyDB[2].ToString();

			//enemyParents[3].SetActive(false);
		}
		else {
			enemyParents[3].SetActive(false);
			enemiesDefeated[3] = true;
		}

		if (currentCombat.enemyTypes.Length > 3 && currentCombat.enemyTypes[3] != CombatScriptable.enemyType.None) {
			enemyTypes[4] = currentCombat.enemyTypes[3];
			enemyParents[4].SetActive(true);
			enemyNameTexts[4].text = currentCombat.enemyNames[3];
			enemiesEPMax[4] = currentCombat.enemyEP[3];
			enemiesEPRemaining[4] = enemiesEPMax[4];
			enemyEPBars[4].fillAmount = 1;
			enemyEPTexts[4].text = "EP: " + enemiesEPRemaining[4] + " / " + enemiesEPMax[4];
			enemyOBTexts[4].text = "OB: " + currentCombat.enemyOB[3].ToString();
			enemyDBTexts[4].text = "DB: " + currentCombat.enemyDB[3].ToString();

			//enemyParents[4].SetActive(false);
		}
		else {
			enemyParents[4].SetActive(false);
			enemiesDefeated[4] = true;
		}

		if (currentCombat.enemyTypes.Length > 4 && currentCombat.enemyTypes[4] != CombatScriptable.enemyType.None) {
			enemyTypes[5] = currentCombat.enemyTypes[4];
			enemyParents[5].SetActive(true);
			enemyNameTexts[5].text = currentCombat.enemyNames[4];
			enemiesEPMax[5] = currentCombat.enemyEP[4];
			enemiesEPRemaining[5] = enemiesEPMax[5];
			enemyEPBars[5].fillAmount = 1;
			enemyEPTexts[5].text = "EP: " + enemiesEPRemaining[5] + " / " + enemiesEPMax[5];
			enemyOBTexts[5].text = "OB: " + currentCombat.enemyOB[4].ToString();
			enemyDBTexts[5].text = "DB: " + currentCombat.enemyDB[4].ToString();

			//enemyParents[5].SetActive(false);
		}
		else {
			enemyParents[5].SetActive(false);
			enemiesDefeated[5] = true;
		}

		if (currentCombat.enemyTypes.Length > 5 && currentCombat.enemyTypes[5] != CombatScriptable.enemyType.None) {
			enemyTypes[6] = currentCombat.enemyTypes[5];
			enemyParents[6].SetActive(true);
			enemyNameTexts[6].text = currentCombat.enemyNames[5];
			enemiesEPMax[6] = currentCombat.enemyEP[5];
			enemiesEPRemaining[6] = enemiesEPMax[6];
			enemyEPBars[6].fillAmount = 1;
			enemyEPTexts[6].text = "EP: " + enemiesEPRemaining[6] + " / " + enemiesEPMax[6];
			enemyOBTexts[6].text = "OB: " + currentCombat.enemyOB[5].ToString();
			enemyDBTexts[6].text = "DB: " + currentCombat.enemyDB[5].ToString();

			//enemyParents[6].SetActive(false);
		}
		else {
			enemyParents[6].SetActive(false);
			enemiesDefeated[6] = true;
		}

		if (currentCombat.enemyTypes.Length > 6 && currentCombat.enemyTypes[6] != CombatScriptable.enemyType.None) {
			enemyTypes[7] = currentCombat.enemyTypes[6];
			enemyParents[7].SetActive(true);
			enemyNameTexts[7].text = currentCombat.enemyNames[6];
			enemiesEPMax[7] = currentCombat.enemyEP[6];
			enemiesEPRemaining[7] = enemiesEPMax[7];
			enemyEPBars[7].fillAmount = 1;
			enemyEPTexts[7].text = "EP: " + enemiesEPRemaining[7] + " / " + enemiesEPMax[7];
			enemyOBTexts[7].text = "OB: " + currentCombat.enemyOB[6].ToString();
			enemyDBTexts[7].text = "DB: " + currentCombat.enemyDB[6].ToString();

			//enemyParents[7].SetActive(false);
		}
		else {
			enemyParents[7].SetActive(false);
			enemiesDefeated[7] = true;
		}

		if (currentCombat.enemyTypes.Length > 7 && currentCombat.enemyTypes[7] != CombatScriptable.enemyType.None) {
			enemyTypes[8] = currentCombat.enemyTypes[7];
			enemyParents[8].SetActive(true);
			enemyNameTexts[8].text = currentCombat.enemyNames[7];
			enemiesEPMax[8] = currentCombat.enemyEP[7];
			enemiesEPRemaining[8] = enemiesEPMax[8];
			enemyEPBars[8].fillAmount = 1;
			enemyEPTexts[8].text = "EP: " + enemiesEPRemaining[8] + " / " + enemiesEPMax[8];
			enemyOBTexts[8].text = "OB: " + currentCombat.enemyOB[7].ToString();
			enemyDBTexts[8].text = "DB: " + currentCombat.enemyDB[7].ToString();

			//enemyParents[8].SetActive(false);
		}
		else {
			enemyParents[8].SetActive(false);
			enemiesDefeated[8] = true;
		}

		if (currentCombat.enemyTypes.Length > 8 && currentCombat.enemyTypes[8] != CombatScriptable.enemyType.None) {
			enemyTypes[9] = currentCombat.enemyTypes[8];
			enemyParents[9].SetActive(true);
			enemyNameTexts[9].text = currentCombat.enemyNames[8];
			enemiesEPMax[9] = currentCombat.enemyEP[8];
			enemiesEPRemaining[9] = enemiesEPMax[9];
			enemyEPBars[9].fillAmount = 1;
			enemyEPTexts[9].text = "EP: " + enemiesEPRemaining[9] + " / " + enemiesEPMax[9];
			enemyOBTexts[9].text = "OB: " + currentCombat.enemyOB[8].ToString();
			enemyDBTexts[9].text = "DB: " + currentCombat.enemyDB[8].ToString();

			//enemyParents[9].SetActive(false);
		}
		else {
			enemyParents[9].SetActive(false);
			enemiesDefeated[9] = true;
		}

		if (currentCombat.enemyTypes.Length > 9 && currentCombat.enemyTypes[9] != CombatScriptable.enemyType.None) {
			enemyTypes[10] = currentCombat.enemyTypes[9];
			enemyParents[10].SetActive(true);
			enemyNameTexts[10].text = currentCombat.enemyNames[9];
			enemiesEPMax[10] = currentCombat.enemyEP[9];
			enemiesEPRemaining[10] = enemiesEPMax[10];
			enemyEPBars[10].fillAmount = 1;
			enemyEPTexts[10].text = "EP: " + enemiesEPRemaining[10] + " / " + enemiesEPMax[10];
			enemyOBTexts[10].text = "OB: " + currentCombat.enemyOB[9].ToString();
			enemyDBTexts[10].text = "DB: " + currentCombat.enemyDB[9].ToString();

			//enemyParents[10].SetActive(false);
		}
		else {
			enemyParents[10].SetActive(false);
			enemiesDefeated[10] = true;
		}

		if (currentCombat.enemyTypes.Length > 10 && currentCombat.enemyTypes[10] != CombatScriptable.enemyType.None) {
			enemyTypes[11] = currentCombat.enemyTypes[10];
			enemyParents[11].SetActive(true);
			enemyNameTexts[11].text = currentCombat.enemyNames[10];
			enemiesEPMax[11] = currentCombat.enemyEP[10];
			enemiesEPRemaining[11] = enemiesEPMax[11];
			enemyEPBars[11].fillAmount = 1;
			enemyEPTexts[11].text = "EP: " + enemiesEPRemaining[11] + " / " + enemiesEPMax[11];
			enemyOBTexts[11].text = "OB: " + currentCombat.enemyOB[10].ToString();
			enemyDBTexts[11].text = "DB: " + currentCombat.enemyDB[10].ToString();

			//enemyParents[11].SetActive(false);
		}
		else {
			enemyParents[11].SetActive(false);
			enemiesDefeated[11] = true;
		}

		if (currentCombat.enemyTypes.Length > 11 && currentCombat.enemyTypes[11] != CombatScriptable.enemyType.None) {
			enemyTypes[12] = currentCombat.enemyTypes[11];
			enemyParents[12].SetActive(true);
			enemyNameTexts[12].text = currentCombat.enemyNames[11];
			enemiesEPMax[12] = currentCombat.enemyEP[11];
			enemiesEPRemaining[12] = enemiesEPMax[12];
			enemyEPBars[12].fillAmount = 1;
			enemyEPTexts[12].text = "EP: " + enemiesEPRemaining[12] + " / " + enemiesEPMax[12];
			enemyOBTexts[12].text = "OB: " + currentCombat.enemyOB[11].ToString();
			enemyDBTexts[12].text = "DB: " + currentCombat.enemyDB[11].ToString();

			//enemyParents[12].SetActive(false);
		}
		else {
			enemyParents[12].SetActive(false);
			enemiesDefeated[12] = true;
		}

		//Set "Active Enemy" UI Elements
		activeEnemy = 1;
		SetNewActiveEnemy();
		
		InitializeWeaponChoiceUI();
		InitializeSpellChoiceUI();

		//print(charmedAnimal);
		if (charmedAnimal) {
			confirmUseCharmedAnimalUI.SetActive(true);
		}
		else {
			DetermineFirstAttacker();
		}
	}


	public void SetCharmedAnimal() {
		//confirmUseCharmedAnimalUI.SetActive(false);
		enemyTargetingCharmedAnimal = true;

		charmedAnimalUIParent.SetActive(true);
		charmedAnimalNameText.text = charmedAnimalName;
		charmedAnimalEPBar.fillAmount = 1;
		charmedAnimalEPText.text = "EP: " + charmedAnimalEPCurrent + " / " + charmedAnimalEPTotal;
		charmedAnimalOBText.text = "OB: " + charmedAnimalOB.ToString();
		charmedAnimalDBText.text = "DB: " + charmedAnimalDB.ToString();

		DetermineFirstAttacker();
	}


	void SetNewActiveEnemy() {
		enemyTypes[0] = enemyTypes[activeEnemy];
		enemyNameTexts[0].text = enemyNameTexts[activeEnemy].text;
		enemiesEPMax[0] = enemiesEPMax[activeEnemy];
		enemiesEPRemaining[0] = enemiesEPMax[0];
		enemyEPBars[0].fillAmount = 1;
		enemyEPTexts[0].text = "EP: " + enemiesEPRemaining[0] + " / " + enemiesEPMax[0];
		enemyOBTexts[0].text = enemyOBTexts[activeEnemy].text;
		enemyDBTexts[0].text = enemyDBTexts[activeEnemy].text;

		enemyParents[activeEnemy].SetActive(false);
	}


	public void OutsmartEnemy(string outsmartedDescription) {
		combatActionText.text += "\nYou outsmarted the enemy! There is no need to fight!";
		EnableCombatWinUI();
		outOfRangeWinText.SetActive(true);
		outOfRangeWinText.GetComponent<Text>().text = outsmartedDescription;
	}


	public void DetermineFirstAttacker() {
		confirmUseCharmedAnimalUI.SetActive(false);
		gameObject.SetActive(true);
		gameObject.GetComponent<Animator>().SetBool("UISlideIn", true);

		if (!playerSurprised) {
			StartPlayerTurn();
		}
		else {
			combatActionText.text += "\n" + "You are surprised. The enemy acts first.";
			StartEnemyTurn();
		}
	}


	void StartPlayerTurn() {
		//Deactivate the buttons for the Shield and Strength spells, as they can only be cast in the first round of combat
		if (combatRound > 1) {
			shieldButton.SetActive(false);
			strengthButton.SetActive(false);
		}

		//Enable Player Combat UI, including possible actions and relevant weapon listings/buttons
		combatActionText.text += "\n" + "Starting " + CharacterManager.myName + "'s Turn";
		EnablePlayerTurnUI();
	}


	public void PlayerConfirmAttack(InventoryWeaponScriptable weaponUsedToAttack) {
		int playerTotalOB = 0;
		int playerAttackDamage = 0;
		bool enemyUnconscious = false;
		bool enemyKilled = false;

		if (combatRound == 1) {
			if (currentCombat.firstAttackBonus != 0) {
				playerTotalOB += currentCombat.firstAttackBonus;
//TOMAYBEDO Text component on CombatUI to display whether the player gets this bonus?

			}
			else if (sneakAttackSuccessful) {
				if (!currentCombat.playerCannotSneakAttack) {
					//Add the player's Trickery bonus to their OB calculation
					playerTotalOB += CharacterManager.mySkillTrickeryTotal;

					//Just checking if this is still active, as it should be disabled by now. It DOESN'T add to the player's Trickery bonus in this case.
					if (CharacterManager.camoSpellActive) {
						CharacterManager.mySkillTrickerySpecialBonuses -= 2;
						CharacterManager.camoSpellActive = false;

						mSCSM.UpdateTrickerySkillTexts();
					}
				}
			}
		}

		//Calculate Player's total OB, including weapon bonuses
		if (weaponUsedToAttack.MyWeaponType == InventoryWeaponScriptable.weaponTypes.Bow || weaponUsedToAttack.MyWeaponType == InventoryWeaponScriptable.weaponTypes.MagicBow) {
			playerTotalOB += (CharacterManager.mySkillMissileOBTotal + weaponUsedToAttack.MissileOBAdjustment);

//TODO The Player can also THROW some melee weapons (really only likely if their MissileOB stat is higher than MeleeOB).
//Add in that calculation/ability


		}
		else {
			playerTotalOB += (CharacterManager.mySkillMeleeOBTotal + weaponUsedToAttack.MeleeOBAdjustment);
		}

		print("OB: " + playerTotalOB + " || DB: " + currentCombat.enemyDB[activeEnemy - 1]);

		playerAttackDamage = CalculateOBvsDB(playerTotalOB, currentCombat.enemyDB[activeEnemy - 1], ref enemyUnconscious, ref enemyKilled) + weaponUsedToAttack.additionalDamage;
		//playerAttackDamage += weaponUsedToAttack.additionalDamage;

//TODO Make sure only ONE enemy is being damaged/killed
		if (enemyKilled) {
			DamageEnemy(CharacterManager.myName, 0, false, true);
		}
		else if (enemyUnconscious) {
			DamageEnemy(CharacterManager.myName, 0, true, false);
		}
		else {
			if (weaponUsedToAttack.MyWeaponType != InventoryWeaponScriptable.weaponTypes.Bow && weaponUsedToAttack.MyWeaponType != InventoryWeaponScriptable.weaponTypes.MagicBow) {
				if (strengthSpellActive) {
					print("Your Strength spell doubled your melee damage");
					playerAttackDamage *= 2;
				}
			}
			if (playerAttackDamage < 0) {
				playerAttackDamage = 0;
			}

			DamageEnemy(CharacterManager.myName, playerAttackDamage, false, false);
		}
	}


	public void DamageEnemy(string attackerName, int damageDealt, bool enemyKO, bool enemyKilled) {
		if (damageDealt >= 0) {
			enemiesEPRemaining[0] -= damageDealt;
			//print("Dealt " + damageDealt + " damage to the enemy");
			combatActionText.text += "\n" + attackerName + " dealt <color=#ff0000ff>" + damageDealt + "</color> damage to the " + enemyNameTexts[0].text;
		}
		//else {
		//	print("Dealt " + damageDealt + " damage to the enemy");
		//	combatActionText.text += "\n" + CharacterManager.myName + " dealt <color=#ff0000ff>0</color> damage to the " + enemyNameTexts[0].text;
		//}

//enemyKilled = true;

		//If the enemy is defeated
		if (enemiesEPRemaining[0] <= 0 || enemyKO || enemyKilled) {
			enemiesEPRemaining[0] = 0;
			enemiesDefeated[activeEnemy] = true;
			enemyParents[activeEnemy].SetActive(true);
			enemyDefeatedOverlays[activeEnemy].gameObject.SetActive(true);

			if (enemyKO) {
				if (!charmedAnimalUIParent.activeInHierarchy && charmedAnimal) {
					combatActionText.text += "\nEnemy animal charmed";

					charmedAnimalName = "Your " + enemyNameTexts[0].text;
					charmedAnimalOB = currentCombat.enemyOB[activeEnemy - 1];
					charmedAnimalOBText.text = charmedAnimalOB.ToString();
					charmedAnimalDB = currentCombat.enemyDB[activeEnemy - 1];
					charmedAnimalDBText.text = charmedAnimalDB.ToString();
					charmedAnimalEPTotal = currentCombat.enemyEP[activeEnemy - 1];
					charmedAnimalEPCurrent = charmedAnimalEPTotal;
					charmedAnimalEPBar.fillAmount = 1;
					charmedAnimalEPText.text = "EP: " + charmedAnimalEPCurrent.ToString() + " / " + charmedAnimalEPTotal.ToString();
				}
				else {
					combatActionText.text += "\nEnemy auto-KO'd";
				}
			}
			else if (enemyKilled) {
				combatActionText.text += "\nEnemy auto-killed";
				MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().enemyHereIsDead = true;
//TODO Make sure ALL of the enemies are dead before activating this bool.
//Until then, just set an int (probably also assigned to the MapTilesScriptableReader) equal to the number of enemies still alive, and subtract from it with each enemy killed. When it hits 0, enemyHereIsDead is set to true.
//Meanwhile, if the player returns, they continue to fight the enemies that are still alive (which I'll need to calculate by subracting that int from the "original total number of enemies"
			}
			else {
				combatActionText.text += "\nEnemy defeated";
			}

			//If the defeated enemy is NOT the last in the current combat
			if (activeEnemy < currentCombat.enemyTypes.Length) {
				//Appoint a new active enemy
				activeEnemy++;
				SetNewActiveEnemy();
			}
			//else {
			//	EnableCombatWinUI();
			//}
		}
		else {
			enemyEPBars[0].fillAmount = (float)enemiesEPRemaining[0] / enemiesEPMax[0];
			enemyEPTexts[0].text = "EP: " + enemiesEPRemaining[0] + " / " + enemiesEPMax[0];
		}

		EndPlayerTurn();
	}


	public void PlayerRunAway() {
//TODO Does the Enemy attack BEFORE the Player gets their chance to run away? Does it matter?
		if (!currentCombat.mustDefeatFirstEnemyToRun || currentCombat.mustDefeatFirstEnemyToRun && enemiesDefeated[1] == true) {
			int runRoll = Random.Range(2, 13);
			//Include the Player's Running skill, as well as the currentCombat's "penaltyToRun" variable in the calculation

			runRoll += CharacterManager.mySkillRunningTotal + currentCombat.penaltyToRun;

			if (CharacterManager.speedSpellActive) {
				runRoll += 2;
				CharacterManager.speedSpellActive = false;
				CharacterManager.mySkillRunningSpecialBonuses -= 2;
				mSCSM.UpdateRunningSkillTexts();
			}

			//if (currentCombat.penaltyToRun != 0) {
			//	runRoll += currentCombat.penaltyToRun;
			//}

			//Include any worn armor's "penalty to running" in the calculation
			if (mapSceneInventoryManager.inventoryParents[1].transform.childCount > 0) {
				runRoll += mapSceneInventoryManager.inventoryParents[1].transform.GetChild(0).GetComponent<InventoryScriptableReader>().armorScript.penaltyToRunning;
			}

			//If the player successfully runs away
			if (runRoll > 7) {
				EnableCombatRunUI();
			}
			else {
				//The player fails to run away
//TODO You MUST FIGHT YOUR OPPONENT, and are SURPRISED
				combatActionText.text += "\n<color=#ff0000ff>You failed to run away.</color>";
				EndPlayerTurn();
			}
		}
		else {
			//Display a Text object telling the player that they cannot run away until they defeat the first enemy in this encounter
			combatActionText.text += "\n<color=#ff0000ff>You must defeat the first enemy before you can run from this encounter</color>";
		}
		
	}


	public void ConfirmRunFromCombat() {
		DisableCombatRunUI();
		mapSceneManager.CloseCombatBG();

		//Follow the instructions in the text, or Move On (Move On Randomly if playing Advanced Rules)
		if (currentCombat.runGoesToEncounter != 0) {
			encounterBG.GetComponent<ScriptableEncounterReader>().UpdateEncounter(currentCombat.runGoesToEncounter);
		}
		else {
			//DisableCombatRunUI();
			mapSceneManager.GetComponent<MapSceneManager>().MoveOn(2);
		}

		playerSurprised = false;
	}


	public void PlayDead() {
		//Special rule for Encounter 180
		encounterBG.GetComponent<ScriptableEncounterReader>().UpdateEncounter(207);
		mapSceneManager.CloseCombatBG();
	}


	public void EndPlayerTurn() {
		DisableWeaponChoiceUI();
		DisableSpellChoiceUI();
		DisablePlayerTurnUI();

		bool combatComplete = true;

		for (int i = 1; i < enemiesDefeated.Length; i++) {
			if (enemiesDefeated[i] == false) {
				combatComplete = false;

				break;
			}
		}

		if (combatComplete) {
			EnableCombatWinUI();
		}
		else {
			if (playerSurprised) {
				combatRound++;
			}

			StartEnemyTurn();
		}
	}


	public void AnimalAttackEnemy() {
		int animalAttackDamage = 0;
		bool enemyKOd = false;
		bool enemyKilled = false;

		animalAttackDamage = CalculateOBvsDB(charmedAnimalOB, currentCombat.enemyDB[activeEnemy - 1], ref enemyKOd, ref enemyKilled);

		//TODO Make sure only ONE enemy is being damaged/killed
		if (enemyKilled) {
			DamageEnemy(charmedAnimalName, 0, false, true);
		}
		else if (enemyKOd) {
			DamageEnemy(charmedAnimalName, 0, true, false);
		}
		else {
			if (animalAttackDamage < 0) {
				animalAttackDamage = 0;
			}

			DamageEnemy(charmedAnimalName, animalAttackDamage, false, false);
		}
	}


	void StartEnemyTurn() {
		combatActionText.text += "\n" + "Starting enemy turn";
		if (enemyTargetingCharmedAnimal) {
			EnemyAttackAnimal();
		}
		else {
			EnemyAttackPlayer();
		}
	}


	void EnemyAttackAnimal() {
		//The Enemy attacks the player's charmed animal
		int enemyAttackDamage = 0;
		bool animalUnconscious = false;
		bool animalKilled = false;

		enemyAttackDamage = CalculateOBvsDB(currentCombat.enemyOB[activeEnemy - 1], charmedAnimalDB, ref animalUnconscious, ref animalKilled);

		if (animalKilled) {
			//print("Charmed animal was killed");
			combatActionText.text += "\n" + charmedAnimalName + " was killed.";

			charmedAnimal = false;
			charmedAnimalUIParent.SetActive(false);
			enemyTargetingCharmedAnimal = false;
		}
		else if (animalUnconscious) {
			//print("Charmed animal knocked unconscious");
			combatActionText.text += "\n" + charmedAnimalName + " was knocked unconscious.";

			charmedAnimal = false;
			charmedAnimalUIParent.SetActive(false);
			enemyTargetingCharmedAnimal = false;
		}
		else {
			if (enemyAttackDamage > 0) {
				charmedAnimalEPCurrent -= enemyAttackDamage;
				combatActionText.text += "\n" + enemyNameTexts[0].text + " dealt <color=#ff0000ff>" + enemyAttackDamage + "</color> damage to " + charmedAnimalName;

				if (charmedAnimalEPCurrent > 0) {
					charmedAnimalEPBar.fillAmount = (float)charmedAnimalEPCurrent / charmedAnimalEPTotal;
					charmedAnimalEPText.text = "EP: " + charmedAnimalEPCurrent.ToString() + " / " + charmedAnimalEPTotal.ToString();
				}
				else {
					combatActionText.text += "\n" + charmedAnimalName + " was defeated. You'll have to finish this fight yourself!";

					charmedAnimal = false;
					charmedAnimalUIParent.SetActive(false);
					enemyTargetingCharmedAnimal = false;
				}
			}
			else {
				combatActionText.text += "\n" + enemyNameTexts[0].text + " dealt <color=#ff0000ff>0</color> damage to " + charmedAnimalName;
			}
		}

		EndEnemyTurn();
	}


	void EnemyAttackPlayer() {
		int enemyAttackDamage = 0;
		bool playerUnconscious = false;
		bool playerKilled = false;

		int playerDB = CharacterManager.mySkillDBTotal;

		//Determine if the player has any armor in the Armor Slot of the inventory, and add that bonus to their DB
		if (mapSceneInventoryManager.inventoryParents[1].transform.childCount > 0) {
			playerDB += mapSceneInventoryManager.inventoryParents[1].transform.GetChild(0).gameObject.GetComponent<InventoryScriptableReader>().armorScript.DBonus;
		}

		if (shieldSpellActive) {
			playerDB += 2;
		}

		enemyAttackDamage = CalculateOBvsDB(currentCombat.enemyOB[activeEnemy - 1], playerDB, ref playerUnconscious, ref playerKilled);

		if (playerKilled) {
			//print("Player was killed");
//TOMAYBEDO Implement a "Combat Game Over" UI, if the distinction from the normal Game Over UI is needed
			mapSceneManager.GameOver();
		}
		else if (playerUnconscious) {
			//print("Player knocked unconscious");
			EnableCombatLoseUI();

			//playerDamagedInLastCombat = true;
		}
		else {
			mapSceneManager.AlterDamage(enemyAttackDamage, true);
			combatActionText.text += "\n" + enemyNameTexts[0].text + " dealt <color=#ff0000ff>" + enemyAttackDamage + "</color> damage to " + CharacterManager.myName;

			playerDamagedInLastCombat = true;
		}

		EndEnemyTurn();
	}


	void EndEnemyTurn () {
		if (!combatWinUI.activeInHierarchy && !combatLoseUI.activeInHierarchy) {
			if (!playerSurprised) {
				combatRound++;
			}

			StartPlayerTurn();
		}
	}


	int CalculateOBvsDB(int attackerOB, int defenderDB, ref bool defenderUnconscious, ref bool defenderKilled) {
		int OBvsDBCalculation = attackerOB - defenderDB;
		print("OB vs DB total: " + OBvsDBCalculation);

		int attackRoll = Random.Range(2, 13);
		int attackDamage = 0;

		if (OBvsDBCalculation <= -4) {
			switch (attackRoll) {
				case 8:
					attackDamage += 1;
					break;
				case 9:
					attackDamage += 2;
					break;
				case 10:
					attackDamage += 3;
					break;
				case 11:
					attackDamage += 5;
					break;
				case 12:
					defenderUnconscious = true;
					break;
				default:
					print("Enemy did 0 damage here");
					break;
			}
		}
		else if (OBvsDBCalculation >= 5) {
			switch (attackRoll) {
				case 3:
					attackDamage += 2;
					break;
				case 4:
					attackDamage += 4;
					break;
				case 5:
					attackDamage += 6;
					break;
				case 6:
					attackDamage += 7;
					break;
				case 7:
					attackDamage += 8;
					break;
				case 8:
					attackDamage += 9;
					break;
				case 9:
					defenderUnconscious = true;
					break;
				case 10:
					defenderUnconscious = true;
					break;
				case 11:
					defenderKilled = true;
					break;
				case 12:
					defenderKilled = true;
					break;
				default:
					print("Error with enemy attack roll");
					break;
			}

			attackDamage += (OBvsDBCalculation - 5);
		}
		else {
			switch (OBvsDBCalculation) {
				case -3:
					switch (attackRoll) {
						case 7:
							attackDamage += 1;
							break;
						case 8:
							attackDamage += 2;
							break;
						case 9:
							attackDamage += 2;
							break;
						case 10:
							attackDamage += 4;
							break;
						case 11:
							attackDamage += 6;
							break;
						case 12:
							defenderUnconscious = true;
							break;
						default:
							break;
					}
					break;
				case -2:
					switch (attackRoll) {
						case 7:
							attackDamage += 1;
							break;
						case 8:
							attackDamage += 2;
							break;
						case 9:
							attackDamage += 3;
							break;
						case 10:
							attackDamage += 5;
							break;
						case 11:
							attackDamage += 7;
							break;
						case 12:
							defenderUnconscious = true;
							break;
						default:
							break;
					}
					break;
				case -1:
					switch (attackRoll) {
						case 6:
							attackDamage += 1;
							break;
						case 7:
							attackDamage += 2;
							break;
						case 8:
							attackDamage += 3;
							break;
						case 9:
							attackDamage += 4;
							break;
						case 10:
							attackDamage += 6;
							break;
						case 11:
							attackDamage += 8;
							break;
						case 12:
							defenderUnconscious = true;
							break;
						default:
							break;
					}
					break;
				case 0:
					switch (attackRoll) {
						case 5:
							attackDamage += 1;
							break;
						case 6:
							attackDamage += 2;
							break;
						case 7:
							attackDamage += 3;
							break;
						case 8:
							attackDamage += 4;
							break;
						case 9:
							attackDamage += 5;
							break;
						case 10:
							attackDamage += 7;
							break;
						case 11:
							defenderUnconscious = true;
							break;
						case 12:
							defenderKilled = true;
							break;
						default:
							break;
					}
					break;
				case 1:
					switch (attackRoll) {
						case 4:
							attackDamage += 1;
							break;
						case 5:
							attackDamage += 2;
							break;
						case 6:
							attackDamage += 3;
							break;
						case 7:
							attackDamage += 4;
							break;
						case 8:
							attackDamage += 5;
							break;
						case 9:
							attackDamage += 6;
							break;
						case 10:
							attackDamage += 7;
							break;
						case 11:
							defenderUnconscious = true;
							break;
						case 12:
							defenderKilled = true;
							break;
						default:
							break;
					}
					break;
				case 2:
					switch (attackRoll) {
						case 3:
							attackDamage += 1;
							break;
						case 4:
							attackDamage += 2;
							break;
						case 5:
							attackDamage += 3;
							break;
						case 6:
							attackDamage += 4;
							break;
						case 7:
							attackDamage += 5;
							break;
						case 8:
							attackDamage += 6;
							break;
						case 9:
							attackDamage += 7;
							break;
						case 10:
							attackDamage += 8;
							break;
						case 11:
							defenderUnconscious = true;
							break;
						case 12:
							defenderKilled = true;
							break;
						default:
							break;
					}
					break;
				case 3:
					switch (attackRoll) {
						case 3:
							attackDamage += 1;
							break;
						case 4:
							attackDamage += 2;
							break;
						case 5:
							attackDamage += 4;
							break;
						case 6:
							attackDamage += 5;
							break;
						case 7:
							attackDamage += 6;
							break;
						case 8:
							attackDamage += 7;
							break;
						case 9:
							attackDamage += 8;
							break;
						case 10:
							defenderUnconscious = true;
							break;
						case 11:
							defenderUnconscious = true;
							break;
						case 12:
							defenderKilled = true;
							break;
						default:
							break;
					}
					break;
				case 4:
					switch (attackRoll) {
						case 3:
							attackDamage += 1;
							break;
						case 4:
							attackDamage += 3;
							break;
						case 5:
							attackDamage += 5;
							break;
						case 6:
							attackDamage += 6;
							break;
						case 7:
							attackDamage += 7;
							break;
						case 8:
							attackDamage += 8;
							break;
						case 9:
							attackDamage += 9;
							break;
						case 10:
							defenderUnconscious = true;
							break;
						case 11:
							defenderKilled = true;
							break;
						case 12:
							defenderKilled = true;
							break;
						default:
							break;
					}
					break;
				default:
					//print("Got a problem with the OB/DB totals. Total: " + (currentCombat.enemy1OB - CharacterManager.mySkillDBTotal));
					break;
			}
		}

		print("Damage: " + attackDamage + " || U: " + defenderUnconscious + " || K: " + defenderKilled);

		return attackDamage;
	}


	void EnablePlayerTurnUI() {
		//print("Enabling player turn UI");

		if (charmedAnimalUIParent.activeInHierarchy) {
			//animalAttackButton.SetActive(true);
			playerAttackButton.SetActive(false);
			playerSpellButton.SetActive(false);
		}
		else {
			//animalAttackButton.SetActive(false);
			playerAttackButton.SetActive(true);
			playerSpellButton.SetActive(true);
		}
		playerRunButton.SetActive(true);
	}


	void DisablePlayerTurnUI() {
		playerAttackButton.SetActive(false);
		playerSpellButton.SetActive(false);
		playerRunButton.SetActive(false);
	}


	void InitializeWeaponChoiceUI() {
		//TODO Disable all buttons to start with.
		weaponChoiceButtonMagicSword.SetActive(false);
		weaponChoiceButtonMagicBow.SetActive(false);
		weaponChoiceButtonOrcSlayingSword.SetActive(false);
		weaponChoiceButtonWarhammer.SetActive(false);
		weaponChoiceButtonBattleaxe.SetActive(false);
		weaponChoiceButtonSword.SetActive(false);
		weaponChoiceButtonDagger.SetActive(false);
		weaponChoiceButtonMace.SetActive(false);
		weaponChoiceButtonClub.SetActive(false);
		weaponChoiceButtonStaff.SetActive(false);
		weaponChoiceButtonSpear.SetActive(false);
		weaponChoiceButtonTwoHandedSword.SetActive(false);
		weaponChoiceButtonBow.SetActive(false);
		weaponChoiceButtonBareHanded.SetActive(true);

		//Determine which Weapon Buttons to enable/disable according to which weapons are in the Player's inventory
		for (int i = 0; i < mapSceneInventoryManager.inventoryParents.Length; i++) {
			GameObject objectParent = mapSceneInventoryManager.inventoryParents[i];
			if (objectParent.transform.childCount > 0) {
				InventoryScriptableReader objectScript = objectParent.transform.GetChild(0).GetComponent<InventoryScriptableReader>();

				if (objectScript.weaponScript != null) {
					switch (objectScript.weaponScript.MyWeaponType) {
						//case InventoryWeaponScriptable.weaponTypes.Default:
						//	break;
						//case InventoryWeaponScriptable.weaponTypes.BareHanded:
						//	break;
						case InventoryWeaponScriptable.weaponTypes.BattleAxe:
							weaponChoiceButtonBattleaxe.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.Bow:
							weaponChoiceButtonBow.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.Club:
							weaponChoiceButtonClub.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.Dagger:
							weaponChoiceButtonDagger.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.Mace:
							weaponChoiceButtonMace.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.Spear:
							weaponChoiceButtonSpear.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.Staff:
							weaponChoiceButtonStaff.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.Sword:
							weaponChoiceButtonSword.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.TwoHandedSword:
							weaponChoiceButtonTwoHandedSword.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.WarHammer:
							weaponChoiceButtonWarhammer.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.MagicSword:
							weaponChoiceButtonMagicSword.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.MagicBow:
							weaponChoiceButtonMagicBow.SetActive(true);
							break;
						case InventoryWeaponScriptable.weaponTypes.OrcSlayingSword:
							weaponChoiceButtonOrcSlayingSword.SetActive(true);
							break;
						default:
							break;
					}
				}
			}
		}
	}


	public void EnableWeaponChoiceUI() {
		playerAttackButton.SetActive(false);
		playerSpellButton.SetActive(false);
		playerRunButton.SetActive(false);
		weaponChoiceUI.SetActive(true);
	}


	public void DisableWeaponChoiceUI() {
		playerAttackButton.SetActive(true);
		playerSpellButton.SetActive(true);
		playerRunButton.SetActive(true);
		weaponChoiceUI.SetActive(false);
	}


	void InitializeSpellChoiceUI() {
		charmAnimalButton.SetActive(false);
		fireBoltButton.SetActive(false);
		shieldButton.SetActive(false);
		strengthButton.SetActive(false);

		for (int i = 0; i < CharacterManager.spellScriptables.Length; i++) {
			if (CharacterManager.spellScriptables[i] != null) {
				if (CharacterManager.spellScriptables[i] == charmAnimalScript) {
					charmAnimalButton.SetActive(true);
				}
				else if (CharacterManager.spellScriptables[i] == fireBoltScript) {
					fireBoltButton.SetActive(true);
				}
				else if (CharacterManager.spellScriptables[i] == shieldScript) {
					shieldButton.SetActive(true);
				}
				else if (CharacterManager.spellScriptables[i] == strengthScript) {
					strengthButton.SetActive(true);
				}
			}
charmAnimalButton.SetActive(true);
		}
	}


	public void EnableSpellChoiceUI() {
		playerAttackButton.SetActive(false);
		playerSpellButton.SetActive(false);
		playerRunButton.SetActive(false);
		spellChoiceUI.SetActive(true);
	}


	public void DisableSpellChoiceUI() {
		playerAttackButton.SetActive(true);
		playerSpellButton.SetActive(true);
		playerRunButton.SetActive(true);
		spellChoiceUI.SetActive(false);
	}
	

	void EnableCombatWinUI() {
		mapSceneManager.CloseEncounterUI();

		combatWinUI.SetActive(true);
		gameObject.SetActive(false);

		//combatActionText.text = "";
	}


	public void WinCombat () {
		if (currentCombat != null) {
			if (currentCombat.winEncounter != 0) {
				encounterBG.GetComponent<ScriptableEncounterReader>().UpdateEncounter(currentCombat.winEncounter);
				combatWinUI.SetActive(false);
				combatLoseUI.SetActive(false);
			}
		}
		else {
			encounterBG.GetComponent<ScriptableEncounterReader>().UpdateEncounter(MapSceneManager.currentEncounter.myEncounterScriptable.skipFightToEncounter);
			combatWinUI.SetActive(false);
			combatLoseUI.SetActive(false);
		}

		combatRound = 0;
		sneakAttackSuccessful = false;
		//bonusToFirstAttack = 0;
		playerSurprised = false;
		//shieldSpellActive = false;
		//strengthSpellActive = false;

		//print(charmedAnimalUIParent.activeSelf);
		if (charmedAnimalUIParent.activeSelf) {
			charmedAnimal = false;
			combatActionText.text += "\nAfter the fight, your Charmed Animal flees!";
		}
		//print(charmedAnimal);
		charmedAnimalUIParent.SetActive(false);
		enemyTargetingCharmedAnimal = false;

		mapSceneManager.CloseCombatBG();
	}


	public void EnableCombatLoseUI() {
		mapSceneManager.CloseEncounterUI();

		combatLoseUI.SetActive(true);
		gameObject.SetActive(false);

		//combatActionText.text = "";
	}


	public void LoseCombat () {
		int defeatRandomEncounter = 0;
		if (currentCombat.altLoseEncounter != 0) {
			defeatRandomEncounter = Random.Range(2, 13);

			if (defeatRandomEncounter <= 6) {
				defeatRandomEncounter = currentCombat.loseEncounter;
			}
			else {
				defeatRandomEncounter = currentCombat.altLoseEncounter;
			}
		}
		else {
			defeatRandomEncounter = currentCombat.loseEncounter;
		}

		encounterBG.GetComponent<ScriptableEncounterReader>().UpdateEncounter(defeatRandomEncounter);
		combatWinUI.SetActive(false);
		combatLoseUI.SetActive(false);

		combatRound = 0;
		sneakAttackSuccessful = false;
		//bonusToFirstAttack = 0;
		playerSurprised = false;
		//shieldSpellActive = false;
		//strengthSpellActive = false;

		//print(charmedAnimalUIParent.activeSelf);
		if (charmedAnimalUIParent.activeSelf) {
			charmedAnimal = false;
			combatActionText.text += "\nAfter the fight, your Charmed Animal flees!";
		}
		//print(charmedAnimal);
		charmedAnimalUIParent.SetActive(false);
		enemyTargetingCharmedAnimal = false;

		mapSceneManager.CloseCombatBG();
	}


	void EnableCombatRunUI() {
		mapSceneManager.CloseEncounterUI();

		combatRunUI.SetActive(true);
	}


	void DisableCombatRunUI() {
		combatRunUI.SetActive(false);
	}

}
