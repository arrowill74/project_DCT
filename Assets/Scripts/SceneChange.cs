using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneChange : MonoBehaviour {

	VideoPlayer VP;
	private Scene scene;

	// Use this for initialization
	void Start () {
		VP = GetComponent<VideoPlayer> ();
		VP.loopPointReached += changeScene;
		scene = SceneManager.GetActiveScene();
	}

	void changeScene(VideoPlayer VP){
		SceneManager.LoadScene(scene.buildIndex+1);
	}
}
