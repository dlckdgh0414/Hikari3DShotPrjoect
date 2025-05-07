using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public float time;
    public float MoveSpeed = 10;
    public bool AbleHit;
    public float HitDelay;
    GameObject m_makedObject;
    public float MaxLength;
    public float DestroyTime2;
    float m_scalefactor;

    private void OnEnable()
    {
        m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor;//transform.parent.localScale.x;
    }
}
