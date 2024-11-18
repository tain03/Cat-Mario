using System.Collections; // Thêm namespace này
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; } = 1;
    public int stage { get; private set; } = 1;
    public int lives { get; private set; } = 3;
    public int coins { get; private set; } = 0;
    public int score { get; private set; } = 0;

    [SerializeField] private TextMeshProUGUI livesText;
    public bool resetting { get; private set; } = false; // Trạng thái đang reset level


    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        UpdateLivesText();
    }

    public void NewGame()
    {
        lives = 3; // Đặt lại mạng sống
        coins = 0;
        score = 0;
        UIManager.Instance.UpdateScore(score);
        UIManager.Instance.UpdateLivesText(lives); // Cập nhật UI với số mạng khi bắt đầu game
        LoadLevel(1, 1); // Tải lại level
    }


    public void GameOver()
    {
        NewGame();
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        Debug.Log("Loading level: " + world + "-" + stage);
        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        if (resetting) return; // Nếu đang trong quá trình reset, bỏ qua
        Debug.Log("Starting level reset with delay: " + delay + " seconds.");
        StartCoroutine(ResetLevelCoroutine(delay));
    }

    private IEnumerator ResetLevelCoroutine(float delay)
    {
        resetting = true; // Đánh dấu đang reset level
        Debug.Log("Waiting for " + delay + " seconds before resetting...");
        yield return new WaitForSeconds(delay);
        Debug.Log("Before reset, lives: " + lives);
        lives--; // Giảm mạng
        UIManager.Instance.UpdateLivesText(lives); // Cập nhật lại UI mạng sau khi giảm
        UIManager.Instance.UpdateScore(score);
        
        Debug.Log("After reset, lives: " + lives);

        if (lives > 0)
        {
            Debug.Log("Respawning player at current level...");
            LoadLevel(world, stage); // Hồi sinh ở level hiện tại
        }
        else
        {
            Debug.Log("Game Over! No lives left.");
            GameOver(); // Nếu hết mạng, kết thúc trò chơi
        }

        resetting = false; // Reset hoàn tất
    }

    public void AddLife()
    {
        lives++;
        Debug.Log("Life added, new lives: " + lives);
        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "LIVE: X" + lives;
            Debug.Log("Lives UI updated: " + lives); // In ra số mạng hiện tại trong UI
        }
    }

    public void AddCoin()
    {
        coins++;
        AddPoints(100);

        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        UIManager.Instance.UpdateScore(score);
        Debug.Log("Score updated: " + score);
    }

    public void ResetScore()
    {
        score = 0;
        Debug.Log("Score reset to 0.");
    }
}
