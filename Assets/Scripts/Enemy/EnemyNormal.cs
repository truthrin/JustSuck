using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemy
{
    public class EnemyNormal : Enemy
    {
        [Title("没被吸时")]
        public float normalAcceleration;
        public float normalMaxSpeed;

        [Title("被吸时")]
        public float suckedAcceleration;
        public float suckedMaxSpeed;

        protected override void NotBeingSuckedIn()
        {
            var direction = ((Vector2)(transform.position - Player.position)).normalized;
            if (Rb.velocity.magnitude < normalMaxSpeed ||
                Rb.velocity.x * direction.x < 0 || Rb.velocity.y * direction.y < 0)
            {
                Rb.AddForce(direction * normalAcceleration);
            }
        }

        protected override void BeingSuckedIn()
        {
            var direction = ((Vector2)(Player.position - transform.position)).normalized;
            if (Rb.velocity.magnitude < suckedMaxSpeed ||
                Rb.velocity.x * direction.x < 0 || Rb.velocity.y * direction.y < 0)
            {
                Rb.AddForce(direction * suckedAcceleration);
            }
        }

        protected override void Died()
        {
            throw new System.NotImplementedException();
        }

        protected override void Lost()
        {
            throw new System.NotImplementedException();
        }
    }
}