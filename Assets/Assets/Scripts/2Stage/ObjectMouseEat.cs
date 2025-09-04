using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMouseEat : MonoBehaviour
{
    [SerializeField] private GameObject[] eat;
    [SerializeField] private GameObject wheel;
    [SerializeField] private GameObject mouse;
    [SerializeField] private GameObject seed;

    public IEnumerator IEOnMouseEat()
    {
        eat[0].SetActive(false);
        eat[1].SetActive(true);

        yield return StartCoroutine(DropRoutine());
        seed.SetActive(false);

        mouse.GetComponent<Animator>().SetBool("OnAnim", true);

        eat[0].SetActive(true);
        eat[1].SetActive(false);
    }

    private float dropSpeed = 5f;
    private IEnumerator DropRoutine()
    {
        Vector3 startPos = seed.transform.position;
        Vector3 endPos = startPos + Vector3.down * 4f;

        while (seed.transform.position.y > endPos.y)
        {
            seed.transform.position += Vector3.down * dropSpeed * Time.deltaTime;
            yield return null;
        }

        // 정확히 목표 위치에 고정
        // seed.transform.position = endPos;
    }
    public void WheelAnim()
    {
        wheel.GetComponent<Animator>().SetBool("OnAnim", true);
    }
}
