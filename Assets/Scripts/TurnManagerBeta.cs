using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagerBeta : MonoBehaviour {

	public int count = 4;
	static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>(); //??
	static Queue<string> turnKey = new Queue<string>(); // TURNO PARA CADA EQUIPO
	static TacticsMove currentUnit;
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
