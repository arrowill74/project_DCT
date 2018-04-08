using UnityEngine;
using System.Collections;
using System.Net.Sockets;

public class Client : MonoBehaviour
{
	private ClientThread ct;
	private bool isSend;
	private bool isReceive;

	private void Start()
	{
		ct = new ClientThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "127.0.0.1", 8000);
		ct.StartConnect();
		isSend = true;
	}

	private void Update()
	{
		if (ct.receiveMessage != null)
		{
			Debug.Log("Server:" + ct.receiveMessage);
			ct.receiveMessage = null;
		}
		if (isSend == true)
			StartCoroutine(delaySend());

		ct.Receive();
	}

	private IEnumerator delaySend()
	{
		isSend = false;
		yield return new WaitForSeconds(1);
		ct.Send("Hello~ My name is Client");
		isSend = true;
	}

	private void OnApplicationQuit()
	{
		ct.StopConnect();
	}
}