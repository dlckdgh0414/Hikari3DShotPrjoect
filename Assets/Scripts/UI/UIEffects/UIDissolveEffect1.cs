using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class UIDissolveEffect1 : MonoBehaviour
{
  [SerializeField]  VideoPlayer vid;
    [SerializeField] float wait;
    private void Awake()
    {
        vid.Prepare();
    }

    private void OnEnable()
    {
        ShowUIEffect();
    }


    public void ShowUIEffect()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(wait);
        vid.Play();
    }
}
