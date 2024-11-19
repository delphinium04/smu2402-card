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

            BaseEnemy component = d.Type switch
            {
                Type.Boatswain => go.AddComponent<Boatswain>(),
                Type.Cleaner => go.AddComponent<Cleaner>(),
                Type.CombatSailor => go.AddComponent<CombatSailor>(),
                Type.PirateCaptain => go.AddComponent<PirateCaptain>(),
                Type.PirateLieutenant => go.AddComponent<PirateLieutenant>(),
                Type.PlunderingCrew => go.AddComponent<PlunderingCrew>(),
                Type.SeaCrow => go.AddComponent<SeaCrow>(),
                Type.WatchfulSallor => go.AddComponent<WatchfulSailor>(),
                _ => throw new ArgumentOutOfRangeException()
            };

            component.Init(d);
            return component;
        }

        void OnDestroy()
        {
            Instance = null;
            Destroy(this);
        }
    }
}