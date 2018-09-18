using UnityEngine;
using System.Collections;

public static class Utility {

	//Fisher - Yates Shuffle
	public static T[] ShufflArray<T>(T[] _arr, int _randomSeed){
		System.Random _rand = new System.Random (_randomSeed);
		T _temp;
		int _randIdx;
		int _end = _arr.Length;
		for (int i = 0; i < _end; i++) {
			_randIdx = _rand.Next (i, _end);

			_temp 			= _arr [_randIdx];
			_arr [_randIdx] = _arr [i];
			_arr [i] 		= _temp;
		}
		return _arr;
	}


	public static T[] ShufflArray<T>(T[] _arr){
		System.Random _rand = new System.Random ();
		T _temp;
		int _randIdx;
		int _end = _arr.Length;
		for (int i = 0; i < _end; i++) {
			_randIdx = _rand.Next (i, _end);

			_temp 			= _arr [_randIdx];
			_arr [_randIdx] = _arr [i];
			_arr [i] 		= _temp;
		}
		return _arr;
	}
}
