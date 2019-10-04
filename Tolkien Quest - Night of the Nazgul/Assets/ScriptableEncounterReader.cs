using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableEncounterReader : MonoBehaviour {

	[SerializeField] EncounterScriptable myEncounterScriptable;

	[SerializeField] GameObject mapSceneManager;

	[SerializeField] GameObject merchantUI;



	void Awake () {
		//SelectScriptable();
    }


    void Update() {
        
    }


	public void ChooseLocationBGEncounter (int whichTileEncounterVariable) {
		int encounterIndex = 0;
		if (whichTileEncounterVariable == 1) {
			encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex1;
		}
		else if (whichTileEncounterVariable == 2) {
			encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex2;
		}
		else if (whichTileEncounterVariable == 3) {
			encounterIndex = MapSceneManager.currentLocation.GetComponent<ScriptableMapTileReader>().encounterOptionIndex3;
		}

		print(encounterIndex.ToString());
	}


	public void UpdateScriptable (int encounterIndex) {
		myEncounterScriptable = mapSceneManager.GetComponent<EncounterManager>().encounterTextScriptables[encounterIndex];

		GetComponentInChildren<Text>().text = myEncounterScriptable.encounterText[0];

		if (myEncounterScriptable.isMerchant) {
			OpenMerchantUI();
		}
	}


	void OpenMerchantUI () {
		merchantUI.GetComponent<Animator>().SetBool("SlideIn", true);

	}
}
