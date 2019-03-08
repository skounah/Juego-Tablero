using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovFicha : ControlMovimiento{

	// Use this for initialization
	void Start () {
		Init ();
		FindCasillasSeleccionables ();
	}
	
	// Update is called once per frame
	void Update () {
		FindCasillasSeleccionables ();
	}
}
