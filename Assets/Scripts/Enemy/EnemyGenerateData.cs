using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "Enemy/GenerateData", fileName = "EnemyGenerateData", order = 0)]
    public class EnemyGenerateData : ScriptableObject
    {
        [TableList(AlwaysExpanded = true, ShowIndexLabels = true)] public List<EnemyGenerate> enemyTimeline;
    }

    [Serializable]
    public class EnemyGenerate
    {
        public float time;

        [Range(1,9)]
        public float distance;

        public EnemyType type;
    }

    public enum EnemyType
    {
        Normal,
        Reverse,
        Attack
    }
}