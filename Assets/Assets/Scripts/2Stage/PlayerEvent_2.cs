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
    public IEnumerator IESeed() // ����
    {
        eventPlayer = true;

        // �������� ��� �ִ� ���
        if (getItem)
        {
            yield return StartCoroutine(IEAnim($"GrapUnknown{num}"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // �̹� ��ȣ�ۿ� �� ���
        if (onSeed)
        {
            // ������ ������ ���
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

        // �ݴ� �ִϸ��̼�
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
    public IEnumerator IEMouseEat() // �� �����ֱ�
    {
        eventPlayer = true;

        // ������ ����µ� ������ �ƴ� ���
        if (getItem && !getSeed)
        {
            yield return StartCoroutine(IEAnim($"GrapUnknown{num}"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // ������ �ȵ���
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

        // ���� �ֱ�
        StartCoroutine(IEAnim("Set_Seed"));
        getItem = false;
        getSeed = false;
        player.anim.SetBool("Grap", false);

        // ������ ������ �ִϸ��̼� ����
        yield return StartCoroutine(objMouseEat.IEOnMouseEat());


        player.move = true;
        player.right = true;
        yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 0.7f));
        player.right = false;
        player.move = false;

        yield return new WaitForSeconds(2f);

        // ���� ���� ������
        onMouseEat = true;

        eventPlayer = false;
    }

    public bool onTrans { get; private set; } = false;
    [SerializeField] private GameObject transMachine; // ���� ���
    [SerializeField] public int num { get; private set; } = 4; // ���� ��ȣ
    public IEnumerator IETrasnMachine(int num) // ����1 ~ 3
    {
        eventPlayer = true;

        // 4���� ������ ���
        if (num == 4)
        {
            StartCoroutine(IETrasnMachineReturn(num));

            yield break;
        }
        // ���� ���� ���
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

        // ������ ������ ���
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

        // ���ű�� �������� �ִϸ��̼�
        yield return new WaitForSeconds(0.5f);
        transMachine.transform.position += new Vector3(0, -7.3f);

        // �ش� ĳ���� ����
        onTrans = true;
        this.num = num;
        string transNum = "isTrans" + this.num;
        player.anim.SetBool(transNum, true);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(IEObjMove(transMachine, new Vector2(0, 7.3f), 1f));
        
        eventPlayer = false;
    }

    public IEnumerator IETrasnMachineReturn(int num) // ���� ����
    {
        eventPlayer = true;

        // ���� ���� ���, ������ ���� ���
        if (!onMouseEat || !onTrans)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // ���ű�� �������� �ִϸ��̼�
        yield return new WaitForSeconds(0.5f);
        transMachine.transform.position += new Vector3(0, -1.3f);

        yield return new WaitForSeconds(0.1f);
        transMachine.transform.position += new Vector3(0, -6f);

        // ��� ĳ���� ���� ����
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
    public IEnumerator IELaserPlate() // ������ ��
    {
        eventPlayer = true;

        // ������ �ߴµ� 2���� �ƴ� ���
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
            // ������ ���� ���
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

        // ������ �� �ı� �ִϸ��̼�
        // �ִϸ��̼ǿ� ���� �ı� �̺�Ʈ �ֱ�
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
        // ����(�պ�)
        for (int i = 0; i < 3; i++)
        {
            yield return RotateZ(15);
            yield return RotateZ(-15);
        }

        // ���� ������ �ǵ�����
        yield return RotateZ(0);

        // ��������
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

        // �������� �Ⱥμ����
        if (getItem)
        {
            animName = $"GrapUnknown{num}";
        }
        else if (onTrans && num != 1) // ������ �ߴµ� 1�� �ƴ� ���
        {
            animName = $"Unknown{num}";
        }
        else if (!onTrans) // ������ ���Ѱ��
        {
            animName = "Player_Unknown";
        }

        if (animName != null) // ���� �ִϸ��̼� ó��
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
    public IEnumerator IELaserWire() // ������ ����
    {
        eventPlayer = true;

        if (onTrans)
        {
            // ������ �ߴµ� 3���� �ƴ� ���
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
            // ������ ���� ���
            if (getItem)
                yield return StartCoroutine(IEAnim("GrapUnknown4"));
            else
                yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // ���۰� ���� ���
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
    public IEnumerator IEDoor() // ��
    {
        eventPlayer = true;

        if (onDoor)
        {
            // Ż��!
            Debug.Log("Ż��");
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

            if (onLasetWire) // ������ ���� �ذ��� ���
            {
                onDoor = true;
                StartCoroutine(doorEvent.IEAnim());
            }
            else
            {
                // ����
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
