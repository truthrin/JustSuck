using Enemy;
using Hmxs.Toolkit.Base.Singleton;
using Hmxs.Toolkit.Flow.FungusTools;
using Hmxs.Toolkit.Flow.Timer;
using Hmxs.Toolkit.Module.Events;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sucker
{
    public class SuckerManager : SingletonMono<SuckerManager>
    {
        public MMF_Player getBallEffect;
        public MMF_Player peakEffect;
        public MMF_Player defendEffect;
        
        public float maxSuckTimeSet;
        public float respirePunishmentTimeSet;

        public GameObject dieParticle;
        public GameObject enemyRoot;
        
        [ReadOnly] public float suckTimer;
        [ReadOnly] public bool isSucking;
        [ReadOnly] public bool hasDied;
        
        private Animator _animator;
        private bool _canPush = false;
        private bool _canRespire = true;
        
                
        private void OnEnable()
        {
            Events.AddListener(EventGroups.General.GameOver, Die);
        }

        private void OnDisable()
        {
            Events.RemoveListener(EventGroups.General.GameOver, Die);
        }

        private void Start()
        {
            _canPush = false;
            suckTimer = maxSuckTimeSet;
            _animator = GetComponent<Animator>();
            getBallEffect.Initialization();
            peakEffect.Initialization();
            defendEffect.Initialization();
        }

        private void Update()
        {
            if (!_canRespire) return;
            if (InputHandler.Instance.IsSucking)
            {
                // 按着空格
                if (suckTimer > 0)
                {
                    // 正常吸
                    _canPush = true;
                    isSucking = true;
                    suckTimer -= Time.deltaTime;
                }
                else
                {
                    // 吸过头了
                    _canPush = false;
                    Events.Trigger(EventGroups.Sucker.Push);
                    peakEffect.PlayFeedbacks();
                    isSucking = false;
                    RespirePunishment();
                }
            }
            else
            {
                // 没按着空格时
                isSucking = false;
                if (suckTimer < maxSuckTimeSet)
                    // 回复吸气条
                    suckTimer += Time.deltaTime;

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
            _animator.Play("Die");
            Instantiate(dieParticle, transform.position, Quaternion.identity);
            Time.timeScale = 0.1f;
            FlowchartManager.ExecuteBlock("Die");
        }

        public void Finish()
        {
            _animator.Play("Die");
            foreach (Transform child in enemyRoot.transform) 
                Destroy(child.gameObject);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Bullet") && !isSucking && !hasDied)
            {
                hasDied = true;
                Events.Trigger(EventGroups.General.GameOver);
            }

            if (other.CompareTag("Bullet") && isSucking && !hasDied)
            {
                defendEffect.PlayFeedbacks();
            }
        }

        public void Respawn()
        {
            Time.timeScale = 1;
            foreach (Transform child in enemyRoot.transform) 
                Destroy(child.gameObject);
            FlowchartManager.ExecuteBlock("Respawn");
            hasDied = false;
            _canRespire = true;
            suckTimer = maxSuckTimeSet;
            _animator.Play("Respawn");
        }

        public void ReStart()
        {
            Debug.Log("Restart");
            EnemyGenerator.Instance.ClearTimers();
            EnemyGenerator.Instance.Generate(EnemyGenerator.Instance.weaveIndex);
        }
    }
}