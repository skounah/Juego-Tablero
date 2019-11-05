using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCombat : MonoBehaviour {

	public GameObject panel;

	public void setLifePanel (float size){
		panel.transform.localScale = new Vector3 (size, 1f);
	}

	void Start(){
		//panel.transform.localScale = new Vector3(.4f, 1f);
	}
	void  Update(){
		
	}


}
