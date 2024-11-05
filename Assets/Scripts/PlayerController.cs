using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private int hp = 60;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 데미지를 입었을 때 체력 감소
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            OnDie();
        }
    }

    // 사망 했을 시
    public void OnDie()
    {
        Debug.Log("캐릭터가 사망했습니다.");
    }

    // hp 수치 반환
    public int GetHp()
    {
        return hp;
    }
    
}
