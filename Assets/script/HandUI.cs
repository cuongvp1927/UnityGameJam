using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class HandUI : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float maxHandSize;
    private readonly List<CardUI> cardList = new();

    public IEnumerator AddCard(CardUI newCard)
    {
        cardList.Add(newCard);
        yield return UpdateCardPos(0.5f);
    }

    public IEnumerator UpdateCardPos(float duration)
    {
        if (cardList.Count == 0) yield break;
        float cardSpacing = 1f / maxHandSize; //(10 cards maximum)
        float firstCardPos = 0.5f - (cardList.Count - 1) * cardSpacing /2;

        Spline spl = splineContainer.Spline;

        for (int i = 0; i < cardList.Count; i++) { 
            float p = firstCardPos + i * cardSpacing; 

            Vector3 splinePos = spl.EvaluatePosition(p);
            Vector3 forward  = spl.EvaluateTangent(p);
            Vector3 up = spl.EvaluateUpVector(p);

            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up,forward).normalized);
            cardList[i].transform.DOMove(splinePos + transform.position + 0.01f * i * Vector3.back, duration);
            cardList[i].transform.DORotate(rotation.eulerAngles, duration);
        }
        yield return new WaitForSeconds(duration);
    }
}
