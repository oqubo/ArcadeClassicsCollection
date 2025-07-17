using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PacMan : MonoBehaviour {

	// GAME MANAGER
	GameManager GM;
	void Awake () {
		GM = GameManager.instancia;
	}

	// UI
	public Text textoPuntuacion;

	// Movimiento
	Vector2 dir = Vector2.zero;
	Vector2 posAnterior = Vector2.zero;
	private Vector2 touchOrigin = -Vector2.one;
	private bool quieto = true;
	private bool inmortal = false;
	private Animator a;

	public Color colorNormal, colorInvencible;


	void Start () {

		reiniciaPacMan();
		a = GetComponent<Animator>();
		this.GetComponent<SpriteRenderer>().color = colorNormal;

		// Puntuacion
		GM.puntosPacman = 0;
		textoPuntuacion.text = GM.puntosPacman.ToString();

		// Si queremos que no mueva en cada update sino cada x tiempo
		InvokeRepeating ("Mover", 1f, 0.15f);
	}


	void Update () {


		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

		float x = (int) (Input.GetAxisRaw ("Horizontal"));
		float y = (int) (Input.GetAxisRaw ("Vertical"));
		//Si nos movemos en horizontal marcamos la vertical a 0
		if(x != 0) y = 0;

		// Derecha
		if (x > 0){
			dir = Vector2.right;
			a.Play("pacmanright");
		}
		// Izquierda
		else if (x < 0){
			dir = -Vector2.right;
			a.Play ("pacmanleft");
		}
		// Arriba
		else if (y > 0){
			dir = Vector2.up;
			a.Play("pacmanup");
		}
		// Abajo
		else if (y < 0){
			dir = -Vector2.up;
			a.Play ("pacmandown");
		}
		
		quieto = false;


		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

		if (Input.touchCount > 0){
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
						if(x>0){
							dir = Vector2.right;
							a.Play("pacmanright");
						}
						else{
							dir = -Vector2.right;
							a.Play("pacmanleft");
						}
					}
					else{
						if(y>0){
							dir = Vector2.up;
							a.Play("pacmanup");
						}
						else{
							dir = -Vector2.up;
							a.Play("pacmandown");
						}
					}
				}

				quieto = false;
			}
		}
		
		#endif

	}

	

	public void Mover() {
		if(!quieto){
			// Movemos a una casilla identificada por posiciones enteras
			posAnterior.x = Mathf.Round(transform.position.x);
			posAnterior.y = Mathf.Round(transform.position.y);
			transform.Translate (dir);
		}
	}


	void OnTriggerEnter2D(Collider2D c) {

		// Tomamos un objeto
		if (c.CompareTag("PickUp")){
			Destroy(c.gameObject);
			GM.puntosPacman++;
			textoPuntuacion.text = GM.puntosPacman.ToString();
			GM.pickupsPacman--;
		
			if(GM.pickupsPacman == 0){
				reiniciaPacMan();
				GameObject.Find ("GM").GetComponent<GeneraNivelesPacMan>().GeneraSiguienteNivel();
			}
		}

		// Tomamos un especial
		if (c.CompareTag("PickUpEspecial")){
			Destroy(c.gameObject);

			if(inmortal == false){
				// poner inmortal
				inmortal = true;
				this.GetComponent<SpriteRenderer>().color = colorInvencible;
				GameObject[] obj = GameObject.FindGameObjectsWithTag ("Enemy");
				foreach(GameObject o in obj) o.SendMessage("hacerVulnerable");
					//o.GetComponent<Fantasma>().hacerVulnerable();
				Invoke("quitarInmortalidad", 5.0f);
				}

		}


		// Si chocamos con un borde paramos
		else if(c.CompareTag("Muro") || c.CompareTag("Border")) {
			quieto = true;
			transform.position = posAnterior;
		}


		// Si chocamos con un enemigo
		else if(c.CompareTag("Enemy")) {

			// si somos inmortales lo comemos
			if(inmortal){
				Destroy(c.gameObject);
				GM.puntosPacman += 10;
				textoPuntuacion.text = GM.puntosPacman.ToString();
			}

			// si no somos inmortales morimos
			else{
				quieto = true;
				transform.position = posAnterior;
				a.Play("pacmandead");
				// desde el final de la animacion se llama a reinicarJuego
			}
		}
	}


	public void quitarInmortalidad(){		
		inmortal = false;
		this.GetComponent<SpriteRenderer>().color = colorNormal;
		GameObject[] obj = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach(GameObject o in obj) o.SendMessage("quitarVulnerable");
			// o.GetComponent<Fantasma>().quitarVulnerable();			
	}


	public void reiniciaPacMan(){
		quieto = true;
		inmortal = false;
		transform.position = Vector2.zero;
		dir = Vector2.zero;
		posAnterior = Vector2.zero;
		touchOrigin = -Vector2.one;
	}

	public void reiniciarJuego(){
		GM.reiniciar();
	}


}