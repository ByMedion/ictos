using UnityEngine;

internal enum Direction
{
	LeftToRight = -1,
	RightToLeft = 1,
	BottomToTop = -2,
	TopToBottom = 2
}

internal static class KeyMap
{
	public static KeyCode TopLeft,
	                      TopMiddle,
	                      TopRight,
	                      MiddleLeft,
	                      Middle,
	                      MiddleRight,
	                      BottomLeft,
	                      BottomMiddle,
	                      BottomRight;

	public static void Load()
	{
		TopLeft      = (KeyCode) PlayerPrefs.GetInt("TopLeft", (int) KeyCode.Q);
		TopMiddle    = (KeyCode) PlayerPrefs.GetInt("TopMiddle", (int) KeyCode.W);
		TopRight     = (KeyCode) PlayerPrefs.GetInt("TopRight", (int) KeyCode.E);
		MiddleLeft   = (KeyCode) PlayerPrefs.GetInt("MiddleLeft", (int) KeyCode.A);
		Middle       = (KeyCode) PlayerPrefs.GetInt("Middle", (int) KeyCode.S);
		MiddleRight  = (KeyCode) PlayerPrefs.GetInt("MiddleRight", (int) KeyCode.D);
		BottomLeft   = (KeyCode) PlayerPrefs.GetInt("BottomLeft", (int) KeyCode.Z);
		BottomMiddle = (KeyCode) PlayerPrefs.GetInt("BottomMiddle", (int) KeyCode.X);
		BottomRight  = (KeyCode) PlayerPrefs.GetInt("BottomRight", (int) KeyCode.C);
	}

	public static void Save()
	{
		PlayerPrefs.SetInt("TopLeft", (int) TopLeft);
		PlayerPrefs.SetInt("TopMiddle", (int) TopMiddle);
		PlayerPrefs.SetInt("TopRight", (int) TopRight);
		PlayerPrefs.SetInt("MiddleLeft", (int) MiddleLeft);
		PlayerPrefs.SetInt("Middle", (int) Middle);
		PlayerPrefs.SetInt("MiddleRight", (int) MiddleRight);
		PlayerPrefs.SetInt("BottomLeft", (int) BottomLeft);
		PlayerPrefs.SetInt("BottomMiddle", (int) BottomMiddle);
		PlayerPrefs.SetInt("BottomRight", (int) BottomRight);

		PlayerPrefs.Save();
	}

	public static void ToDefault()
	{
		TopLeft      = KeyCode.Q;
		TopMiddle    = KeyCode.W;
		TopRight     = KeyCode.E;
		MiddleLeft   = KeyCode.A;
		Middle       = KeyCode.S;
		MiddleRight  = KeyCode.D;
		BottomLeft   = KeyCode.Z;
		BottomMiddle = KeyCode.X;
		BottomRight  = KeyCode.C;
	}
}