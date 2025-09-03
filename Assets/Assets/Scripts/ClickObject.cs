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

    void OnMouseDown()  // ������Ʈ Ŭ�� �� ����
    {
        Debug.Log("Ŭ��");

        /*
        if (playerEvent == null || string.IsNullOrEmpty(methodName)) { Debug.Log("!playerEvent"); }
        else
        {
            if (!playerEvent.eventPlayer)
            {
                // PlayerController Ÿ�Կ��� �ش� �̸��� �޼��� ã��
                MethodInfo method = typeof(PlayerEvent_1).GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

                if (method == null)
                {
                    Debug.LogWarning($"�ڷ�ƾ {methodName} ��(��) ã�� �� �����ϴ�.");
                    return;
                }


                // �޼��尡 �ڷ�ƾ(IEnumerator)���� Ȯ��
                if (typeof(IEnumerator).IsAssignableFrom(method.ReturnType))
                {
                    StartCoroutine((IEnumerator)method.Invoke(playerEvent, null));
                }
                else
                {
                    Debug.LogWarning($"�޼��� {methodName} �� �ڷ�ƾ(IEnumerator)�� �ƴմϴ�.");
                }
            }
        }

        if (objectEvent == null || string.IsNullOrEmpty(objectName)) { }
        else
        {
            if (!objectEvent.eventObject)
            {
                // PlayerController Ÿ�Կ��� �ش� �̸��� �޼��� ã��
                MethodInfo method = typeof(ObjectEvent_1).GetMethod(objectName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

                if (method == null)
                {
                    Debug.LogWarning($"�ڷ�ƾ {objectName} ��(��) ã�� �� �����ϴ�.");
                    return;
                }


                // �޼��尡 �ڷ�ƾ(IEnumerator)���� Ȯ��
                if (typeof(IEnumerator).IsAssignableFrom(method.ReturnType))
                {
                    StartCoroutine((IEnumerator)method.Invoke(objectEvent, null));
                }
                else
                {
                    Debug.LogWarning($"�޼��� {objectName} �� �ڷ�ƾ(IEnumerator)�� �ƴմϴ�.");
                }
            }
        }
        */

        // playerEvent ����
        TryInvokeCoroutine(playerEvent, methodName, nameof(PlayerEvent_1), () => !playerEvent.eventPlayer);

        // objectEvent ����
        TryInvokeCoroutine(objectEvent, objectName, nameof(ObjectEvent_1), () => !objectEvent.eventObject);
    }

    private void TryInvokeCoroutine(object target, string methodName, string typeName, System.Func<bool> condition)
    {
        if (target == null)
        {
            Debug.LogWarning($"{typeName}�� null�Դϴ�.");
            return;
        }

        if (string.IsNullOrEmpty(methodName))
        {
            Debug.LogWarning($"{typeName}�� �޼��� �̸��� ����ֽ��ϴ�.");
            return;
        }

        if (!condition())
            return;

        MethodInfo method = target.GetType().GetMethod(methodName,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

        if (method == null)
        {
            Debug.LogWarning($"{typeName}���� �ڷ�ƾ {methodName}��(��) ã�� �� �����ϴ�.");
            return;
        }

        if (typeof(IEnumerator).IsAssignableFrom(method.ReturnType))
        {
            StartCoroutine((IEnumerator)method.Invoke(target, null));
        }
        else
        {
            Debug.LogWarning($"{typeName}�� �޼��� {methodName}�� �ڷ�ƾ(IEnumerator)�� �ƴմϴ�.");
        }
    }
}