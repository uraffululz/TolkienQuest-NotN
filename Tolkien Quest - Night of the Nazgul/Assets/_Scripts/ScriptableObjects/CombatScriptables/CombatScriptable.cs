using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCombatScriptable", menuName = "ScriptableObjects/CombatScriptable", order = 0)]
public class CombatScriptable : ScriptableObject {

	[SerializeField] [TextArea(2, 5, order = 0)] string[] notes;

	public enum enemyType{None, Animal, Humanoid, Nazgul, Orc, Troll, Wight};

	public int enemyCount;

	public enemyType[] enemyTypes;
	public string[] enemyNames;
	public int[] enemyOB;
	public int[] enemyDB;
	public int[] enemyEP;

	[Space]

	public bool hasRandomEnemy;
	public int enemy1RangeMax;
	public int enemy2RangeMax;
	public int enemy3RangeMax;
	public int enemy4RangeMax;
	[TextArea(2, 4, order = 0)]  public string outOfEnemyRangeDescription;

	public enemyType[] randomEnemyTypes;
	public string[] randomEnemyNames;
	public int[] randomEnemyOB;
	public int[] randomEnemyDB;
	public int[] randomEnemyEP;

	[Space]

	public bool playerSurprised;
	public int firstAttackBonus;
	public bool playerCannotSneakAttack;
	public bool mustDefeatFirstEnemyToRun;
	public int penaltyToRun;
	public int runGoesToEncounter;
	public bool canPlayDead;

	[Space]

	public int winEncounter;
	public int loseEncounter;
	//If defeat has multiple possible encounters
	public int altLoseEncounter;
	
}
