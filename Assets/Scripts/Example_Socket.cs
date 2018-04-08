using UnityEngine;
using System.Collections;

public class Example_Socket : MonoBehaviour {

	private SocketMgr mSocketMgr;

	void Start () {
		mSocketMgr = new SocketMgr();
	}

	public void OnClickConnect() {
		mSocketMgr.Connect(Define.IP, Define.Port);
	}

	public void OnClickClose() {
		mSocketMgr.Close();
	}

	public void OnClickSend() {
		mSocketMgr.SendServer("{Test:123456}");
	}
}