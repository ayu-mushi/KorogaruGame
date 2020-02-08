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
    public void initializeHp(){
      hp = (int)(maxHp*0.8f);
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
        Destroy(child.transform.Find("Player(Clone)").gameObject);
      }
    }
    public bool canEat(Life l){
      int diff = this.classInHierarchy - l.classInHierarchy;
      bool result;
      if(this.classInHierarchy > 2){
         result = (diff <= 2 && 0 < diff);
      }
      else {
         result = (diff <= 1 && 0 < diff);
      }
      return result;
    }

    protected Dictionary<Material, Color> _mats;
    protected void DetectAllRenderer(GameObject target)
    {
        _mats = new Dictionary<Material, Color>();
        Renderer[] renderers = target.GetComponentsInChildren<Renderer>();
        foreach (Renderer render in renderers)//レンダラーすべてを調べる
        {

            foreach (Material material in render.materials)//マテリアルすべてを調査
            {
                if (material.HasProperty("_Color"))
                {
                    _mats.Add(material, material.color);
                    break;
                }
            }
        }
    }
  float flickerStop=0;
  public void whenAttacked(){
    flickerStop = Time.time + 0.7f;
  }
  public virtual void Update(){
     if(_mats!=null){
       if( flickerStop < Time.time){
         foreach(KeyValuePair<Material, Color> kvp in _mats){
           kvp.Key.color = kvp.Value;
         }
       }
       else {
          Color flickerColor = new Color(2*(Mathf.Sin(Time.time*100)+1f), 0, 0, 0);
          foreach(KeyValuePair<Material, Color> kvp in _mats){
            kvp.Key.color = kvp.Value + flickerColor;
          }
       }
     }
  }
  public virtual void Start(){
    DetectAllRenderer(gameObject);
  }
}
