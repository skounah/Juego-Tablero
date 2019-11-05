using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class Tile : MonoBehaviour//, IDropHandler, IPointerEnterHandler, IPointerExitHandler 
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
	public bool atackable = false;
	public bool aliHere = false;
	public bool enemyHere = false;
	public bool dropAliArea = false;
	public bool dropEnemyArea = false;


    public List<Tile> adjacencyList = new List<Tile>();
	public List<Tile> playerOnRangeList = new List<Tile> ();

    //Needed BFS (breadth first search)
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    //For A*
    public float f = 0;
    public float g = 0;
    public float h = 0;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (current/* && !enemyHere && !aliHere*/)
        {
			GetComponent<Renderer> ().material.color = Color.green;//new Color(1f,0.5f,0f,1f);
        }
        else if (target /*&& !enemyHere && !aliHere*/)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
		else if (selectable && !enemyHere && !aliHere && !atackable)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
		}
		else if (atackable){
			GetComponent<Renderer>().material.color = Color.red;
		}
		else if (aliHere)
		{
			GetComponent<Renderer>().material.color = Color.blue;
		}
		else if (enemyHere)
		{
			GetComponent<Renderer>().material.color = Color.red;
		}
		else if(dropEnemyArea || dropAliArea){
			
		}
        else
        {
			GetComponent<Renderer>().material.color = Color.white;//new Color(0.7f,0.7f,0f,1f);
        }
	}

    public void Reset()
    {
        adjacencyList.Clear();
		playerOnRangeList.Clear ();

        current = false;
        target = false;
        selectable = false;
		aliHere = false;
		enemyHere = false;
		atackable = false;
        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;
    }

    public void FindNeighbors(float jumpHeight, Tile target)
    {
        Reset();

        CheckTile(Vector3.forward, jumpHeight, target);
        CheckTile(-Vector3.forward, jumpHeight, target);
        CheckTile(Vector3.right, jumpHeight, target);
        CheckTile(-Vector3.right, jumpHeight, target);
		CheckTile2(Vector3.zero, jumpHeight, target);
    }



    public void CheckTile(Vector3 direction, float jumpHeight, Tile target)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();

            if (tile != null && tile.walkable)
            {
                RaycastHit hit;

				// SI NO HAY UNA FICHA ENCIMA 
				if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target) )
				{
					adjacencyList.Add(tile);
				}
				//PRUEBAS
				if (Physics.Raycast (tile.transform.position, Vector3.up, out hit, 1) || (tile == target)) 
				{
					playerOnRangeList.Add (tile);
				}	
            }
        }
    }

	//PRUEBAS DE COMPROVACION DE CASILLA(PARA LA LISTA DEL MOV)
	public void CheckTile2(Vector3 direction, float jumpHeight, Tile target)
	{
		Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
		Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

		foreach (Collider item in colliders)
		{
			Tile tile = item.GetComponent<Tile>();

			if (tile != null && tile.walkable)
			{
				RaycastHit hit;

				// SI HAY UNA FICHA ENCIMA !! REVISAR
				if (Physics.Raycast (tile.transform.position, Vector3.up, out hit, 1) || (tile == target)) {
					//COMPROBAR SI CHOCA CON OBJETO ALI O RIVAL
					if (hit.transform.tag == "NPC") {
						enemyHere = true;

					} 
					if (hit.transform.tag == "Player") {
						aliHere = true;
					}
					//playerList.Add(tile);
					//adjacencyList.Add(tile);

				}
			}
		}
	}


	//PRUEBAS PARA DESPLEGAR FICHAS MAS ADELANTE
	//public Arrastrar.Slot tipoCasilla = Arrastrar.Slot.MANO;//cojer de casilla tablero

	/*public void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log("OnPointerEnter");
	}

	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log("OnPointerExit");
	}

	public void OnDrop(PointerEventData eventData) {
		
		Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);
		Arrastrar d = eventData.pointerDrag.GetComponent<Arrastrar>();
		if(d != null) {
			if (d.yaJugada != true) {
					//HACER TRANSFORM
					d.parentToReturnTo = this.transform;
			}
		}
	}*/
}
