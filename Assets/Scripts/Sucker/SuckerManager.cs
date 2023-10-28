using Hmxs.Toolkit.Base.Singleton;
using Hmxs.Toolkit.Flow.Timer;
using Hmxs.Toolkit.Module.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sucker
{
    public class SuckerManager : SingletonMono<SuckerManager>
    {
        public float deathRadius;
        public float maxSuckTimeSet;
        public float respirePunishmentTimeSet;

        public GameObject dieParticle;
        
        [ReadOnly] public float suckTimer;
        [ReadOnly] public bool isSucking;

        private SpriteRenderer _spriteRenderer;
        private bool _canPush;
        private bool _canRespire = true;
        
        private void Start()
        {
            suckTimer = maxSuckTimeSet;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!_canRespire) return;
            
            if (InputHandler.Instance.IsSucking)
            {
                // 按着空格时
                _canPush = true;

                if (suckTimer > 0)
                {
                    // 正常吸
                    isSucking = true;
                    suckTimer -= Time.deltaTime;
                }
                else
                {
                    // 吸过头了
                    _canPush = false;
                    Events.Trigger(EventGroups.Sucker.Push);
                    isSucking = false;
                    RespirePunishment();
                }
            }
            else
            {
                // 没按着空格时
                isSucking = false;
                if (suckTimer < maxSuckTimeSet)
                {
                    // 回复吸气条
                    suckTimer += Time.deltaTime;
                }

                if (_canPush)
                {
                    _canPush = false;
                    Events.Trigger(EventGroups.Sucker.Push);
                }
            }
        }

        private void RespirePunishment()
        {
            _canRespire = false;
            this.AttachTimer(respirePunishmentTimeSet, () => _canRespire = true);
        }

        private void Die()
        {
            _spriteRenderer.enabled = false;
            var obj = Instantiate(dieParticle, transform.position, Quaternion.identity);
            Timer.Register(2f, () => Destroy(obj));
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Bullet") && !isSucking) 
                Die();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, deathRadius);
        }
    }
}