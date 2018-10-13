using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Input.gyro.enabled = false;
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.gyro.enabled)
        {
            var rotRH = Input.gyro.attitude;
            var rot = new Quaternion(-rotRH.x, 0f, -rotRH.y, rotRH.w);
            transform.localRotation = rot;
        }
    }
}
