using Sucker;
using UnityEngine;

namespace Juicy
{
    public class JuicySucker : MonoBehaviour
    {
        public Color turnColor;
        
        private SpriteRenderer _sprite;

        private void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (SuckerManager.Instance.suckTimer < SuckerManager.Instance.maxSuckTimeSet * 0.2)
            {
                _sprite.color = Color.Lerp(Color.white, turnColor,
                    (float)(1 - SuckerManager.Instance.suckTimer / (SuckerManager.Instance.maxSuckTimeSet * 0.2)));
            }
            else
            {
                _sprite.color = Color.white;
            }
        }
    }
}