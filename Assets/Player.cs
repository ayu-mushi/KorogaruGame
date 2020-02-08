using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;
using UnityEngine.SceneManagement;

public class Player : Life {
  //オンライン化に必要なコンポーネントを設定
  //public PhotonView myPV;
  //public PhotonTransformView myPTV;

  //private Camera mainCam;

  GameObject hijacked;
  GameObject camera;
  public int level=1;
  int playerExp;
  Text playerHpText;
  Text playerExpText;
  Text levelText;
  public GameObject hyouiLaser;
  Text descriptionText;
  public bool gameOverIdou;
  public GameObject itembox;
  public Rigidbody rigidbody;

	GameObject pauseUI;
	void Start () {
    /*if (!myPV.isMine)    //自キャラであれば実行
    {return;}*/
    base.Start();


    pauseUI = GameObject.Find("Pause");
    pauseUI.SetActive(false);
    /*//MainCameraのtargetにこのゲームオブジェクトを設定
    mainCam = Camera.main;
    mainCam.transform.parent = gameObject.transform;*/

    camera = transform.Find("Camera").gameObject;
    itembox = GameObject.Find("itembox").gameObject;
    playerExpText = GameObject.Find("Exp").GetComponent<Text>();
    levelText = GameObject.Find("Level").GetComponent<Text>();
    descriptionText = GameObject.Find("Description").GetComponent<Text>();
    playerHpText = GameObject.Find("PlayerHP").GetComponent<Text>();
    refreshLevel(0);
    refreshPlayerExp(0);
	}
  public void refreshLevel(int diff){
    level += diff;
    levelText.text = "レベル:" + level.ToString();
  }
  public void refreshPlayerExp(int diff){
    playerExp += diff;
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
      Destroy(hijacked.GetComponent<UnityEngine.AI.NavMeshAgent>());
    }
	}
  bool isHijacked(){
    return (hijacked!=null);
  }

  bool is_on_terrain = true;
  void OnCollisionEnter(Collision collision){
   if(collision.gameObject.tag == "Terrain"){
      is_on_terrain = true;
    }
  }
  public bool canMultipleJump;
  void Jump(){
    if(isHijacked()){
      hijacked.GetComponent<Mob>().Jump();
    } else {
      if (!canMultipleJump && !is_on_terrain) return;
      is_on_terrain = false;
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
  void MakeBlock(){
    if(playerExp < 2) {return;}
    refreshPlayerExp(-2);
    GameObject jibun;
    if(isHijacked()){
      jibun = hijacked;
    } else {
      jibun = gameObject;
    }
    GameObject block;
    block= Instantiate(itembox.GetComponent<ItemboxController>().focusPrefab()) as GameObject;
    block.transform.parent = jibun.transform;
    block.transform.localPosition = block.transform.position;
    block.transform.localEulerAngles = block.transform.eulerAngles;
  }

  public void independentMode(){
    this.transform.localScale = new Vector3 (1f, 1f, 1f);
    rigidbody.isKinematic=false;
    gameObject.layer = LayerMask.NameToLayer("Default");
  }

  public void dependentMode(){
    this.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
    this.transform.localPosition = new Vector3(0,0,0);
    this.transform.localEulerAngles = new Vector3(0,0,0);
    rigidbody.isKinematic=true;
    gameObject.layer = LayerMask.NameToLayer("NoHantei");
  }

  public void eatMob(string eatenMob, int exp){
    //Debug.Log(eatenMob);
    refreshPlayerExp(exp);
    //Debug.Log(playerExp);
  }

  void LevelUpIfPossible(){
    if(playerExp >= 20){
      refreshLevel(1);
      refreshPlayerExp(-20);
    }
  }

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
        /*if (!myPV.isMine)
        {
            return;
        }*/

    base.Update();
    float x=0;
    float z=0;
    x = Input.GetAxis("Horizontal");
    if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))){
      float y = Input.GetAxis("Vertical");
      camera.transform.Rotate(-y, 0, 0);

      if(isHijacked()){
        hijacked.transform.Translate(x*Time.deltaTime*8, 0, 0);
      }
      else {
        transform.Translate(x*Time.deltaTime*8, 0, 0);
      }
      x=0;
    }
    else{
      z = Input.GetAxis("Vertical");
    }
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
      if(transform.Find("hyouiLaserPivot(Clone)") == null)
      HyouiStart();
      else Destroy(transform.Find("hyouiLaserPivot(Clone)").gameObject);
    }
    if(Input.GetKeyDown(KeyCode.B)){
      MakeBlock();
    }
    if(Input.GetKeyDown(KeyCode.C)){
      hp+=hijacked.GetComponent<Life>().hp/2;
      hijacked.GetComponent<Life>().hp=0;
    }
    if(Input.GetKeyDown(KeyCode.M)){
      maxHp=1000000;
      hp=100000;
      refreshPlayerExp(19);
      refreshLevel(6);
    }
    if(transform.position.y < -40){
      Debug.Log("落下死");
      hp=0;
    }
    if(hp <= 0){
      camera.transform.parent = null;
      Destroy(gameObject);
      if(gameOverIdou){SceneManager.LoadScene("GameOver");}
    }
    if (Input.GetMouseButtonDown(0)) {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit = new RaycastHit();

      if (Physics.Raycast(ray, out hit)) {
        GameObject clickedGameObject = hit.collider.gameObject;
        if(clickedGameObject.tag == "Mob"){
          descriptionText.text = clickedGameObject.GetComponent<Mob>().Show();
        }
      }
    }
    EnablePause();
    //Debug.Log(_mats.ToString());
	}
}
