using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEvent_2 : PlayerEvent
{
    // yield return StartCoroutine(IEMove(new Vector2(-1f, -2.5f), 1f));

    private bool getSeed = false;
    public IEnumerator IESeed() // ����
    {
        eventPlayer = true;

        // �������� ��� �ִ� ���
        if (getItem)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // ������ ������ ���
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

        // �ݴ� �ִϸ��̼�
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
    public IEnumerator IEMouseEat() // �� �����ֱ�
    {
        eventPlayer = true;

        // ������ ����µ� ������ �ƴ� ���
        if (!getSeed)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }

        // ������ ������ ���
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

        // ���� �ֱ�
        getItem = false;
        getSeed = false;

        // ������ ������ �ִϸ��̼� ����
        yield return new WaitForSeconds(1f);

        // ���� ���� ������
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
    [SerializeField] private GameObject transMachine; // ���� ���
    [SerializeField] public int num { get; private set; } = 4; // ���� ��ȣ
    public IEnumerator IETrasnMachine(int num) // ����1 ~ 3
    {
        eventPlayer = true;

        // 4���� ������ ���
        if (num == 4)
        {
            StartCoroutine(IETrasnMachineReturn());

            yield break;
        }
        // ���� ���� ���, �̹� ���� �� ���, ������ �� ���
        if (!onMouseEat || onTrans || getItem)
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

        // �ش� ĳ���� ����
        onTrans = true;
        this.num = num;
        string transNum = "isTrans" + this.num;
        player.anim.SetBool(transNum, true);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(IEObjMove(transMachine, new Vector2(0, 7.3f), 1f));
        
        eventPlayer = false;
    }

    [SerializeField] private GameObject[] transScreen4; // ���� ȭ���
    public IEnumerator IETrasnMachineReturn() // ���� ����
    {
        eventPlayer = true;

        // ���� ���� ���, ������ ���� ��� ������ �� ���
        if (!onMouseEat || !onTrans || getItem)
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
    public IEnumerator IELaserPlate() // ������ ��
    {
        eventPlayer = true;

        // ������ �ߴµ� 2���� �ƴ� ���
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

        // ������ �� �ı� �ִϸ��̼�
        // �ִϸ��̼ǿ� ���� �ı� �̺�Ʈ �ֱ�
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

    private bool onLasetWire = false;
    public IEnumerator IELaserWire() // ������ ����
    {
        eventPlayer = true;

        // ������ �ߴµ� 3���� �ƴ� ���
        if (!(onTrans && num == 3))
        {
            // �մ�� ����
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }




        eventPlayer = false;
    }
    public IEnumerator IEDoor() // ��
    {
        // ������ �� ���
        if (onTrans)
        {
            yield return StartCoroutine(IEAnim("Player_Unknown"));

            yield return new WaitForSeconds(0.5f);
            eventPlayer = false;

            yield break;
        }
        else if (!onLasetWire) // ������ ���� �ذ���� ���
        {
            // �ɾ�� ����?
        }

    }
}
