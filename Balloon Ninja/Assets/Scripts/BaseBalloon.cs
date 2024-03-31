using UnityEngine;

public abstract class BaseBalloon : MonoBehaviour
{
    [SerializeField] float baseMoveSpeed = 1f;
    [SerializeField][Range(0f, 1f)] float speedVariationPercentage = 0.3f;
    [SerializeField] float sway;
    public abstract BalloonType Type { get; }

    float moveSpeed;

    SpriteRenderer spriteRenderer;


    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Spawned();
    }

    public virtual void Update()
    {
        Move();
    }


    public void Spawned()
    {
        float variation = 1f + Random.Range(-speedVariationPercentage, speedVariationPercentage);
        moveSpeed = baseMoveSpeed * variation;

        spriteRenderer.color = GetColor();
    }


    public virtual void Move()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    public virtual void Pop()
    {
        EventManager.BalloonPopped(gameObject, Type);
    }

    public abstract Color GetColor();
}

public enum BalloonType
{
    Normal,
    Bomb
}
