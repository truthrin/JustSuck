using System;
using Hmxs.Toolkit.Base.Singleton;
using Hmxs.Toolkit.Module.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sucker
{
    public class SuckerManager : SingletonMono<SuckerManager>
    {
        public float deathRadius;
        
        public float maxSuckTimeSet;
        
        [ReadOnly] public float suckTimer;
        [ReadOnly] public bool isSucking;

        private bool _canPush;
        
        private void Start()
        {
            suckTimer = maxSuckTimeSet;
        }

        private void Update()
        {
            if (InputHandler.Instance.IsSucking)
            {
                if (suckTimer > 0)
                {
                    isSucking = true;
                    suckTimer -= Time.deltaTime;
                }
                else
                {
                    isSucking = false;
                }

                _canPush = true;
            }
            else
            {
                isSucking = false;
                if (suckTimer < maxSuckTimeSet)
                {
                    suckTimer += Time.deltaTime;
                }
                
                if (_canPush)
                {
                    _canPush = false;
                    Events.Trigger(EventGroups.Sucker.Push);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, deathRadius);
        }
    }
}