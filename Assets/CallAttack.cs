using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnCollisionEnter(Collision other){
      //if(other.gameObject.tag=="Mob"){
      GameObject p = transform.parent.gameObject;
      p.GetComponent<Mob>().OnEatOther(other.gameObject);
      //}
    }
}
