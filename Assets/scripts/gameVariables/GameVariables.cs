using UnityEngine;
using System.Collections;

public static class GameVariables {

	public static int MaxRows = 5;
	public static int MaxColumns = 5;
	public static int MaxSize = MaxRows * MaxColumns;

}

public enum GameState {
	Playing,
	Animating,
	End
}