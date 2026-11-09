using UnityEngine;

public class CardHoverSystem : Singleton<CardHoverSystem>
{
    [SerializeField] private CardUI cardHover;

    public void Show(CardObj card, Vector3 position)
    {
        cardHover.gameObject.SetActive(true);
        cardHover.createCard(card);
        cardHover.transform.position = position;
    }

    public void Hide() { 
        cardHover.gameObject.SetActive(false);
    }
}
