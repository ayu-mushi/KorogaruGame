using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

using System.Threading.Tasks;

public class Mob : Life {
  float timeout;
  private float timeElapsed;
  Animator anim;
  Vector3 defaultCameraPosition=(new Vector3 (0, 4, -10));
  Slider hpSlider;

	// Use this for initialization
	void Start () {
    base.Start();
    timeout = 30+UnityEngine.Random.Range(0, 10);
    gameObject.tag = "Mob";
    anim = gameObject.GetComponent<Animator>();
    initialHp = hp;
    if(hp==0){this.initializeHp();}
    hpSlider = transform.Find("HPCanvasPrefab/HPSlider").gameObject.GetComponent<Slider>();
  }
  public String Show (){
    return ("" + gameObject.name +
        "("+ hp.ToString() + ""
        + "/" + maxHp.ToString() + ")");
  }
  public void Move(float x, float z){
    transform.Rotate(new Vector3(0,x*Time.deltaTime*60,0));
    transform.Translate(0, 0, z*Time.deltaTime*8);
    if(x==0 && z==0){
      anim.SetInteger("Walk", 0);
    } else {
      anim.SetInteger("Walk", 1);
      this.hp -= 1;
    }
  }
  public void Jump(){
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    GetComponent<Rigidbody>().AddForce(0,200,0);
    anim.SetTrigger("jump");
    this.hp -= 5;
  }

  GameObject nerai;
  public void OnSakutekiStay(Collider col, Transform nerai_hanni){
    if(col.gameObject.tag == "Player" || col.gameObject.tag == "Mob" ||
        col.gameObject.tag =="Plant"){
      if(this.canEat(col.gameObject.GetComponent<Life>()) && nerai==null){
        nerai = col.gameObject;
        Debug.Log("索敵入る");
        nerai_hanni.localScale = new Vector3(10, 0.0537f, 10);
      }
    }
  }
  public void OnSakutekiExit(Collider col, Transform nerai_hanni){
    if(col.gameObject==nerai){
      nerai = null;
      Debug.Log("索敵出る");
      nerai_hanni.transform.localScale = new Vector3(5,0.037f,5);
    }
  }
  void Automatic(){
    timeElapsed += Time.deltaTime;
    if(hp > this.maxHp){
      try{
        this.Parthenogenesis();
      } catch(NullReferenceException e){
        Debug.Log("ok");
      } finally {
        timeElapsed = 0.0f;
        Debug.Log("parthonogenesis : " + gameObject.name + ":" + timeElapsed.ToString());
      }
    }

    if(nerai != null) {
      Vector3 diffVect = nerai.transform.position - transform.position;
      Quaternion targetRotation = Quaternion.LookRotation(new Vector3(diffVect.x, 0, diffVect.z));
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
      Debug.Log("角度"+(transform.eulerAngles - targetRotation.eulerAngles).ToString());
      Debug.Log("長さ"+Math.Abs(Mathf.Cos((transform.rotation.eulerAngles.y  - targetRotation.eulerAngles.y )* Mathf.Deg2Rad)).ToString());
      this.Move(0, Math.Abs(Mathf.Cos((transform.rotation.eulerAngles.y  - targetRotation.eulerAngles.y )* Mathf.Deg2Rad)));
    }

   /* RaycastHit hits_forward = shotRay(Vector3.forward, Vector3.up*0.7f, Color.blue, 4);
    if (hits_forward.collider != null &&
        (hits_forward.collider.gameObject.tag == "Mob" || hits_forward.collider.gameObject.tag == "Plant" || hits_forward.collider.tag == "Player")){
      if (this.canEat(hits_forward.collider.gameObject.GetComponent<Life>())){
        this.Move(0, 4);
      } else if (hits_forward.collider.gameObject.GetComponent<Life>().canEat(this)){
        this.Move(10, 0);
      }
      else {
        this.Move(1, 0);
      }
    } else if (hits_forward.collider != null && hits_forward.collider.gameObject.tag == "Terrain"){
      this.Move(1, 0);
    } else {
      RaycastHit[] hits_jimen = shotRayAll(new Vector3(0,-1,0.6f), Vector3.up*2, Color.red, 10);
      bool jimen_flag = false;
      foreach(RaycastHit hit in hits_jimen){
        if(hit.collider.gameObject.tag=="Terrain"){
          jimen_flag = true; continue;
        };
      }
      if(jimen_flag){
        this.Move(0, 1);
      } else {
        this.Move(1, 0);
      }
    }*/
  }

  RaycastHit shotRay(Vector3 dir, Vector3 pos, Color color, int distance){
    Vector3 position = gameObject.transform.position + pos;
    Ray ray = new Ray (position, transform.TransformDirection(dir));
    Debug.DrawLine (ray.origin, ray.direction*100, color);
    RaycastHit hits_all;
    Physics.Raycast(ray, out hits_all, distance);
    return hits_all;
  }
  RaycastHit[] shotRayAll(Vector3 dir, Vector3 pos, Color color, int distance){
    Vector3 position = gameObject.transform.position + pos;
    Ray ray = new Ray (position, transform.TransformDirection(dir));
    Debug.DrawLine (ray.origin, ray.direction*100, color);
    RaycastHit[] hits_all = Physics.RaycastAll(ray, distance);
    return hits_all;
  }

  // 捕食
  public void OnEatOther(GameObject other){
    Debug.Log("捕食！");
  }

  IEnumerator eatProcess(GameObject colliObj){
    while(true){
      if(colliObj == null) {
        transform.Find("Sakuteki").localScale = new Vector3(5,0.0537f,5);
        yield break;
      }
      if(colliObj.tag == "Mob" || colliObj.tag == "Player"){
        if(this.canEat(colliObj.GetComponent<Life>())){
          Life foodMob = colliObj.GetComponent<Life>();
          foodMob.whenAttacked();
          if(gameObject.transform.Find("Player(Clone)") != null){
            Player player = gameObject.transform.Find("Player(Clone)").GetComponent<Player>();
            player.eatMob(foodMob.name, foodMob.exp);
          }
          int beforeHP = foodMob.hp;
          int afterHp = foodMob.hp - 1000;
          foodMob.hp = afterHp;
          if(afterHp <= 0) { this.hp += beforeHP; }
        }
      }
      else if(colliObj.tag == "Plant"){
        if(this.canEat(colliObj.GetComponent<Life>())){
          PlantController foodPlant = colliObj.GetComponent<PlantController>();
          this.hp += foodPlant.hp;
          foodPlant.hp = 0;
          if(gameObject.transform.Find("Player(Clone)") != null){
            Player player = gameObject.transform.Find("Player(Clone)").GetComponent<Player>();
            player.eatMob(foodPlant.name, foodPlant.exp);
          }
        }
      }
      yield return new WaitForSeconds(0.5f);
    }
  }

  IEnumerator eatingRoutine;
  void OnCollisionEnter (Collision collision){
    eatingRoutine = eatProcess(collision.gameObject);
    StartCoroutine(eatingRoutine);
  }
  void OnCollisionExit (Collision collision){
    StopCoroutine(eatingRoutine);
  }
  public bool isAutomatic;
	void Update () {
    base.Update();
    if(gameObject.transform.Find("Player(Clone)") == null){
      Automatic();
    } else {
      if(Input.GetKeyDown("l")){
        isAutomatic=true;
      }
    }

    if(isAutomatic){
      Automatic();
      if(Input.GetKeyUp("l")){
        isAutomatic=false;
      }
    }


    if(transform.position.y < -40){
      hp=0;
    }
    hpSlider.maxValue = maxHp;
    hpSlider.value = hp;
    if(hp <= 0){
      if(gameObject.transform.Find("Player(Clone)") != null){
        Transform player = gameObject.transform.Find("Player(Clone)");
        player.parent = null;
        Transform camera=gameObject.transform.Find("Camera");
        camera.parent = player;
        camera.rotation = player.transform.rotation;
        camera.localPosition = defaultCameraPosition;
        player.GetComponent<Player>().independentMode();
      }
      Destroy(gameObject);
    }
  }
}
