using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour//Photon.PunBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {

    /*if (!PhotonNetwork.connected)   //Phootnに接続されていなければ
    {
      //SceneManager.LoadScene("Launcher"); //ログイン画面に戻る
      return;
    }*/
      //GameObject Player = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(10f, 10f, 10f), Quaternion.identity, 0);
      GameObject Player = Instantiate(this.playerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
