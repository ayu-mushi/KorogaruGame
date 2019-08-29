using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGenerator : MonoBehaviour
{
  // Plant以外も
    public GameObject terrain;
    // Start is called before the first frame update
    public GameObject plant;

    void Start()
    {
      InvokeRepeating("Generate", 1, 1);
    }

    // Update is called once per frame
    void Generate()
    {
        GameObject newplant = Instantiate(plant);
        Vector3 pos = newplant.transform.position;
        pos.x = Random.RandomRange(0, 50f);
        pos.y = 0;
        pos.z = Random.RandomRange(0, 60f);
        newplant.transform.position = pos;
    }
}
