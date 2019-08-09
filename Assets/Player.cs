using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    Rigidbody rigidbody = GetComponent<Rigidbody>();

    Debug.Log(x);
    // xとzに10をかけて押す力をアップ
    rigidbody.AddForce(x * 10, 0, z * 10);
	}
}
