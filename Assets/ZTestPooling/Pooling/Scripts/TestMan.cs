using UnityEngine;
using System.Collections;

public class TestMan : MonoBehaviour {

	public float fireSpeed = 10;
	public float moveSpeed = 5;
	public float turnSpeed = 180;
	public int cursor = 0;
	public Transform firePoint; 
	string[] strNames;

	void Start(){
		Debug.Log ("1,2 커서가 변경된다.");

		strNames = new string[PoolManager.ins.objList.Count];
		for (int i = 0; i < PoolManager.ins.objList.Count; i++) {
			strNames[i] = PoolManager.ins.objList [i].name;
		}
	}

	// FPS 총변경처럼 구성...
	void Update () {
		if (Input.GetKey (KeyCode.Alpha1)) {
			cursor--;
			if (cursor < 0) {
				cursor = strNames.Length - 1;
			}
		} else if (Input.GetKey (KeyCode.Alpha2)) {
			cursor++;
			if (cursor >= strNames.Length) {
				cursor = 0;
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			//이름을 알면 직접, 전 Pooling에 순서대로...
			GameObject _obj = PoolManager.ins.Instantiate (strNames[cursor], firePoint.position, firePoint.rotation);
			Rigidbody _rb = _obj.GetComponent<Rigidbody> ();
			_rb.velocity = firePoint.forward * fireSpeed;
		}

		float _h = Input.GetAxis ("Horizontal");
		float _v = Input.GetAxis ("Vertical");
		if (Mathf.Abs (_h) > 0.1f || Mathf.Abs (_v) > 0.1f) {
			transform.Translate (transform.forward * _v * moveSpeed * Time.deltaTime);
			transform.Rotate(Vector3.up * _h * turnSpeed * Time.deltaTime);
		}
	
	}
}
