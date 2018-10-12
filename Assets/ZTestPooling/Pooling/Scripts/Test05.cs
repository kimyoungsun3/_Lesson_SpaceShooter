using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test05 : MonoBehaviour {

	Plane planeXY;

	// Use this for initialization
	void Start () {
		planeXY = new Plane (-Camera.main.transform.forward, Vector3.zero);

	}

	Vector3 offset;
	float distance;
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Ray _ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (planeXY.Raycast (_ray, out distance)) {
				offset = _ray.GetPoint(distance) - transform.position;
			}
		}else if (Input.GetMouseButton (0)) {
			Ray _ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (planeXY.Raycast (_ray, out distance)) {
				//Vector3 _hitPos = _ray.origin + _ray.direction * _distance;
				//transform.position = _hitPos;
				transform.position = _ray.GetPoint(distance) - offset;
			}
		}
	}
}
