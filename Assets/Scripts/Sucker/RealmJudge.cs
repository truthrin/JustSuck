using UnityEngine;

namespace Sucker
{
    public class RealmJudge : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Emeny"))
            {
                other.gameObject.GetComponent<Enemy.Enemy>().Lost();
            }
        }
    }
}