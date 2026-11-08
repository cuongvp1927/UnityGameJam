using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]private Hand hand;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Card c = CardViewCreator.Instance.CreateCard(transform.position, Quaternion.identity);
            StartCoroutine(hand.AddCard(c));
        }
    }
}
