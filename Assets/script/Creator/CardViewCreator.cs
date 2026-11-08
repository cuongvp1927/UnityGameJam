using DG.Tweening;
using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] private Card cardPrefab;

    public Card CreateCard(Vector2 position , Quaternion rotation)
    {
        Card card = Instantiate(cardPrefab, position, rotation);
        card.transform.localScale = Vector3.zero;
        card.transform.DOScale(Vector3.one, 0.15f);
        return card;
    }
}
