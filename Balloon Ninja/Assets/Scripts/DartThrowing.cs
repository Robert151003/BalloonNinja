using HietakissaUtils.QOL;
using HietakissaUtils;
using UnityEngine;

public class DartThrowing : MonoBehaviour
{
    [SerializeField] Dart dartPrefab;
    [SerializeField] LayerMask dartArea;

    [SerializeField] float throwForce = 7f;
    [SerializeField] float minThrowThreshold = 3f;

    Vector3 dragStartPos;
    bool dragging;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = CalculateWorldMousePos();

            if (QOL.Raycast2D(worldPos, Vector2.zero, out RaycastHit2D hit, 1f, dartArea))
            {
                dragging = true;
                dragStartPos = worldPos;
            }
        }
        else if (Input.GetMouseButtonUp(0) && dragging)
        {
            Vector3 worldPos = CalculateWorldMousePos();
            dragging = false;


            //Vector2 direction = -Maf.Direction(dragStartPos, worldPos);
            Vector2 direction = -(worldPos - dragStartPos);
            float force = Vector3.Distance(dragStartPos, worldPos) * throwForce;

            Debug.Log($"force: {force}");
            if (force <= minThrowThreshold) return;


            Dart dart = Instantiate(dartPrefab, dragStartPos, Quaternion.identity);
            dart.Throw(direction, force);
            Destroy(dart.gameObject, 5f);
        }
    }

    Vector3 CalculateWorldMousePos()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.Set(Mathf.Clamp(screenPos.x, 0f, Screen.width), Mathf.Clamp(screenPos.y, 0f, Screen.height), 0f);

        return GameManager.Instance.cam.ScreenToWorldPoint(screenPos).SetZ(0f);
    }
}
