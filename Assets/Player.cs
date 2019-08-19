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
      this.dependentMode();
      ps.Stop();
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
      transform.Translate(0, 0, z * 0.1f);
      transform.Rotate(0, x, 0);
    }
  }

  void HyouiStart(){
    ps.Play();
  }

  public void independentMode(){
    this.transform.localScale = new Vector3 (1f, 1f, 1f);
  }

  public void dependentMode(){
    this.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
  }

	// Update is called once per frame
	void Update () {
    float x=0;
    float z=0;
    if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))){
      float y = Input.GetAxis("Vertical");
      camera.transform.Rotate(-y, 0, 0);
    }
    else{
      z = Input.GetAxis("Vertical");
    }
    x = Input.GetAxis("Horizontal");
    Move(x, z);

    if(Input.GetButtonDown("Jump")){
      Jump();
    }


    if(Input.GetKeyDown(KeyCode.Z)){
      HyouiStart();
    }
	}
}
