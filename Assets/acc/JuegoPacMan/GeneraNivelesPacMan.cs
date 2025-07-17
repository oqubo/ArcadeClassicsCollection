using UnityEngine;
using System.Collections;
using System.IO;

public class GeneraNivelesPacMan : MonoBehaviour {

	// GAME MANAGER
	GameManager GM;
	void Awake () {
		GM = GameManager.instancia;
	}

	// Niveles simetricos (definimos el cuadro inferior derecho) 16x12
	// 0 =piedra, 1 =pickup, 2 =pickupespecial, 3 =interseccion+pickup, 4 =fantasma, otro =nada
	public TextAsset[] ficherosCSV;
	private char separadorLinea = '\n';
	private char separadorCampo = ',';
	private int nivelActual = -1;

	// Elementos construccion niveles
	public GameObject prefabPared, prefabPickup, prefabPickupEspecial, prefabFantasma, prefabInterseccion;
	public int numeroPickups = 0;

	// Limites de pantalla	
	private int xMax;
	private int yMax;
	public float espacioLateral = 1f;
	public float espacioSuperior = 4f;
	public float espacioInferior = 1f;
	
	
	
	// Inicializacion
	void Start () {
		
		// asigno los limites donde quiero que se generen elementos (mitad superior de la pantalla)
		xMax = (int)((Camera.main.orthographicSize * Camera.main.aspect) - espacioLateral*2);
		yMax = (int)(Camera.main.orthographicSize - espacioSuperior - espacioInferior);

		GeneraSiguienteNivel();
	}


	public void GeneraSiguienteNivel(){

		// genera un nivel nuevo distinto al actual
		int numeroDeNivel;
		do{
			numeroDeNivel = Random.Range(0, ficherosCSV.Length);
		}while(numeroDeNivel == nivelActual);

		BorraNivel();
		GM.pickupsPacman = 0;
		GeneraNivel(ficherosCSV[numeroDeNivel]);

		GM.pickupsPacman = numeroPickups;
		nivelActual = numeroDeNivel;
	}


	// Genera un nivel con objetos en posicion aleatoria dentro de los bordes
	public void GeneraNivel(TextAsset fichero) {

		// reiniciamos elementos
		numeroPickups = 0;
		GameObject.FindWithTag("Player").transform.position = new Vector2(0, 0);

		// Proceso el fichero. Traigo todas las lineas
		string[] lineas = fichero.text.Split (separadorLinea);
		string[] campos;

		// fila 0
		campos = lineas[0].Split(separadorCampo);
		for(int col = 0; col < campos.Length; col++){
			switch(int.Parse(campos[col])){
				// 0 pared
				case 0:
					pinta(prefabPared, col, 0);
					break;
				// 1 pickup
				case 1:
					pinta(prefabPickup, col, 0);
					numeroPickups += 2;
					break;
				// 2 pickup especial
				case 2:
					pinta(prefabPickupEspecial, col, 0);
					break;
				// 3 interseccion y pickup
				case 3:
					pinta(prefabInterseccion, col, 0);
					pinta(prefabPickup, col, 0);
					numeroPickups += 2;
					break;
				// 4 fantasma
				case 4:
					pinta(prefabFantasma, col, 0);
					pinta(prefabPickup, col, 0);
					numeroPickups += 2;
					break;
				default:
					break;
					}
			}

		// columna 0
		for(int fila = 0; fila < lineas.Length-1; fila++){
			campos = lineas[fila].Split(separadorCampo);
			switch(int.Parse(campos[0])){
			case 0:
				pinta(prefabPared, 0, fila);
				break;
			case 1:
				pinta(prefabPickup, 0, fila);
				numeroPickups += 2;
				break;
			case 2:
				pinta(prefabPickupEspecial, 0, fila);
				break;
			case 3:
				pinta(prefabInterseccion, 0, fila);
				pinta(prefabPickup, 0, fila);
				numeroPickups += 2;
				break;
			case 4:
				pinta(prefabFantasma, 0, fila);
				pinta(prefabPickup, 0, fila);
				numeroPickups += 2;
				break;
			default:
				break;
			}
		}

		// casillas intermedias simetricas
		for(int fila = 1; fila < lineas.Length-1; fila++){
			campos = lineas[fila].Split(separadorCampo);
			for(int col = 1; col < campos.Length; col++){
				switch(int.Parse (campos[col])){
				case 0:
					pinta(prefabPared, col, fila);
					break;
				case 1:
					pinta(prefabPickup, col, fila);
					numeroPickups += 4;
					break;
				case 2:
					pinta(prefabPickupEspecial, col, fila);
					break;
				case 3:
					pinta (prefabInterseccion, col, fila);
					pinta (prefabPickup, col, fila);
					numeroPickups += 4;
					break;
				case 4:
					pinta (prefabFantasma, col, fila);
					pinta (prefabPickup, col, fila);
					numeroPickups += 4;
					break;
				default:
					break;
				}
			}
		}
	}

	public void pinta(GameObject prefab, int c, int f){

		//si la columna es la central solo pinto 2
		if(c == 0){
			Instantiate(prefab, new Vector2(0, f), Quaternion.identity);
			Instantiate(prefab, new Vector2(0, -f), Quaternion.identity);
		}

		//si el la fila central solo pinto 2
		else if(f == 0){
			Instantiate(prefab, new Vector2(c, 0), Quaternion.identity);
			Instantiate(prefab, new Vector2(-c, 0), Quaternion.identity);
		}

		//si es casilla normal pinto los 4 simetricos
		else{
			Instantiate(prefab, new Vector2(c, -f), Quaternion.identity);
			Instantiate(prefab, new Vector2(c, f), Quaternion.identity);
			Instantiate(prefab, new Vector2(-c, f), Quaternion.identity);
			Instantiate(prefab, new Vector2(-c, -f), Quaternion.identity);
		}
	}


	// borra todos los objetos del nivel
	public void BorraNivel(){
		GameObject[] objs;
		// muro
		objs = GameObject.FindGameObjectsWithTag("Muro");
		foreach(GameObject go in objs) Destroy (go);
		// comida
		objs = GameObject.FindGameObjectsWithTag("PickUp");
		foreach(GameObject go in objs) Destroy (go);
		// comida especial
		objs = GameObject.FindGameObjectsWithTag("PickUpEspecial");
		foreach(GameObject go in objs) Destroy (go);
		// fantasma
		objs = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject go in objs) Destroy (go);
	}


	public int comerPickup(){
		if(numeroPickups>0) numeroPickups--;
		return numeroPickups;
	}
}
