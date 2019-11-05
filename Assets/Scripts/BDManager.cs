using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDManager : MonoBehaviour {

	public GameObject prefabDragBox;
	public GameObject prefabDropBox;

	void Start(){
		CallBDQuerys ();
	}

	void Update() {
		//SelectHeros ();
		//Debug.Log ("Probando ");
	}

	public void CallBDQuerys(){
		StartCoroutine (SelectHeros ());
		StartCoroutine (SelectHerosTeam ());
	}

    

	IEnumerator SelectHerosTeam(){
		WWW request = new WWW ("http://localhost/myGame/selectHerosTeam.php");
		//Debug.Log ("prueba");
		yield return request;
		//Debug.Log(request.text);

		//PRUEBAS PARA EL POSICIONAMIENTO
		RectTransform m_RectTransform;
		float m_XAxis = 55;
		float m_YAxis = -105;
		int contSalto = 0;

		string[] result = request.text.Split('+');
		for (int i = 0; i < result.Length - 2; i++) { //LO PONGO A -2 POR QUE DE MOMENTO. HAY QUE VER LO DEL 5 HERORE
			//Instantiate (prefabdragBox, new Vector3 (0, 0, 0), Quaternion.identity);
			GameObject hero = Instantiate (prefabDropBox, new Vector3(0,0,0),Quaternion.identity) as GameObject;
			hero.transform.SetParent (GameObject.FindGameObjectWithTag("DropBox").transform, false); 

			m_RectTransform = hero.GetComponent<RectTransform>();
			m_RectTransform.anchoredPosition = new Vector2(m_XAxis, m_YAxis);
			m_XAxis += 100;
			contSalto++;
			if (contSalto == 4) {
				contSalto = 0;
				m_YAxis -= 95;
				m_XAxis = 55;
			}
			//GUI.Label(new Rect(20, 20, 150, 80), "Rect : " + m_RectTransform.rect);
			Debug.Log(result[i]);
		}
		//Debug.Log (result[0]);
	}


    IEnumerator SelectHeros() {
		WWW request = new WWW ("http://localhost/myGame/selectHeros.php");
		//Debug.Log ("prueba");
		yield return request;
        //Debug.Log(request.text);

		//PRUEBAS PARA EL POSICIONAMIENTO
		RectTransform m_RectTransform;
		float m_XAxis = 55;
		float m_YAxis = -150;
		int contSalto = 0;

		string[] result = request.text.Split('+');
		for (int i = 0; i < result.Length - 1; i++) {
			//Instantiate (prefabdragBox, new Vector3 (0, 0, 0), Quaternion.identity);
			GameObject hero = Instantiate (prefabDragBox, new Vector3(0,0,0),Quaternion.identity) as GameObject;
			hero.transform.SetParent (GameObject.FindGameObjectWithTag("DragBox").transform, false); 

			m_RectTransform = hero.GetComponent<RectTransform>();
			m_RectTransform.anchoredPosition = new Vector2(m_XAxis, m_YAxis);
			m_XAxis += 95;
			contSalto++;
			if (contSalto == 4) {
				contSalto = 0;
				m_YAxis -= 95;
				m_XAxis = 55;
			}
			//GUI.Label(new Rect(20, 20, 150, 80), "Rect : " + m_RectTransform.rect);
			//Debug.Log(result[i]);
		}
		//Debug.Log (result[0]);
    }
}