using UnityEngine;
using System.Collections;

public class PongPala : MonoBehaviour {

	public KeyCode up, down;
	public float velocidad = 0.1f;
	private Vector2 tamanoInicial, tamano;


	void Start () {

		tamanoInicial = transform.localScale;
		tamano = tamanoInicial;
	}
	

	void Update () {
	
	}

	void FixedUpdate () {

		if (Input.GetKey(up))
		{
			transform.Translate (new Vector2(0, velocidad));
		}

		if (Input.GetKey(down))
		{
			transform.Translate (new Vector2(0, -velocidad));

		}


	}

	void OnCollisionEnter2D(Collision2D col){
		
		// si golpea la bola, reduzco el tamaño de la pala en 0.2
		// pero dejo como tamaño mínimo 0.2
		if (col.gameObject.name == "Ball")
		{
			if(tamano.y > 0.2)
			{
				tamano.y -= 0.2f;
				this.transform.localScale = tamano;
			}

		}
	}

	/* esta función vuelve a poner el tamaño original de las 
	 * palas cuando se mete un gol */
	void reiniciarTamanoPala(){
		tamano = tamanoInicial;
		this.transform.localScale = tamano;

	}
	

}
