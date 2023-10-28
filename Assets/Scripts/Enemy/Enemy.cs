using System;
using Hmxs.Toolkit.Module.Events;
using Sucker;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Enemy : MonoBehaviour
    {
        protected Rigidbody2D Rb;
        protected Transform Sucker;
        protected Vector2 Direction;
        
        private bool _isBeingSuckedIn;

        private void OnEnable()
        {
            Events.AddListener(EventGroups.Sucker.Push, BePushed);
        }

        private void OnDisable()
        {
            Events.RemoveListener(EventGroups.Sucker.Push, BePushed);
        }

        protected virtual void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            Sucker = GameObject.FindWithTag("Player").transform;
        }

        protected virtual void Update()
        {
            Direction = ((Vector2)(transform.position - Sucker.position)).normalized;
            _isBeingSuckedIn = SuckerManager.Instance.isSucking;
            if (_isBeingSuckedIn) 
                BeingSuckedIn();
            else
                NotBeingSuckedIn();
            Check();
        }

        private void Check()
        {
            if (Vector2.Distance(Sucker.position, transform.position) > SuckerManager.Instance.deathRadius)
            {
                Lost();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Died();
            }
        }

        // 玩家没吸时
        protected abstract void NotBeingSuckedIn();
        
        // 玩家正在吸时
        protected abstract void BeingSuckedIn();

        // 被推出
        protected abstract void BePushed();
        
        // 被吸入了
        protected abstract void Died();
        
        // 超出边界了
        protected abstract void Lost();
    }
}