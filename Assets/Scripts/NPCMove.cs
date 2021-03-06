﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCMove : TacticsMove 
{
    GameObject target;
	public GameObject panel;

	// Use this for initialization
	void Start () 
	{
        Init();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Debug.DrawRay(transform.position, transform.forward);
		//FindNearestTarget();
        if (!turn)
        {
            return;
        }

        if (!moving /*&& !moved*/)
        {
			FindSelectableTiles();
            FindNearestTarget();
            CalculatePath();
            //actualTargetTile.target = true;
        }
        else
        {
			//InvokeRepeating ("Move", 0, 0.4f);
            Move();
        }
		if (moved == true) {
			moving = false;
			EndTurn ();
		}
	}

	public void OnMouseEnter(){
		Image img = panel.GetComponent<Image> ();
		img.color = Color.yellow;
	}

	public void OnMouseExit(){
		Image img = panel.GetComponent<Image> ();
		img.color = Color.red;
	}


    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    public void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);

            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }
}
