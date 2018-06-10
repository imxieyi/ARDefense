using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBase : MonoBehaviour {

	public static Transform trans;
	public static float scale;

	void Awake() {
		trans = transform;
		scale = trans.localScale.x;
	}

}
