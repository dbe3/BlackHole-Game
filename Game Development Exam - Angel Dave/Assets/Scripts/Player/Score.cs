using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int Points;

    public void AddScore(int point)
    {
        Points = Points + point;
    }
}
