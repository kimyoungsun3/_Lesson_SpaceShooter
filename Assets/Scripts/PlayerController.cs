using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	Transform trans;
	float y;
	Vector3 move, pos;
	public Vector3 boundX, boundZ;
	public float moveSpeed = 10f;
	public float tilt = 5f;

	void Start(){
		trans = transform;
		y = trans.position.y;
	}
	void Update(){
		//이동...
		float h = Input.GetAxis("Horizontal");	//좌,우이동.
		float v = Input.GetAxis("Vertical");		//상,하이동.
		move.Set(h, y, v); 
		move = move.normalized;			//대각선이동의 값을 동일하게 하기위해.
		trans.Translate(move * moveSpeed * Time.deltaTime, Space.World);

		//경계라인 검사하기....
		pos = trans.position;
		pos.x = Mathf.Clamp(pos.x, boundX.x, boundX.y);
		pos.z = Mathf.Clamp(pos.z, boundZ.x, boundZ.y);
		trans.position = pos;

		//틸트 회전.
		trans.rotation = Quaternion.Euler(0, 0, -h * tilt);
	}
}
