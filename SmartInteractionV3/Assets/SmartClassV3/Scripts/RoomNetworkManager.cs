using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

[System.Serializable]
public class DefaultRoom
{
    public string name;
    public int sceneIndex;
    public int maxPlayer;
}
public class RoomNetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> defaultRooms;
    public TextMeshProUGUI OccupacyRate;
    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server!...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("We Join the Lobby");
        this.initializeRoom();
    }

    public void initializeRoom()
    {
        DefaultRoom roomSettings = defaultRooms[0];
        //LOAD ROOM
        PhotonNetwork.LoadLevel(roomSettings.sceneIndex);
        //CREATE ROOM
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.maxPlayer;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom(roomSettings.name, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("Salon");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New Player Joined");
        base.OnPlayerEnteredRoom(newPlayer);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            OccupacyRate.text = 0 + " / " + 20;
        }
        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_SALON))
            {
                Debug.Log("Room is Salon. Player count is: " + room.PlayerCount);
                OccupacyRate.text = room.PlayerCount + " / " + 20;
            }
        }
    }

}
