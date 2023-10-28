using System;
using Sucker;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Juicy
{
    public class JuicyPostProcess : MonoBehaviour
    {
        public float minVignetteIntensity;
        public float maxVignetteIntensity;
        
        private Volume _volume;
        private Vignette _vignetteEffect;

        private void Start()
        {
            _volume = GetComponent<Volume>();
            _volume.profile.TryGet(out _vignetteEffect);
        }

        private void Update()
        {
            _vignetteEffect.intensity.value = 
                (maxVignetteIntensity - minVignetteIntensity) *
                (1 - SuckerManager.Instance.suckTimer / SuckerManager.Instance.maxSuckTimeSet) +
                minVignetteIntensity;
        }
    }
}