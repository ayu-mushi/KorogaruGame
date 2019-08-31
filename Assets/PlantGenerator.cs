using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGenerator : MonoBehaviour
{
    // Plant以外も
    // Start is called before the first frame update
    public GameObject plant;
    public GameObject penguin;
    public GameObject lion;
    public GameObject cat;
    public GameObject dog;
    public GameObject chicken;
    public GameObject hunter;



    void Start()
    {
      GenerateN(2, plant);
      GenerateN(2, penguin);
      GenerateN(2, chicken);
    }
    float genCount;
    float r = 0.3f;
    float K = 40;
    float maxCount = 1;
    void Update(){
      genCount += Time.deltaTime;
      /*if(transform.childCount==0){
        maxCount = 5;
      }
      else {
        maxCount = 5/(transform.childCount * (1 - transform.childCount/500));
      }*/
      int N = transform.childCount;
      maxCount = 1/(N*0.1f); ///(N*(r*N*(1-(N/K))));
      if(genCount > maxCount){
        Generate(plant);
        genCount = 0;
      }
    }

    public void GenerateN(float n_, GameObject nanika){
      if(n_ <= 0) {return;}
      /*float saikoro = UnityEngine.Random.Range(0, 1);
      float sikiichi = (saikoro + (float)System.Math.Floor(n_));
      int n;
      if(sikiichi < n_){
        n = (int)(System.Math.Floor(n_) + 1);
      } else {
        n = (int)System.Math.Floor(n_);
      }*/
      int n = (int)System.Math.Floor(n_);
      for(int i =0; n!=i; i++){
        Generate(nanika);
      }
    }
    // Update is called once per frame
    public void Generate(GameObject nanika)
    {
        GameObject newplant = Instantiate(nanika);
        Vector3 pos = newplant.transform.position;
        pos.x = Random.RandomRange(0, 50f);
        pos.y = 0;
        pos.z = Random.RandomRange(0, 60f);
        newplant.transform.position = pos;
        newplant.transform.parent = gameObject.transform;
    }
}
