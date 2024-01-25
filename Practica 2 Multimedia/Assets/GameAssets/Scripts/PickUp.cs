using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] GameObject OnPickUpParticle;
    private Renderer ren;
    private Light lig;
    private bool hassBeenPicked = false;

    private void Awake()
    {
        ren = GetComponent<Renderer>();
        lig = GetComponent<Light>();
    }

    private void Start()
    {
        StartCoroutine(ChangeColorOnTime(5f));
    }

    public void OnPickUp()
    {
        if (hassBeenPicked)
            return;
        ChangeColor(Color.green);
        GetComponent<Rotator>().enabled = false;
        GetComponent<Collider>().enabled = false;
        particle.SetActive(false);
        hassBeenPicked = true;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Destroy(Instantiate(OnPickUpParticle, pos, Quaternion.identity), 4f);
    }

    private void ChangeRandomColor()
    {
        ren.material.EnableKeyword("_EMISSION");
        Color c = new Color(Random.value, Random.Range(0, 0.5f), Random.value);
        ren.material.SetColor("_EmissionColor", c * 3);
        ren.material.color = c;
        lig.color = c;
    }

    private void ChangeColor(Color color)
    {
        ren.material.EnableKeyword("_EMISSION");
        ren.material.SetColor("_EmissionColor", color * 3);
        ren.material.color = color;
        lig.color = color;
    }

    private IEnumerator ChangeColorOnTime(float t)
    {
        ChangeRandomColor();
        yield return new WaitForSeconds(t);
        if(!hassBeenPicked)
            StartCoroutine(ChangeColorOnTime(t));
    }
}
