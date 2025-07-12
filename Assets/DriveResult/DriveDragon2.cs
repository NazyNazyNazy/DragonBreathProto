using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DriveDragon2 : MonoBehaviour
{
    public GameObject Dragon;
    public GameObject PopUp;
    public static int Level = 1;
    public float DragonHeight;
    private float MovePitch;
    private float DragonHightIncrement = 0.001f;

    // 🎵 ファンファーレ用
    public AudioSource audioSource;
    public AudioClip levelUpFanfareClip;

    void Start()
    {
        DragonHeight = Dragon.transform.position.y;
    }

    void Update()
    {
        // LevelUp();
    }

    public void LevelUp()
    {
        Level += 1;
        DragonHeight += 1.8f;

        Debug.Log("Dragon Hight" + DragonHeight.ToString() + ", Level:" + Level.ToString());

        transform.DOMoveY(DragonHeight, 1);

        // 🎵 ファンファーレを再生（音声ファイル名: ドラアップ）
        if (audioSource != null && levelUpFanfareClip != null)
        {
            audioSource.PlayOneShot(levelUpFanfareClip);
        }

        PopUp.SetActive(true);
        PopUp.transform.localScale = Vector3.one * 0.2f;
        PopUp.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBack, 5f);
    }
}