/*
 * 2016.12.29 영선 ( kyssmart@naver.com )....
 * 
 * [미처리 내용입니다.]
 * - 생성을 처음에 개별로 지정하기.
 * - 생성관리를 환영큐..
 * 
 * [보류]
 * - 받아오는 부분을(사용을 위해) 현는 string만 지원, GameObject로 하면 사용에 번거러움이 있을것 같아서...
 *   구현은 GameObject.name으로 overriding해 사용하시면 됩니다.
 * */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PoolManager : MonoBehaviour {
	public static PoolManager ins;

	public List<GameObject> objList = new List<GameObject> ();
	public int poolCount = 20;
	public bool willGrow = true;

	//public Dictionary<string, List<GameObject>> poolList = new Dictionary<string, List<GameObject>>();
	public Dictionary<int, List<GameObject>> poolList = new Dictionary<int, List<GameObject>>();
	//public bool view = false;

	void Awake(){
		if (ins == null) {
			ins = this;
		}

		init ();
	}

	private void init(){
		GameObject _go, _obj;
		List<GameObject> _list;
		for (int j = 0; j < objList.Count; j++) {
			_obj 	= objList [j];
			_list 	= new List<GameObject> ();
			poolList.Add(_obj.name.GetHashCode(), _list);

			for (int i = 0; i < poolCount; i++) {
				_go = Instantiate (_obj) as GameObject;
				_go.transform.SetParent (transform);
				_go.SetActive (false);
				_list.Add (_go);
			}
		}
	}

	public GameObject Instantiate(string _name, Vector3 _pos, Quaternion _qua){
		GameObject _rtnObject = Instantiate (_name);
		_rtnObject.transform.position = _pos;
		_rtnObject.transform.rotation = _qua;

		return _rtnObject;
	}

	public GameObject Instantiate(string _name){
		if (!poolList.ContainsKey (_name.GetHashCode())) {
			Debug.LogError ("Not found Pooling GameObject _name[" + _name + "]");
			return null;
		}
		GameObject _rtn = null;
		bool _find = false;

		List<GameObject> _list = poolList [_name.GetHashCode()];
		for (int i = 0; i < _list.Count; i++) {
			if (!_list[i].activeInHierarchy) {
				_rtn = _list[i];
				_find = true;
				_rtn.SetActive (true);
				break;
			}
		}

		//not found the pooling gameobject and create gameobject 
		if (!_find && willGrow) {
			GameObject _obj = GetObject (_name);
			GameObject _go = Instantiate (_obj) as GameObject;
			_list.Add (_go);
			_go.transform.SetParent (transform);
			_rtn = _go;
		}
		return _rtn;
	}

	GameObject GetObject(string _name){
		GameObject _obj = null;
		for (int i = 0; i < objList.Count; i++) {
			if (objList [i].name == _name) {
				_obj = objList [i];
			}
		}
		return _obj;
	}
}
