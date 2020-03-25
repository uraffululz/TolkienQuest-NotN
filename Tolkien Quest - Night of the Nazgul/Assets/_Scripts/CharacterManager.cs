using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterManager {
	//TOMAYBEDO Change this class into a SINGLETON? What's the difference?
	//Name
	public static bool nameSet;
	public static string myName;

	//TODO Make this an enum (not possible as a static), or something else?
	//Race
	public static bool raceSet;
	public enum Races {Default, Dwarf, Elf, Hobbit, Human};
	public static Races myRace;

	//Stats
	public static bool allStatsSet;

	public static int myStatStrength;
	public static int myStatAgility;
	public static int myStatIntelligence;

	public static int myStatStrengthBonus;
	public static int myStatAgilityBonus;
	public static int myStatIntelligenceBonus;

	public static int enduranceTotal = 10;
	public static int damageTaken;

	public static bool statusDiseased;
	public static int diseaseTimer;

	//Skills
	public static bool allSkillsSet;

	public static int skillPointsAvailable;

	public static int mySkillMeleeOBTotal;
	public static int mySkillMeleeOBSkillBonus;
	public static int mySkillMeleeOBStatBonus;
	public static int mySkillMeleeOBSpecialBonuses;

	public static int mySkillMissileOBTotal;
	public static int mySkillMissileOBSkillBonus;
	public static int mySkillMissileOBStatBonus;
	public static int mySkillMissileOBSpecialBonuses;

	public static int sneakAttackBonus;

	public static int mySkillDBTotal;
	public static int mySkillDBStatBonus;
	public static int mySkillDBSpecialBonuses;

	public static int mySkillRunningTotal;
	public static int mySkillRunningStatBonus;
	public static int mySkillRunningSpecialBonuses;

	public static int mySkillGeneralTotal;
	public static int mySkillGeneralSkillBonus;
	public static int mySkillGeneralStatBonus;
	public static int mySkillGeneralSpecialBonuses;

	public static int mySkillTrickeryTotal;
	public static int mySkillTrickerySkillBonus;
	public static int mySkillTrickeryStatBonus;
	public static int mySkillTrickerySpecialBonuses;

	public static int mySkillPerceptionTotal;
	public static int mySkillPerceptionSkillBonus;
	public static int mySkillPerceptionStatBonus;
	public static int mySkillPerceptionSpecialBonuses;

	public static int mySkillMagicalTotal;
	public static int mySkillMagicalSkillBonus;
	public static int mySkillMagicalStatBonus;
	public static int mySkillMagicalSpecialBonuses;

	//Spell Slot Variables
	public static bool allSpellsSet;

	public static string[] assignedSpells = new string[7] {"","","","","","",""};

	public static int totalTimeTaken;
	public static int currentDayTimeTaken;
	public static int daysTaken;

	public static int totalXP;


	public static bool metTom;
	public static bool metGildor;
}
