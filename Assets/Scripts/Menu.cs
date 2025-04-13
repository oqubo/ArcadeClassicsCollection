using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// GAME MANAGER
	GameManager GM;
	void Awake () {
		GM = GameManager.instancia;
	}

		
	// Etiquetas donde se mostraran los puntos
	public Text puntosSnake;
	public Text puntosArkanoid;
	public Text puntosPacman;



	void Start(){

		// Al comenzar cargamos los puntos de cada juego
		puntosSnake.text = PlayerPrefs.GetInt("puntosSnake").ToString();
		puntosArkanoid.text = PlayerPrefs.GetInt("puntosArkanoid").ToString();
		puntosPacman.text = PlayerPrefs.GetInt("puntosPacman").ToString();


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

	public void OpcionSnake(){
		GM.cambiarEstado(Estado.JUEGO);
		SceneManager.LoadScene("JuegoSnake");
	}

	public void OpcionArkanoid(){
		GM.cambiarEstado(Estado.JUEGO);
		SceneManager.LoadScene("JuegoArkanoid");
	}

	public void OpcionPacman(){
		GM.cambiarEstado(Estado.JUEGO);
		SceneManager.LoadScene("JuegoPacMan");
	}


}
