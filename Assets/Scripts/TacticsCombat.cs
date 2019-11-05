using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCombat : MonoBehaviour {

	public GameObject panel;
	public float currentlive = 1f;

	public void setLifePanel (float damage){
		Vector3 health = panel.transform.localScale;
		currentlive -= damage;
		Debug.Log(currentlive);
		//health = new Vector3(currentlive, 1f, 1f);
		if (currentlive > 0) {
			panel.transform.localScale = new Vector3 (currentlive, 1f);
		} else {
			panel.transform.localScale = new Vector3 (0, 1f);
		}

	}

	void Start(){
		//panel.transform.localScale = new Vector3(.4f, 1f);
	}
	void  Update(){
		
	}


}
