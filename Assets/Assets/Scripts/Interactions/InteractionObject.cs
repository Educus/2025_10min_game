using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public InteractionData interactionData;
    public InteractionRunner runner;

    void OnMouseDown()  // ������Ʈ Ŭ�� �� ����
    {
        Debug.Log("Ŭ��");
        runner.RunInteraction(interactionData);
    }
}