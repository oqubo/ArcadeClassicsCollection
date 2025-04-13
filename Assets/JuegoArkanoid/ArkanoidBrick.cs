using UnityEngine;
using System.Collections;

public class ArkanoidBrick : MonoBehaviour {

	public int dureza;
	public GameObject prefabPowerUp;
	public int porcentajePowerUps = 50;

	void Start () {

	}
	

	void Update () {

	}

	public void asignarDureza (int d){

		dureza = d;
		
		// Segun la dureza, cambiamos el color
		Color miColor;
		switch(dureza){
		case 1:
			miColor = Color.white;
			break;
		case 2:
			miColor = Color.grey;
			break;
		case 3:
			miColor = Color.red;
			break;
		default:
			miColor = Color.black;
			break;
		}
		this.GetComponent<SpriteRenderer>().color = miColor;
	}


	public void golpea(){
		dureza--;
		if (dureza == 0){
			/*
			// Power up
			int i = Random.Range(1,100);
			if(i <= porcentajePowerUps){
				Instantiate(prefabPowerUp, transform.position, Quaternion.identity);
			}
			*/

			Destroy (this.gameObject);

			GameObject go = GameObject.Find("GM");
			go.GetComponent<ArkanoidGestor>().eliminarBrick();
		}
		else asignarDureza (dureza);
	}

}
