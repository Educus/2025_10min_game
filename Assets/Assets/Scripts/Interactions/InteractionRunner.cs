using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionRunner : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public Animator playerAnimator; // 애니메이터 연결

    private HashSet<string> flags = new HashSet<string>(); // 게임 플래그 관리용

    public void AddFlag(string flag)
    {
        flags.Add(flag);
    }

    public void RunInteraction(InteractionData data)
    {
        // 선행조건 체크
        foreach (var req in data.requiredFlags)
        {
            if (!flags.Contains(req))
            {
                // 조건 만족 X → Player_Unknown 애니메이션 실행
                if (playerAnimator != null)
                {
                    Debug.Log("선행조건 실패");
                    playerAnimator.Play("Player_Unknown");
                }
                return;
            }
        }

        // 조건 만족 시 이동 + 이벤트 실행
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
                Debug.Log("이벤트 실행: " + step.eventName);
                // 이벤트 실행 로직 추가 가능
            }
        }
    }
}