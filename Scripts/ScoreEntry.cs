using UnityEngine;

public class ScoreEntry {
    public string playerID;
    public int score;

    public ScoreEntry(string id, int score) {
        this.playerID = id;
        this.score = score;
    }
}

