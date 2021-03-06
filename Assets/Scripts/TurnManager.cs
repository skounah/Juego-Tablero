﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour 
{
    static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();
    static Queue<string> turnKey = new Queue<string>(); // TURNO PARA CADA EQUIPO
    static Queue<TacticsMove> turnTeam = new Queue<TacticsMove>(); // TURNO PARA CADA FICHA - VER COMO CAMBIAR PARA QUE NO SEA COLA

	public Text countText; 
	public static int count;
	static TacticsMove currentUnit;


	// Use this for initialization
	void Start () 
	{
		//count = 4;
		//countText = GameObject.Find ("Movimientos").GetComponent<Text>();
		countText.text = "Movimientos restantes : " + count.ToString();
		//SetMovText ();
		InitTeamTurnQueue();
	}
	
	// Update is called once per frame
	void Update () 
	{
		SelectUnit ();
        /*if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }*/

		if(count == 0)
		{
			InitTeamTurnQueue ();
			Debug.Log ("NO QUEDAN MAS MOVIMIENTOS");
		}

		//PRUEBA
		//countText.text = "Movimientos restantes : " + count.ToString();

	}

    static void InitTeamTurnQueue()
    {
		count = 4;
		try
		{
			List<TacticsMove> teamList = units[turnKey.Peek()];
	        foreach (TacticsMove unit in teamList)
	        {
	            turnTeam.Enqueue(unit);
				Debug.Log (unit);
				//Debug.Log (count);
				//Debug.Log (turnTeam);
	        }
			StartTurn2();
		} catch 
		{
			Debug.Log ("Jugador no clicado");
		}
    }

    public static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
			Debug.Log (count);
			//Debug.Log (turnTeam);
			//Debug.Log (turnTeam.Count);
        }
    }
	public static void StartTurn2()
	{
		if (count > 0)
		{
			currentUnit.BeginTurn();
			Debug.Log (count);
			//Debug.Log (turnTeam);
			//Debug.Log (turnTeam.Count);
		}
	}
    public static void EndTurn()
    {
        TacticsMove unit = turnTeam.Dequeue();
        unit.EndTurn();
		count--;
		//SetMovText ();
        if (turnTeam.Count > 0)
        {
            StartTurn2();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            //InitTeamTurnQueue();
        }
    }

	public void ForceEndTurn(){
		Debug.Log ("FIN DE TURNO");
		TacticsMove unit = turnTeam.Dequeue();
		unit.EndTurn();

		if (turnTeam.Count > 0)
		{
			StartTurn();
		}
		else
		{
			string team = turnKey.Dequeue();
			turnKey.Enqueue(team);
			//InitTeamTurnQueue();
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
					Debug.Log ("Jugador DESDE Turn Manager clicado");
					TurnManager.AddUnit(hit.collider.GetComponent <TacticsMove>());
					currentUnit = hit.collider.GetComponent <TacticsMove> ();
					StartTurn2 ();//OJO 
					//InitTeamTurnQueue();
				}
			}
		}
	}

    public static void AddUnit(TacticsMove unit)
    {
        List<TacticsMove> list;
        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TacticsMove>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }

	/*public static void SetMovText(){
		countText.text = "Movimientos restantes : " + count.ToString();
	}*/
}
