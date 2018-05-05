using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;

public class GameController_game1 : MonoBehaviour {
	private ServerThread st;
	private bool isSend;//儲存是否發送訊息完畢
	private SocketMgr SocketClient;
	private Define Define;
	string jsonStr;
	Status jsonData = new Status();
	private Scene scene;
	void Start () {
		//		Client part
		SocketClient = new SocketMgr();
		SocketClient.Connect(Define.ServerIP, Define.ServerPort);

		//		Server part
		st = new ServerThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, Define.ServerIP, Define.ServerPort);
		Debug.Log (Define.ServerIP);

		st.Listen();//讓Server socket開始監聽連線
		st.StartConnect();//開啟Server socket
		isSend = true;

		scene = SceneManager.GetActiveScene();
		SocketClient.SendServer("{scene:" + scene.buildIndex + " }");
	}

	void Update(){
		if (st.receiveMessage != null)
		{
			Debug.Log("Client:" + st.receiveMessage);
			jsonStr = st.receiveMessage;

			try {
				jsonData = (Status)JsonUtility.FromJson<Status> (jsonStr);
			}
			catch(Exception e){
				st.Send("Fail!!!");
			}

			Debug.Log (jsonData.horus);
			if (jsonData.Stele == 0) {
				SceneManager.LoadScene("video_tombTranslation");
			}
			if (jsonData.scene != scene.buildIndex){
				SceneManager.LoadScene(jsonData.scene);
			}
			st.receiveMessage = null;
			Debug.Log(st.receiveMessage);

		}
		if (isSend == true)
			StartCoroutine(delaySend());//延遲發送訊息

		st.Receive();
	}


	private IEnumerator delaySend()
	{
		isSend = false;
		st.Send("Accept!!!");
		yield return new WaitForSeconds(1);//延遲1秒後才發送
		isSend = true;
	}

	private void OnApplicationQuit()//應用程式結束時自動關閉連線
	{
		st.StopConnect();
	}
}
