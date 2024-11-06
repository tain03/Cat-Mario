using UnityEngine;

public class SessionManager : MonoBehaviour {
    public PlayerData playerData; // Assign in the inspector
    private int sessionScore;

    public void StartSession() {
        sessionScore = 0;
        playerData.lives = 3; // Reset lives
    }

    public void UpdateScore(int points) {
        sessionScore += points;
        // Update UI (call a method to update score display if you have one)
    }

    public void UpdateLives(int change) {
        playerData.lives += change;
        if (playerData.lives <= 0) {
            EndSession();
        }
    }

public ScoreManager scoreManager; // Assign in the inspector

    public void EndSession() {
        playerData.highScore = Mathf.Max(playerData.highScore, sessionScore);
        // Save score to ranking module (coming up next)
        scoreManager.AddScore(playerData.playerID, sessionScore);
    }
}
