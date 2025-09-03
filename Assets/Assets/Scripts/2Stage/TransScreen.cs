using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransScreen : MonoBehaviour
{
    [SerializeField] PlayerEvent_2 playerEvent;

    [SerializeField] private GameObject offScreen;
    [SerializeField] private GameObject onScreen;
    [SerializeField] private GameObject machineOffScreen;
    [SerializeField] private GameObject[] machineOnScreen;

    private void Start()
    {
        offScreen.SetActive(true);
        onScreen.SetActive(false);
        machineOffScreen.SetActive(true);
        foreach (GameObject obj in machineOnScreen)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    void Update()
    {
        if (playerEvent.onMouseEat == false) return;

        offScreen.SetActive(false);
        onScreen.SetActive(true);
        machineOffScreen.SetActive(false);

        if (!playerEvent.onTrans || playerEvent.num == 4)
        {
            machineOnScreen[0].SetActive(false);
            machineOnScreen[1].SetActive(false);
            machineOnScreen[2].SetActive(false);
            machineOnScreen[3].SetActive(true);
        }
        else
        {
            machineOnScreen[0].SetActive(playerEvent.num == 1);
            machineOnScreen[1].SetActive(playerEvent.num == 2);
            machineOnScreen[2].SetActive(playerEvent.num == 3);
            machineOnScreen[3].SetActive(playerEvent.num == 4);
        }
    }
}
