using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test04 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0)) {
			Ray _ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit _hit;

			if (Physics.Raycast (_ray, out _hit, 100)) {
				Vector3 _pos = _hit.collider.transform.position;
				_pos.Set (_hit.point.x, _hit.point.y, 0);
				_hit.collider.transform.position = _pos;
			}
		}
	}
}
