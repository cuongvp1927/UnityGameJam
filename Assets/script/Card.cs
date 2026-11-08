using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text desc;
    [SerializeField] private TMP_Text valueT;

    [SerializeField] private float valueF;

    [SerializeField] private GameObject sprite;
    [SerializeField] private SpriteRenderer cardImg;
}
