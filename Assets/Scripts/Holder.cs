using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public int GRABI;
    public float grabPower = 10.0f;
    public float throwPower = 10f;   //скорость толчка
    public float RayDistance = 30.0f;   //дистанция

    private bool Grab = false;   //ф-ция притяжения
    private bool Throw = false;   //ф-ция толчка
    public Transform offset;
    public Camera camera;
    RaycastHit hit;   //луч


    private void Start()
    {
        GRABI = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Physics.Raycast(camera.transform.position,camera.transform.forward, out hit, RayDistance);
            if (hit.rigidbody)
            {
                GRABI = GRABI + 1;
                switch (GRABI)
                {
                    case 1:
                        Grab = true;
                        hit.transform.rotation = offset.rotation;
                        break;
                    case 2:
                        Grab = false;
                        break;
                    default:
                        break;
                }
                if (GRABI == 3)
                {
                    GRABI = 0;
                }
                if (Grab == false)
                {
                    GRABI = 0;
                }
            }
            //  Debug.Log(GRABI);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Grab)
            {
                GRABI = 0;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {//если нажата лев кн мыши
            if (Grab)
            {
                Grab = false;
                Throw = true;
            }
        }

        if (Grab)
        {//ф-ция притяжения
            if (hit.rigidbody)
            {
                hit.rigidbody.velocity = (offset.position - (hit.transform.position + hit.rigidbody.centerOfMass)) * grabPower;
                hit.transform.Rotate(0, Input.GetAxis("Mouse ScrollWheel") * 100, 0);
            }
        }

        if (Throw)
        {//ф-ция толчка
            if (hit.rigidbody)
            {
                hit.rigidbody.velocity = camera.transform.forward * throwPower;
                Throw = false;
            }
        }
    }

    private void Grabb()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, RayDistance);
        if (hit.rigidbody)
        {
            Grab = true;
        }
    }
}