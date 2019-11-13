using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//NOTA 03/07/2019: NO ARREGLADO
//ARREGLAR EL NUMERO DE MOVIMIENTOS PARA EL CAMBIO DE TURNO. EN EL TURNO DE NPC HACER QUE LOS NPC
//SE MUEVAN 1 a 1 NO TODOS A LA VEZ Y REVISAR EL LA ACTUALIZACION DE MOVED PARA LOS CAMBIOS DE TURNO
//NOTA 13/11/2019: NO ARREGLADO
//AREGLAR EL SETEO DE COUNTMOVS CUANDO ACABA TURNO DE EL NPC SE SETEA A 0 DEBERIA SETEARSE a 4

public class TurnManagerBeta : MonoBehaviour {
	
	public Text countMovText;
	public Text countAtkText;
	public TextMeshProUGUI endGameText;

	public static int countMovs = 4;
	public static int countAtks = 4;
	public static TacticsMove currentUnit;
	public static TacticsMove currentEnemy;
	public static TacticsCombat currentAtk;


	//PRUEBAS PANELES
	public GameObject panel;
	public GameObject endGame;
	public static TacticsCombat enemyHealthBar;

	//public static bool npcTurn = false;
	//static Queue<TacticsMove> turnKey = new Queue<TacticsMove>(); // TURNO PARA CADA EQUIPO

	void Start () 
	{
		currentUnit = null;
		currentEnemy = null;
		enemyHealthBar = null;
		currentAtk = null;
		endGameText = gameObject.GetComponent<TextMeshProUGUI>();
		countMovText.text = "Movimientos restantes : " + countMovs.ToString();
		countAtkText.text = "Movimientos restantes : " + countAtks.ToString();

	}

	// Update is called once per frame
	void Update () 
	{
		SelectUnit ();
		SelectEnemy ();
		EndGame ();
		countAtkText.text = "Ataques restantes : " + countAtks.ToString (); // VER SI ACTUALIZAR SOLO AL ACABAR ATK 
		countMovText.text = "Movimientos restantes : " + countMovs.ToString(); // VER SI ACTUALIZAR SOLO AL ACABAR MOVIMIENTO 
	}

	public void EndGame(){
		GameObject[] units = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] rivals = GameObject.FindGameObjectsWithTag("NPC");
		//Debug.Log (units.Length);
		//Debug.Log (rivals.Length);
		if (units.Length == 0) {
			endGame.SetActive (true);
			//endGameText.text = "DERROTA";

		}
		if (rivals.Length == 0) {
			endGame.SetActive (true);
			//endGameText.text = "DERROTA";
		}
	}

	public static void InitPlayerTurn() {
		Debug.Log ("TurnoJugador");
		GameObject[] units = GameObject.FindGameObjectsWithTag("Player");
		Debug.Log (units.Length);
		countMovs = units.Length;
		countAtks = units.Length;
		foreach (GameObject unit in units) {
			TacticsMove player = unit.GetComponent<TacticsMove>();
			TacticsCombat combat = unit.GetComponent<TacticsCombat>();
			combat.atacked = false;
			player.moved = false;
			player.turn = false;
			player.moveRange = 4; //CAMBIAR POR EL VALOR QUE TOQUE A CADA "HEROE"
		}
		//HACER EL CAMBIO DE NPC A PLAYER 
		
	}

	public static void InitNPCTurn() {
		Debug.Log ("TurnoNPC");
		GameObject[] units = GameObject.FindGameObjectsWithTag("NPC");
		Debug.Log (units.Length);
		countMovs = units.Length;
		countAtks = 0;//units.Length;
		foreach(GameObject unit in units){
			TacticsMove npc = unit.GetComponent<TacticsMove>();
			//npc.moved = false;
			currentUnit = npc;
			//PROBAR EL INVOKE REPEATING
			NPCMove prueba = unit.GetComponent<NPCMove> ();
			prueba.FindNearestTarget ();
			currentUnit.BeginTurn ();
				
		}
		InitPlayerTurn (); //??? - REVISAR POR QUE NO LO SETEA A 4 Y LO SETEA A 0
	}

	public static void InitUnitTurn(){
		if (countMovs > 0) {
			currentUnit.BeginTurn ();
			/*Poner De momento aqui - es para que funcione bien el turno de los npc si lo quitas no se le va el turno a los npc  */
			GameObject[] units = GameObject.FindGameObjectsWithTag("NPC");
			foreach (GameObject unit in units) {
				TacticsMove npc = unit.GetComponent<TacticsMove>();
				npc.moved = false;
			}
		}
	}
		
	public static void EndUnitMoveTurn(){
		countMovs--;
		//Image img = currentAtk.panel.GetComponent<Image> ();
		//img.color = Color.green;
		//PRUEBA PARA NO PINTAR LAS CASILLAS DMOVIIMIENTO CUANDO SE HA MOVIDO
		//currentUnit.moveRange = 0;
		if (countMovs == 0 && countAtks == 0) {
			//npcTurn = true;
			InitNPCTurn (); // VER COMO CAMBIAR TURNO AL OTRO JUGADOR (4v4)
		}
	}

	public static void EndUnitAtkTurn(){
		//Debug.Log ("prueba");
		countAtks--;
		//Image img = currentAtk.panel.GetComponent<Image> ();
		//img.color = Color.green;
		if (countMovs == 0 && countAtks == 0) { //FALTAN LOS ATAQUES
			//npcTurn = true;
			InitNPCTurn (); // VER COMO CAMBIAR TURNO AL OTRO JUGADOR (4v4)
		}
	}

	//METODO EN PRUEBAS
	public void changeTurn(){
		if (currentUnit != null && currentAtk.atacked == false) {
			currentAtk.atacked = true;
			EndUnitAtkTurn ();
		} else {
			Debug.Log ("No hay current unit");
		}
	}

	public void SelectUnit()
	{
		if (Input.GetMouseButtonDown (0)) 
		{ 
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			panel.SetActive(false);
			//Image img1 = currentAtk.panel.GetComponent<Image> ();
			//img1.color = Color.green;
			if (Physics.Raycast (ray, out hit)) 
			{
				if (hit.collider.tag == "Player") {
					//Debug.Log ("Jugador " + currentUnit + "clicado");

					if (currentUnit != null) {
						currentUnit.turn = false;//PARA LIMPIAR LA ACTUAL AL CLICAR OTRA
						Image img1 = currentAtk.panel.GetComponent<Image> ();
						img1.color = Color.green;
					}

					currentUnit = hit.collider.GetComponent <TacticsMove> ();
					currentAtk = hit.collider.GetComponent <TacticsCombat> ();
					//PRUEBAS
					Image img = currentAtk.panel.GetComponent<Image> ();
					img.color = Color.blue;
					if (currentUnit.moved == true) {
						currentUnit.moveRange = 0;
						Debug.Log ("Esta unidad ya se ha movido.");
					} //else {
					panel.SetActive (true); // panel de skills
					InitUnitTurn ();

					//}

				}/* else if (hit.collider.tag == "Player"){ //SI CLICAS ALGO QUE NO SEA LA FICHA -> CREO QUE SOBRA
					//currentUnit = null;
				} else {
					currentUnit = null;
				}*/
			}
		}
	}

	//PRUEBAS PARA ATACAR FALTAN MUCHAS COMPROVACIONES COMO VER SI A ATACADO
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
					//Debug.Log (currentEnemy + " prueba de Log.");
					if (currentUnit.turn != false) {
						//Debug.Log ("Iniciar proceso de ataque");
						//PRUEBA SOLO VALIDA PARA RANGO DE ATAQUE 1.
						// !!! CAMBIAR ADJENCYLIST POR UNA LISTA cON LAS CASILLAS EN RANGO. POR ESO NO FUNCIONA(X) - REVISAR playerOnRangeList(V);

						List<Tile> adjacencyList = currentUnit.currentTile.playerOnRangeList;
						foreach (Tile tile in adjacencyList) {
							//Debug.Log ("entra a las casillas");
							if (tile.enemyHere == true) {
								//Debug.Log ("Enemigo fijado iniciar ataque.");
								//TacticsCombat currentAtk = hit.collider.GetComponent <TacticsCombat> ();
								//PRUEBAS ATQUE
								if (currentAtk.atacked == false) {
									//currentAtk.Atack ();
									enemyHealthBar.Atack (0.4f, hit.collider.gameObject);
								} else {
									Debug.Log ("Esta unidad ya ha atacado.");
								}
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