using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public int hp;
    public int classInHierarchy;
    public int exp;
    public int initialHp;
    public int maxHp;
    Text hpText;
    void Jump(){
    }
    public void initializeHpText(){
      GameObject hpTextObj = transform.Find("HPCanvasPrefab/HP").gameObject;
      if(hpTextObj !=null){
      hpText = hpTextObj.GetComponent<Text>();
      }
    }
    public void initializeMaxHp(){
      maxHp = (int)(initialHp * 1.5);
    }
    public void updateHpText(){
      if(hpText !=null) {
        hpText.text = hp.ToString() + "/" + maxHp.ToString();
      }
      //transform.Find("HPCanvasPrefab").LookAt(GameObject.Find("Player").transform);
    }
    public void Parthenogenesis() { // 単為生殖
      Jump();
      GameObject child;
      child = Instantiate(gameObject) as GameObject;
      child.transform.Translate(0,0,-3);
      this.hp /= 2;
      Life childLife = child.GetComponent<Life>();
      childLife.hp = this.hp;
      childLife.maxHp = this.maxHp;
      if(child.transform.Find("Camera") != null){
        Destroy(child.transform.Find("Camera").gameObject);
        Destroy(child.transform.Find("Player").gameObject);
      }
    }
    public bool canEat(Life l){
      int diff = this.classInHierarchy - l.classInHierarchy;
      return (diff <= 1 && 0 < diff);
    }
}
