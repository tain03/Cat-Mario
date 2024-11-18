using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public Button startGame;
    public Button howToPlayButton;
    public GameObject howToPlayPanel; // Tham chiếu đến Panel hiển thị cách chơi
    public Button closeButton;

    private void Start()
    {
        // Gán sự kiện cho các nút
        startGame.onClick.AddListener(OnStartGame);
        howToPlayButton.onClick.AddListener(OpenHowToPlayPanel);
        closeButton.onClick.AddListener(CloseHowToPlayPanel);

        // Đảm bảo Panel cách chơi tắt mặc định
        howToPlayPanel.SetActive(false);
    }

    private void OnStartGame()
    {
        GameManager.Instance.LoadLevel(1, 1);
    }

    private void OpenHowToPlayPanel()
    {
        howToPlayPanel.SetActive(true); // Hiển thị Panel
    }

    private void CloseHowToPlayPanel()
    {
        howToPlayPanel.SetActive(false); // Ẩn Panel
    }
}