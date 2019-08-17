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
      camera.transform.parent = obj.transform;
      camera.transform.rotation = obj.transform.rotation;
      this.transform.parent = hijacked.transform;
      camera.transform.position = obj.transform.position;
      camera.transform.Translate(new Vector3(0, 4, -10));
    }
	}
  bool isHijacked(){
    return (hijacked!=null);
  }
  void Jump(){
    hijacked.GetComponent<Mob>().Jump();
  }

  void Move(float x,float z){
    if(isHijacked()){
      hijacked.GetComponent<Mob>().Move(x, z);
      transform.position = hijacked.transform.position;
    }
    else {
      transform.Translate(x * 0.1f, 0, z * 0.1f);
    }
  }

  void HyouiStart(){
      ps.Play();
  }

	// Update is called once per frame
	void Update () {
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");
    Move(x, z);

    if(Input.GetButtonDown("Jump")){
      Jump();
    }

    if(Input.GetKeyDown(KeyCode.Z)){
      HyouiStart();
    }
	}
}
