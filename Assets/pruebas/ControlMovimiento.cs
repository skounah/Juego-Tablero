using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMovimiento : MonoBehaviour {

	List<Casilla> casillasSeleccionables = new List<Casilla>();
	GameObject[] casillas;

	Stack<Casilla> path = new Stack<Casilla> ();
	Casilla casillaActual;

	public int movimiento = 5;
	public float numCasillas = 2;
	public float movSpeed = 2;

	public Vector3 velocidad = new Vector3();
	Vector3 direccion = new Vector3();

	float halfHeight = 0;

	protected void Init(){
		
		casillas = GameObject.FindGameObjectsWithTag("Cube-Visual");
		//Debug.Log (casillas);	
		halfHeight = GetComponent<Collider>().bounds.extents.y;
		/*foreach (GameObject casilla in casillas) {
			Debug.Log ("Casilla : " + casilla);
		}*/
		Debug.Log ("HalfHeight : " + halfHeight);
	}

	public void GetCurrentCasilla(){
		//Debug.Log ("Hola1");
		casillaActual = GetTargetCasilla(gameObject);
		casillaActual.actual = true;
		Debug.Log ("Actual : " + casillaActual);
	}

	public Casilla GetTargetCasilla(GameObject target){
		//AQUI NO ENTRA
		Debug.Log ("Hola2");
		RaycastHit hit;
		Casilla casilla = null;
		//Debug.Log ("NoentraIF");
		if (Physics.Raycast (target.transform.position, -(Vector3.up) / 2f, out hit, 1)) {
			Debug.Log ("EntraIF");
			casilla = hit.collider.GetComponent<Casilla> ();
		} else {
			Debug.Log ("NOEntraIF");
		}
		return casilla;
	}
	// AQUI NO ENTRA
	public void listaCasillasAdjuntas(){
		Debug.Log ("Hola3");
		int count = 0;
		//casillas = GameObject.FindGameObjectsWithTag("Cube-Visual")
		foreach (GameObject casilla in casillas) {
			Casilla c = casilla.GetComponent<Casilla> ();
			//c.CasillasVecinas (numCasillas);
			//Debug.Log ("Variablefoeach : " + c);
			//Debug.Log ("Casillaforeach : " + casilla);
			count = count + 1;
			//Debug.Log ("ENTRA FOREACH");
		}
		Debug.Log ("Contador1 :" + count);
	}

	public void FindCasillasSeleccionables(){

		listaCasillasAdjuntas();
		GetCurrentCasilla();
		int count = 0;
		Queue<Casilla> proceso = new Queue<Casilla>();

		proceso.Enqueue(casillaActual);
		casillaActual.visited = true;
		while (proceso.Count>0){
			Casilla c = proceso.Dequeue();
			casillasSeleccionables.Add(c);
			c.seleccionable = true;

			if(c.distance<movimiento){
				foreach(Casilla casilla in c.adyacentes){
					if(!casilla.visited){
						casilla.parent = c;
						casilla.visited = true;
						casilla.distance = 1 + c.distance;
						proceso.Enqueue (casilla);
						count++;
					}
				}
				//Debug.Log ("Contador2 :" + count);
			}

		}
	}
}