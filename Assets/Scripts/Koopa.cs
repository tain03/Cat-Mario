using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;

    private bool shelled;
    private bool pushed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player") && collision.gameObject.TryGetComponent(out Player player))
        {
            if (player.starpower) {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) {
                EnterShell();
            } else {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shelled && other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (!pushed)
            {
                Vector2 direction = new(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                if (player.starpower) {
                    Hit();
                } else {
                    player.Hit();
                }
            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void EnterShell()
    {
        shelled = true;

        // Cộng điểm khi chuyển vào trạng thái vỏ (shell)
        GameManager.Instance.AddPoints(200);

        GetComponent<SpriteRenderer>().sprite = shellSprite;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
    }

    private void PushShell(Vector2 direction)
    {
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        // Cộng điểm khi Koopa bị tiêu diệt
        GameManager.Instance.AddPoints(300);

        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;

        // Hủy đối tượng sau 3 giây
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (pushed) {
            Destroy(gameObject);
        }
    }
}
