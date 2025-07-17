using UnityEngine;
using UnityEngine.SceneManagement;

public enum Estado { INTRO, MENU, JUEGO, CONTRIBUIR }

public class GameManager : MonoBehaviour {
	
	// Singleton
	static GameManager _instancia;
	static public GameManager instancia
	{
		get
		{
			if (_instancia == null)
			{
				_instancia = Object.FindObjectOfType(typeof(GameManager)) as GameManager;
				
				// si no la encuentro la creo
				if (_instancia == null)
				{
					GameObject go = new GameObject("_gamemanager");
					DontDestroyOnLoad(go);
					_instancia = go.AddComponent<GameManager>();
				}
			}
			return _instancia;
		}
	}
	
	// Estado
	public Estado estadoActual {get; private set;}
	public void cambiarEstado(Estado e) { this.estadoActual = e; }
	
	
	
	// Atributos y metodos unicos y accesibles desde cualquier parte

	// Atributos
	public int puntosSnake;
	public int puntosArkanoid;
	public int puntosPacman;
	public int pickupsPacman;
	public int puntosInvaders;
	public int puntosTetris;
	public int puntosOtro;
	public float valorTimeScale;

	// Metodos

	public void irAlMenu(){
		guardarPartida();
		quitarPausa ();
		cambiarEstado(Estado.MENU);
		SceneManager.LoadScene("Menu");
	}
	
	//Si esta activo lo pausamos y si esta pausado ponemos el valor normal
	public void pausar(){
		if(Time.timeScale > 0){
			valorTimeScale = Time.timeScale;
			Time.timeScale = 0;
		}
		else{
			quitarPausa ();
		}
	}
	public void quitarPausa(){
		if(Time.timeScale == 0) Time.timeScale = valorTimeScale;
	}


	public void reiniciar(){
		guardarPartida ();
		quitarPausa();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void guardarPartida(){
		// si la puntuacion mejora el record, la guardamos
		if(PlayerPrefs.GetInt ("puntosSnake") < puntosSnake) PlayerPrefs.SetInt("puntosSnake", puntosSnake);
		if(PlayerPrefs.GetInt ("puntosArkanoid") < puntosArkanoid) PlayerPrefs.SetInt("puntosArkanoid", puntosArkanoid);
		if(PlayerPrefs.GetInt ("puntosPacman") < puntosPacman) PlayerPrefs.SetInt("puntosPacman", puntosPacman);
	}
		
	
}