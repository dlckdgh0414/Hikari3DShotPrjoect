using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Member.Ysc._01_Code.Feedbacks
{
    public class BlinkFeedback : Feedback
    {
        [SerializeField] private Transform matTarget;
        [SerializeField] private float blinkDuration;
        [SerializeField] private float blinkIntensity;

        private readonly int _blinkHash = Shader.PropertyToID("_BlinkValue");
        
        private List<Material> _materials;

        private void Awake()
        {
            _materials = new List<Material>();
        }

        public override void PlayFeedback()
        {
            MeshRenderer[] meshs = matTarget.GetComponentsInChildren<MeshRenderer>();
            meshs.ToList().ForEach(x => _materials.Add(x.material));
            
            foreach (Material mat in _materials)
            {
                mat.SetFloat(_blinkHash, blinkIntensity);
            }
            DOVirtual.DelayedCall(blinkDuration, StopFeedback);
        }

        public override void StopFeedback()
        {
            if (_materials.Count == 0) return;
            
            foreach (Material mat in _materials)
            {
                mat.SetFloat(_blinkHash, 0);
            }
        }
    }
}