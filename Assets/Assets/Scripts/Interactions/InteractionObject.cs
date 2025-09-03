using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public InteractionData interactionData;
    public InteractionRunner runner;

    void OnMouseDown()  // 오브젝트 클릭 시 실행
    {
        Debug.Log("클릭");
        runner.RunInteraction(interactionData);
    }
}