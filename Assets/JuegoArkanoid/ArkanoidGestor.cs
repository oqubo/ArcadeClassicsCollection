using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArkanoidGestor : MonoBehaviour {

			
	// Elementos construccion niveles
	public int porcentajeBricks = 50;
	public GameObject prefabBrick;
	public int numeroBricks = 0;
	

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

		GeneraNivel();
	}



	// Genera un nivel con objetos en posicion aleatoria dentro de los bordes
	void GeneraNivel() {

		// reiniciamos elementos
		numeroBricks = 0;
		GameObject.FindWithTag("Player").transform.position = new Vector2(0, -yMax + (yMax*0.05f));
		GameObject.FindWithTag("Bola").transform.position = new Vector2(0, -yMax + (yMax*0.05f) + 1);


		// variables temporales
		int fila, col, i, tipo;
		GameObject go;

		// generamos elementos en una zona de la pantalla
		// FILAS consideramos 2/3 de la mitad superior de la pantalla
		// COLUMNAS recorremos la mitad, para hacer escenarios simetricos verticalmente
		for(fila = yMax-1; fila>yMax/3; fila=fila-1){

			// Brick aleatoriamente si es < del porcentaje, sino dejo un hueco
			i = Random.Range (0,100);
			if( i <= porcentajeBricks){
				// el tipo es aleatorio de la lista de bricks (por dureza)
				tipo = Random.Range(1, 4);
				go = (GameObject) Instantiate(prefabBrick, new Vector2(0, fila), Quaternion.identity);
				go.GetComponent<ArkanoidBrick>().asignarDureza (tipo);
				// incremento el contador
				numeroBricks += 1;
			}

			// coloco los restantes de modo simetrico
			for(col=3; col<=xMax; col=col+3){
				// Brick aleatoriamente si es < del porcentaje, sino dejo un hueco
				i = Random.Range (0,100);
				if( i <= porcentajeBricks){
					// el tipo es aleatorio de la lista de bricks (por dureza)
					tipo = Random.Range(1, 4);
					// genero 2 simetricos
					go = (GameObject) Instantiate(prefabBrick, new Vector2(col, fila), Quaternion.identity);
					go.GetComponent<ArkanoidBrick>().asignarDureza (tipo);
					go = (GameObject) Instantiate(prefabBrick, new Vector2(-col, fila), Quaternion.identity);
					go.GetComponent<ArkanoidBrick>().asignarDureza (tipo);
					// incremento el contador
					numeroBricks += 2;
				}

			}
		}
	}


	public void eliminarBrick(){
		numeroBricks--;
		if(numeroBricks == 0) GeneraNivel ();
	}




}