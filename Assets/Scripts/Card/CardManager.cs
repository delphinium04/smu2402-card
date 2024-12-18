using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Card
{
    public class CardManager : MonoBehaviour
    {
        private GameObject _cardPrefab;
        private readonly CardDeck _cardDeck = new CardDeck();
        public static CardManager Instance { get; private set; }

        public List<BaseCard> TestCardList = new List<BaseCard>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 기존 초기화 작업 (_cardPrefab 로드)
            if (_cardPrefab == null)
            {
                _cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
            }
        }

        void Start()
        {
            _cardDeck.Reset();
        }

        /// <summary>
        /// 가능한 amount만큼 카드 오브젝트를 만들어 반환합니다.
        /// </summary>
        public CardBehaviour[] DrawCard(int amount)
        {
            if (_cardDeck.GetRemainCardCount() < amount)
                amount = _cardDeck.GetRemainCardCount();

            if (amount == 0)
            {
                Debug.LogWarning("No Card to draw");
                return null;
            }

            CardBehaviour[] cards = new CardBehaviour[amount];

            for (int i = 0; i < amount; i++)
            {
                var cardData = _cardDeck.GetRandomCard();
                cards[i] = CreateCardInstance(cardData);
                cards[i]?.Init(cardData);
            }

            return cards;
        }


        /// <summary>
        /// 덱 내의 모든 카드의 "데이터"를 반환합니다.
        /// </summary>
        public IReadOnlyList<BaseCard> GetAllCardDataInDeck()
            => _cardDeck.GetAllCard();

        /// <summary>
        /// 카드 데이터를 통해 만든 오브젝트(CardBehaviour Component)를 반환합니다.
        /// <param name="cardData">카드 데이터 (CardGeneralGun ...)</param>
        /// </summary>
        public CardBehaviour GetCardBehaviour(BaseCard cardData)
            => CreateCardInstance(cardData);

        public void AddCardToDeck(params CardBehaviour[] cards)
            => AddCardToDeck((from card in cards select card.Data).ToArray());

        void AddCardToDeck(params BaseCard[] cards)
            => _cardDeck.AddCard(cards);

        /// <summary>
        /// General 카드가 강화 가능한 경우 true를 반환합니다.
        /// </summary>
        /// <param name="upgradedCards">반환받을 업그레이드 카드 목록</param>
        /// <param name="selectedCards">선택된 카드</param>
        public bool CanUpgrade(out List<CardBehaviour> upgradedCards, params CardBehaviour[] selectedCards)
        {
            // pass
            upgradedCards = selectedCards.ToList().Where(c => c.Data.CardType == CardType.Normal).ToList();
            return upgradedCards.Count >= 2;
            // else -> Sort + classify by Card
        }

        public bool IsEmpty => _cardDeck.IsEmpty();

        private CardBehaviour CreateCardInstance(BaseCard c)
        {
            if (_cardPrefab == null)
            {
                Debug.LogError("CardManager: No CardPrefab!");
            }

            var cardObject = Instantiate(_cardPrefab).GetComponent<CardBehaviour>();
            cardObject.Init(c);
            return cardObject;
        }

        private class CardDeck
        {
            private readonly List<BaseCard> _cards = new List<BaseCard>();

            private List<BaseCard> _allCards;

            public int GetRemainCardCount() => _cards.Count;

            public bool IsEmpty() => _cards.Count == 0;

            public void Reset()
            {
                // default set
                _allCards = new List<BaseCard>();
                _allCards.AddRange(Resources.LoadAll<BaseCard>("Card/Normal"));
                _cards.AddRange(_allCards);
                _cards.AddRange(_allCards);

                _allCards.AddRange(Resources.LoadAll<BaseCard>("Card/Skill"));
                _allCards.AddRange(Resources.LoadAll<BaseCard>("Card/Special"));
                
                _cards.AddRange(_allCards);
            }

            public BaseCard GetRandomCard()
            {
                if (_cards.Count == 0)
                {
                    Debug.LogError("CardDeck: No card to return");
                    return null;
                }

                int randomIndex = Random.Range(0, _cards.Count);
                var cardData = _cards[randomIndex];
                _cards.RemoveAt(randomIndex);
                return cardData;
            }

            public IReadOnlyList<BaseCard> GetAllCard()
            {
                return _cards.AsReadOnly();
            }

            public void AddCard(params BaseCard[] cards)
            {
                _cards.AddRange(cards);
            }
        }
    }
}