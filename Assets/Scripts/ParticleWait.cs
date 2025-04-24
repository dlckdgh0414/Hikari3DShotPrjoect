using UnityEngine;
using System.Collections;
public class ParticleWait : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(ParticleEmit());
    }

   IEnumerator ParticleEmit()
    {
        yield return new WaitForSeconds(5f);

        gameObject.GetComponent<ParticleSystem>().Play();
    }
}
