using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour 
{
	public bool airView = false;
	//TODO ARREGLAR LAS ROTACIONES Y BLOQUEAR BOTONONES PARA IMPEDIR ROTACION EN "TOPES DE CAMARA"
    public void RotateLeft()
	{
		if (airView == false) {
			//transform.Rotate(Vector3.up, 90, Space.Self);
			transform.Rotate (0, 90, 0);
		} else {
			//ARREGLAR
			//transform.Rotate(0, 90, 0);
		}
	}

    public void RotateRight()
    {
		if(airView == false){
			transform.Rotate(0,-90, 0);
			//transform.Rotate(Vector3.up, -90, Space.Self);
		} else {
			//ARREGLAR
			//transform.Rotate(0, -90, 0);
		}
	}

	public void AirView()
	{
		if (airView == false) {
			transform.Rotate (30, 0, 0);
			airView = true;
		}
	}

	public void NormalView()
	{
		if (airView == true) {
			airView = false;
			transform.Rotate (-30, 0, 0);
		}
	}
}
