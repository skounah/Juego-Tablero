using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCombat : MonoBehaviour {

	public GameObject panel;
	public float currentlive = 1f;
	public bool atacked = false;


	public int live = 100;
	public int patk = 10;
	public int matk = 10;
	public int parmor = 10;
	public int marmor = 10;
	public int critchance = 1;
	public int dodgechance = 1;
	public int atkspeed = 1;
	public int armorpen = 1;
	public string description = "bla bla bla";

	public void changePanelcolor (){
		
	}

	public void Atack (float damage, GameObject target){
		//HACER ANIMACION DE ATAQUE (SIMPLE)
		//Vector3 health = panel.transform.localScale;
		currentlive -= damage;
	
		//Debug.Log(currentlive);
		//health = new Vector3(currentlive, 1f, 1f);
		if (currentlive > 0) {
			//HACER CON ANIMACION VER TUTO DE YOUTUBE
			panel.transform.localScale = new Vector3 (currentlive, 1f);
		} else {
			Debug.Log ("prueba destroy");
			Destroy (target, 1f);
			panel.transform.localScale = new Vector3 (0, 1f);
		}
		//TurnManagerBeta.currentUnit = null;
		TurnManagerBeta.currentUnit.turn = false;//PRUEBA
		TurnManagerBeta.currentAtk.atacked = true;
		TurnManagerBeta.EndUnitAtkTurn ();
	}



	void Start(){
		//panel.transform.localScale = new Vector3(.4f, 1f);
	}
	void  Update(){
		
	}


}
