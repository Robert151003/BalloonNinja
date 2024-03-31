using UnityEngine;

public class DartFollow : MonoBehaviour
{
    Camera cam;

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject dart;

    [SerializeField] LayerMask dartMask;

    Vector3 dartP;
    bool dartF;

    public float dartSpeed;

    Vector2 m;

    void Awake()
    {
        //cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam = Camera.main; // cache using Camera.main instead, since GameObject.Find is slow, wouldn't matter much in this game, but it doesn't hurt either
        sprite.enabled = false;
    }

    void Update()
    {
        // clamp mouse position in the screen bounds so ScreenToWorldPoint() doesn't return an error if mousePosition has not been set yet due to the game being out of focus
        Vector3 mousePos = Input.mousePosition;
        mousePos.Set(Mathf.Clamp(mousePos.x, 0f, Screen.width), Mathf.Clamp(mousePos.y, 0f, Screen.height), 0f);

        Vector3 position = cam.ScreenToWorldPoint(mousePos);
        dartP = new Vector3(position.x, position.y, position.z + 10);
        transform.position = dartP;

        if (dartF)
        {
           
        }


        if (Input.GetKey(KeyCode.Mouse0))
        {
            dartF = true;

            // not sure what this code is meant to do, but the sprite hasn't been set in the inspector so I'll just comment it out to prevent errors
            sprite.enabled = true;
            //Debug.Log("fzhqjfbzlibf");
        }
        else
        {
            dartF = false;
            sprite.enabled = false;
        }

       
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m = cam.ScreenToWorldPoint(Input.mousePosition);

            // old
             RaycastHit2D dartL = Physics2D.Raycast(m, Vector2.zero, gameObject.layer = 6);

            RaycastHit2D hit = Physics2D.Raycast(m, Vector2.zero, 1f, dartMask);

            // old
            // if (dartL.collider.gameObject != null)
            // 
            if (hit.collider) // you should use RaycastHit2D.collider for detecting if it hit something, not the RaycastHit2D.collider.gameObject, as that will throw an error since it's inside the collider, which itself will be null
            {
                GameObject Cube = Instantiate(dart, transform.position, Quaternion.EulerRotation(0, 0, 1.5f));
                Rigidbody rig = Cube.GetComponent<Rigidbody>();
                rig.AddForce(transform.forward * dartSpeed, ForceMode.VelocityChange);
            }
        }

        
    }
}
