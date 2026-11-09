using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    // create a performer for the game action
    private void OnEnable()
    {
        GameActionSystem.AttachPerformer<TurnAction>(TurnPerformer);
    }
    private void OnDisable()
    {
        GameActionSystem.DetachPerformer<TurnAction>();
    }
    private IEnumerator TurnPerformer(TurnAction turnAction)
    {
        Debug.Log("Turn over");
        yield return new WaitForSeconds(2);
        Debug.Log("Turn really over");

    }
}
