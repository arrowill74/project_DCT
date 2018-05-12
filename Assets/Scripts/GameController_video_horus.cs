using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;

public class GameController_video_horus : MonoBehaviour {
	private SocketMgr SocketClient; //Client
	private Scene scene;
	VideoPlayer VP;

	void Start () {
		//		Client part
		SocketClient = new SocketMgr();
		SocketClient.Connect(Define.TargetIP, Define.TargetPort);
		Debug.Log ("Connect to "+Define.TargetIP+":"+Define.TargetPort);

		//		Send the active scene 
		scene = SceneManager.GetActiveScene();
		VP = GetComponent<VideoPlayer> ();
		VP.loopPointReached += changeScene;
		SocketClient.SendServer("{'scene':" + scene.buildIndex + " }");
	}
	void changeScene(VideoPlayer VP){
		SceneManager.LoadScene(scene.buildIndex+1);
	}
}