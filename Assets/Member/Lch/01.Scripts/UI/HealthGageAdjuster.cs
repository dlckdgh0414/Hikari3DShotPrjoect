using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Member.Ysc._01_Code.Agent;

public class HealthGageAdjuster : MonoBehaviour
{
    [SerializeField] private EntityHealth entityhealth;
    [SerializeField] private Image gage;

    private void Awake()
    {
        if (gage == null)
        {
            gage = GetComponent<Image>();
        }
    }

    public void ApplyHealth(float health)
    {
        float endHealth = health / entityhealth.maxHealth;
        gage.DOFillAmount(endHealth, .5f);
    }
}