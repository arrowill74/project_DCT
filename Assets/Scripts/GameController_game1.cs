using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;

public class GameController_game1 : MonoBehaviour {
	private Define Define; //IP and Port

	private SocketMgr SocketClient; //Client
	private ServerThread st; //Server
	private bool isSend;//儲存是否發送訊息完畢

	string jsonStr;
	Status jsonData = new Status();

	private Scene scene;

	void Start () {
		//		Client part
		SocketClient = new SocketMgr();
		SocketClient.Connect(Define.TargetIP, Define.TargetPort);
		Debug.Log ("Connect to "+Define.TargetIP+":"+Define.TargetPort);

		//		Server part
		st = new ServerThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, Define.ServerIP, Define.ServerPort);
		Debug.Log ("Server is on "+Define.ServerIP+":"+Define.ServerPort);
		st.Listen();//讓Server socket開始監聽連線
		st.StartConnect();//開啟Server socket
		isSend = true;

		//		Send the active scene 
		scene = SceneManager.GetActiveScene();
		SocketClient.SendServer("{'scene':" + scene.buildIndex + " }");
	}

	void Update(){

		if (st.receiveMessage != null)
		{
			Debug.Log("Message Content: " + st.receiveMessage);
			jsonStr = st.receiveMessage;

			try {
				jsonData = (Status)JsonUtility.FromJson<Status> (jsonStr);
			}
			catch(Exception e){
				st.Send("Fail!!!");
				st.StopConnect();
				st = new ServerThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, Define.ServerIP, Define.ServerPort);
				Debug.Log ("Server restart on "+Define.ServerIP+":"+Define.ServerPort);
				st.Listen();//讓Server socket開始監聽連線
				st.StartConnect();//開啟Server socket
				isSend = true;
			}
			if (jsonData.Stele == 1) { //trigger signal
				SceneManager.LoadScene(scene.buildIndex+1); //load next scene
			}
			if (jsonData.loadScene != 10){
				SceneManager.LoadScene(jsonData.loadScene);
			}
			st.receiveMessage = null;
		}
		if (isSend == true)
			StartCoroutine(delaySend());//延遲發送訊息

		st.Receive();
	}


	private IEnumerator delaySend()
	{
		isSend = false;
		st.Send("Message Received");
		yield return new WaitForSeconds(1);//延遲1秒後才發送
		isSend = true;
	}

	private void OnApplicationQuit()//應用程式結束時自動關閉連線
	{
		st.StopConnect();
	}
}
