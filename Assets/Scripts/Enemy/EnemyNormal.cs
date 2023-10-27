using UnityEngine;

namespace Enemy
{
    public class EnemyNormal : Enemy
    {
        protected override void NotBeingSuckedIn()
        {
            Debug.Log("Not Being Sucked In");
        }

        protected override void BeingSuckedIn()
        {
            Debug.Log("Is Being Sucked In");
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