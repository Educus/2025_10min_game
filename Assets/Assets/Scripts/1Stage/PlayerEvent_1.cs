using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEvent_1 : PlayerEvent
{
    private bool restraint = false;
    public IEnumerator IERestraint()
    {
        eventPlayer = true;

        if (restraint)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        restraint = true;
        player.anim.SetBool("Restraint", true);
        // ���� ������ �ȵǾ��� ��� �ٸ��� ����x => �ٸ� �ڷ�ƾ�鿡�� �߰�
        Debug.Log(("��������"));
        // �����̴� �ִϸ��̼� ����
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(IEMove(new Vector2(-0.5f, -3.55f), 1.5f));

        yield return new WaitForSeconds(3f);

        eventPlayer = false;
    }

    public bool onLight { get; private set; } = false; // ��ȣ�ۿ� ����
    [SerializeField] private GameObject[] lightButton; // �����°͵�
    public IEnumerator IEButton() // ���ѱ�
    {
        eventPlayer = true;

        if (!restraint)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown_Restraint"));
            eventPlayer = false;
            yield break;
        }

        if (onLight)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));
            
            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        onLight = true;

        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-3.9f, -4f), 1f));
        player.behind = true;
        yield return StartCoroutine(IEMove(new Vector2(-3f, -2f), 1f));
        player.behind = false;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(1f, -0.5f), 1.2f));
        player.right = false;
        player.move = false;

        yield return StartCoroutine(IEAnim("Player_Click"));
        lightButton[0].SetActive(true);
        lightButton[1].SetActive(true);

        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-3f, -2f), 1.2f));
        yield return StartCoroutine(IEMove(new Vector2(-3.9f, -4f), 0.7f));
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(0, -4f), 1f));
        player.right = false;
        player.move = false;


        yield return new WaitForSeconds(0.5f);

        eventPlayer = false;
    }

    private bool onFireEx = false; // ��ȣ�ۿ� ����
    private bool getFireEx = false; // ���� ����
    [SerializeField] private GameObject objFire;
    [SerializeField] private GameObject myFire;
    public IEnumerator IEFireExtinguisher() // ��ȭ��
    {
        eventPlayer = true;

        if (!restraint)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown_Restraint"));
            eventPlayer = false;
            yield break;
        }

        // ���� ���� ���, �������� ��� �ִ� ���, �̹� ��ȣ�ۿ� �� ���
        if (!onLight || getItem || onFireEx)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        onFireEx = true;
        getItem = true;
        getFireEx = true;

        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-3.9f, -4f), 1f));
        player.behind = true;
        yield return StartCoroutine(IEMove(new Vector2(-4.35f, -0.5f), 1.3f));
        player.behind = false;
        player.move = false;

        // �ݴ� �ִϸ��̼�
        yield return StartCoroutine(IEAnim("Player_Grap"));
        objFire.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        myFire.SetActive(true);

        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(-3.9f, -4f), 1.3f));
        yield return StartCoroutine(IEMove(new Vector2(0f, -4f), 1f));
        player.right = false;
        player.move = false;

        objFire.SetActive(false);
        eventPlayer = false;
    }

    private bool onLadder1 = false;
    private bool onLadder2 = false;
    private bool getLadder = false;
    [SerializeField] private GameObject objLadder;
    [SerializeField] private GameObject myLadder;
    public IEnumerator IELadder() // ��ٸ�
    {
        eventPlayer = true;

        if (!restraint)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown_Restraint"));
            eventPlayer = false;
            yield break;
        }
        
        // ���� ���� ���, �̹� ��ȣ�ۿ� ��� �� ���, ��ȣ�ۿ� ���ߴµ� ��ȭ�⸦ �ȵ���, ������ ����µ� ��ȭ�Ⱑ �ƴ� ���
        if (!onLight || onLadder2 || (!onLadder1 && !getFireEx) || (getItem && !getFireEx))
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));
        
            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;
        
            yield break;
        }

        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(3.5f, -4.5f), 0.7f));
        yield return StartCoroutine(IEMove(new Vector2(6.5f, -4.35f), 0.7f));
        player.right = false;
        player.behind = true;
        yield return StartCoroutine(IEMove(new Vector2(5.75f, -2.3f), 0.7f));
        player.behind = false;
        player.move = false;

        if (!onLadder1)
        {
            onLadder1 = true;

            // �μ��� �ִϸ��̼�
            player.right = true;
            player.objBreak = true;
            yield return StartCoroutine(IEAnim("Player_Break_Left"));
            myFire.SetActive(false);
            player.right = false;
            getItem = false;
            getFireEx = false;

            player.objBreak = false;
        }
        else
        {
            onLadder2 = true;

            // �ݴ� �ִϸ��̼�
            player.right = true;
            yield return StartCoroutine(IEAnim("Player_Grap"));
            objLadder.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            myLadder.SetActive(true);

            player.right = false;
            getItem = true;
            getLadder = true;
        }

        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(6.5f, -4.35f), 0.7f));
        player.right = false;
        yield return StartCoroutine(IEMove(new Vector2(3.5f, -4.5f), 0.7f));
        yield return StartCoroutine(IEMove(new Vector2(0f, -4f), 0.7f));
        player.move = false;

        eventPlayer = false;
    }

    private bool onCardKey = false;
    private bool getCardKey = false;
    [SerializeField] private GameObject cardkey; // ī��Ű
    [SerializeField] private GameObject myCardKey;
    [SerializeField] private GameObject setLadder; // ��ٸ� ��ġ
    public IEnumerator IECardKey() // ī��Ű
    {
        eventPlayer = true;

        if (!restraint)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown_Restraint"));
            eventPlayer = false;
            yield break;
        }
        
        // ���� ���� ���, �̹� ��ȣ�ۿ� �� ���, ������ ���� ���� ���, ������ ����µ� ��ٸ��� �ƴ� ���
        if (!onLight || onCardKey || !getItem || (getItem && !getLadder))
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));
        
            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;
        
            yield break;
        }

        onCardKey = true;

        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-3.9f, -4f), 1f));
        player.behind = true;
        yield return StartCoroutine(IEMove(new Vector2(-4f, -0.5f), 1.3f));
        player.behind = false;
        player.move = false;

        // ��ٸ� ���� �ִϸ��̼�
        // ��ٸ� Ÿ�� �ö󰡼� ī���� ��� �ִϸ��̼�
        player.objBreak = true;
        setLadder.SetActive(true);

        player.move = true;
        player.behind = true;
        yield return StartCoroutine(IEMove(new Vector2(-4f, 2f), 1.3f));

        cardkey.SetActive(false);
        myLadder.SetActive(false);
        player.objBreak = false;

        yield return StartCoroutine(IEMove(new Vector2(-4f, -0.5f), 1.3f));
        StartCoroutine(IEAnim("Player_Move_Idle"));
        myCardKey.SetActive(true);
        player.move = false;
        player.behind = false;
        getLadder = false;
        getCardKey = true;

        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(-3.9f, -4f), 1.3f));
        yield return StartCoroutine(IEMove(new Vector2(0f, -4f), 1f));
        player.right = false;
        player.move = false;

        eventPlayer = false;
    }

    private bool onCardTap = false;
    [SerializeField] private GameObject cardTap;
    [SerializeField] private ObjectEvent_1 cardTapEvent;
    public IEnumerator IECardTap() // ī����
    {
        eventPlayer = true;

        if (!restraint)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown_Restraint"));
            eventPlayer = false;
            yield break;
        }

        // ���� ���� ���, �̹� ��ȣ�ۿ� �� ���, ������ ���� ���� ���, ������ ����µ� ī��Ű�� �ƴ� ���
        if (!onLight || onCardTap || !getItem || (getItem && !getCardKey))
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));
        
            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;
        
            yield break;
        }

        onCardTap = true;

        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(3.5f, -4.5f), 0.7f));
        yield return StartCoroutine(IEMove(new Vector2(6.5f, -4.35f), 0.7f));
        player.right = false;
        player.behind = true;
        yield return StartCoroutine(IEMove(new Vector2(5.2f, -1f), 1.2f));
        player.behind = false;
        player.move = false;

        // ī���ǿ� ī��Ű ���
        player.objBreak = true;
        yield return StartCoroutine(IEAnim("Player_Click"));
        myCardKey.SetActive(false);
        player.objBreak = false;

        getItem = false;
        getCardKey = false;
        cardTap.SetActive(true);
        StartCoroutine(cardTapEvent.IEAnim());

        yield return StartCoroutine(IEDoor());

        eventPlayer = false;
    }

    [SerializeField] private CircleMaskShrinkController maskController;
    public IEnumerator IEDoor() // ��
    {
        eventPlayer = true;

        // if (!restraint)
        // {
        //     yield return StartCoroutine(IEAnim("Player_Unknown_Restraint"));
        //     eventPlayer = false;
        //     yield break;
        // }

        if (!onCardTap)
        {
            // Ż��!
            Debug.Log("Ż��");
            yield return StartCoroutine(maskController.ShrinkRoutine());

            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene("2Stage");
        }
        else
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));
            
            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;
        }

        eventPlayer = false;
    }
}
