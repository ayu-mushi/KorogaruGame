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

    Transform sizeViewer;


    void Start()
    {
      sizeViewer = transform.Find("SizeViewer");
      GenerateN(2, plant);
      GenerateN(2, penguin);
      GenerateN(2, chicken);
      GenerateN(1,cat);
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
      int N = countMobs("Plant");
      if(N > 200){
        maxCount = 1;
      }
      if(N==0){
        maxCount = 0.1f;
      }
      else {
        maxCount = 1/(N*0.1f); ///(N*(r*N*(1-(N/K))));
      }
      if(genCount > maxCount){
        Generate(plant);
        genCount = 0;
      }
      if((countMobs("Penguin") > 20 || countMobs("Chicken") > 20)&& countMobs("Cat") == 0){
        GenerateN(1,cat);
      }
      if(countMobs("Cat") > 20 && countMobs("Lion") == 0){
        GenerateN(1,lion);
      }
      if(countMobs("Lion") > 20 && countMobs("unity") == 0){
        GenerateN(1,hunter);
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
        if(sizeViewer==null){Debug.Log("sizeViewerがnull");}
        else{
        pos.x = Random.RandomRange(transform.position.x, transform.position.x + sizeViewer.lossyScale.x);
        pos.y = 2;
        pos.z = Random.RandomRange(transform.position.z, transform.position.z + sizeViewer.lossyScale.z);
        newplant.transform.position = pos;
        }
    }

    int countMobs(string name){
      int animalNumber=0;
      // typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
      GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
      foreach (GameObject obj in mobs)
      {
        if (obj.activeInHierarchy){
          if(obj.name.Contains(name)){
            animalNumber+=1;
          }
        }
      }
      // typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
      foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Plant"))
      {
          // シーン上に存在するオブジェクトならば処理.
          if (obj.activeInHierarchy)
          {
            if(obj.name.Contains(name)){
              animalNumber+=1;
            }
          }
      }
      return animalNumber;
    }
}
