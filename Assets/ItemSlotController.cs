using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    // Update is called once per frame
    void Update()
    {
    }

    public void MyPointerDownUI(){
      Debug.Log("clicked"+gameObject.name);
      //player = GameObject.Find("Player(Clone)").GetComponent<Player>();
      //player.blockPrefab=prefab;
      transform.parent.gameObject.GetComponent<ItemboxController>().focus(gameObject);
    }
}
