using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // 싱글톤 패턴

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("SFX")]
    public AudioClip swordSound; // 칼날 휘두르는 소리
    public AudioClip bombSound;  // 폭탄 터지는 소리
    public float sfxVolume;
    AudioSource sfxPlayer;

    void Awake()
    {
        instance = this;
        Init();
        PlayBgm(true); // 배경음을 초기화하면서 즉시 재생
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false; // 초기에는 자동 재생하지 않음
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayer = sfxObject.AddComponent<AudioSource>();
        sfxPlayer.playOnAwake = false;
        sfxPlayer.volume = sfxVolume;
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay) {
            if (!bgmPlayer.isPlaying) {
                bgmPlayer.Play(); // 배경음이 재생 중이지 않으면 재생
            }
        } else {
            if (bgmPlayer.isPlaying) {
                bgmPlayer.Stop(); // 배경음이 재생 중이면 정지
            }
        }
    }

    public void PlaySfx(string sfx)
    {
        switch (sfx)
        {
            case "SwordSound":
                sfxPlayer.clip = swordSound;
                break;
            case "BombSound":
                sfxPlayer.clip = bombSound;
                break;
        }
        sfxPlayer.Play();
    }
}
