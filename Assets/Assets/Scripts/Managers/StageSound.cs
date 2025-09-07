using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSound : MonoBehaviour
{
    [SerializeField] private AudioClip stageBGM;
    void Awake()
    {
        stageBGM = Resources.Load<AudioClip>("Stage_BGM");

    }
    private void OnEnable()
    {
        AudioManager.Instance.Scenery(stageBGM);
    }

    private void OnDisable()
    {
        AudioManager.Instance.StopSound();

    }
}
