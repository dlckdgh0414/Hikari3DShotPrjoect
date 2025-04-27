using DG.Tweening;
using UnityEngine;

public class TEST : MonoBehaviour
{
    RectTransform rect;
  
    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(0, transform.position.y * -4, 0);
    }

    private void Update()
    {
        rect.DOSizeDelta(new Vector2(0, 0), 2f, false);
    }

}
