using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private Animator door;
    public bool code = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && code)
        {
            door.SetBool("Opener", true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && code)
        {
            door.SetBool("Opener", false);
        }
    }
}
