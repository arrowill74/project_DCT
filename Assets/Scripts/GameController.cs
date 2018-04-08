using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	private SocketMgr mSocketMgr;
	// Use this for initialization
	void Start () {
		mSocketMgr = new SocketMgr();
		mSocketMgr.Connect(Define.IP, Define.Port);
	}



}
