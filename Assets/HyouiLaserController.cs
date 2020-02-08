using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyouiLaserController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(0,0,Time.deltaTime*5);
    }

    private void OnTriggerEnter(Collider col){
      //Debug.Log("当たったよ");
      if(col.gameObject.tag=="Mob"){
      Player player = gameObject.transform.parent.gameObject.GetComponent<Player>();
      player.OnHyouiLaserCollision(col.gameObject);
      Destroy(gameObject);
      }
    }
}
