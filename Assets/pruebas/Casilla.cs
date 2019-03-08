using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla : MonoBehaviour {

	public bool actual = false;
	public bool seleccionado = false;
	public bool seleccionable = false;
	public bool andable = true;

	public List<Casilla> adyacentes = new List<Casilla> ();

	//BFS "RUTA"
	public bool visited= false;
	public Casilla parent = null;
	public int distance = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (actual) {
			GetComponent<Renderer> ().material.color = Color.yellow;
		} else if (seleccionado) {
			GetComponent<Renderer> ().material.color = Color.blue;
		} else if (seleccionable) {
			GetComponent<Renderer> ().material.color = Color.red;
		} else {
			//GetComponent<Renderer> ().material.color = Color.white;
		}
	}

	public void Reset(){
		actual = false;
		seleccionado = false;
		seleccionable = false;

		adyacentes.Clear ();

		visited = false;
		parent = null;
		distance = 0;
	}

	public void CasillasVecinas(float numCasillas){
		Reset ();
		CompruebaCasilla (Vector3.forward,numCasillas);
		//CompruebaCasilla (Vector3.forward);
		CompruebaCasilla (Vector3.right,numCasillas);
		//CompruebaCasilla (Vector3.forward);
		CompruebaCasilla (-Vector3.forward,numCasillas);
		//CompruebaCasilla (Vector3.forward);
		CompruebaCasilla (-Vector3.right,numCasillas);
		//CompruebaCasilla (Vector3.forward);
	}

	public void CompruebaCasilla(Vector3 direction, float numCasillas){
		Vector3 mediaExtension = new Vector3 (0.25f, (1+numCasillas)/2f, 0.25f);
		Collider[] colliders = Physics.OverlapBox (transform.position + direction, mediaExtension);
		Debug.Log ("Hola4");
		foreach (Collider item in colliders) {
			Casilla casilla = item.GetComponent<Casilla>();
			if (casilla != null && casilla.andable) {
				RaycastHit hit;
				//Debug.Log ("Debug clase casilla : " + casilla);
				if(!Physics.Raycast(casilla.transform.position,Vector3.up, out hit,1)){//si no
					adyacentes.Add (casilla);
				}
			}
		}
	}
}
