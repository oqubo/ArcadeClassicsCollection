using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	// GAME MANAGER
	GameManager GM;
	void Awake () {
		GM = GameManager.instancia;
	}
	
	// Al comenzar cambiamos al menu principal después de una espera
	void Start () {
		GM.cambiarEstado(Estado.MENU);
		Invoke("CargaEscena", 3f);
	}
	
		
	public void CargaEscena(){
		Application.LoadLevel("Menu");
	}


}
