using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	private GameObject[] puzzlePieces;
	private Sprite[] puzzleImages;
	private Vector3 screenPosToAnim;
	private PuzzlePiece pieceToAnim;
	private PuzzlePiece[,] matrix = new PuzzlePiece[GameVariables.MaxRows, GameVariables.MaxColumns];
	private int toAnimRow, toAnimColumn;
	private float animSpeed = 10f;
	private int puzzleIndex;
	private GameState gameState;

	private void Awake () {
		MakeSingleton ();
	}

	private void Start () {
		puzzleIndex = -1;
	}

	private void OnLevelWasLoaded () {
		if (SceneManager.GetActiveScene ().name == "main") {
			if (puzzleIndex > 0) {
				LoadPuzzle ();
				GameStarted ();
			}
		}
	}

	public void SetIndex (int puzzleIndex) {
		this.puzzleIndex = puzzleIndex;
	}

	private void LoadPuzzle () {
		puzzleImages = Resources.LoadAll<Sprite> ("sprites/BG " + puzzleIndex);
		puzzlePieces = GameObject.FindGameObjectWithTag ("puzzleHolder").GetComponent<PuzzleHolder> ().puzzlePieces;

		for (int i = 0; i < puzzlePieces.Length; i++) {
			puzzlePieces [i].GetComponent<SpriteRenderer> ().sprite = puzzleImages [i];
		}
	}

	private Vector3 GetScreenCoordsFromViewport (int row, int column) {
		Vector3 point = Camera.main.ViewportToWorldPoint (new Vector3((0.185f * row) + 0.1f, (1 - 0.19f * column) - 0.1f, 0));
		point.z = 0;
		return point;
	}

	private void GameStarted () {
		int index = Random.Range (0, GameVariables.MaxSize);
		print (index);
		puzzlePieces [index].gameObject.SetActive(false);

		for (int row = 0; row < GameVariables.MaxRows; row++) {
			for (int column = 0; column < GameVariables.MaxColumns; column++) {

				if (puzzlePieces [row * GameVariables.MaxColumns + column].activeInHierarchy) {
					Vector3 point = GetScreenCoordsFromViewport (row, column);
					puzzlePieces [row * GameVariables.MaxColumns + column].transform.position = point;
					matrix [row, column] = new PuzzlePiece ();
					matrix [row, column].GameObject = puzzlePieces [row * GameVariables.MaxColumns + column];
					matrix [row, column].OriginalRow = row;
					matrix [row, column].OriginalColumn = column;
				} else {
					matrix [row, column] = null;
				}

			}
		}
		Shuffle ();
		gameState = GameState.Playing;
	}

	private void Shuffle () {
		for (int row = 0; row < GameVariables.MaxRows; row++) {
			for (int column = 0; column < GameVariables.MaxColumns; column++) {
				if (matrix [row, column] == null) {
					continue;
				} else {
					int randomRow = Random.Range (0, GameVariables.MaxRows);
					int randomColumn = Random.Range (0, GameVariables.MaxColumns);

					Swap (row, column, randomRow, randomColumn);
				}
			}
		}
	}

	private void Swap (int row, int column, int randomRow, int randomColumn) {
		PuzzlePiece temp = matrix [row, column];
		matrix [row, column] = matrix [randomRow, randomColumn];
		matrix [randomRow, randomColumn] = temp;

		if (matrix [row, column] != null) {
			matrix [row, column].GameObject.transform.position = GetScreenCoordsFromViewport (row, column);
			matrix [row, column].CurrentRow = row;
			matrix [row, column].CurrentColumn = column;
		}

		matrix [randomRow, randomColumn].GameObject.transform.position = GetScreenCoordsFromViewport (randomRow, randomColumn);
		matrix [randomRow, randomColumn].CurrentRow = row;
		matrix [randomRow, randomColumn].CurrentColumn = column;
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
