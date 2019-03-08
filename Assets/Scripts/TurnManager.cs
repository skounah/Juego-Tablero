using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour 
{
    static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<TacticsMove> turnTeam = new Queue<TacticsMove>();
	static int count = 4;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		SelectUnit ();
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }

		if(count == 0)
		{
			Debug.Log ("NO QUEDAN MAS MOVIMIENTOS");
		}

	}

    static void InitTeamTurnQueue()
    {
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
		} catch {
			Debug.Log ("Jugador no clicado");
		}

		//count = 4;
        StartTurn();
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

    public static void EndTurn()
    {
        TacticsMove unit = turnTeam.Dequeue();
        unit.EndTurn();
		count--;
        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
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
			InitTeamTurnQueue();
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
					Debug.Log ("Jugador DESDE TacticaMove Update() clicado");
					TurnManager.AddUnit(hit.collider.GetComponent <TacticsMove>());
					StartTurn ();
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
}
