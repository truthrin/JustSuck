using UnityEngine;

namespace Enemy
{
    public class HaloController : MonoBehaviour
    {
        public void Died() => Destroy(gameObject);
    }
}