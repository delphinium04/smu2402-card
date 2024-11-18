using System;
using Entity;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager: MonoBehaviour
    {
        public static EnemyManager Instance { get; private set; }

        void Awake()
        {
            if (Instance is not null) Destroy(this);

            Instance = this;
        }

        public static BaseEnemy GetEnemy(EntityData d)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/BaseEnemy"));
            go.GetComponent<BaseEnemy>().Init(d);
            return go.GetComponent<BaseEnemy>();
        }

        void OnDestroy()
        {
            Instance = null;
            Destroy(this);
        }
    }
}