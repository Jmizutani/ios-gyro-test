using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballposition : MonoBehaviour {

    private GameObject ball;
	// Use this for initialization
	void Start () {
        ball = GameObject.Find("ball");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = ball.transform.position;
	}
}
