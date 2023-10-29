using Hmxs.Toolkit.Flow.Timer;
using UnityEngine;

namespace Particle
{
    public class BoomFade : MonoBehaviour
    {
        private void Start()
        {
            this.AttachTimer(2f, () => Destroy(gameObject));
        }
    }
}