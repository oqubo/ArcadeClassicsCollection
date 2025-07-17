using UnityEngine;
using System.Collections;

public class GeneradorInterfaz : MonoBehaviour {

	// GAME MANAGER
	GameManager GM;
	void Awake () {
		GM = GameManager.instancia;
	}


	// Prefab
	public GameObject bordePrefab;

	// Limites de pantalla
	public float espacioLateral = 1f;
	public float espacioSuperior = 4f;
	public float espacioInferior = 1f;
	private float xMax;
	private float yMax;


	// Inicializacion
	void Start () {

		xMax = (Camera.main.orthographicSize * Camera.main.aspect);
		yMax = Camera.main.orthographicSize;

		GeneraBordes();
	}
	

	// Establece los bordes segun el tamano de pantalla
	void GeneraBordes() {

		GameObject g;

		//izquierdo
		g = (GameObject)Instantiate(bordePrefab,
		            				new Vector2(-xMax + espacioLateral/2, 0),
		            				Quaternion.identity);
		g.transform.localScale = new Vector2(espacioLateral, yMax*2);

		//derecho
		g = (GameObject)Instantiate(bordePrefab,
		                            new Vector2(xMax - espacioLateral/2, 0),
		                            Quaternion.identity);
		g.transform.localScale = new Vector2(espacioLateral, yMax*2);

		//inferior
		g = (GameObject)Instantiate(bordePrefab,
		                            new Vector2(0, -yMax + espacioInferior/2),
		                            Quaternion.identity);
		g.transform.localScale = new Vector2(xMax*2, espacioInferior);

		//superior
		g = (GameObject)Instantiate(bordePrefab,
		                            new Vector2(0, yMax - espacioSuperior/2),
		                            Quaternion.identity);
		g.transform.localScale = new Vector2(xMax*2, espacioSuperior);
	}

	public void botonReiniciar(){ GM.reiniciar(); }
	public void botonPausa(){ GM.pausar(); }
	public void botonMenu(){ GM.irAlMenu(); }


}