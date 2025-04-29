using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimalWait : MonoBehaviour
{
    [SerializeField] private List<GameObject> obj;
    public float wait = 0.0f;

    private void Start()
    {
        foreach (GameObject item in obj)
        {
            item.SetActive(false);
        }
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(wait);
        foreach (GameObject item in obj)
        {
            item.SetActive(true);
        }
    }

}

