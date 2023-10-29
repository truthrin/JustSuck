using System;
using MoreMountains.Feedbacks;
using Sucker;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Juicy
{
    public class JuicyPostProcess : MonoBehaviour
    {
        public MMF_Player shakeEffect;
        
        public float minVignetteIntensity;
        public float maxVignetteIntensity;

        public Color finalColor;
        
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
            if (SuckerManager.Instance.suckTimer < SuckerManager.Instance.maxSuckTimeSet * 0.2)
            {
                shakeEffect.PlayFeedbacks();
                _vignetteEffect.color.value = Color.Lerp(Color.black, finalColor,
                    (float)(1 - SuckerManager.Instance.suckTimer / (SuckerManager.Instance.maxSuckTimeSet * 0.2)));
            }
            else
                _vignetteEffect.color.value = Color.black;
        }
    }
}