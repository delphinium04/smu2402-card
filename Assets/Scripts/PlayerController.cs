using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private int hp = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            OnDie();
        }
    }
    public void OnDie()
    {
        Debug.Log("캐릭터가 사망했습니다.");
    }
    public int GetHp()
    {
        return hp;
    }
}
