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
        Vector3 new_pos =new Vector3 (0,0,0);
        Vector3 size = transform.lossyScale;
        new_pos.x = (float)(Mathf.Round(transform.position.x / size.x) * size.x);
        new_pos.y = (float)(Mathf.Round(transform.position.y / size.y) * size.y)+size.y/2;
        new_pos.z = (float)(Mathf.Round(transform.position.z / size.z) * size.z);
        transform.position = new_pos;
        transform.rotation = Quaternion.identity;
      }
    }
}
