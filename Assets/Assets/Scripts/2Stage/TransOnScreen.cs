using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransOnScreen : MonoBehaviour
{
    [SerializeField] private int num;
    [SerializeField] private GameObject onScreen;
    [SerializeField] private GameObject offScreen;

    [SerializeField] PlayerEvent_2 playerEvent;

    private void Awake()
    {
        onScreen.SetActive(false);
        offScreen.SetActive(true);
    }

    public void OnScreen()
    {
        onScreen.SetActive(true);
        offScreen.SetActive(false);
        StartCoroutine(playerEvent.IETrasnMachine(num));
    }
    public void OffScreen()
    {
        onScreen.SetActive(false);
        offScreen.SetActive(true);
    }
}
