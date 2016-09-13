using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameplayController : MonoBehaviour {

	public void BackToLevelSelect () {
		SceneManager.LoadScene ("levelSelect", LoadSceneMode.Single);
	}
}
