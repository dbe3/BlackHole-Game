using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public int Points;
    public TMP_Text TextScore;

    public void AddScore(int point)
    {
        Points = Points + point;
    }

    public void Update()
    {
        TextScore.text = Points.ToString();
    }
}
