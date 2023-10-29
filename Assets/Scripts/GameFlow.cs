using System;
using Sucker;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameFlow : MonoBehaviour
    {
        private bool _canTrigger;

        private void OnEnable()
        {
            _canTrigger = true;
        }

        private void Update()
        {
            if (InputHandler.Instance.IsSucking && _canTrigger)
            {
                _canTrigger = false;
                SuckerManager.Instance.Respawn();
                gameObject.SetActive(false);
            }
        }
    }
}