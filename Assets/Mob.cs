﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using System.Threading.Tasks;

public class Mob : Life {
  float timeout;
  private float timeElapsed;
  Animator anim;
  //int hp = 3000;
  Vector3 defaultCameraPosition=new Vector3 (0, 4, -10);

	// Use this for initialization
	void Start () {
    timeout = 30+UnityEngine.Random.Range(0, 10);
    gameObject.tag = "Mob";
    anim = gameObject.GetComponent<Animator>();
    hp = 5000;
  }
  /*
  void Parthenogenesis() { // 単為生殖
    Jump();
    GameObject child;
    child = Instantiate(gameObject) as GameObject;
    child.transform.Translate(0,0,-3);
    Destroy(child.transform.Find("Camera").gameObject);
    Destroy(child.transform.Find("Player").gameObject);
  }*/
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

    RaycastHit[] hits_forward = shotRay(Vector3.forward, Vector3.up*0.7f, Color.blue, 4);
    if (hits_forward.Length != 0){
      this.Move(10, 0);
    } else {
      RaycastHit[] hits_jimen = shotRay(new Vector3(0,-1,2), Vector3.up*2, Color.red, 10);
      bool exist_terrain = false;
      foreach (RaycastHit hit in hits_jimen) {
        if(hit.collider.gameObject.name=="Terrain"){
          exist_terrain = exist_terrain || true;
        }
      }

      if(exist_terrain){
        this.Move(0, 1);
      } else {
        this.Move(10, 0);
      }
    }
  }

  RaycastHit[] shotRay(Vector3 dir, Vector3 pos, Color color, int distance){
    Vector3 position = gameObject.transform.position + pos;
    Ray ray = new Ray (position, transform.TransformDirection(dir));
    Debug.DrawLine (ray.origin, ray.direction*100, color);
    RaycastHit[] hits_all = Physics.RaycastAll(ray, distance);
    return hits_all;
  }

	void Update () {
    if(gameObject.transform.Find("Player") == null){
      Automatic();
    } else {
    }

    hp-=1;
    if(transform.position.y < -40){
      hp=0;
    }
    if(hp <= 0){
      if(gameObject.transform.Find("Player") != null){
        Transform player = gameObject.transform.Find("Player");
        player.parent = null;
        Transform camera=gameObject.transform.Find("Camera");
        camera.parent = player;
        camera.rotation = player.transform.rotation;
        camera.localPosition = defaultCameraPosition;
        player.GetComponent<Player>().independentMode();
      }
      Destroy(gameObject);
    }
    GameObject camera2=GameObject.Find("Camera");
  }
}
