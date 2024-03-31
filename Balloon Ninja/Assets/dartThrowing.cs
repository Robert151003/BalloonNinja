using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class dartThrowing : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private float throwForce = 0.01f;
    private bool isThrown = false;
    public Rigidbody2D rb;
    public GameObject manager;
    public bool stopped;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isThrown)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Record start position
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    // Record end position
                    endTouchPosition = touch.position;
                    ThrowDart();
                    break;
            }
        }
    }

    private void ThrowDart()
    {
        manager.GetComponent<Manager>().newDart = true;
        // Ensure the Rigidbody is not kinematic to allow it to be affected by physics
        rb.isKinematic = false;

        // Apply force to the new dart
        Vector2 swipeDirection = endTouchPosition - startTouchPosition;
        Vector2 force = new Vector2(swipeDirection.x, swipeDirection.y) * throwForce;
        rb.AddForce(force, ForceMode2D.Impulse);

        // Start scale down and rotation coroutines for the new dart
        StartCoroutine(ScaleDownDart());
        StartCoroutine(RotateDart());
        StartCoroutine(stopDart());

        // Mark the dart as thrown
        isThrown = true;
    }

    private IEnumerator ScaleDownDart()
    {
        float duration = 1.0f; // Duration in seconds over which the dart scales down
        float currentTime = 0f;

        Vector3 originalScale = transform.localScale; // Dart's original scale
        Vector3 targetScale = originalScale * 0.5f; // Target scale, adjust as needed

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }
    }

    private IEnumerator RotateDart()
    {
        float targetAngle = 45f; // Target tilt angle, adjust as needed
        float rotationSpeed = 90f; // Degrees per second, adjust as needed

        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(targetAngle, startRotation.eulerAngles.y, startRotation.eulerAngles.z);

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 1.5f; // Normalized time
            transform.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime);
            yield return null;
        }
    }

    public IEnumerator stopDart()
    {
        yield return new WaitForSeconds(1f);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
        StopCoroutine(RotateDart());
        stopped = true;
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Balloon") && stopped)
        {
            Destroy(other.gameObject);
            manager.GetComponent<Manager>().score++;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Balloon") && stopped)
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("bombBalloon") && stopped)
        {
            Destroy(other.gameObject);
            manager.GetComponent<Manager>().health -= 1;
        }
    }

}
