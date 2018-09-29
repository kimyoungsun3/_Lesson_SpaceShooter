using UnityEngine;
using System.Collections;

public class MapScroll : MonoBehaviour {
	public Transform bottom;
	public float scrollSpeed;
	public float underPosZ = -36f;
	public float tileSize = 30f;

	void Update(){
		if (transform.localPosition.z < underPosZ) {
			//Debug.Log (transform.localPosition + ":" + underPosZ
			//	+":"+ (underPosZ - transform.localPosition.z));
			transform.localPosition = new Vector3 (
				bottom.localPosition.x,
				bottom.localPosition.y,
				bottom.localPosition.z + tileSize);
		}

		transform.Translate(Vector3.back * Time.deltaTime * scrollSpeed, Space.World);

	}
}
