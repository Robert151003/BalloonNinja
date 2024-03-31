using HietakissaUtils;
using UnityEngine;

public class TapInputTest : MonoBehaviour
{
    [SerializeField] GameObject tapVisual;

    [SerializeField] Camera cam;

    Vector3 vel;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 worldTargetPos = cam.ScreenToWorldPoint(Input.mousePosition).SetZ(0f);
            tapVisual.transform.position = Vector3.SmoothDamp(tapVisual.transform.position, worldTargetPos, ref vel, 0.5f);
        }

        

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //
        //    tapVisual.transform.position = cam.ScreenToWorldPoint(touch.position).SetZ(0f);
        //}
    }
}
