using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField] float baseMoveSpeed = 1f; // Adjust the speed as needed
    [SerializeField] float moveSpeedVariation = 0.3f; // percentage-based variation, actual moveSpeed is 'baseMoveSpeed ± moveSpeedVariation%'
    [SerializeField] bool useRandomColor;

    float moveSpeed;

    void Awake()
    {
        //moveSpeed = Random.Range(0.8f, 1.8f);
        float variation = 1f + Random.Range(-moveSpeedVariation, moveSpeedVariation);
        moveSpeed = baseMoveSpeed * variation;

        if (useRandomColor)
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(r, g, b);
            
            //GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            GetComponent<SpriteRenderer>().color = new Color(r, g, b);
        }
    }

    void Update()
    {
        // Move the object upward in the Y-axis
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        if(transform.position.y >= 25)
        {
            Destroy(gameObject);
        }
    }
}
