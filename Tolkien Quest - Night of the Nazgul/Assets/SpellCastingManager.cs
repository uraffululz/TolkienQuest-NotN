using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCastingManager : MonoBehaviour {

	[SerializeField] MapSceneManager mapSceneManager;
	[SerializeField] MapSceneCharacterSheetManager mSCSM;
	[SerializeField] CombatManager combatManager;
	[SerializeField] Text overallText;
	[SerializeField] ItemListBGScript itemListScript;


	[Header("Clairvoyance")]
	[SerializeField] GameObject clairvoyanceUILeft;
	[SerializeField] Text clairvoyanceLeftHeaderText;
	[SerializeField] Text clairvoyanceLeftEncounterText;
	[SerializeField] Button clairvoyanceLeftFurtherButton;
	[SerializeField] Text clairvoyanceLeftButtonText;

	[SerializeField] GameObject clairvoyanceUIRight;
	[SerializeField] Text clairvoyanceRightHeaderText;
	[SerializeField] Text clairvoyanceRightEncounterText;
	[SerializeField] Button clairvoyanceRightFurtherButton;
	[SerializeField] Text clairvoyanceRightButtonText;

	[SerializeField] GameObject clairvoyanceChoicesUI;
	[SerializeField] GameObject choice1Button;
	[SerializeField] Text choice1ButtonText;
	[SerializeField] GameObject choice2Button;
	[SerializeField] Text choice2ButtonText;
	[SerializeField] GameObject choice3Button;
	[SerializeField] Text choice3ButtonText;
	[SerializeField] GameObject choice4Button;
	[SerializeField] Text choice4ButtonText;
	bool clairvoyanceChoice1Made;
	int clairvoyanceFurtherEncounter1;
	bool clairvoyanceChoice2Made;
	int clairvoyanceFurtherEncounter2;

	[Header("Healing")]
	public bool healSpellActive;

	[Header("Shield")]
	[SerializeField] InventoryArmorScriptable shieldScript;

	[Header("Telekinesis")]
	[SerializeField] GameObject telekinesisUI;

	
	
	void Start() {
        
    }


    void Update() {
        
    }


	public void CastItemAnalysis() {
		if (RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(3, false);
			overallText.text += "\nYou took <color=#ff0000ff>3</color> damage from casting the Item Analysis spell";

		}
		else {
			overallText.text += "\nYou failed to cast your Item Analysis spell";

		}
	}


	public void CastBalance() {
		if (!CharacterManager.balanceSpellActive && RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(2, false);
			overallText.text += "\nYou took <color=#ff0000ff>2</color> damage from casting the Balance spell.\nYour next General skill attempt at this location will have a <color=#008000ff>+2</color> bonus.";

			CharacterManager.balanceSpellActive = true;
			CharacterManager.mySkillGeneralSpecialBonuses += 2;

			mSCSM.UpdateGeneralSkillTexts();
//TOMAYBEDO Disable the Balance spell button
		}
		else {
			overallText.text += "\nYou failed to cast your Balance spell";

			if (CharacterManager.balanceSpellActive) {
				overallText.text += "\nThe Balance spell is already active!";
			}
		}
	}


	public void CastCalm() {
		if (RollSpellCheck(7)) {
			mapSceneManager.AlterDamage(5, false);
			overallText.text += "\nYou took <color=#ff0000ff>5</color> damage from casting the Calm spell!";

			combatManager.OutsmartEnemy("You calmed the enemy and slipped past him unharmed!");
		}
		else {
//TOMAYBEDO Start combat? The enemy must be hostile to cast the spell, and if the player fails, shouldn't the enemy attack?
			overallText.text += "\nYou failed to cast your Calm spell!";
		}
	}


	public void CastCamouflage() {
		if (!CharacterManager.camoSpellActive && RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(3, false);
			overallText.text += "\nYou took <color=#ff0000ff>3</color> damage from casting the Camouflage spell.\nYour next Trickery skill attempt will have a <color=#008000ff>+2</color> bonus.";

			CharacterManager.camoSpellActive = true;
			CharacterManager.mySkillTrickerySpecialBonuses += 2;

			mSCSM.UpdateTrickerySkillTexts();
//TOMAYBEDO Disable the Camouflage spell button
		}
		else {
			overallText.text += "\nYou failed to cast your Camouflage spell";

			if (CharacterManager.camoSpellActive) {
				overallText.text += "\nYour Camouflage spell is already active!";
			}
		}
	}


	public void CastCharmAnimal() {
		if (!CombatManager.charmedAnimal && combatManager.enemyTypes[0] == CombatScriptable.enemyType.Animal && RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(6, true);
			overallText.text += "\nYou took <color=#ff0000ff>6</color> damage from casting the Charm Animal spell";

			//Charm the animal
			CombatManager.charmedAnimal = true;
			combatManager.DamageEnemy(CharacterManager.myName, 0, true, false);
		}
		else {
			CombatManager.charmedAnimal = false; //Just to be sure

			overallText.text += "\nYou failed to cast your Charm Animal spell";

			if (combatManager.enemyTypes[0] != CombatScriptable.enemyType.Animal) {
				overallText.text += "\nThis spell cannot charm an enemy of this type";
			}
			else if (CombatManager.charmedAnimal) {
				overallText.text += "\nYou already have an animal charmed";
			}
		}

		combatManager.EndPlayerTurn();
	}


	public void CastClairvoyance() {
		if (MapSceneManager.currentEncounter.myEncounterScriptable.canCastClairvoyance) {
			if (RollSpellCheck(6)) {
				mapSceneManager.AlterDamage(5, true);
				overallText.text += "\nYou took <color=#ff0000ff>5</color> damage from casting the Clairvoyance spell";

				if (MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter4Index != 0) {
					//Enable the ClairvoyanceOptionsUI
					clairvoyanceChoicesUI.SetActive(true);
					clairvoyanceLeftFurtherButton.gameObject.SetActive(false);
					clairvoyanceRightFurtherButton.gameObject.SetActive(false);

					mapSceneManager.encounterTextBG.GetComponent<Animator>().SetBool("SlideTopUIOpen", false);

					//The player can choose which of the 4 available encounters to preview
					choice4Button.SetActive(true);
					choice3Button.SetActive(true);
					choice2Button.SetActive(true);
					choice1Button.SetActive(true);

					choice1ButtonText.text = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter1Text;
					choice2ButtonText.text = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter2Text;
					choice3ButtonText.text = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter3Text;
					choice4ButtonText.text = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter4Text;

				}
				else if (MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter3Index != 0) {
					//Enable the ClairvoyanceOptionsUI
					clairvoyanceChoicesUI.SetActive(true);
					clairvoyanceLeftFurtherButton.gameObject.SetActive(false);
					clairvoyanceRightFurtherButton.gameObject.SetActive(false);

					mapSceneManager.encounterTextBG.GetComponent<Animator>().SetBool("SlideTopUIOpen", false);

					//The player can choose which of the 3 available encounters to preview
					choice4Button.SetActive(false);
					choice3Button.SetActive(true);
					choice2Button.SetActive(true);
					choice1Button.SetActive(true);

					choice1ButtonText.text = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter1Text;
					choice2ButtonText.text = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter2Text;
					choice3ButtonText.text = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter3Text;
				}
				else {
					//When the encounter only has 2 outcomes
					//The player views both of the optional encounters
					clairvoyanceChoice1Made = true;
					clairvoyanceChoice2Made = true;

					clairvoyanceFurtherEncounter1 = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter1Index;
					clairvoyanceFurtherEncounter2 = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter2Index;

					clairvoyanceUILeft.SetActive(true);
					clairvoyanceUILeft.GetComponent<Animator>().SetBool("OpenClairvoyance", true);

					clairvoyanceLeftHeaderText.text = "Clairvoyance: Encounter " + clairvoyanceFurtherEncounter1.ToString();
					clairvoyanceLeftEncounterText.text = mapSceneManager.GetComponent<EncounterManager>().encounterTextScriptables[clairvoyanceFurtherEncounter1].encounterTextWhole;
					clairvoyanceLeftButtonText.text = "Go To Encounter " + clairvoyanceFurtherEncounter1;

					clairvoyanceUIRight.SetActive(true);
					clairvoyanceUIRight.GetComponent<Animator>().SetBool("OpenClairvoyance", true);

					clairvoyanceRightHeaderText.text = "Clairvoyance: Encounter " + clairvoyanceFurtherEncounter2.ToString();
					clairvoyanceRightEncounterText.text = mapSceneManager.GetComponent<EncounterManager>().encounterTextScriptables[clairvoyanceFurtherEncounter2].encounterTextWhole;
					clairvoyanceRightButtonText.text = "Go To Encounter " + clairvoyanceFurtherEncounter2;

					mapSceneManager.encounterTextBG.GetComponent<Animator>().SetBool("SlideTopUIOpen", false);
				}
			}
			else {
				overallText.text += "\nYou failed to cast your Clairvoyance spell.";
			}
		}
		else {
			Debug.Log("You cannot cast Clairvoyance here. This button should be disabled right now");
		}
	}


	public void ClairvoyanceChooseFromMultipleEncounters(int whichEncounterPreviewed) {
		if (!clairvoyanceChoice1Made) {
			//Assign properties to the first (left) UI
			switch (whichEncounterPreviewed) {
				case 1:
					clairvoyanceFurtherEncounter1 = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter1Index;
					choice1Button.SetActive(false);
					break;
				case 2:
					clairvoyanceFurtherEncounter1 = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter2Index;
					choice2Button.SetActive(false);
					break;
				case 3:
					clairvoyanceFurtherEncounter1 = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter3Index;
					choice3Button.SetActive(false);
					break;
				case 4:
					clairvoyanceFurtherEncounter1 = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter4Index;
					choice4Button.SetActive(false);
					break;
				default:
					break;
			}

			clairvoyanceUILeft.SetActive(true);
			clairvoyanceUILeft.GetComponent<Animator>().SetBool("OpenClairvoyance", true);

			clairvoyanceLeftHeaderText.text = "Clairvoyance: Encounter " + clairvoyanceFurtherEncounter1;
			clairvoyanceLeftEncounterText.text = mapSceneManager.GetComponent<EncounterManager>().encounterTextScriptables[clairvoyanceFurtherEncounter1].encounterTextWhole;
			clairvoyanceLeftButtonText.text = "Go To Encounter " + clairvoyanceFurtherEncounter1;

			clairvoyanceChoice1Made = true;
		}
		else {
			//Assign properties to the second (right) UI and finish up
			switch (whichEncounterPreviewed) {
				case 1:
					clairvoyanceFurtherEncounter2 = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter1Index;
					choice1Button.SetActive(false);
					break;
				case 2:
					clairvoyanceFurtherEncounter2 = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter2Index;
					choice2Button.SetActive(false);
					break;
				case 3:
					clairvoyanceFurtherEncounter2 = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter3Index;
					choice3Button.SetActive(false);
					break;
				case 4:
					clairvoyanceFurtherEncounter2 = MapSceneManager.currentEncounter.myEncounterScriptable.furtherEncounter4Index;
					choice4Button.SetActive(false);
					break;
				default:
					break;
			}

			clairvoyanceUIRight.SetActive(true);
			clairvoyanceUIRight.GetComponent<Animator>().SetBool("OpenClairvoyance", true);

			clairvoyanceRightHeaderText.text = "Clairvoyance: Encounter " + clairvoyanceFurtherEncounter2.ToString();
			clairvoyanceRightEncounterText.text = mapSceneManager.GetComponent<EncounterManager>().encounterTextScriptables[clairvoyanceFurtherEncounter2].encounterTextWhole;
			clairvoyanceRightButtonText.text = "Go To Encounter " + clairvoyanceFurtherEncounter2;

			clairvoyanceChoice2Made = true;

			mapSceneManager.encounterTextBG.GetComponent<Animator>().SetBool("SlideTopUIOpen", false);

			clairvoyanceLeftFurtherButton.gameObject.SetActive(true);
			clairvoyanceRightFurtherButton.gameObject.SetActive(true);

			clairvoyanceChoicesUI.SetActive(false);
		}
	}

	
	public void ConfirmClairvoyanceFurtherEncounter(int whichFurtherEncounterSelected) {
		switch (whichFurtherEncounterSelected) {
			case 1:
				MapSceneManager.currentEncounter.UpdateEncounter(clairvoyanceFurtherEncounter1);
				break;
			case 2:
				MapSceneManager.currentEncounter.UpdateEncounter(clairvoyanceFurtherEncounter2);
				break;
			default:
				break;
		}

		clairvoyanceChoice1Made = false;
		clairvoyanceChoice2Made = false;

		clairvoyanceUILeft.GetComponent<Animator>().SetBool("OpenClairvoyance", false);
		clairvoyanceUIRight.GetComponent<Animator>().SetBool("OpenClairvoyance", false);
	}


	public void CastFireBolt() {
		if (RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(6, true);
			overallText.text += "\nYou took <color=#ff0000ff>6</color> damage from casting the Firebolt spell";

			int fireDamage = (Random.Range(2, 13) + Random.Range(2, 13)) + (CharacterManager.mySkillMagicalTotal * 2);
			combatManager.DamageEnemy(CharacterManager.myName, fireDamage, false, false);
		}
		else {
			overallText.text += "\nYou failed to cast your Firebolt spell";
		}

		combatManager.EndPlayerTurn();
	}


	public void CastHealing() {
		CharacterManager.healIncrementTime = 20;
		CharacterManager.currentHealTimeElapsed = 0;

		overallText.text += "\nYou successfully cast the Heal Spell! Your wounds will recover faster for the moment!";
	}


	public void CastHealAtRest() {
		CharacterManager.healSpellActiveAtRest = true;
		overallText.text += "\nCasting Heal before resting will fully recover your endurance points";
	}


	public void CastLuck() {
		if (RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(5, false);
			overallText.text += "\nYou took <color=#ff0000ff>5</color> damage from casting the Luck spell";

		}
		else {
			overallText.text += "\nYou failed to cast your Luck spell";

		}
	}


	public void CastProtectionFromMagic() {
		if (RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(4, false);
			overallText.text += "\nYou took <color=#ff0000ff>4</color> damage from casting the Protection From Magic spell";

		}
		else {
			overallText.text += "\nYou failed to cast your Protection From Magic spell";

		}
	}


	public void CastShield() {
		if (InventoryManager.armorWornScriptable != shieldScript && RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(4, true);
			overallText.text += "\nYou took <color=#ff0000ff>4</color> damage from casting the Shield spell";

			CombatManager.shieldSpellActive = true;
		}
		else {
			overallText.text += "\nYou failed to cast your Shield spell";

			if (InventoryManager.armorWornScriptable == shieldScript) {
				overallText.text += "\nYou cannot cast this spell while using a shield";
			}

			CombatManager.shieldSpellActive = false; //Just making sure
		}

		combatManager.EndPlayerTurn();
	}


	public void CastSpeed() {
		if (RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(3, false);
			overallText.text += "\nYou took <color=#ff0000ff>3</color> damage from casting the Speed spell";

			CharacterManager.speedSpellActive = true;

			CharacterManager.mySkillRunningSpecialBonuses += 2;
			mSCSM.UpdateRunningSkillTexts();
		}
		else {
			overallText.text += "\nYou failed to cast your Speed spell";

		}
	}


	public void CastStrength() {
		if (RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(6, true);
			overallText.text += "\nYou took <color=#ff0000ff>6</color> damage from casting the Strength spell";

			CombatManager.strengthSpellActive = true;
		}
		else {
			overallText.text += "\nYou failed to cast your Strength spell";
		}
		
		combatManager.EndPlayerTurn();
	}


	public void CastSustainSelf() {
		if (RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(2, false);
			overallText.text += "\nYou took <color=#ff0000ff>2</color> damage from casting the Sustain Self spell";

			CharacterManager.ateMealToday = true;
			overallText.text += "\nYou successfully cast Sustain Self! You will not go hungry today!";
		}
		else {
			overallText.text += "\nYou failed to cast your Sustain Self spell";
		}
	}


	public void CastTelekinesis() {
		if (RollSpellCheck(6)) {
			mapSceneManager.AlterDamage(5, false);
			overallText.text += "\nYou took <color=#ff0000ff>5</color> damage from casting the Telekinesis spell";

			overallText.text += "\nYou successfully cast Telekinesis! You may attempt to steal from your opponent.";
			telekinesisUI.SetActive(true);
		}
		else {
			overallText.text += "\nYou failed to cast your Telekinesis spell";
		}
	}

	public void ConfirmTelekinesisAttempt() {
		int telekinesisRoll = Random.Range(2, 13) + CharacterManager.mySkillMagicalTotal;
		int goToEncounter = 0;

		if (telekinesisRoll > 8) {
			//If the result is more than 8, you have "outsmarted" your opponent
			//Read the text indicated (?), only taking ONE ITEM
			//The enemy is still alive
			itemListScript.canOnlyTakeOneItem = true;
			goToEncounter = MapSceneManager.currentEncounter.myEncounterScriptable.skipFightToEncounter;
			overallText.text += "\nYou succeeded in bypassing and stealing from the enemy!\nHowever, the enemy is still alive, should you choose to return.";

			if (!mapSceneManager.gameObject.GetComponent<EncounterManager>().encounterTextScriptables[MapSceneManager.currentEncounter.myEncounterScriptable.skipFightToEncounter].obtainsItems) {
				itemListScript.canOnlyTakeOneItem = false;
				overallText.text += "\nYour opponent has nothing to steal!";
			}
			else {
				overallText.text += "\nYou may take ONE of the following items.";
			}
		}
		else {
			//If the result is 8 or less, you must fight the enemy and you are surprised
			goToEncounter = MapSceneManager.currentEncounter.myEncounterScriptable.fightEncounter;
			overallText.text += "\nYou were spotted stealing from the enemy!\nYou must fight!";
			//CombatManager.playerSurprised = true;
		}

		//actionResultProceedButton.SetActive(true);

		//actionResultUI.SetActive(true);
		//mapSceneManager.CloseEncounterUI();
		telekinesisUI.SetActive(false);

		MapSceneManager.currentEncounter.UpdateEncounter(goToEncounter);
	}


	bool RollSpellCheck(int successMin) {
		bool spellSuccessful = false;
		int spellCheck = 10;//Random.Range(2, 13) + CharacterManager.mySkillMagicalTotal;
		if (spellCheck > successMin) {
			spellSuccessful = true;
		}
		return spellSuccessful;
	}
}
