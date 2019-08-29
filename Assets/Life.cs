using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int hp;
    public int classInHierarchy;
    public int exp;
    void Jump(){
    }
    protected void Parthenogenesis() { // 単為生殖
      Jump();
      GameObject child;
      child = Instantiate(gameObject) as GameObject;
      child.transform.Translate(0,0,-3);
      Destroy(child.transform.Find("Camera").gameObject);
      Destroy(child.transform.Find("Player").gameObject);
    }
    public bool canEat(Life l){
      int diff = this.classInHierarchy - l.classInHierarchy;
      return (diff <= 2 && 0 < diff);
    }
}
