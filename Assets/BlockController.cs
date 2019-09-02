using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      rigidbody.AddForce(0, 0, -1);
    }
    void OnCollisionEnter(Collision other){
      if(other.gameObject.tag == "Terrain" || other.gameObject.tag == "Block" ){
        Debug.Log("BlockがTerrainに衝突");
        //rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        transform.parent = null;
      }
    }
}
