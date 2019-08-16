using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;       //プレイヤーゲームオブジェクトへの参照を格納する Public 変数


    private Vector3 offset;         //プレイヤーとカメラ間のオフセット距離を格納する Public 変数


    // イニシャライゼーションに使用ます。
    void Start () 
    {
        //プレイヤーとカメラ間の距離を取得してそのオフセット値を計算し、格納します。
        offset = transform.position - player.transform.position;
    }
    
    // 各フレームで、Update の後に LateUpdate が呼び出されます。
    void LateUpdate () 
    {
        //カメラの transform 位置をプレイヤーのものと等しく設定します。ただし、計算されたオフセット距離によるずれも加えます。
        transform.position = player.transform.position + offset;
    }
}
