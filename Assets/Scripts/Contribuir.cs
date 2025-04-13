using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Contribuir : MonoBehaviour{

	// GAME MANAGER
	GameManager GM;
	void Awake () {
		GM = GameManager.instancia;
	}

		
	public Button btn1, btn2, btn3;
	public Text texto1, texto2, texto3;


	void Start(){



		if(PlayerPrefs.GetInt("haContribuido") == 1){		
			texto1.text = "";
			texto2.text = "Thanks";
			texto3.text = "";
			btn1.interactable = btn2.interactable = btn3.interactable = false;
		}


	}


	// OPCIONES DEL MENU


	public void OpcionIntro(){
		GM.cambiarEstado(Estado.INTRO);
		SceneManager.LoadScene("Intro");
	}
	
	public void OpcionContribuir(){
		GM.cambiarEstado(Estado.CONTRIBUIR);
		SceneManager.LoadScene("Contribuir");
	}

	public void OpcionMenu(){
		GM.cambiarEstado(Estado.MENU);
		SceneManager.LoadScene("Menu");
	}



}
