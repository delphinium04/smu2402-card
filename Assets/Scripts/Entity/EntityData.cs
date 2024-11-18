using UnityEngine;

namespace Entity
{
    /*
     * 플레이어나 적들이 공유하는 기본적 스탯 ScriptableObject입니다
     * 이름, HP, ATK, 이미지, 설명 등이 있습니다
     */
    [CreateAssetMenu(fileName = "New Entity", menuName = "Entity/Data", order = 1)]
    public class EntityData : ScriptableObject
    {
        [SerializeField] string _name;
        public string Name => _name;

        [SerializeField] int _hp;
        public int Hp => _hp;

        [SerializeField] int _atk;
        public int Atk => _atk;

        [SerializeField] Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField] [TextArea] string _description;
        public string Description => _description;

        [SerializeField] [Header("Enemy Only")]
        int _weight;
        public int Weight => _weight;
    }
}