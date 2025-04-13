using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArkanoidBola : MonoBehaviour {

	// GAME MANAGER
	GameManager GM;
	void Awake () {
		GM = GameManager.instancia;
	}

	// Publicos
	public Text textoPuntuacion;
	public float velocidad = 20f;


	void Start () {
		GetComponent<Rigidbody2D>().velocity = -Vector2.up * velocidad;
		GM.puntosArkanoid = 0;
	}


	void OnCollisionEnter2D(Collision2D c) {

		Vector2 dir = this.GetComponent<Rigidbody2D>().velocity;

		if (c.gameObject.CompareTag("Player")) {
			// calcula el lugar de golpeo y la direccion
			dir.x = transform.position.x - c.transform.position.x;
			dir.y = 1;
			dir = dir.normalized;
			// mueve
			this.GetComponent<Rigidbody2D>().velocity = dir * velocidad;
		}

		else if(c.gameObject.CompareTag("Enemy")){
			GM.puntosArkanoid++;
			textoPuntuacion.text = GM.puntosArkanoid.ToString();

			c.gameObject.GetComponent<ArkanoidBrick>().golpea();
		}

		else if(c.gameObject.CompareTag("Border")) {
			// vemos si la bola golpea un borde de abajo de la pala
			GameObject go = GameObject.FindGameObjectWithTag("Player");
			if (transform.position.y < go.transform.position.y){
				GM.reiniciar ();
			}

		}
	}



}
