using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagerBeta : MonoBehaviour {

	public int count = 4;
	static TacticsMove currentUnit;
	//static bool turno;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		SelectUnit ();
		if (count == 0) {
			InicioTurnoEquipo ();
		}
	}

	public void InicioTurnoEquipo () 
	{
		count=4;
	}

	public void CambioTurnoEquipo()
	{
	
	}

	public void InicioJugada () 
	{
		currentUnit.BeginTurn();
	}

	public void FinTurno ()
	{
		count--;
		currentUnit.EndTurn();
		currentUnit.moved = true;
		if (count == 0) 
		{
			CambioTurnoEquipo ();
		}
	}

	public void SelectUnit()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) 
			{
				if (hit.collider.tag == "Player") 
				{
					//HACER IF PARA COMPROBAR SI YA SE HA MOVIDO.
					Debug.Log ("Jugador clicado");
					currentUnit = hit.collider.GetComponent <TacticsMove> ();
					InicioJugada ();
				}
			}
		}
	}


}
