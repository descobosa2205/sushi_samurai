using UnityEngine;

public partial class GameManager
{
    public void AddScore(int delta)
    {
        pointsScored += delta;
        if (pointsScored < 0)
            pointsScored = 0;

        UpdateScoreText();
    }

    public int GetScore()
    {
        return pointsScored;
    }
}
