using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEvent_2 : PlayerEvent
{
    // yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 1f));

    private bool getSeed = false;
    public IEnumerator IESeed() // 씨앗
    {
        eventPlayer = true;

        // 아이템을 들고 있는 경우
        if (getItem)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // 변신한 상태인 경우
        if (onTrans)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-5.85f, -5.65f), 1.5f));
        player.move = false;

        // 줍는 애니메이션
        yield return StartCoroutine(IEAnim("Player_Grap"));
        getItem = true;
        getSeed = true;

        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 1.5f));
        player.right = false;
        player.move = false;

        eventPlayer = false;
    }

    [SerializeField] private GameObject[] objMouseEat;
    public bool onMouseEat { get; private set; } = false;
    public IEnumerator IEMouseEat() // 쥐 먹이주기
    {
        eventPlayer = true;

        // 물건을 들었는데 씨앗이 아닌 경우
        if (!getSeed)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // 변신한 상태인 경우
        if (onTrans)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        player.move = true;
        player.behind = true;
        yield return StartCoroutine(IEMove(new Vector2(-1.6f, -0.5f), 1.2f));
        player.behind = false;
        player.move = false;

        // 먹이 주기
        getItem = false;
        getSeed = false;

        // 먹이통 열리는 애니메이션 실행
        yield return new WaitForSeconds(1f);

        // 기계들 전원 켜지기
        yield return new WaitForSeconds(1f);
        onMouseEat = true;

        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 1.2f));
        player.right = false;
        player.move = false;

        eventPlayer = false;
    }

    public bool onTrans { get; private set; } = false;
    [SerializeField] private GameObject transMachine; // 변신 기계
    [SerializeField] public int num { get; private set; } = 4; // 변신 번호
    public IEnumerator IETrasnMachine(int num) // 변신1 ~ 3
    {
        eventPlayer = true;

        // 4번을 눌렀을 경우
        if (num == 4)
        {
            StartCoroutine(IETrasnMachineReturn());

            yield break;
        }
        // 먹이 안준 경우, 이미 변신 한 경우, 물건을 든 경우
        if (!onMouseEat || onTrans || getItem)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // 변신기계 내려오는 애니메이션
        yield return new WaitForSeconds(0.5f);
        transMachine.transform.position += new Vector3(0, -1.3f);

        yield return new WaitForSeconds(0.1f);
        transMachine.transform.position += new Vector3(0, -6f);

        // 해당 캐릭터 변신
        onTrans = true;
        this.num = num;
        string transNum = "isTrans" + this.num;
        player.anim.SetBool(transNum, true);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(IEObjMove(transMachine, new Vector2(0, 7.3f), 1f));
        
        eventPlayer = false;
    }

    [SerializeField] private GameObject[] transScreen4; // 변신 화면들
    public IEnumerator IETrasnMachineReturn() // 변신 해제
    {
        eventPlayer = true;

        // 먹이 안준 경우, 변신을 안한 경우 물건을 든 경우
        if (!onMouseEat || !onTrans || getItem)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // 변신기계 내려오는 애니메이션
        yield return new WaitForSeconds(0.5f);
        transMachine.transform.position += new Vector3(0, -1.3f);

        yield return new WaitForSeconds(0.1f);
        transMachine.transform.position += new Vector3(0, -6f);

        // 모든 캐릭터 변신 해제
        onTrans = false;
        for (int i = 1; i < 4; i++)
        {
            string transNum = "isTrans" + i;
            player.anim.SetBool(transNum, false);
        }

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(IEObjMove(transMachine, new Vector2(0, 7.3f), 1f));

        eventPlayer = false;
    }

    [SerializeField] private GameObject laserPlate;
    [SerializeField] private GameObject laserWire;
    public IEnumerator IELaserPlate() // 레이저 판
    {
        eventPlayer = true;

        // 변신을 했는데 2번이 아닌 경우
        if (!(onTrans && num == 2))
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));
        
            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;
        
            yield break;
        }

        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(5.65f, -6f), 1.2f));
        player.right = false;
        player.move = false;

        // 레이저 판 파괴 애니메이션
        // 애니메이션에 물건 파괴 이벤트 넣기
        yield return StartCoroutine(IEAnim("Player_Click"));
        laserWire.SetActive(true);
        yield return StartCoroutine(IEPlate());

        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 1f));
        player.move = false;

        laserPlate.SetActive(false);

        eventPlayer = false;
    }

    private IEnumerator IEPlate()
    {
        // 흔들기(왕복)
        for (int i = 0; i < 3; i++)
        {
            yield return RotateZ(15);
            yield return RotateZ(-15);
        }

        // 원래 각도로 되돌리기
        yield return RotateZ(0);

        // 떨어지기
        while (laserPlate.transform.position.y > -10)
        {
            laserPlate.transform.position += Vector3.down * 5f * Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator RotateZ(float targetAngle)
    {
        Quaternion startRot = laserPlate.transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, targetAngle);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / 0.25f;
            laserPlate.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
    }

    private bool onLasetWire = false;
    public IEnumerator IELaserWire() // 레이저 전선
    {
        eventPlayer = true;

        // 변신을 했는데 3번이 아닌 경우
        if (!(onTrans && num == 3))
        {
            // 손대고 감전
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }




        eventPlayer = false;
    }
    public IEnumerator IEDoor() // 문
    {
        // 변신을 한 경우
        if (onTrans)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }
        else if (!onLasetWire) // 레이저 전선 해결안한 경우
        {
            // 걸어가다 감전?
        }

    }
}
