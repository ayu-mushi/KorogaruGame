﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;
using UnityEngine.SceneManagement;

public class Player : Life {

  GameObject hijacked;
  ParticleSystem ps;
  GameObject camera;
  int level=1;
  int playerExp;
  public GameObject hijackedMobHPTextObj;
  Text hijackedMobHPText;
  public Text playerExpTextObj;
  Text playerExpText;
  public Text levelTextObj;
  Text levelText;


	void Start () {
    ps = GetComponent<ParticleSystem>();
    camera = GameObject.Find("Camera");
    hijackedMobHPText = hijackedMobHPTextObj.GetComponent<Text>();
    playerExpText = playerExpTextObj.GetComponent<Text>();
    levelText = levelTextObj.GetComponent<Text>();
    refreshLevel();
    refreshPlayerExp();
	}
  public void refreshLevel(){
    levelText.text = "レベル:" + level.ToString();
  }
  public void refreshPlayerExp(){
    playerExpText.text = "経験値:" + playerExp.ToString();
  }
	void OnParticleCollision(GameObject obj) {
    if(obj.tag == "Mob" && level >= obj.GetComponent<Life>().classInHierarchy){
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
    if(isHijacked()){
      hijacked.GetComponent<Mob>().Jump();
    } else {
      GetComponent<Rigidbody>().AddForce(0, 500, 0);
    }
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
    this.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
  }

  public void eatMob(string eatenMob, int exp){
    Debug.Log(eatenMob);
    playerExp += exp;
    Debug.Log(playerExp);
  }

  void LevelUpIfPossible(){
    if(playerExp >= 20){
      level +=1;
      playerExp -= 20;
      refreshLevel();
      refreshPlayerExp();
    }
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

    if(isHijacked()){
      hijackedMobHPText.text = "憑依モブのHP:" + hijacked.GetComponent<Mob>().hp.ToString();
    }
    else {
      hijackedMobHPText.text = "憑依モブのHP:" + "なし";
    }
    LevelUpIfPossible();


    if(Input.GetKeyDown(KeyCode.Z)){
      HyouiStart();
    }
    if(Input.GetKeyDown(KeyCode.C)){
      hijacked.GetComponent<Life>().hp=0;
    }
    if(transform.position.y < -40){
      hp=0;
    }
    if(hp <= 0){
      Destroy(gameObject);
      SceneManager.LoadScene("GameOver");
    }
	}
}
