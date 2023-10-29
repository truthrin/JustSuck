using System;
using Hmxs.Toolkit.Flow.Timer;
using Hmxs.Toolkit.Module.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemy
{
    public class EnemySphereAttack : Enemy
    {
        [Title("没被吸时")]
        public float normalAcceleration;
        public float normalMaxSpeed;

        [Title("被吸时")]
        public float suckedAcceleration;
        public float suckedMaxSpeed;

        [Title("被推")] 
        public float pushForce;

        [Title("光环")]
        public GameObject halo;
        public float attackLoop;

        private Timer _timer;
        
        protected override void Start()
        {
            base.Start();
            _timer = Timer.Register(attackLoop, Attack, isLooped: true);
        }

        private void OnDestroy()
        {
            Timer.Remove(_timer);
        }

        private void Attack()
        {
            Instantiate(halo, transform.position, Quaternion.identity, transform.parent);
        }

        protected override void NotBeingSuckedIn()
        {
            if (Rb.velocity.magnitude < normalMaxSpeed ||
                Rb.velocity.x * Direction.x < 0 || Rb.velocity.y * Direction.y < 0)
            {
                Rb.AddForce(Direction * normalAcceleration);
            }
            else
            {
                Rb.AddForce(-Direction * normalAcceleration);
            }
        }

        protected override void BeingSuckedIn()
        {
            if (Rb.velocity.magnitude < suckedMaxSpeed ||
                Rb.velocity.x * -Direction.x < 0 || Rb.velocity.y * -Direction.y < 0)
            {
                Rb.AddForce(-Direction * suckedAcceleration);
            }
            else
            {
                Rb.AddForce(Direction * normalAcceleration);
            }
        }

        protected override void BePushed()
        {
            Rb.AddForce(Direction * pushForce, ForceMode2D.Impulse);
        }
    }
}