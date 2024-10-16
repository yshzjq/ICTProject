using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;

[System.Serializable]
public class Room
{
    public string name;
    public int scene;
}
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<Room> listRoom;
    //public GameObject roomUI;
    // Start is called before the first frame update
    public SenarioProcessReadyScript sprs;

    public void ServerConnect() // 서버 접속 후 방 들어가기
    {
        PhotonNetwork.ConnectUsingSettings();
        //CreateRoom("MainScene");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("룸 조인");
        sprs.joinRoomtrue();
        //roomUI.SetActive(true);
    }

    public void CreateRoom(string roomname)
    {
        PhotonNetwork.LoadLevel(roomname);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 15;

        PhotonNetwork.JoinOrCreateRoom(roomname, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }
}
