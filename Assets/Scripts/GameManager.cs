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
    public int healthPoints { get; private set; } = 100;  // Sử dụng HP thay cho lives
    public int coins { get; private set; } = 0;
    public int score { get; private set; } = 0;

    [SerializeField] private TextMeshProUGUI healthText;  // Text hiển thị HP
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
        UpdateHealthText();  // Cập nhật HP khi bắt đầu game
    }

    public void NewGame()
    {
        healthPoints = 100; // Đặt lại HP ban đầu
        coins = 0;
        score = 0;
        UIManager.Instance.UpdateScore(score);
        UIManager.Instance.UpdateHealthText(healthPoints);  // Cập nhật UI với HP
        LoadLevel(1, 1); // Tải lại level
    }

    public void GameOver()
    {
        NewGame();  // Reset lại game khi hết HP
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
        Debug.Log("Before reset, HP: " + healthPoints);
        
        healthPoints -= 10; // Giảm HP sau mỗi lần reset
        UIManager.Instance.UpdateHealthText(healthPoints); // Cập nhật lại UI HP sau khi giảm
        UIManager.Instance.UpdateScore(score);

        Debug.Log("After reset, HP: " + healthPoints);

        if (healthPoints > 0)
        {
            Debug.Log("Respawning player at current level...");
            LoadLevel(world, stage); // Hồi sinh ở level hiện tại
        }
        else
        {
            Debug.Log("Game Over! No HP left.");
            GameOver(); // Nếu hết HP, kết thúc trò chơi
        }

        resetting = false; // Reset hoàn tất
    }

    public void AddLife()  // Hàm này có thể giữ lại nếu bạn vẫn muốn thêm mạng vào game
    {
        healthPoints += 10;  // Mỗi mạng cộng thêm 10 HP
        Debug.Log("Life added, new HP: " + healthPoints);
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + healthPoints;
            Debug.Log("Health UI updated: " + healthPoints); // In ra số HP hiện tại trong UI
        }
    }

    public void AddCoin()
    {
        coins++;
        AddPoints(100);

        if (coins == 100)
        {
            coins = 0;
            AddLife();  // Cộng thêm mạng nếu đủ 100 coins
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
