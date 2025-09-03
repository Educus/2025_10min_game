using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public PlayerEvent_1 playerEvent;
    public ObjectEvent_1 objectEvent;

    [SerializeField] private string methodName;
    [SerializeField] private string objectName;

    void OnMouseDown()  // 오브젝트 클릭 시 실행
    {
        Debug.Log("클릭");

        /*
        if (playerEvent == null || string.IsNullOrEmpty(methodName)) { Debug.Log("!playerEvent"); }
        else
        {
            if (!playerEvent.eventPlayer)
            {
                // PlayerController 타입에서 해당 이름의 메서드 찾기
                MethodInfo method = typeof(PlayerEvent_1).GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

                if (method == null)
                {
                    Debug.LogWarning($"코루틴 {methodName} 을(를) 찾을 수 없습니다.");
                    return;
                }


                // 메서드가 코루틴(IEnumerator)인지 확인
                if (typeof(IEnumerator).IsAssignableFrom(method.ReturnType))
                {
                    StartCoroutine((IEnumerator)method.Invoke(playerEvent, null));
                }
                else
                {
                    Debug.LogWarning($"메서드 {methodName} 은 코루틴(IEnumerator)이 아닙니다.");
                }
            }
        }

        if (objectEvent == null || string.IsNullOrEmpty(objectName)) { }
        else
        {
            if (!objectEvent.eventObject)
            {
                // PlayerController 타입에서 해당 이름의 메서드 찾기
                MethodInfo method = typeof(ObjectEvent_1).GetMethod(objectName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

                if (method == null)
                {
                    Debug.LogWarning($"코루틴 {objectName} 을(를) 찾을 수 없습니다.");
                    return;
                }


                // 메서드가 코루틴(IEnumerator)인지 확인
                if (typeof(IEnumerator).IsAssignableFrom(method.ReturnType))
                {
                    StartCoroutine((IEnumerator)method.Invoke(objectEvent, null));
                }
                else
                {
                    Debug.LogWarning($"메서드 {objectName} 은 코루틴(IEnumerator)이 아닙니다.");
                }
            }
        }
        */

        // playerEvent 실행
        TryInvokeCoroutine(playerEvent, methodName, nameof(PlayerEvent_1), () => !playerEvent.eventPlayer);

        // objectEvent 실행
        TryInvokeCoroutine(objectEvent, objectName, nameof(ObjectEvent_1), () => !objectEvent.eventObject);
    }

    private void TryInvokeCoroutine(object target, string methodName, string typeName, System.Func<bool> condition)
    {
        if (target == null)
        {
            Debug.LogWarning($"{typeName}가 null입니다.");
            return;
        }

        if (string.IsNullOrEmpty(methodName))
        {
            Debug.LogWarning($"{typeName}의 메서드 이름이 비어있습니다.");
            return;
        }

        if (!condition())
            return;

        MethodInfo method = target.GetType().GetMethod(methodName,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

        if (method == null)
        {
            Debug.LogWarning($"{typeName}에서 코루틴 {methodName}을(를) 찾을 수 없습니다.");
            return;
        }

        if (typeof(IEnumerator).IsAssignableFrom(method.ReturnType))
        {
            StartCoroutine((IEnumerator)method.Invoke(target, null));
        }
        else
        {
            Debug.LogWarning($"{typeName}의 메서드 {methodName}은 코루틴(IEnumerator)이 아닙니다.");
        }
    }
}