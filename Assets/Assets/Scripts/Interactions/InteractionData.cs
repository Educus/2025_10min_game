using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InteractionStep
{
    public Vector2 moveOffset;  // �̵��� ����� �Ÿ� (��: ��� 5 �� (5,0))
    public float duration;      // �̵� �ð� (��)
    public string eventName;    // �̵� �� ������ �̺�Ʈ �̸� (������ ���)
}

[CreateAssetMenu(fileName = "InteractionData", menuName = "Game/InteractionData")]
public class InteractionData : ScriptableObject
{
    public string interactionName;           // ��ȣ�ۿ� �̸�
    public List<string> requiredFlags;       // �������� �÷��� (������ ������ ����)
    public List<InteractionStep> steps;      // �̵� + �̺�Ʈ ������
}