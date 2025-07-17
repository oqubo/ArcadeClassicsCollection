using UnityEngine;
using System.Collections;

public class GeneradorObjetos : MonoBehaviour {
	
	// Prefab
	public GameObject objPrefab;

	// Parametros de generacion
	public float velocidad = 1f;

	// Offsets
	public float espacioLateral = 1f;
	public float espacioSuperior = 4f;
	public float espacioInferior = 1f;

	// Limites de pantalla
	private float xMax;
	private float yMax;


	// Inicializacion
	void Start () {

		xMax = (Camera.main.orthographicSize * Camera.main.aspect) - espacioLateral;
		yMax = Camera.main.orthographicSize - espacioSuperior/2 - espacioInferior/2;

		InvokeRepeating("GeneraPickUps", velocidad, velocidad);
	}
	
	// Genera un objeto en posicion aleatoria dentro de los bordes
	void GeneraPickUps() {
	
		// posicion entre los bordes
		int x = (int)Random.Range(-xMax, xMax);
		int y = (int)Random.Range(-yMax, yMax);
		
		// generamos el objeto en la posicion (x, y)
		Instantiate(objPrefab,
		            new Vector2(x, y),
		            Quaternion.identity);
	}
	
}