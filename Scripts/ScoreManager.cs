
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour {
    public List<ScoreEntry> scoreList = new List<ScoreEntry>();

    public void AddScore(string playerID, int score) {
        scoreList.Add(new ScoreEntry(playerID, score));
        scoreList = scoreList.OrderByDescending(s => s.score).Take(10).ToList(); // Keep top 10
        UpdateLeaderboardUI();
    }

    private void UpdateLeaderboardUI() {
        // Code to update the UI with the top scores
    }
}
