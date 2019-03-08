using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ControlCamara : MonoBehaviour 
{
	public void RotateLeft()
	{
		transform.Rotate(Vector3.up, 90, Space.Self);
		Debug.Log ("Has Pulsado rotar izquierda");
	}

	public void RotateRight()
	{
		transform.Rotate(Vector3.up, -90, Space.Self);
		Debug.Log ("Has Pulsado rotar derecha");
	}
}
