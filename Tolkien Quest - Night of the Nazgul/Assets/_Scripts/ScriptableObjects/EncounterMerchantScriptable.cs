using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewMerchantScriptable", menuName = "ScriptableObjects/EncounterMerchantScriptable", order = 0)]
public class EncounterMerchantScriptable : ScriptableObject {

	public string[] itemsForSale;
	public int[] itemPrices;
	public enum coinTypes { silver, copper }
	public coinTypes[] coinType;


	void Start() {
        
    }


    void Update() {
        
    }
}
