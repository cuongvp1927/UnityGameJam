using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]private HandUI hand;
    [SerializeField] private CardData cardData;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CardObj cardObj = new(cardData);
            CardUI c = CardViewCreator.Instance.CreateCard(cardObj,transform.position, Quaternion.identity);
            StartCoroutine(hand.AddCard(c));
        }
    }
}
