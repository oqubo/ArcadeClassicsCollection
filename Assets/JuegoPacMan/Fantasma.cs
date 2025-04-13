using UnityEngine;
using System.Collections;

public class Fantasma : MonoBehaviour {
	
	private Vector2 posicionPacman = Vector2.zero;
	private Vector2 dir = Vector2.right;
	private Vector2 posAnterior = Vector2.zero;

	private bool soyVulnerable = false;
	private bool cambiaDireccion = true;
	public Color colorNormal, colorVulnerable;

	private Animator a;

	//public BoxCollider2D boxCentral, boxArriba, boxAbajo, boxDerecha, boxIzquierda;


	void Start () {

		a = GetComponent<Animator>();
		this.GetComponent<SpriteRenderer>().color = colorNormal;

		// Si queremos que no mueva en cada update sino cada x tiempo
		InvokeRepeating ("Mover", 1f, 0.2f);

	}


	void Update () {


	}


	public void Mover() {

		// Patron de movimiento

		// mantengo la direccion hasta que choco o encuentro interseccion
		if(cambiaDireccion){

			int tipo = Random.Range (1,100);

			// si no soy vulnerable voy a por pacman o muevo aleatorio
			if(!soyVulnerable){
				if(tipo < 20) movimientoPersecucion();
				else movimientoAleatorio();
			}
			// si soy vulnerable o huyo o muevo aleatorio
			else{
				if(tipo < 30) movimientoHuida();
				else movimientoAleatorio();
			}

			cambiaDireccion = false;
		}

		// Movemos a una casilla identificada por posiciones enteras
		posAnterior.x = Mathf.Round(transform.position.x);
		posAnterior.y = Mathf.Round(transform.position.y);
		transform.Translate (dir);
	}


	public void hacerVulnerable(){
		soyVulnerable = true;
		this.GetComponent<SpriteRenderer>().color = colorVulnerable;
	}

	public void quitarVulnerable(){
		soyVulnerable = false;
		this.GetComponent<SpriteRenderer>().color = colorNormal;
	}


	void OnTriggerEnter2D(Collider2D c) {
		// Si chocamos retrocedo y pido que cambie la direccion
		if(c.CompareTag("Muro") || c.CompareTag("Border") || c.CompareTag("Enemy")) {
			transform.position = posAnterior;
			cambiaDireccion = true;
		}
		// Si llego a una interseccion, a veces cambio de direccion
		else if(c.CompareTag("Interseccion")) {
			int n = Random.Range(1,100);
			if(n < 20) cambiaDireccion = true;
		}
	}

	void manoDerecha(){

		if(dir == Vector2.down){
			dir = Vector2.left;
			a.Play ("fantasmaleft");
		}
		else if(dir == Vector2.left){
			dir = Vector2.up;
			a.Play ("fantasmaup");
		}
		else if(dir == Vector2.up){
			dir = Vector2.right;
			a.Play ("fantasmaright");
		}
		else if(dir == Vector2.right){
			dir = Vector2.down;
			a.Play ("fantasmadown");
		}
	}

	void movimientoAleatorio(){
		int d = Random.Range (1, 5);
		switch(d){
		case 1:
			dir = Vector2.right;
			a.Play ("fantasmaright");
			break;
		case 2:
			dir = -Vector2.right;
			a.Play ("fantasmaleft");
			break;
		case 3:
			dir = Vector2.up;
			a.Play ("fantasmaup");
			break;
		case 4:
			dir = -Vector2.up;
			a.Play ("fantasmadown");
			break;
		}
	}

	void movimientoPersecucion(){
		// posicion de pacman
		posicionPacman = GameObject.FindGameObjectWithTag ("Player").transform.position;

		//Diferencia entre el principio y final del arrastre
		float x = posicionPacman.x - transform.position.x;
		float y = posicionPacman.y - transform.position.y;
		
		if (Mathf.Abs(x) > Mathf.Abs(y)){
			if(x>0){
				dir = Vector2.right;
				a.Play ("fantasmaright");
			}
			else{
				dir = -Vector2.right;
				a.Play ("fantasmaleft");
			}
		}
		else{
			if(y>0){
				dir = Vector2.up;
				a.Play ("fantasmaup");
			}
			else{
				dir = -Vector2.up;
				a.Play ("fantasmadown");
			}
		}
	}

	void movimientoHuida(){
		// posicion de pacman
		posicionPacman = GameObject.FindGameObjectWithTag ("Player").transform.position;
		
		//Diferencia entre el principio y final del arrastre
		float x = posicionPacman.x - transform.position.x;
		float y = posicionPacman.y - transform.position.y;
		
		if (Mathf.Abs(x) > Mathf.Abs(y)){
			if(x<0){
				dir = Vector2.right;
				a.Play ("fantasmaright");
			}
			else{
				dir = -Vector2.right;
				a.Play ("fantasmaleft");
			}
		}
		else{
			if(y<0){
				dir = Vector2.up;
				a.Play ("fantasmaup");
			}
			else{
				dir = -Vector2.up;
				a.Play ("fantasmadown");
			}
		}
	}
}
