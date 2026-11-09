using DG.Tweening;
using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] private CardUI cardPrefab;

    public CardUI CreateCard(CardObj cardObj, Vector2 position , Quaternion rotation)
    {
        CardUI card = Instantiate(cardPrefab, position, rotation);
        card.createCard(cardObj);
        card.transform.localScale = Vector3.zero;
        card.transform.DOScale(Vector3.one, 0.15f);
        return card;
    }
}
