using System;
using Enemy;
using Hmxs.Toolkit.Module.Events;
using UnityEngine;

namespace Sucker
{
    public class ProgressingCircle : MonoBehaviour
    {
        private float _scalePercentage;
        private int _enemyDiedAmount;

        private void OnEnable()
        {
            Events.AddListener<FailedType>(EventGroups.General.GameOver, ResetEnemyCounter);
        }

        private void OnDisable()
        {
            Events.RemoveListener<FailedType>(EventGroups.General.GameOver, ResetEnemyCounter);
        }

        private void ResetEnemyCounter(FailedType type)
        {
            Debug.Log("Reset Amount");
            EnemyGenerator.Instance.enemyDiedAmount = _enemyDiedAmount;
            UpdateProgressingCircle();
        }

        public void UpdateProgressingCircle()
        {
            _enemyDiedAmount = EnemyGenerator.Instance.enemyDiedAmount;
            _scalePercentage = (float)  _enemyDiedAmount/
                               (float) EnemyGenerator.Instance.allEnemyAmount;
            transform.localScale = new Vector3(_scalePercentage, _scalePercentage, _scalePercentage);
        }
    }
}