using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public PlayerMovement movement { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }

    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    public bool big => bigRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starpower { get; private set; }

    public int maxHealth = 100;  // Maximum health points
    private int currentHealth;  // Current health points

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        movement = GetComponent<PlayerMovement>();
        deathAnimation = GetComponent<DeathAnimation>();
        activeRenderer = smallRenderer;

        currentHealth = maxHealth;  // Initialize current health to max health
    }

    public void Hit()
{
    if (!dead && !starpower)
    {
        // Nếu nhân vật đang ở trạng thái lớn, chỉ cần nhỏ lại mà không trừ máu
        if (big)
        {
            Shrink();
        }
        else
        {
            // Nếu nhân vật không lớn, trừ 50 HP và giảm điểm
            currentHealth -= 50;
            GameManager.Instance.AddPoints(-100);

            // Cập nhật lại UI
            UIManager.Instance.UpdateHealthText(currentHealth);

            // Kiểm tra nếu HP bằng 0, gọi Death()
            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }
}


    public void Death()
{
    smallRenderer.enabled = false;
    bigRenderer.enabled = false;
    deathAnimation.enabled = true;

    StartCoroutine(DeathDelayCoroutine());
}


private IEnumerator DeathDelayCoroutine()
{

    yield return new WaitForSeconds(3f);

    GameManager.Instance.GameOver();
}



    public void Grow()
    {
        smallRenderer.enabled = false;
    bigRenderer.enabled = true;
    activeRenderer = bigRenderer;

    capsuleCollider.size = new Vector2(1f, 2f);
    capsuleCollider.offset = new Vector2(0f, 0.5f);

    // Cộng thêm 10 HP khi nhân vật lớn lên
    currentHealth += 10;
	UIManager.Instance.UpdateHealthText(currentHealth);
    // Đảm bảo HP không vượt quá giá trị tối đa, nếu bạn muốn giới hạn tối đa HP.
    currentHealth = Mathf.Min(currentHealth, maxHealth); // maxHealth là HP tối đa
	
    StartCoroutine(ScaleAnimation());
    }

    public void Shrink()
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void Starpower()
    {
        StartCoroutine(StarpowerAnimation());
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true;

        float elapsed = 0f;
        float duration = 10f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
    }
}
