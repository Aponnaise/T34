using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

	
	// Update is called once per frame
	void FixedUpdate () {
		if (gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {

        }
	}
}
