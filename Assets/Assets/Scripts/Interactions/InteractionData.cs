using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InteractionStep
{
    public Vector2 moveOffset;  // 이동할 방향과 거리 (예: 우로 5 → (5,0))
    public float duration;      // 이동 시간 (초)
    public string eventName;    // 이동 후 실행할 이벤트 이름 (없으면 비움)
}

[CreateAssetMenu(fileName = "InteractionData", menuName = "Game/InteractionData")]
public class InteractionData : ScriptableObject
{
    public string interactionName;           // 상호작용 이름
    public List<string> requiredFlags;       // 선행조건 플래그 (없으면 무조건 실행)
    public List<InteractionStep> steps;      // 이동 + 이벤트 시퀀스
}