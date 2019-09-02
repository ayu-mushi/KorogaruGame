using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakuteki : MonoBehaviour
{
    Mob doubutu;
    // Start is called before the first frame update
    //
    void Start()
    {
      doubutu = transform.parent.gameObject.GetComponent<Mob>();
    }

    // Update is called once per frame
    void Update()
    {
    }
	  public void OnTriggerStay(Collider col){
      if(doubutu!=null){
	  	  doubutu.OnSakutekiStay(col, transform);
      }
	  }
	  public void OnTriggerExit(Collider col){
	  	doubutu.OnSakutekiExit(col, transform);
	  }
}
