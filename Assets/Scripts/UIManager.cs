using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton Instance

    public TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;  // Thay livesText thành healthText

    private void Awake()
    {
        // Đảm bảo chỉ có một Instance tồn tại
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Đảm bảo UIManager không bị hủy khi chuyển cảnh
        }
        else
        {
            Destroy(gameObject); // Hủy nếu đã có Instance khác
        }
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned in the UIManager.");
        }
    }

    public void UpdateHealthText(int healthPoints)  // Cập nhật HP thay vì lives
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + healthPoints.ToString(); // Hiển thị HP thay vì mạng
        }
        else
        {
            Debug.LogWarning("Health Text is not assigned in the UIManager.");
        }
    }
}
