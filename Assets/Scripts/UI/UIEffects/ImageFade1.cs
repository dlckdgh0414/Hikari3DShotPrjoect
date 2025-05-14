using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class ImageFade1 : MonoBehaviour
{

    TextMeshProUGUI[] img;
  public float colorDuraction = 1f;
    Color[] imgColor;
    float timer = 0f;
    void Start()
    {
        imgColor = new Color[4];

        for (int i = 0; i < 4; i++)
        {
          img = GetComponentsInChildren<TextMeshProUGUI>();
          
            imgColor[i] = img[i].color;
        }
        StartCoroutine(FadeInCorutine());
    }

    IEnumerator FadeInCorutine()
    {
        yield return new WaitForSeconds(0.5f);

        float timer1 = 0f;
        while (timer1 <= colorDuraction)
        {
            timer1 += Time.deltaTime;
            float t = timer1 / colorDuraction;
            foreach (var item in img)
            {
                item.color = Color.Lerp(Color.black, Color.white, t);
            }
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);


        float timer2 = 0f;
        while (timer2 <= colorDuraction)
        {
            timer2 += Time.deltaTime;
            float t = timer2 / colorDuraction;
            foreach (var item in img)
            {
                item.color = Color.Lerp(Color.white, Color.black, t);
            }
            yield return null;
        }
    }
}
