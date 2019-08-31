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
    void Jump(){
    }
    public void initializeMaxHp(){
      maxHp = (int)(initialHp * 1.5);
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
