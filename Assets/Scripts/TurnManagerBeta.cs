using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagerBeta : MonoBehaviour {

	public int count = 4;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (count == 0) {
			inicioTurnoEquipo ();
		}
	}

	public void inicioTurnoEquipo () {
		count=4;
	}

	public void inicioTurno () {

	}

	public void finTurno () {
		count--;
	}
}
