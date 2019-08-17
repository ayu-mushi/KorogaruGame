using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using System.Threading.Tasks;

public class Mob : MonoBehaviour {
  float timeout=30.0f;
  private float timeElapsed;
  Animator anim;
  int hp;

	// Use this for initialization
	void Start () {
    gameObject.tag = "Mob";
    timeout+=UnityEngine.Random.Range(0, 10.0f);
    Debug.Log(gameObject.name + ":" + timeout.ToString());
    anim = gameObject.GetComponent<Animator>();
  }
  void Parthenogenesis() { // 単為生殖
    Jump();
    GameObject child;
    child = Instantiate(gameObject) as GameObject;
    child.transform.Translate(0,0,-3);
    Destroy(child.transform.Find("Camera").gameObject);
    Destroy(child.transform.Find("Player").gameObject);
  }
  public void Move(float x, float z){
    transform.Rotate(new Vector3(0,x,0));
    transform.Translate(0, 0, z*0.1f);
    if(x==0 && z==0){
      anim.SetInteger("Walk", 0);
    } else {
      anim.SetInteger("Walk", 1);
    }
  }
  public void Jump(){
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    GetComponent<Rigidbody>().AddForce(0,200,0);
    anim.SetTrigger("jump");
  }

  void Automatic(){
    timeElapsed += Time.deltaTime;
    if(timeElapsed > timeout){
      try{
        this.Parthenogenesis();
      } catch(NullReferenceException e){
        Debug.Log("ok");
      } finally {
        timeElapsed = 0.0f;
        Debug.Log("parthonogenesis : " + gameObject.name + ":" + timeElapsed.ToString());
      }
    }

    this.Move(0, UnityEngine.Random.Range(0, 2));
  }

	void Update () {
    if(gameObject.transform.Find("Player") == null){
      Automatic();
    } else {
    }

    if(transform.position.y < -40){
      Destroy(gameObject);
    }
  }
}
