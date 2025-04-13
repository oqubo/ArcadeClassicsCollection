using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MoveObject : MonoBehaviour {

	private Rigidbody compo;

	void Start() {
		compo=GetComponent<Rigidbody>();
	}

	public void Move(Vector2 axis) {
		compo.AddForce(new Vector3(axis.x, 0, axis.y) * Time.deltaTime * 1000, ForceMode.Force);
	}
}