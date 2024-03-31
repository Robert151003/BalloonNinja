using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonDie : MonoBehaviour
{
    bool b;

    private Vector2 m;

    bool ballon;

    public LayerMask balloonLayer;

    public Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetComponent<Manager>().mainGame)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log(gameObject);
                if (gameObject.tag == "bombBalloon")
                {
                    manager.bomb = true;
                }
                else
                {
                    manager.score += 1;
                }
                Destroy(gameObject);               
            }
        }
        else if (manager.GetComponent<Manager>().miniGame)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit2D ballon = Physics2D.Raycast(m, Vector2.zero, gameObject.layer = 3);

                if (ballon.collider.gameObject != null)
                {
                    Debug.Log(gameObject);
                    if (ballon.collider.tag == "bombBalloon")
                    {
                        manager.bomb = true;
                    }
                    else
                    {
                        manager.score += 1;
                    }
                    Destroy(ballon.collider.gameObject);
                }
            }
        }

        m = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
    

    

        
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "mouse")
        {
            b = true;
            
        }
        else
        {
            b= false;
        }
    }
}
