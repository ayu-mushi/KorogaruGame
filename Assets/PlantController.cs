using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : Life
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      if(transform.position.y < -40){
        hp=0;
      }
      if(hp <= 0){
        Destroy(gameObject);
      }
    }
    void OnCollisionEnter(Collision other){
      if(other.gameObject.name.Contains("Plant")){
        hp=0;
      }
    }
}
