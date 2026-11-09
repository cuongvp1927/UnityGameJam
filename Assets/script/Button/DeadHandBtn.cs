using Unity.VisualScripting;
using UnityEngine;

public class DeadHandBtn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClick()
    {
        TurnAction turnAction = new();
        Debug.Log(GameActionSystem.Instance);

        GameActionSystem.Instance.Perform(turnAction);

    }
}
