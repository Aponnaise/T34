using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotationResetter : MonoBehaviour {

	
	// Update is called once per frame
	void LateUpdate () {
        transform.rotation = Quaternion.Euler(90,0,0);
	}
}
