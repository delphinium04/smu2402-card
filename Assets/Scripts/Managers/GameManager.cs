using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BattleData
{
    
}

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Managers.Battle.StartBattle();
    }
}
