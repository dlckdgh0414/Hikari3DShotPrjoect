using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class ImageFade : MonoBehaviour
{
    RawImage img;
  public float colorDuraction = 1f;
    Color imgColor;
    float timer = 0f;
    void Start()
    {
        img = GetComponent<RawImage>();
        imgColor = img.color;
        Debug.Log(img);
        Debug.Log(imgColor);
        StartCoroutine(FadeInCorutine());
    }

    IEnumerator FadeInCorutine()
    {
        yield return new WaitForSeconds(2.5f);
        while (timer <= colorDuraction)
        {
         timer += Time.deltaTime;
         float t = timer / colorDuraction;
         img.color = Color.Lerp(imgColor, Color.black, t);
            yield return null;
        }
        imgColor = Color.black;
    }
}
