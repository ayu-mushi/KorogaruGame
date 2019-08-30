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
    Text hpText;
    void Jump(){
    }
    public void initializeHpText(){
      GameObject hpTextObj = transform.FindChild("Canvas/HP");
      hpText = hpTextObj.GetComponent<Text>();
    }
    public void updateHpText(){
      if(hpText !=null) {
        hpText.text = hp.ToString();
      }
    }
    protected void Parthenogenesis() { // 単為生殖
      Jump();
      GameObject child;
      child = Instantiate(gameObject) as GameObject;
      child.transform.Translate(0,0,-3);
      this.hp /= 2;
      child.GetComponent<Life>().hp = this.hp;
      Destroy(child.transform.Find("Camera").gameObject);
      Destroy(child.transform.Find("Player").gameObject);
    }
    public bool canEat(Life l){
      int diff = this.classInHierarchy - l.classInHierarchy;
      return (diff <= 2 && 0 < diff);
    }
}
