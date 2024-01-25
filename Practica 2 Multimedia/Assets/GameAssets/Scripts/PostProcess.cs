using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Luis Escolano Piquer

// Manages bloom intensity, unused
public class PostProcess : MonoBehaviour
{
    private bool blinkBloom = false;
    private Bloom bloom;

    private void Awake()
    {
        GetComponent<Volume>().profile.TryGet<Bloom>(out bloom);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (blinkBloom)
            BlinkBloom();
    }

    private void BlinkBloom(float speed = 1.5f)
    {
        bloom.intensity.value = Mathf.PingPong(Time.time * speed, 1) + 0.2f;
    }
    
    public IEnumerator BlinkBloomCoroutine(float time = 5f, float speed = 3f)
    {
        blinkBloom = true;
        yield return new WaitForSeconds(time);
        blinkBloom = false;
        bloom.intensity.value = 1;
    }

}
