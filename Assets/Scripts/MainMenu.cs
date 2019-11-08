using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayButton ()
	{
		SceneManager.LoadScene ("Scenes/Game");
	}

	public void TeamsButton(){
		SceneManager.LoadScene ("Scenes/TeamCreation");
	}

	public void StoreButton(){
		SceneManager.LoadScene ("Scenes/Store");
	}

	public void OptionsButton(){
		SceneManager.LoadScene ("Scenes/Options");
	}

	public void QuitButton(){
		Debug.Log ("Saliendo...");
		Application.Quit();
	}

	public void ToMenu(){
		Debug.Log("Volviendo a inicio");
		SceneManager.LoadScene("MainMenu");
	}

}
