using UnityEngine;

[CreateAssetMenu(menuName = "Card/Data")]
public class CardData: ScriptableObject
{
    [field: SerializeField] public Sprite image {  get; set; }
    [field: SerializeField] public float value {  get; set; }

}
