using UnityEngine;

public class CardObj : MonoBehaviour
{
    public Sprite image => data.image;
    public float value => data.value;

    private readonly CardData data;
    public CardObj(CardData cardData)
    {
        data = cardData;
    }
}
