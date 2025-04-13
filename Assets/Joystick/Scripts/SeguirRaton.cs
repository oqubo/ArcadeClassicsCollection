using UnityEngine;
using System.Collections;

public class SeguirRaton : MonoBehaviour {


	private Vector2 a, b;
	public float velocidad = 15f;

	void Start () {

	}
	

	void Update () {

		seguirRaton();
	}


	void seguirRaton(){

		// posicion actual
		a = transform.position;

		// posicion del raton
		b = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		b.y = transform.position.y;

		// diferencia de posiciones
		b = b - a;

		// no considero un movimiento corto
		if(Mathf.Abs(b.x) > 0.5){  
			b.Normalize();
			b = b * velocidad + a;
			transform.position = Vector3.Lerp( a, b, Time.deltaTime);
			}
	}
	
}
