using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PongBola : MonoBehaviour {

	public float velocidad = 2.0f;
	private float velocidadInicial;
	public float incremento = 0.2f;
	public Text textoIzq, textoDer;
	public int puntuacionIzq, puntuacionDer;
	private Vector2 inicial;


	// Use this for initialization
	void Start () {
		inicial = transform.position;
		puntuacionIzq = 0;
		puntuacionDer = 0;
		velocidadInicial = velocidad;
		GetComponent<Rigidbody2D>().velocity = Vector2.one.normalized * velocidad;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){


		if (col.gameObject.name == "RacketLeft")
		{
			float y = hitFactor(transform.position, 
			                    col.transform.position, 
			                    col.collider.bounds.size.y);
			Vector2 dir = new Vector2(1, y);
			velocidad += incremento;
			GetComponent<Rigidbody2D>().velocity = dir * velocidad;
		}
		if (col.gameObject.name == "RacketRight")
		{
			float y = hitFactor(transform.position, 
			                    col.transform.position, 
			                    col.collider.bounds.size.y);
			Vector2 dir = new Vector2(-1, y);
			velocidad += incremento;
			GetComponent<Rigidbody2D>().velocity = dir * velocidad;		
		}

		// si choca con la pared izq, gol del jugador de la der
		if (col.gameObject.name == "WallLeft"){ 
			puntuacionDer++;
			textoDer.text = puntuacionDer.ToString();
			saqueCentral(-1);
		}

		// si choca con la pared der, gol del jugador de la izq
		if (col.gameObject.name == "WallRight"){ 
			puntuacionIzq++;
			textoIzq.text = puntuacionIzq.ToString();
			saqueCentral(1);
		}
	}

	/* Al meter un gol, hacemos un saque central hacia el 
	 * que ha perdido.
	 * Lo indicamos con sentido -1 izquierda o 1 derecha
	 */
	void saqueCentral(int sentido){
		transform.position = inicial;
		Vector2 dir = new Vector2(sentido, 0);

		// volvemos a poner la velocidad inicial
		velocidad = velocidadInicial;
		GetComponent<Rigidbody2D>().velocity = dir * velocidad;

		// volvemos a poner el tamaño original de las palas
		GameObject rl = GameObject.Find("RacketLeft");
		rl.SendMessage("reiniciarTamanoPala");
		GameObject rr = GameObject.Find("RacketRight");
		rr.SendMessage("reiniciarTamanoPala");
	}


	/* devuelve la zona donde ha tocado en la pala: 
	 * 1 arriba, 0 mitad, -1 abajo
	 */
	float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
	{
		return (ballPos.y - racketPos.y) / racketHeight;
	}
}
