using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGenerator : MonoBehaviour
{
    // Plant以外も
    // Start is called before the first frame update
    public GameObject plant;
    public int plantNumber;
    public GameObject penguin;
    public int penguinNumber;
    public GameObject lion;
    public int lionNumber;
    public GameObject cat;
    public int catNumber;
    public GameObject dog;
    public int dogNumber;
    public GameObject chicken;
    public int chickenNumber;
    public GameObject hunter;
    public int hunterNumber;

    Transform sizeViewer;


    void Start()
    {
      sizeViewer = transform.Find("SizeViewer");
      GenerateN(plantNumber, plant);
      GenerateN(penguinNumber, penguin);
      GenerateN(chickenNumber, chicken);
      GenerateN(dogNumber,dog);
      GenerateN(catNumber,cat);
      GenerateN(lionNumber,lion);
      GenerateN(hunterNumber,hunter);
    }
    void GetPlayerHyoui(){
      GameObject dog2 = GameObject.Find("Dog Variant(Clone)");
      GameObject player = GameObject.Find("Player(Clone)");
      Player playerp = player.GetComponent<Player>();
      playerp.level=6;
      playerp.OnHyouiLaserCollision(dog2);
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
        if(sizeViewer==null){Debug.Log("sizeViewerがnull");}
        else{
        Vector3 origin = transform.position;
        Vector3 hantaikaku_vector = new Vector3(sizeViewer.lossyScale.x, 0, sizeViewer.lossyScale.z);
        Vector3 end = origin + transform.rotation * hantaikaku_vector;
        /*float kaiten = transform.eulerAngles.y * Mathf.Deg2Rad;
        Debug.Log(kaiten);
        Vector3 kaitened_vector = new Vector3 (Mathf.Cos(kaiten) * end.x - Mathf.Sin(kaiten) * end.z,
            0,
                           Mathf.Sin(kaiten) * end.x + Mathf.Cos(kaiten) * end.z);
        end = origin + kaitened_vector;*/

        Vector3 hanni_vector = new Vector3(0, 0, 0);
        hanni_vector.x = Random.RandomRange(0.5f, hantaikaku_vector.x-1);
        hanni_vector.y = 0;
        hanni_vector.z = Random.RandomRange(0.5f, hantaikaku_vector.z-1);
        Vector3 pos = origin + transform.rotation * hanni_vector;


        newplant.transform.position = pos;
        //newplant.transform.parent = transform;
        //hyoui.transform.localPosition = hyoui.transform.position;
        //hyoui.transform.localEulerAngles = hyoui.transform.eulerAngles;
        }
    }

    int countMobs(string name){
      int animalNumber=0;
      // typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
      //foreach (Transform child in transform)
      foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Mob"))
      {
        //GameObject obj = transform.gameObject;
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
