using System;
using System.Collections.Generic;
using System.Linq;
using CardAsset;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _cardPrefab;

    private CardDeck _cardDeck = new CardDeck();
    
    /// <summary>
    /// 덱에 있는 카드 데이터를 랜덤으로 사용해서 카드 오브젝트의 배열로 반환합니다.
    /// 만약 덱의 카드 개수가 부족할 경우 가능한 만큼만 반환합니다.
    /// Draw된 카드는 덱에서 삭제됩니다.
    /// </summary>
    public CardBehaviour[] DrawCard(int amount)
    {
        int availableCards = _cardDeck.GetRemainCardCount();
        if (availableCards < amount)
        {
            Debug.LogWarning("카드 덱 수량 부족: " + availableCards + "장만 반환합니다.");
            amount = availableCards;
            if (amount == 0) return null;
        }

        CardBehaviour[] cards = CreateCardInstances(amount);

        for (int i = 0; i < amount; i++)
        {
            var cardData = _cardDeck.GetRandomCard();
            cards[i]?.Init(cardData);
        }

        return cards;
    }

    /// <summary>
    /// 덱 내의 모든 카드를 카드 오브젝트로 생성해서 반환합니다.
    /// </summary>
    public CardBehaviour[] GetAllCardInDeck()
    {
        var cardData = _cardDeck.GetAllCard();
        var arr = CreateCardInstances(cardData.Count());
        for (int i = 0; i < cardData.Count; i++)
        {
            arr[i].Init(cardData[i]);
        }

        return arr;
    }

    /// <summary>
    /// 덱 내의 모든 카드의 "데이터"를 반환합니다.
    /// </summary>
    public IReadOnlyList<Card> GetAllCardDataInDeck()
        => _cardDeck.GetAllCard();

    public void AddCardToDeck(params CardBehaviour[] cards)
    {
        Debug.LogWarning("카드 데이터가 아닌 카드 오브젝트가 들어옴");
        AddCardToDeck((from card in cards select card.GetComponent<Card>()).ToArray());
    }

    public void AddCardToDeck(params Card[] cards)
        => _cardDeck.AddCard(cards);

    /// <summary>
    /// General 카드가 강화 가능한 경우 true를 반환합니다.
    /// </summary>
    /// <param name="upgradedCards">업그레이드 되었을 시 선택된 카드 목록</param>
    /// <param name="selectedCards">현재 플레이어가 선택한 카드들</param>
    public bool CanUpgrade(out List<CardBehaviour> upgradedCards, params CardBehaviour[] selectedCards)
    {
        // pass
        upgradedCards = new List<CardBehaviour>();
        return true;
    }

    /// <summary>
    /// 지정된 수만큼 카드 오브젝트 인스턴스를 생성합니다.
    /// </summary>
    /// <param name="amount">생성할 수</param>
    /// <returns>생성된 카드 게임 오브젝트 배열</returns>
    private CardBehaviour[] CreateCardInstances(int amount)
    {
        if (_cardPrefab == null)
        {
            Debug.LogError("카드 프리팹이 설정되지 않았습니다.");
            return new CardBehaviour[amount];
        }

        CardBehaviour[] cards = new CardBehaviour[amount];
        for (int i = 0; i < amount; i++)
        {
            cards[i] = Instantiate(_cardPrefab, transform).GetComponent<CardBehaviour>();
        }

        return cards;
    }

    private class CardDeck
    {
        private List<Card> _cards = new List<Card>();

        public int GetRemainCardCount() => _cards.Count;

        public Card GetRandomCard()
        {
            if (_cards.Count == 0)
            {
                Debug.LogError("No card in deck, but GetRandomCard is Called.");
                return null;
            }

            int randomIndex = Random.Range(0, _cards.Count);
            var cardData = _cards[randomIndex];
            _cards.RemoveAt(randomIndex);
            return cardData;
        }

        public IReadOnlyList<Card> GetAllCard()
        {
            return _cards.AsReadOnly();
        }

        public void AddCard(params Card[] cards)
        {
            _cards.AddRange(cards);
        }
    }
}
