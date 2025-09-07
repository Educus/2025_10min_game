using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionRunner : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public Animator playerAnimator; // �ִϸ����� ����

    private HashSet<string> flags = new HashSet<string>(); // ���� �÷��� ������

    public void AddFlag(string flag)
    {
        flags.Add(flag);
    }

    public void RunInteraction(InteractionData data)
    {
        // �������� üũ
        foreach (var req in data.requiredFlags)
        {
            if (!flags.Contains(req))
            {
                // ���� ���� X �� Player_Unknown �ִϸ��̼� ����
                if (playerAnimator != null)
                {
                    Debug.Log("�������� ����");
                    playerAnimator.Play("Player_Unknown");
                }
                return;
            }
        }

        // ���� ���� �� �̵� + �̺�Ʈ ����
        StartCoroutine(RunSteps(data.steps));
    }

    IEnumerator RunSteps(List<InteractionStep> steps)
    {
        foreach (var step in steps)
        {
            Vector3 start = player.position;
            Vector3 target = start + (Vector3)step.moveOffset;

            float t = 0;
            while (t < 1f)
            {
                t += Time.deltaTime / step.duration;
                player.position = Vector3.Lerp(start, target, t);
                yield return null;
            }

            if (!string.IsNullOrEmpty(step.eventName))
            {
                Debug.Log("�̺�Ʈ ����: " + step.eventName);
                // �̺�Ʈ ���� ���� �߰� ����
            }
        }
    }
}