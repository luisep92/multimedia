using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour
{
    [SerializeField] SpriteRenderer backImage;
    private bool appear = true;
    // Start is called before the first frame update
    void Start()
    {
        backImage = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color currentC = backImage.color;
        if (currentC.a >= 0.5f)
            appear = false;
        if (appear)
            backImage.color = new Color(currentC.r, currentC.g, currentC.b, currentC.a + (Time.deltaTime / 0.4f));
        else
            backImage.color = new Color(backImage.color.r, backImage.color.g, backImage.color.b, backImage.color.a - Time.deltaTime /0.4f);

        if (backImage.color.a <= 0f)
            Destroy(this.gameObject);
    }
}
