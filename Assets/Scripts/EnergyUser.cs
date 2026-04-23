using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyUser : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private int energy;    
    [SerializeField] private List<Light> lightToKeepOn = new List<Light>();    
    [SerializeField] private List<Light> allLight = new List<Light>();
    [SerializeField] private GameObject EmissionReference;
    [SerializeField] private List<GameObject> AllEmiss = new List<GameObject>();
    [SerializeField] private Material EmissionFirst, PureBlack;
    [SerializeField] private Material[] camerasTexture;
    [SerializeField] private GameObject[] camerasObject;
    private bool lightsOff;
    // Start is called before the first frame update
    void Start()
    {
        allLight = new List<Light>(FindObjectsOfType<Light>());
        AllEmiss = new List<GameObject>(FindObjectsOfType<GameObject>());
        List<GameObject> temp = new List<GameObject>();
        foreach(GameObject obj in AllEmiss)
        {
            Renderer r = obj.GetComponent<Renderer>();
            if(r != null && (r.material == EmissionFirst || obj.name == EmissionReference.name))
            {
                temp.Add(obj);
            }
        }
        AllEmiss = temp;
        lightsOff = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (energy == 0 && lightsOff)
        {
            StopAllCoroutines();
            lightsOff = false;
            foreach (Light light in allLight)
            {
                if(!(lightToKeepOn.Contains(light))){
                    light.enabled = false;
                }
            }
            foreach (GameObject t in camerasObject)
            {
                Renderer b = t.GetComponent<Renderer>();
                b.material = PureBlack;
            }
            foreach (GameObject obj in AllEmiss)
            {
                Renderer r = obj.GetComponent<Renderer>();
                r.material = PureBlack;
            }
        }
        else if(energy > 0 && !lightsOff)
        {
            lightsOff = true;
            StartCoroutine(counter());
            foreach (Light light in allLight)
            {
                if (!(lightToKeepOn.Contains(light)))
                {
                    light.enabled = true;
                }
            }
            int i = 0;
            foreach (GameObject t in camerasObject)
            {
                Renderer b = t.GetComponent<Renderer>();
                b.material = camerasTexture[i];
                i += 1;
            }
            foreach (GameObject obj in AllEmiss)
            {
                Renderer r = obj.GetComponent<Renderer>();
                r.material = EmissionFirst;
            }

        }
        text.text = energy + " êÄæ";
    }
    private IEnumerator counter()
    {
        while (energy > 0)
        {
            energy -= 1;
            yield return new WaitForSeconds(1f);
        }
        StopAllCoroutines();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "User")
        {
            EnergyBank bank = other.gameObject.GetComponent<EnergyBank>();
            //StartCoroutine(Destroyer(other.gameObject));
            Destroy(other.gameObject);
            energy += bank.energy;
        }
    }
    private IEnumerator Destroyer(GameObject obj)
    {
        Destroy(obj, 0.4f);
        while(obj != null)
        {
            obj.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
