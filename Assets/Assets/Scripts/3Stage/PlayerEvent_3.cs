using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEvent_3 : PlayerEvent
{
    // yield return StartCoroutine(IEMove(new Vector2(2.5f, -4f), 1f));

    public int itemNum { get; private set; } = 0;
    public void GetItems(int num)
    {
        // ���� 9
        // �� 0
        // ������ 1 ~ ��������

        itemNum = num;
        getItem = true;
    }

    [SerializeField] private PiggyBank piggyBank;
    public IEnumerator IEPiggyBank() // ����������
    {
        eventPlayer = true;

        if (getItem)
        {
            yield return StartCoroutine(IEAnim("GrapUnknown"));
            yield return new WaitForSeconds(0.5f);

            eventPlayer = false;
            yield break;
        }

        if (piggyBank.setGold == false)
        {
            piggyBank.SetGold();
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            player.move = true;
            yield return StartCoroutine(IEMove(new Vector2(0.4f, -5.75f), 1f));
            player.move = false;

            yield return StartCoroutine(IEAnim("Player_Grap"));
            piggyBank.GetGold();
            player.anim.SetBool("Grap", true);
            GetItems(9);

            player.right = true;
            player.move = true;
            yield return StartCoroutine(IEMove(new Vector2(2.5f, -4f), 1f));
            player.move = false;
            player.right = false;
        }

        eventPlayer = false;
    }

    [SerializeField] Stage3Door1 door1;
    [SerializeField] GameObject objTrap;
    [SerializeField] Stage3Robot objRobot;
    private bool onTrap = false;
    private bool onRobot = false;
    public IEnumerator IEDoor1() // ��� ��
    {
        eventPlayer = true;

        if (onRobot) // �κ��� �׾��°�?
        {
            if (getItem)
                yield return StartCoroutine(IEAnim("GrapUnknown"));
            else
                yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);

            eventPlayer = false;
            yield break;
        }

        if (getItem && !(itemNum == 0)) // ���� �ƴ� ������ ��� �ִ°�?
        {
            yield return StartCoroutine(IEAnim("GrapUnknown"));
            yield return new WaitForSeconds(0.5f);

            eventPlayer = false;
            yield break;
        }

        // ���� ������ �ִ°�?
        if (getItem && itemNum == 0)
        {
            // �� �� �̵�
            player.move = true;
            yield return StartCoroutine(IEMove(new Vector2(-2.15f, -2.35f), 1.2f));
            player.move = false;

            // �� ��ġ
            player.behind = true;
            yield return new WaitForSeconds(0.5f);
            objTrap.SetActive(true);
            onTrap = true;
            getItem = false;
            player.anim.SetBool("Grap", false);

            yield return new WaitForSeconds(0.5f);
            player.behind = false;

            // ����
            player.right = true;
            player.move = true;
            yield return StartCoroutine(IEMove(new Vector2(2.5f, -4f), 1f));
            player.move = false;
            player.right = false;

            eventPlayer = false;
            yield break;
        }

        // �⺻ �ൿ ��ũ�Ϸ�����
        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-3.35f, -2.75f), 1.6f));
        player.behind = true;
        yield return StartCoroutine(IEMove(new Vector2(-4.4f, -1.7f), 0.4f));
        player.move = false;
        player.behind = false;

        player.right = true;
        yield return StartCoroutine(IEAnim("Player_Knock"));
        player.right = false;

        // ����
        player.behind = false;
        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(-4f, -2.15f), 0.2f));
        player.right = false;
        yield return StartCoroutine(IEMove(new Vector2(-8.3f, -3.65f), 0.8f));
        yield return StartCoroutine(IEMove(new Vector2(-9.55f, -3f), 0.2f));
        player.move = false;

        yield return StartCoroutine(door1.IEDoorOn());
        yield return StartCoroutine(objRobot.IEOnRobot());

        if (onTrap)
        {
            // ���� ����
            yield return StartCoroutine(objRobot.IEDeadRobot());
            yield return StartCoroutine(layger.IELaygerOff());
            Debug.Log("�κ����");
            onRobot = true;
        }
        else
        {
            yield return new WaitForSeconds(1.5f);

            yield return StartCoroutine(objRobot.IEOffRobot());
            yield return StartCoroutine(door1.IEDoorOff());
        }

        yield return new WaitForSeconds(1.5f);

        player.right = true;
        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-8.3f, -3.65f), 0.2f));
        yield return StartCoroutine(IEMove(new Vector2(2.5f, -4f), 2f));
        player.move = false;
        player.right = false;

        eventPlayer = false;
    }

    [SerializeField] private CircleMaskShrinkController maskController;
    [SerializeField] private Stage3Door1 layger;
    [SerializeField] ObjectEvent_3 doorEvent;
    private bool onDoor = false;
    public IEnumerator IEDoor2() // Ż�� ��
    {
        eventPlayer = true;

        if (onDoor)
        {
            // Ż��!
            Debug.Log("Ż��");
            yield return StartCoroutine(maskController.ShrinkRoutine());

            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene("Ending");
        }
        else
        {
            player.move = true;
            player.right = true;
            yield return StartCoroutine(IEMove(new Vector2(5.5f, -2f), 1f));
            player.move = false;
            player.right = false;

            if (onRobot) // �κ� �ذ��� ���
            {
                onDoor = true;
                StartCoroutine(doorEvent.IEAnim());
            }
            else
            {
                // ����
                layger.gameObject.SetActive(true);
                StartCoroutine(layger.IELayger());
                yield return StartCoroutine(IEAnim("Player_Shock"));

                player.move = true;
                yield return StartCoroutine(IEMove(new Vector2(2.5f, -4f), 1f));
                player.move = false;
            }
        }

        eventPlayer = false;
    }

    [SerializeField] Stage3VendingMachine objVendingMachine;
    public IEnumerator IEVendingMachine() // ���Ǳ�
    {
        eventPlayer = true;

        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-4.85f, -2.25f), 2.5f));
        player.move = false;
        player.behind = true;
        player.right = true;
        yield return StartCoroutine(IEAnim("Player_Click"));
        player.right = false;
        player.behind = false;

        if (getItem && itemNum == 9)
        {
            // ���� ���Ǳ� ����

            getItem = false;
            player.anim.SetBool("Grap", false);
            yield return StartCoroutine(objVendingMachine.IEMachine());
            yield return new WaitForSeconds(0.5f);

            player.move = true;
            yield return StartCoroutine(IEMove(new Vector2(-6.65f, -3.15f), 0.6f));
            player.move = false;
            player.behind = true;

            yield return new WaitForSeconds(0.8f);
            player.anim.SetBool("Grap", true);
            objVendingMachine.OffItem();
            getItem = true;
            player.behind = false;
        }
        else
        {
            if (!getItem)
                yield return StartCoroutine(IEAnim("Player_Unknown"));
            else
                yield return StartCoroutine(IEAnim("GrapUnknown"));

        }
        yield return new WaitForSeconds(0.5f);

        player.right = true;
        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(2.5f, -4f), 2.5f));
        player.move = false;
        player.right = false;

        eventPlayer = false;
    }

    public IEnumerator IEWastebasket() // ������
    {
        eventPlayer = true;

        if (!getItem)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));
            yield return new WaitForSeconds(0.5f);

            eventPlayer = false;
            yield break;
        }
        else if (getItem && itemNum == 0)
        {
            yield return StartCoroutine(IEAnim("GrapUnknown"));
            yield return new WaitForSeconds(0.5f);

            eventPlayer = false;
            yield break;
        }

        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-1.7f, -3.25f), 1f));
        yield return StartCoroutine(IEMove(new Vector2(-7.6f, -4.45f), 1.2f));
        player.move = false;

        player.anim.SetBool("Grap", false);
        getItem = false;
        yield return StartCoroutine(IEAnim("Player_Grap"));

        yield return new WaitForSeconds(0.5f);

        player.right = true;
        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-1.7f, -3.25f), 1.2f));
        yield return StartCoroutine(IEMove(new Vector2(2.5f, -4f), 1f));
        player.move = false;
        player.right = false;

        eventPlayer = false;
    }
}
