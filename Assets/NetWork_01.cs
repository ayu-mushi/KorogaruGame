using UnityEngine;
using System.Collections;
 
public class NetWork_01 : MonoBehaviour {
 
    void Start() {
        // Photonに接続する(引数でゲームのバージョンを指定できる)
        PhotonNetwork.ConnectUsingSettings(null);
    }
 
    // ロビーに入ると呼ばれる
    void OnJoinedLobby() {
        Debug.Log("ロビーに入りました。");
 
        // ルームに入室する
        PhotonNetwork.JoinRandomRoom();
    }
 
    // ルームに入室すると呼ばれる
    void OnJoinedRoom() {
        Debug.Log("ルームへ入室しました。");
    }
 
    // ルームの入室に失敗すると呼ばれる
    void OnPhotonRandomJoinFailed() {
        Debug.Log("ルームの入室に失敗しました。");
 
        // ルームがないと入室に失敗するため、その時は自分で作る
        // 引数でルーム名を指定できる
        PhotonNetwork.CreateRoom("myRoomName");
    }
}
