using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelController : MonoBehaviour {

	public void SelectLevel () {

		string[] name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name.Split ();

		int index = int.Parse (name [1]);

		SceneManager.LoadScene ("main", LoadSceneMode.Single);
	}

	public void BackToMainMenu () {
		SceneManager.LoadScene ("menu", LoadSceneMode.Single);
	}
}
