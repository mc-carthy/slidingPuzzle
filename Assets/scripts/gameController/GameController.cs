using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	private GameObject[] puzzlePieces;
	private Sprite[] puzzleImages;
	private Vector3 screenPosToAnim;
	private PuzzlePiece pieceToAnim;
	private int toAnimRow, toAnimColumn;
	private float animSpeed = 10f;
	private int puzzleIndex;
	private GameState gameState;

	private void Awake () {
		MakeSingleton ();
	}

	public void SetIndex (int puzzleIndex) {
		this.puzzleIndex = puzzleIndex;
	}

	private void MakeSingleton () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

}
