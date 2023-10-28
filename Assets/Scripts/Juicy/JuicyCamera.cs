using Sucker;
using UnityEngine;

namespace Juicy
{
    public class JuicyCamera : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int RespireRate = Animator.StringToHash("RespireRate");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetFloat(RespireRate, SuckerManager.Instance.suckTimer / SuckerManager.Instance.maxSuckTimeSet);
        }
    }
}