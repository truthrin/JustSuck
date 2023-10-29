using System.Collections.Generic;
using Hmxs.Toolkit.Base.Singleton;
using Hmxs.Toolkit.Flow.FungusTools;
using Hmxs.Toolkit.Flow.Timer;
using Hmxs.Toolkit.Module.Events;
using Sirenix.OdinInspector;
using Sucker;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyGenerator : SingletonMono<EnemyGenerator>
    {
        [InfoBox("Enemy Time Line Data")] public List<EnemyGenerateData> enemyWeaveData = new();
        [InfoBox("Enemy Type")] public SerializedDictionary<EnemyType, GameObject> enemies = new();
        public Transform enemyRoot;

        [ReadOnly] public List<Timer> timers;
        [ReadOnly] public int weaveIndex;
        private bool _weaveFinished;
        [ShowInInspector] private bool _allEnemyDied;

        private void OnEnable()
        {
            Events.AddListener(EventGroups.Enemy.CheckNextWeave, Check);
        }

        private void OnDisable()
        {
            Events.RemoveListener(EventGroups.Enemy.CheckNextWeave, Check);
        }

        public void Generate(int weaveDataIndex)
        {
            Debug.Log("Weave" + weaveDataIndex);
            if (weaveDataIndex == 5)
            {
                FlowchartManager.ExecuteBlock("Finish");
                SuckerManager.Instance.Finish();
                return;
            }
            var data = enemyWeaveData[weaveDataIndex];
            for (int i = 0; i < data.enemyTimeline.Count; i++)
            {
                var enemy = data.enemyTimeline[i];
                var index = i;
                var timer = Timer.Register(enemy.time, () =>
                {
                    var randAng = Random.Range(0f, Mathf.PI);
                    var pos = new Vector2(Mathf.Cos(randAng), Mathf.Sin(randAng)) * enemy.distance;
                    Instantiate(enemies[enemy.type], pos, Quaternion.identity, enemyRoot);
                    if (index == data.enemyTimeline.Count - 1)
                    {
                        _weaveFinished = true;
                    }
                });
                timers.Add(timer);
            }
        }

        public void ClearTimers()
        {
            foreach (var timer in timers)
            {
                timer.Remove();
            }

            timers.Clear();
        }

        private void Start()
        {
            Generate(weaveIndex);
        }

        private void Check()
        {
            _allEnemyDied = enemyRoot.GetComponentsInChildren<Enemy>().Length <= 1;
            
            if (_weaveFinished && _allEnemyDied && !SuckerManager.Instance.hasDied)
            {
                Timer.Register(3f, NextWeave);
            }
        }
        public void NextWeave()
        {
            Debug.Log("NextWeave");
            _allEnemyDied = false;
            _weaveFinished = false;
            weaveIndex++;
            Generate(weaveIndex);
        }
    }
}