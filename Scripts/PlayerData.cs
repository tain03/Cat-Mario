using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
public class PlayerData : ScriptableObject {
    public string playerID;
    public int highScore;
    public int lives;
}
