using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public Button startGame; // Sửa "button" thành "Button"

    private void Start()
    {
        startGame.onClick.AddListener(OnStartGame); // Sửa StartGame thành OnStartGame để tránh xung đột tên
    }

    private void OnStartGame() // Đổi tên phương thức để không trùng tên với biến
    {
        GameManager.Instance.NewGame();
    }
}