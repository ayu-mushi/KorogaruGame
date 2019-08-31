using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;
using UnityEngine.SceneManagement;

public class Player : Life {

  GameObject hijacked;
  GameObject camera;
  int level=1;
  int playerExp;
  Text playerHpText;
  public Text playerExpTextObj;
  Text playerExpText;
  public Text levelTextObj;
  Text levelText;
  public GameObject hyouiLaser;
  Text descriptionText;


	void Start () {
    camera = GameObject.Find("Camera");
    playerExpText = playerExpTextObj.GetComponent<Text>();
    levelText = levelTextObj.GetComponent<Text>();
    descriptionText = GameObject.Find("Description").GetComponent<Text>();
    playerHpText = GameObject.Find("PlayerHP").GetComponent<Text>();
    refreshLevel();
    refreshPlayerExp();
	}
  public void refreshLevel(){
    levelText.text = "レベル:" + level.ToString();
  }
  public void refreshPlayerExp(){
    playerExpText.text = "経験値:" + playerExp.ToString();
  }
	public void OnHyouiLaserCollision(GameObject obj) {
    if(obj.tag == "Mob" && level >= obj.GetComponent<Life>().classInHierarchy){
      hijacked = obj;
		  Debug.Log("衝突");
      camera.transform.parent = obj.transform;
      camera.transform.rotation = obj.transform.rotation;
      this.transform.parent = hijacked.transform;
      hijacked.transform.parent =null;
      camera.transform.position = obj.transform.position;
      camera.transform.Translate(new Vector3(0, 4, -10));
      this.dependentMode();
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
      transform.Translate(0, 0, z*Time.deltaTime*8);
      transform.Rotate(0, x*60f*Time.deltaTime, 0);
    }
  }

  void HyouiStart(){
    GameObject hyoui = Instantiate(hyouiLaser) as GameObject;
    hyoui.transform.parent = gameObject.transform;
    hyoui.transform.localPosition = hyoui.transform.position;
    hyoui.transform.localEulerAngles = hyoui.transform.eulerAngles;
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

	[SerializeField]
	public GameObject pauseUI;
	void EnablePause(){
		if (Input.GetKeyDown ("q")) {
			//　ポーズUIのアクティブ、非アクティブを切り替え
			pauseUI.SetActive (!pauseUI.activeSelf);

			//　ポーズUIが表示されてる時は停止
			if(pauseUI.activeSelf) {
				Time.timeScale = 0f;
			//　ポーズUIが表示されてなければ通常通り進行
			} else {
				Time.timeScale = 1f;
			}
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
      Life hijackedLife = hijacked.GetComponent<Life>();
      if(hijackedLife.hp > hijackedLife.maxHp){
        hijackedLife.hp = hijackedLife.maxHp;
      }
      if(hijackedLife.hp >= hijackedLife.maxHp && Input.GetKey(KeyCode.X)){
        Debug.Log("hp:" + hijackedLife.hp.ToString());
        Debug.Log("maxHp:" + hijackedLife.maxHp.ToString());
        hijackedLife.Parthenogenesis();
      }
      //hijackedMobHPText.text = "憑依モブのHP:" + hijackedLife.hp.ToString()
       //                                        + "/" + hijackedLife.maxHp.ToString();
    }
    else {
      //hijackedMobHPText.text = "憑依モブのHP:" + "なし";
    }
    playerHpText.text = "プレイヤーHP:" + hp;
    LevelUpIfPossible();

    if(Input.GetKeyDown(KeyCode.Z)){
      HyouiStart();
    }
    if(Input.GetKeyDown(KeyCode.C)){
      hijacked.GetComponent<Life>().hp=0;
    }
    if(Input.GetKeyDown(KeyCode.M)){
      maxHp=1000000;
      hp=100000;
      level=6;
      refreshLevel();
    }
    if(transform.position.y < -40){
      hp=0;
    }
    if(hp <= 0){
      Destroy(gameObject);
      SceneManager.LoadScene("GameOver");
    }
    if (Input.GetMouseButtonDown(0)) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit)) {
          GameObject clickedGameObject = hit.collider.gameObject;
          descriptionText.text = clickedGameObject.ToString();
        }
    }
    EnablePause();
	}
}
