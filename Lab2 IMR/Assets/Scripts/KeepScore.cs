using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepScore : MonoBehaviour
{
    public static int Score = 0;
    public static int bestScore = 0;
    void OnGUI()
    {
        string score = "Score: " + Score + "\nBest Score: " + bestScore;
        GUI.Box(new Rect(100, 100, 100, 100), score);
    }
}
