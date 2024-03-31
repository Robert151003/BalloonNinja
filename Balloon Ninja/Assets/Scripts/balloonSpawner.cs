using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject balloon;
    [SerializeField] GameObject bombBalloon;

    [SerializeField] float spawnTimer;
    [SerializeField] float bombTimer;

    [Header("Do Not Edit - Set via Code")]
    [SerializeField] float endX1;
    [SerializeField] float endX2;

    public float startTimer;
    public Animator instructions;

    void Awake()
    {
        spawnTimer = 0.8f;

        ///Set the spawn object the same width as the screen///
      
        // Set the object's position to the center of the screen
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, transform.position.z);
        // Get the screen width in world units
        float screenWidth = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        // Adjust the object's scale based on the screen width
        transform.localScale = new Vector3(screenWidth, transform.localScale.y, transform.localScale.z);


        ///Get the possible spawn positions///
       
        // Get the SpriteRenderer component (or other renderer component) of the object
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Calculate the end x-coordinate based on the object's position and size
            endX1 = transform.position.x + spriteRenderer.bounds.size.x / 2f;
            endX2 = transform.position.x - spriteRenderer.bounds.size.x / 2f;
        }
    }

    void Update()
    {
        startTimer -= Time.deltaTime;
        if (startTimer <= 0)
        {
            instructions.SetBool("Out", true);

            spawnTimer -= Time.deltaTime;
            bombTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                Instantiate(balloon, GetRandomSpawnPos(), Quaternion.identity);
                spawnTimer = 0.8f;
            }
            if (bombTimer <= 0)
            {
                Instantiate(bombBalloon, GetRandomSpawnPos(), Quaternion.identity);
                bombTimer = 2f;
            }
        }

        
    }

    Vector2 GetRandomSpawnPos() => new Vector2(Random.Range(endX1, endX2), transform.position.y);
}
