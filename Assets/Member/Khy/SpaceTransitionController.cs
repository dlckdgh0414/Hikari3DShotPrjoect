using System.Collections;
using UnityEngine;

public class SpaceTransitionController : MonoBehaviour
{
    [SerializeField] private Material material; 
    [SerializeField] private float transitionDuration = 1.0f; 

    private Coroutine currentTransition;


    private const string ScalingProperty = "_Scaling";

    private void Start()
    {
        SpaceTransitionOut();
    }

    public void SpaceTransitionIn()
    {
        StartTransition(234.0f); 
    }

    public void SpaceTransitionOut()
    {
        StartTransition(0.0f); 
    }

    private void StartTransition(float target)
    {
        if (currentTransition != null)
            StopCoroutine(currentTransition);

        currentTransition = StartCoroutine(TransitionScaling(target));
    }

    private IEnumerator TransitionScaling(float targetValue)
    {
        float startValue = material.GetFloat(ScalingProperty);
        float timeElapsed = 0f;

        while (timeElapsed < transitionDuration)
        {
            float t = timeElapsed / transitionDuration;
            float currentValue = Mathf.Lerp(startValue, targetValue, t);
            material.SetFloat(ScalingProperty, currentValue);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        material.SetFloat(ScalingProperty, targetValue);
        currentTransition = null;
    }
}