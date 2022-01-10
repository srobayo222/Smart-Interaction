 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is responsible for creating and joining rooms",MessageType.Info);

        RoomManager roomManager = (RoomManager)target;
        //if(GUILayout.Button("Join Salon"))
        if(GUILayout.Button("Join Random Room"))
        {
            roomManager.JoinRandomRoom();
            //roomManager.OnEnterButtonClicked_Salon();
        }
        if (GUILayout.Button("Join Salon"))
        {
            roomManager.OnEnterButtonClicked_Salon();
        }
        RoomNetworkManager roomNetworkManager = (RoomNetworkManager)target;
        //if(GUILayout.Button("Join Salon"))
        if (GUILayout.Button("Join Random Room"))
        {
            roomNetworkManager.initializeRoom();
            //roomManager.OnEnterButtonClicked_Salon();
        }

    }

}
