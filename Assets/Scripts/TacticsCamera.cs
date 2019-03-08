using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour 
{

	//TODO ARREGLAR LAS ROTACIONES Y BLOQUEAR BOTONONES PARA IMPEDIR ROTACION EN "TOPES DE CAMARA"
    public void RotateLeft()
    {
        //transform.Rotate(Vector3.up, 90, Space.Self);
		transform.Rotate(0,90,0);
    }

    public void RotateRight()
    {
		transform.Rotate(0,-90,0);
        //transform.Rotate(Vector3.up, -90, Space.Self);
    }

	public void AirWiew()
	{
		transform.Rotate(30,0,0);



	}

	public void NormalView()
	{
		transform.Rotate(-30, 0, 0);
	}
}
