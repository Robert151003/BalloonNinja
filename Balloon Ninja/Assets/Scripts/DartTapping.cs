using UnityEngine;

public class DartTapping : MonoBehaviour
{
    [SerializeField] float tapRadius = 0.15f;
    [SerializeField] LayerMask balloonLayer;

    Collider2D[] overlaps = new Collider2D[10];


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.Set(Mathf.Clamp(screenPos.x, 0f, Screen.width), Mathf.Clamp(screenPos.y, 0f, Screen.height), 0f);

            Vector3 worldPos = GameManager.Instance.cam.ScreenToWorldPoint(screenPos);

            int overlapCount = Physics2D.OverlapCircleNonAlloc(worldPos, tapRadius, overlaps, balloonLayer);
            Debug.Log($"tapped, {overlapCount} overlaps");

            for (int i = 0; i < overlapCount; i++)
            {
                if (overlaps[i].TryGetComponent(out Balloon balloon)) balloon.Pop();
            }
        }
    }
}