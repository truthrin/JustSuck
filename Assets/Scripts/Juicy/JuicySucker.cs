using Sucker;
using UnityEngine;

namespace Juicy
{
    public class JuicySucker : MonoBehaviour
    {
        public Color turnColor;

        private void Update()
        {
            if (SuckerManager.Instance.suckTimer < SuckerManager.Instance.maxSuckTimeSet * 0.2)
            {
                SuckerManager.Instance.haloSpriteRenderer.color = Color.Lerp(Color.white, turnColor,
                    (float)(1 - SuckerManager.Instance.suckTimer / (SuckerManager.Instance.maxSuckTimeSet * 0.2)));
                SuckerManager.Instance.circleSpriteRenderer.color = Color.Lerp(Color.white, turnColor,
                    (float)(1 - SuckerManager.Instance.suckTimer / (SuckerManager.Instance.maxSuckTimeSet * 0.2)));
            }
            else
            {
                SuckerManager.Instance.haloSpriteRenderer.color = Color.white;
                SuckerManager.Instance.circleSpriteRenderer.color = Color.white;
            }
        }
    }
}