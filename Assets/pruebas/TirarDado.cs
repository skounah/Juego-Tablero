using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TirarDado : MonoBehaviour {
	public int valor;

	/*// Use this for initialization
	void Start () {
		valor = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

	// Roll the Dice
	public void TiraDado(){
		valor = Random.Range(1,7);
		Debug.Log ("Has sacado : " + valor + ".");
	}
}
