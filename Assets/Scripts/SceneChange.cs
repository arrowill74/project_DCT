using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneChange : MonoBehaviour {

	VideoPlayer VP;

	// Use this for initialization
	void Start () {
		VP = GetComponent<VideoPlayer> ();
		VP.loopPointReached += changeScene;
	}

	void changeScene(VideoPlayer VP){
		SceneManager.LoadScene("game1-1");
	}
}
