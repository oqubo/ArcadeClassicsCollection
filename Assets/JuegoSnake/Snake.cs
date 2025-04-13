using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Snake : MonoBehaviour {

	// GAME MANAGER
	GameManager GM;
	void Awake () {
		GM = GameManager.instancia;
	}

	// UI
	public Text textoPuntuacion;

	// Direccion de movimiento (inicialmente derecha)
	Vector2 posAnterior, posSiguiente;
	Vector2 dir = -Vector2.up;
	private Vector2 touchOrigin = -Vector2.one;
	private bool mover = true;

	// Cola 
	public GameObject colaPrefab;
	List<Transform> cola = new List<Transform>();
	private bool comer = false;
	

	void Start () {

		// Reiniciamos puntuacion
		GM.puntosSnake = 0;
		textoPuntuacion.text = GM.puntosSnake.ToString();

		// Si queremos que no mueva en cada update sino cada x tiempo
		InvokeRepeating ("Mover", 1f, 0.15f);
	}


	void Update () {

		if(!mover) return;

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

		float x = (int) (Input.GetAxisRaw ("Horizontal"));
		float y = (int) (Input.GetAxisRaw ("Vertical"));
		//Si nos movemos en horizontal marcamos la vertical a 0
		if(x != 0) y = 0;

		if (x > 0) dir = Vector2.right;
		else if (x < 0) dir = -Vector2.right;
		else if (y > 0) dir = Vector2.up;
		else if (y < 0) dir = -Vector2.up;
		
		

		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

		if (Input.touchCount > 0)
		{
			//Guardamos el primer toque
			Touch myTouch = Input.touches[0];
			
			//Si comienza un arrastre cogemos la posicion de comienzo
			if (myTouch.phase == TouchPhase.Began)
			{
				touchOrigin = myTouch.position;
			}
			
			//Si finaliza el arrastre
			else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
			{
				//Posicion de final del toque
				Vector2 touchEnd = myTouch.position;
				
				//Diferencia entre el principio y final del arrastre
				float x = touchEnd.x - touchOrigin.x;
				float y = touchEnd.y - touchOrigin.y;
				
				//No repetimos hasta que se origine un nuevo arrastre
				touchOrigin = -Vector2.one;

				//Si el arrastre ha sido muy corto (menos de 8 px)
				//ha sido una pulsacion que descartamos
				if(Mathf.Abs(x) > 8 || Mathf.Abs(y) > 8){ 
					//Obtenemos la direccion del arrastre
					if (Mathf.Abs(x) > Mathf.Abs(y)){
						if(x>0) dir = Vector2.right;
						else dir = -Vector2.right;
					}
					else{
						if(y>0) dir = Vector2.up;
						else dir = -Vector2.up;
					}
				}

			}

		}
		
		#endif

	}

	

	public void Mover() {

		// Movemos la cabeza
		posAnterior = transform.position;
		transform.Translate (dir);

		// Si ha comido, no movemos la cola sino que metemos una pieza en la primera posicion
		if(comer){
			// Agregamos un elemento a la cola
			GameObject g = (GameObject)Instantiate(colaPrefab, posAnterior, Quaternion.identity);
			cola.Insert(0, g.transform);
			comer = false;
		}

		// Si no ha comido, movemos la cola
		else{
			if(cola.Count>0){
				// movemos el ultimo elemento al primero
				cola.Last().position = posAnterior;
				cola.Insert(0, cola.Last());
				cola.RemoveAt(cola.Count-1);
			}
		}

	}


	void OnTriggerEnter2D(Collider2D c) {

		// Tomamos un objeto
		if (c.CompareTag("PickUp")){
			comer = true;
			Destroy(c.gameObject);
			GM.puntosSnake++;
			textoPuntuacion.text = GM.puntosSnake.ToString();
		}

		// Si chocamos con otra cosa (el borde o la cola), perdemos
		else if(c.CompareTag("Border") || c.CompareTag("Player") ) {
			// paramos el movimiento
			dir = Vector2.zero;
			transform.Translate(dir);

			GM.reiniciar();
		}
	}
	

}
