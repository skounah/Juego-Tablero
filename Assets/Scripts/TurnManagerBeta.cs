using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//NOTA 03/07/2019:
//ARREGLAR EL NUMERO DE MOVIMIENTOS PARA EL CAMBIO DE TURNO EN EL TURNO DE NPC HACER QUE LOS NPC
//SE MUEVAN 1 a 1 NO TODOS A LA VEZ Y REVISAR EL LA ACTUALIZACION DE MOVED PARA LOS CAMBIOS DE TURNO

public class TurnManagerBeta : MonoBehaviour {
	
	public Text countMovText;
	public Text countAtkText;
	public static int countMovs = 4;
	public static int countAtks = 4;
	public static TacticsMove currentUnit;
	public static TacticsMove currentEnemy;

	//PRUEBAS PANELES
	public GameObject panel;
	public static TacticsCombat enemyHealthBar;

	public static bool npcTurn = false;
	static Queue<TacticsMove> turnKey = new Queue<TacticsMove>(); // TURNO PARA CADA EQUIPO


	void Start () 
	{
		currentUnit = null;
		currentEnemy = null;
		enemyHealthBar = null;

		countMovText.text = "Movimientos restantes : " + countMovs.ToString();

	}

	// Update is called once per frame
	void Update () 
	{
		SelectUnit ();
		SelectEnemy ();
		countMovText.text = "Movimientos restantes : " + countMovs.ToString();
	}

	static void InitPlayerTurn() {
		//Debug.Log ("TurnoJugador");
		countMovs = 4;
		GameObject[] units = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject unit in units) {
			TacticsMove player = unit.GetComponent<TacticsMove>();
			player.moved = false;
		}
		//HACER EL CAMBIO DE NPC A PLAYER 
		
	}

	static void InitNPCTurn() {
		//Debug.Log ("TurnoNPC");
		countMovs = 4;
		GameObject[] units = GameObject.FindGameObjectsWithTag("NPC");
		foreach(GameObject unit in units){
			TacticsMove npc = unit.GetComponent<TacticsMove>();
			currentUnit = npc;
			//PRUEBA
			NPCMove prueba = unit.GetComponent<NPCMove> ();
			prueba.FindNearestTarget ();

			currentUnit.BeginTurn ();
				
		}
		InitPlayerTurn ();
	}

	static void InitUnitTurn(){
		if (countMovs > 0) {
			currentUnit.BeginTurn ();
			/*Poner De momento aqui*/
			GameObject[] units = GameObject.FindGameObjectsWithTag("NPC");
			foreach (GameObject unit in units) {
				TacticsMove npc = unit.GetComponent<TacticsMove>();
				npc.moved = false;
			}
		}
	}

	public static void EndUnitTurn(){
		
		countMovs--;
		if (countMovs == 0) { //FALTAN LOS ATAQUES
			//npcTurn = true;
			InitNPCTurn (); // VER COMO CAMBIAR TURNO AL OTRO JUGADOR
		}
	}

	public void SelectUnit()
	{
		if (Input.GetMouseButtonDown (0)) 
		{ 
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			panel.SetActive(false);
			if (Physics.Raycast (ray, out hit)) 
			{
				if (hit.collider.tag == "Player") {
					//Debug.Log ("Jugador " + currentUnit + "clicado");

					if (currentUnit != null) {
						currentUnit.turn = false;//PARA LIMPIAR LA ACTUAL AL CLICAR OTRA

					}

					currentUnit = hit.collider.GetComponent <TacticsMove> ();
					if (currentUnit.moved == true) {
						Debug.Log ("Unidad ya movida, solo puede Atacar");
					} else {
						panel.SetActive (true);
						InitUnitTurn ();
					}

				} else { //SI CLICAS ALGO QUE NO SEA LA FICHA -> CREO QUE SOBRA
					//currentUnit = null;
				}
			}
		}
	}

	//PRUEBAS PARA ATACAR VER SI PONER EN EL UPDATE O DENTRO DEL SELECT UNIT
	public void SelectEnemy()
	{
		if (Input.GetMouseButtonDown (0)) { 
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "NPC") {
					//Debug.Log ("Rival clicado");
					currentEnemy = hit.collider.GetComponent <TacticsMove> ();
					enemyHealthBar = hit.collider.GetComponent <TacticsCombat> ();
					Debug.Log (currentEnemy + " prueba.");
					if (currentUnit.turn != false) {
						//Debug.Log ("Iniciar proceso de ataque");
						//PRUEBA SOLO VALIDA PARA RANGO DE ATAQUE 1.
						//CAMBIAR ADJENCYLIST POR UNA LISTA cON LAS CASILLAS EN RANGO. POR ESO NO FUNCIONA

						List<Tile> adjacencyList = currentUnit.currentTile.playerOnRangeList;
						foreach (Tile tile in adjacencyList) {
							Debug.Log ("entra a las casillas");
							if (tile.enemyHere == true) {
								Debug.Log ("Enemigo fijado iniciar ataque");
								//PRUEBAS ATQUE
								enemyHealthBar.setLifePanel(0.4f);
							}
						}
						//VER COMO COMPROBAR SI ESTA EN RANGO DE ATAQUE 
						//SI ESTA EN RANGO DE ATAQUE
							//ATACAR

						// ELSE (NO EN RANGO)
								//SI SE HA MOVIDO
						/*if (currentUnit.moved == true) {
								Debug.Log ("No puedes hacer nada más");
							} else {
								//MOVER A CASILLA MAS CERCANA Y VOLVER A COMPRAROVAR SI ESTA EN RANGO DE ATAQUE
							}*/
						//}
					} else { 
						Debug.Log ("No hay currentUnit");
					}
					/*} else { //SI CLICAS ALGO QUE NO SEA LA FICHA
						Debug.Log("Agua");
						currentEnemy = null;
					}*/
				}
			}
		}
	}
}