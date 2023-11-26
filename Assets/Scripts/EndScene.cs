using Enemy;
using Sucker;
using UnityEngine;

namespace DefaultNamespace
{
    public class EndScene : MonoBehaviour
    {
        private void Awake()
        {
            SuckerManager.Instance.enabled = false;
            EnemyGenerator.Instance.enabled = false;
        }
    }
}