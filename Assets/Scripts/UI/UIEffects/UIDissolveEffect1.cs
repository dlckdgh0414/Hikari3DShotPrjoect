using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class UIDissolveEffect1 : MonoBehaviour
{
  [SerializeField]  VideoPlayer vid;
    private void Awake()
    {
        vid.Prepare();
    }

  public void ShowUIEffect()
    {
       vid.Play();
    }
}
