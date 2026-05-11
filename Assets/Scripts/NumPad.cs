using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumPad : MonoBehaviour
{
    [SerializeField] private string Password;
    [SerializeField] private Animator door;
    public string Code;
    public GameObject Bulb;
    public Material redLight, greenLight, def;
    public Doors doors;
    public void OpenDoor()
    {
        Renderer lightBulb = Bulb.GetComponent<Renderer>();
        if (Password == Code)
        {
            door.SetBool("Opener", true);
            lightBulb.material = greenLight;
            StartCoroutine(RollBack());
           // Debug.Log("1");
        }
        else
        {
           // Debug.Log("0");
            doors.code = false;
            lightBulb.material = redLight;
            StartCoroutine(RollWrong(lightBulb));
        }
    }
    public IEnumerator RollBack()
    {
        Code = Password;
        yield return new WaitForSeconds(2.5f);
        Code = "";
        yield return new WaitForSeconds(4.5f);
        door.SetBool("Opener", false);
        Renderer lightBulb = Bulb.GetComponent<Renderer>();
        lightBulb.material = def;
    }
    public IEnumerator RollWrong(Renderer lightBulb)
    {
        Code = "";
        yield return new WaitForSeconds(2.5f);
        Code = "";
        lightBulb.material = def;
    }

}
