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
        // 구속 해제가 안되었을 경우 다른거 실행x => 다른 코루틴들에게 추가
        Debug.Log(("구속해제"));
        // 움직이는 애니메이션 실행
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(IEMove(new Vector2(-0.5f, -3.55f), 1.5f));

        yield return new WaitForSeconds(3f);

        eventPlayer = false;
    }

    public bool onLight { get; private set; } = false; // 상호작용 여부
    [SerializeField] private GameObject[] lightButton; // 켜지는것들
    public IEnumerator IEButton() // 불켜기
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

    private bool onFireEx = false; // 상호작용 여부
    private bool getFireEx = false; // 보유 여부
    [SerializeField] private GameObject objFire;
    [SerializeField] private GameObject myFire;
    public IEnumerator IEFireExtinguisher() // 소화기
    {
        eventPlayer = true;

        if (!restraint)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown_Restraint"));
            eventPlayer = false;
            yield break;
        }

        // 불이 꺼진 경우, 아이템을 들고 있는 경우, 이미 상호작용 한 경우
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

        // 줍는 애니메이션
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
    public IEnumerator IELadder() // 사다리
    {
        eventPlayer = true;

        if (!restraint)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown_Restraint"));
            eventPlayer = false;
            yield break;
        }
        
        // 불이 꺼진 경우, 이미 상호작용 모두 한 경우, 상호작용 안했는데 소화기를 안든경우, 물건을 들었는데 소화기가 아닌 경우
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

            // 부수는 애니메이션
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

            // 줍는 애니메이션
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
    [SerializeField] private GameObject cardkey; // 카드키
    [SerializeField] private GameObject myCardKey;
    [SerializeField] private GameObject setLadder; // 사다리 설치
    public IEnumerator IECardKey() // 카드키
    {
        eventPlayer = true;

        if (!restraint)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown_Restraint"));
            eventPlayer = false;
            yield break;
        }
        
        // 불이 꺼진 경우, 이미 상호작용 한 경우, 물건을 들지 않은 경우, 물건을 들었는데 사다리가 아닌 경우
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

        // 사다리 놓는 애니메이션
        // 사다리 타고 올라가서 카드지 잡는 애니메이션
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
    public IEnumerator IECardTap() // 카드탭
    {
        eventPlayer = true;

        if (!restraint)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown_Restraint"));
            eventPlayer = false;
            yield break;
        }

        // 불이 꺼진 경우, 이미 상호작용 한 경우, 물건을 들지 않은 경우, 물건을 들었는데 카드키가 아닌 경우
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

        // 카드탭에 카드키 찍기
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
    public IEnumerator IEDoor() // 문
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
            // 탈출!
            Debug.Log("탈출");
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
