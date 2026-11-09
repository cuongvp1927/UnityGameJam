using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{ 
    [SerializeField] private GameObject warper;
    [SerializeField] private SpriteRenderer cardImg;
    [SerializeField] private float cardValue;

    public CardObj card {  get; private set; }
    public void createCard(CardObj cardObj)
    {
        card = cardObj;
        cardImg.sprite = card.image;
        cardValue = card.value;
    }

    private void OnMouseEnter()
    {
        warper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        CardHoverSystem.Instance.Show(card, pos);
    }

    private void OnMouseExit()
    {
        CardHoverSystem.Instance.Hide();
        warper.SetActive(true);

    }
}
