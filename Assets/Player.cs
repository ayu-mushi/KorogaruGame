using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class Player : MonoBehaviour {

  GameObject hijacked;
  ParticleSystem ps;
  GameObject camera;

	void Start () {
    ps = GetComponent<ParticleSystem>();
    camera = GameObject.Find("Camera");
	}
	void OnParticleCollision(GameObject obj) {
    if(obj.tag == "Mob"){
      hijacked = obj;
		  Debug.Log("衝突");
      ps.Stop();
      camera.GetComponent<CameraController>().player = obj;
      this.transform.parent = hijacked.transform;
    }
	}
	// Update is called once per frame
	void Update () {
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    Rigidbody rigidbody = GetComponent<Rigidbody>();

    // xとzに10をかけて押す力をアップ
    if(hijacked!=null){
      hijacked.GetComponent<Rigidbody>().AddForce(x * 10, 0, z * 10, ForceMode.Acceleration);
      transform.position = hijacked.transform.position;
    }
    else{
      rigidbody.AddForce(x * 10, 0, z * 10, ForceMode.Acceleration);
    }

    if(Input.GetKeyDown(KeyCode.Space)){
      ps.Play();
    }
	}
}
