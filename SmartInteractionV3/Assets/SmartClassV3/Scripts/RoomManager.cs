using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;
    public TextMeshProUGUI OccupacyRate;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();

    }

    public void OnEnterButtonClicked_Salon()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_SALON;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }

    #region Callbacks Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to servers again");
        PhotonNetwork.JoinLobby();

    }
    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The local player: " + PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " Player Count " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
        {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType))
            {
                Debug.Log("Joined room with the map: " + (string)mapType);
                if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_SALON)
                {
                    PhotonNetwork.LoadLevel("Salon");
                }
            }

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to: " + "Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
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

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the lobby");
    }


    #endregion

    #region Private Methods

    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;


        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }

    #endregion
}