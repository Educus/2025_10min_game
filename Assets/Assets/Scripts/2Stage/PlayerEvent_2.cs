using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEvent_2 : PlayerEvent
{
    // yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 1f));

    private bool onSeed = false;
    public bool getSeed { get; private set; } = false;
    public IEnumerator IESeed() // 씨앗
    {
        eventPlayer = true;

        // 아이템을 들고 있는 경우
        if (getItem)
        {
            yield return StartCoroutine(IEAnim($"GrapUnknown{num}"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // 이미 상호작용 한 경우
        if (onSeed)
        {
            // 변신한 상태인 경우
            if (onTrans)
            {
                string unknow;
                if (getItem)
                    unknow = "GrapUnknown" + this.num;
                else
                    unknow = "Unknown" + this.num;

                yield return StartCoroutine(IEAnim(unknow));

                yield return new WaitForSeconds(0.5f);
                eventPlayer = false;

                yield break;
            }
            else
            {
                yield return StartCoroutine(IEAnim("Player_Unknown"));

                yield return new WaitForSeconds(0.5f);
                eventPlayer = false;

                yield break;
            }
        }

        onSeed = true;

        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-5.85f, -5.65f), 1.5f));
        player.move = false;

        // 줍는 애니메이션
        yield return StartCoroutine(IEAnim("Player_Grap"));
        getItem = true;
        getSeed = true;
        player.anim.SetBool("Grap", true);

        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 1.5f));
        player.right = false;
        player.move = false;

        eventPlayer = false;
    }

    [SerializeField] private ObjectMouseEat objMouseEat;
    public bool onMouseEat { get; private set; } = false;
    public IEnumerator IEMouseEat() // 쥐 먹이주기
    {
        eventPlayer = true;

        // 물건을 들었는데 씨앗이 아닌 경우
        if (getItem && !getSeed)
        {
            yield return StartCoroutine(IEAnim($"GrapUnknown{num}"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // 물건을 안든경우
        if (onTrans && !getItem)
        {
            string unknow = "Unknown" + this.num;

            yield return StartCoroutine(IEAnim(unknow));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }
        else if (!onTrans && !getItem)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        player.move = true;
        player.behind = true;
        yield return StartCoroutine(IEMove(new Vector2(-1.6f, -0.5f), 0.7f));
        player.behind = false;
        player.move = false;

        // 먹이 주기
        StartCoroutine(IEAnim("Set_Seed"));
        getItem = false;
        getSeed = false;
        player.anim.SetBool("Grap", false);

        // 먹이통 열리는 애니메이션 실행
        yield return StartCoroutine(objMouseEat.IEOnMouseEat());


        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 0.7f));
        player.right = false;
        player.move = false;

        yield return new WaitForSeconds(2f);

        // 기계들 전원 켜지기
        onMouseEat = true;

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
            StartCoroutine(IETrasnMachineReturn(num));

            yield break;
        }
        // 먹이 안준 경우
        if (!onMouseEat)
        {
            if (getItem)
                yield return StartCoroutine(IEAnim("GrapUnknown4"));
            else
                yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // 변신한 상태인 경우
        if(onTrans)
        {
            string unknow;
            if (getItem)
                unknow = "GrapUnknown" + this.num;
            else
                unknow = "Unknown" + this.num;

            yield return StartCoroutine(IEAnim(unknow));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // 변신기계 내려오는 애니메이션
        yield return new WaitForSeconds(0.5f);
        transMachine.transform.position += new Vector3(0, -7.3f);

        // 해당 캐릭터 변신
        onTrans = true;
        this.num = num;
        string transNum = "isTrans" + this.num;
        player.anim.SetBool(transNum, true);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(IEObjMove(transMachine, new Vector2(0, 7.3f), 1f));
        
        eventPlayer = false;
    }

    public IEnumerator IETrasnMachineReturn(int num) // 변신 해제
    {
        eventPlayer = true;

        // 먹이 안준 경우, 변신을 안한 경우
        if (!onMouseEat || !onTrans)
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
        this.num = num;
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
    private bool onLaserPlate = false;
    public IEnumerator IELaserPlate() // 레이저 판
    {
        eventPlayer = true;

        // 변신을 했는데 2번이 아닌 경우
        if (onTrans)
        {
            if (!(num == 2))
            {
                string unknow;
                if (getItem)
                    unknow = "GrapUnknown" + this.num;
                else
                    unknow = "Unknown" + this.num;

                yield return StartCoroutine(IEAnim(unknow));

                yield return new WaitForSeconds(0.5f);
                eventPlayer = false;

                yield break;
            }
        }
        else 
        {
            // 변신을 안한 경우
            if (getItem)
                yield return StartCoroutine(IEAnim("GrapUnknown4"));
            else
                yield return StartCoroutine(IEAnim("Player_Unknown"));
        
            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;
        
            yield break;
        }

        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(6f, -6.25f), 1.4f));
        player.right = false;
        player.move = false;

        // 레이저 판 파괴 애니메이션
        // 애니메이션에 물건 파괴 이벤트 넣기
        yield return StartCoroutine(IEAnim("Strong_Break"));
        laserWire.SetActive(true);
        yield return StartCoroutine(IEPlate());
        onLaserPlate = true;

        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 1.4f));
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

    [SerializeField] private GameObject objNipper;
    public bool getNipper { get; private set; } = false;
    public IEnumerator IENipper()
    {
        eventPlayer = true;

        string animName = null;

        // 레이저판 안부순경우
        if (getItem)
        {
            animName = $"GrapUnknown{num}";
        }
        else if (onTrans && num != 1) // 변신을 했는데 1이 아닌 경우
        {
            animName = $"Unknown{num}";
        }
        else if (!onTrans) // 변신을 안한경우
        {
            animName = "Player_Unknown";
        }

        if (animName != null) // 공통 애니메이션 처리
        {
            yield return StartCoroutine(IEAnim(animName));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        player.move = true;
        yield return StartCoroutine(IEMove(new Vector2(-7.55f, 1f), 2f));
        player.anim.SetBool("Grap",true);
        getItem = true;
        getNipper = true;
        objNipper.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(1f);

        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 1.5f));
        player.right = false;
        player.move = false;

        objNipper.SetActive(false);

        eventPlayer = false;
    }

    private bool onLasetWire = false;
    [SerializeField] MyItem2 myNipper;
    public IEnumerator IELaserWire() // 레이저 전선
    {
        eventPlayer = true;

        if (onTrans)
        {
            // 변신을 했는데 3번이 아닌 경우
            if (!(num == 3))
            {
                string unknow;
                if (getItem)
                    unknow = "GrapUnknown" + this.num;
                else
                    unknow = "Unknown" + this.num;

                yield return StartCoroutine(IEAnim(unknow));

                yield return new WaitForSeconds(0.5f);
                eventPlayer = false;

                yield break;
            }
        }
        else
        {
            // 변신을 안한 경우
            if (getItem)
                yield return StartCoroutine(IEAnim("GrapUnknown4"));
            else
                yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // 니퍼가 없을 경우
        if (!getNipper)
        {
            if (onTrans)
            {
                string unknow;
                if (getItem)
                    unknow = "GrapUnknown" + this.num;
                else
                    unknow = "Unknown" + this.num;

                yield return StartCoroutine(IEAnim(unknow));

                yield return new WaitForSeconds(0.5f);
                eventPlayer = false;

                yield break;
            }
            else
            {
                yield return StartCoroutine(IEAnim("Player_Unknown"));

                yield return new WaitForSeconds(0.5f);
                eventPlayer = false;

                yield break;
            }
        }

        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(6.2f, -6.65f), 1.8f));

        player.anim.SetBool("UseNipper", true);
        myNipper.UseNipper();

        yield return new WaitForSeconds(2f);
        getItem = false;
        getNipper = false;
        player.anim.SetBool("Grap", false);
        onLasetWire = true;
        laser.OffLaser();

        player.right = false;
        player.anim.SetBool("UseNipper", false);

        yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 1.8f));
        player.move = false;

        eventPlayer = false;
    }

    [SerializeField] private CircleMaskShrinkController maskController;
    [SerializeField] ObjectEvent_2 doorEvent;
    [SerializeField] ObjectLaser laser;
    private bool onDoor = false;
    public IEnumerator IEDoor() // 문
    {
        eventPlayer = true;

        if (onDoor)
        {
            // 탈출!
            Debug.Log("탈출");
            yield return StartCoroutine(maskController.ShrinkRoutine());

            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene("3Stage");
        }
        else
        {
            player.move = true;
            player.right = true;
            yield return StartCoroutine(IEMove(new Vector2(2f, -3.65f), 0.6f));
            yield return StartCoroutine(IEMove(new Vector2(6f, -2.35f), 1f));
            player.move = false;
            player.right = false;

            if (onLasetWire) // 레이저 전선 해결한 경우
            {
                onDoor = true;
                StartCoroutine(doorEvent.IEAnim());
            }
            else
            {
                // 감전
                player.behind = true;
                StartCoroutine(laser.IEShotLaser());
                yield return StartCoroutine(IEAnim("Player_Shock"));
                player.behind = false;

                player.move = true;
                yield return StartCoroutine(IEMove(new Vector2(2f, -3.65f), 1f));
                yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 0.6f));
                player.move = false;
            }
        }

        eventPlayer = false;
    }
}
