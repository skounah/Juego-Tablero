using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove 
{

	// Use this for initialization
	void Start () 
	{
        Init();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Debug.DrawRay(transform.position, transform.forward);

		//HACER SPAWN 
		if(!desplegada){
			//turn = false;
			//DRAG AND DROP
		}
			
        if (!turn)
        {
            return;
        }

        if (!moving)
        {
            FindSelectableTiles();
			CheckMouseForTile ();
			//CheckMouseForCurrent ();
        }
        else
        {
            Move();
        }
	}

    void CheckMouseForTile()
	{
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) 
			{
				if (hit.collider.tag == "Tile") 
				{
					Tile t = hit.collider.GetComponent<Tile> ();
					if (t.selectable) 
					{
						MoveToTile (t);
					}
				}
			}
		}
	}


	/*USAR PARA SELECCIONAR RIVALES O ALIADOS 
	 * void CheckMouseForCurrent()
	{
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) 
			{
				if (hit.collider.tag == "Player") 
				{
					//Tile t = hit.collider.GetComponent<PlayerMove> ();
					//GetCurrentTile2 ();
					Debug.Log ("Jugador clicado");
					//GetTargetTile (gameObject);
					//FindSelectableTiles ();
				}
			}
		}
	}*/

}
