using HietakissaUtils;
using UnityEngine;

public class Dart : MonoBehaviour
{
    [SerializeField] LayerMask balloonLayer;
    [SerializeField] Vector2 size;
    [SerializeField] float drag = 1f;

    Vector2 velocity;

    RaycastHit2D[] hits;


    void Update()
    {
        velocity += Vector2.down * 9.81f * Time.deltaTime;
        velocity = velocity * Mathf.Max(0.2f, (1 - Time.deltaTime * drag));

        Vector3 translation = velocity * Time.deltaTime;
        Vector3 direction = Maf.Direction(transform.position, transform.position + translation);

        hits = Physics2D.BoxCastAll(transform.position, size, transform.rotation.eulerAngles.z, direction, translation.magnitude, balloonLayer);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.TryGetComponent(out Balloon balloon)) balloon.Pop();
        }

        transform.right = direction;
        transform.Translate(translation, Space.World);
    }

    public void Throw(Vector2 direction, float force)
    {
        velocity = direction * force;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, size);
    }

    //void DrawWireRectangle(Vector2 pos, Vector2 size, Color color, float duration = 0f)
    //{
    //    Gizmos.DrawRay(pos + (Vector2.down + Vector2.left) * size * 0.5f, Vector2.up * size.y, color, duration);
    //    Gizmos.DrawRay(pos + (Vector2.down + Vector2.left) * size * 0.5f, Vector2.right * size.x, color, duration);
    //    Gizmos.DrawRay(pos + (Vector2.up + Vector2.right) * size * 0.5f, Vector2.down * size.y, color, duration);
    //    Gizmos.DrawRay(pos + (Vector2.up + Vector2.right) * size * 0.5f, Vector2.left * size.x, color, duration);
    //}
}
