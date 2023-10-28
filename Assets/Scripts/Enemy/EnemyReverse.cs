using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemy
{
    public class EnemyReverse : Enemy
    {
        [Title("没被吸时")]
        public float normalAcceleration;
        public float normalMaxSpeed;

        [Title("被吸时")]
        public float suckedAcceleration;
        public float suckedMaxSpeed;

        [Title("被推")] 
        public float pushForce;

        protected override void BeingSuckedIn()
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

        protected override void NotBeingSuckedIn()
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

        protected override void Died()
        {
            Destroy(gameObject);
        }

        protected override void Lost()
        {
            Destroy(gameObject);
        }
    }
}