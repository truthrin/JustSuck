using System;
using UnityEngine;
using UnityEngine.Accessibility;

namespace Enemy
{
    public class HaloController : MonoBehaviour
    {
        public float expandSpeed;
        public float maxScale;
        
        
        private void Start()
        {
            transform.localScale = new Vector3(0, 0, 0);
        }

        private void Update()
        {
            transform.localScale += Vector3.one * (expandSpeed * Time.deltaTime);

            if (transform.localScale.x > maxScale) Died();
        }

        private void Died() => Destroy(gameObject);
    }
}