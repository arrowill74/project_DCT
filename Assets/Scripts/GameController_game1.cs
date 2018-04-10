using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

public class GameController_game1 : MonoBehaviour {
	private ServerThread st;
	private bool isSend;//儲存是否發送訊息完畢
	private SocketMgr SocketClient;
	public GameObject tomb;

	string jsonStr;
	Status jsonData = new Status();

    void Start () {
//		Client part
		SocketClient = new SocketMgr();
		SocketClient.Connect(Define.IP, Define.Port);

//		Server part
		st = new ServerThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "127.0.0.1", 8080);
		st.Listen();//讓Server socket開始監聽連線
		st.StartConnect();//開啟Server socket
		isSend = true;

//		jsonStr part
//		jsonStr = File.ReadAllText (Application.dataPath+"/Scripts/status.json");
//		SocketClient.SendServer (jsonStr);
	}

	void Update(){
		if (st.receiveMessage != null)
		{
			Debug.Log("Client:" + st.receiveMessage);
			jsonStr = st.receiveMessage;
			jsonData = (Status)JsonUtility.FromJson<Status> (jsonStr);
			Debug.Log (jsonData.card_horus);
			if (jsonData.game1 == 0) {
				SceneManager.LoadScene("video_tombTranslation");
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
		yield return new WaitForSeconds(1);//延遲1秒後才發送
		st.Send("Hello~ My name is Server");
		isSend = true;
	}

	private void OnApplicationQuit()//應用程式結束時自動關閉連線
	{
		st.StopConnect();
	}
}
