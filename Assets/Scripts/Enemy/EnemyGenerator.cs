using System.Collections.Generic;
using Hmxs.Toolkit.Base.Singleton;
using Hmxs.Toolkit.Flow.FungusTools;
using Hmxs.Toolkit.Flow.Timer;
using Sirenix.OdinInspector;
using Sucker;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyGenerator : SingletonMono<EnemyGenerator>
    {
        [Title("Enemy Time Line Data")]
        public List<EnemyGenerateData> enemyWeaveData = new();
        [Title("Enemy Type")]
        public SerializedDictionary<EnemyType, GameObject> enemies = new();
        [Title("Reference")]
        public Transform enemyRoot;
        public GameObject celebrateHalo;
        public ProgressingCircle progressingCircle;

        [Title("Info")]
        [ReadOnly] public List<Timer> timers;
        [ReadOnly] public int weaveIndex;
        [ReadOnly] public bool weaveGenerateFinished;
        [ReadOnly] public bool allEnemyDied;
        [ReadOnly] public int enemyDiedAmount;
        [ReadOnly] public int allEnemyAmount;

        [Button]
        private void CountEnemyAmount()
        {
            allEnemyAmount = 0;
            foreach (var enemyGenerateData in enemyWeaveData)
                allEnemyAmount += enemyGenerateData.enemyTimeline.Count;
            Debug.Log("Enemy Weave Amount:  " + enemyWeaveData.Count);
            Debug.Log("Enemy Amount:        " + allEnemyAmount);
        }

        public void Generate(int weaveDataIndex)
        {
            if (weaveDataIndex == enemyWeaveData.Count)
            {
                Debug.Log("Finish");
                FlowchartManager.ExecuteBlock("Finish");
                SuckerManager.Instance.Finish();
                return;
            }
            Debug.Log("Weave" + weaveDataIndex);
            weaveGenerateFinished = false;
            allEnemyDied = false;
            ClearTimers();
            var data = enemyWeaveData[weaveDataIndex];
            for (int i = 0; i < data.enemyTimeline.Count; i++)
            {
                var enemy = data.enemyTimeline[i];
                var index = i;
                var timer = Timer.Register(enemy.time, () =>
                {
                    var randAng = Random.Range(0f,2 * Mathf.PI);
                    var pos = new Vector2(Mathf.Cos(randAng), Mathf.Sin(randAng)) * enemy.distance;
                    Instantiate(enemies[enemy.type], pos, Quaternion.identity, enemyRoot);
                    if (index == data.enemyTimeline.Count - 1)
                    {
                        weaveGenerateFinished = true;
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
            CountEnemyAmount();
            Generate(weaveIndex);
        }

        public void Check()
        {
            allEnemyDied = enemyRoot.GetComponentsInChildren<Enemy>().Length <= 1;
            
            if (weaveGenerateFinished && allEnemyDied && !SuckerManager.Instance.hasDied)
            {
                Instantiate(celebrateHalo);
                progressingCircle.UpdateProgressingCircle();
                Timer.Register(4f, NextWeave);
            }
        }

        private void NextWeave()
        {
            Debug.Log("NextWeave");
            weaveIndex++;
            Generate(weaveIndex);
        }
    }
}