using UnityEngine;
using System.Collections;

public class UVScroll : MonoBehaviour {
	public float scrollSpeed;
	Renderer rend;
	Vector2 offset;

	void Start(){
		rend = GetComponent<Renderer> ();
		offset = Vector2.zero;
	}

	void Update(){
		offset.y += Time.deltaTime * scrollSpeed;
		rend.material.SetTextureOffset ("_MainTex", offset);
	}
}
