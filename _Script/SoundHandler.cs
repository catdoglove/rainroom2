using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundHandler : MonoBehaviour
{

    public Slider BGM_sld, SE_sld, BGS_sld;
    public AudioSource BGM, BGS, BGS_2, SE, SE_2;
    float BGMVol_f, BGSVol_f, SEVol_f;

    public GameObject audio_obj;

    // 볼륨 변화를 감지하기 위해 이전 볼륨을 기억해둘 변수 추가
    private float lastSEVol = -1f;

    public static SoundHandler instance;
    void Awake()
    {
        // 2. 게임이 시작될 때 자기 자신을 instance에 등록해 둡니다.
        if (instance == null)
        {
            instance = this;
        }
    }
    public void SetMute(bool isMute)
    {
        if (BGM != null) BGM.mute = isMute;
        if (BGS != null) BGS.mute = isMute;
        if (BGS_2 != null) BGS_2.mute = isMute;
    }

    IEnumerator soundsound()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (audio_obj == null)
            {
                audio_obj = GameObject.Find("AudioSound");
                if (audio_obj != null)
                {
                    SE = audio_obj.GetComponent<AudioSource>();
                    SE_sld.value = PlayerPrefs.GetFloat("se", 1f);
                    SE.volume = SE_sld.value;
                    lastSEVol = SE_sld.value; // 찾았을 때의 볼륨도 기억
                }

            }// 슬라이더 값이 이전에 기억해둔 값과 "다를 때만" 적용!
            if (SE_sld != null && SE_sld.value != lastSEVol)
            {
                SESlider();
                lastSEVol = SE_sld.value; // 방금 바뀐 값을 다시 기억해둠
            }
        }
    }
    public void SESlider()
    {
        // 오브젝트가 없는 찰나의 순간에 에러가 나는 것을 방지하기 위해 null 체크 추가
        if (SE != null) SE.volume = SE_sld.value;
        if (SE_2 != null) SE_2.volume = SE_sld.value;
        PlayerPrefs.SetFloat("se", SE_sld.value);
        PlayerPrefs.Save(); // 이제 값이 변할 때만 실행되므로 안전하게 Save() 추가 가능!
    }

    void Start()
    {
        // 1. 저장된 볼륨 값 불러오기 및 초기화
        StartCoroutine(soundsound());
        OnLoadSound();

        // 2. Update() 대신 슬라이더의 값이 변할 때만 함수가 실행되도록 이벤트 연결
        if (BGM_sld != null) BGM_sld.onValueChanged.AddListener(SetBGMVolume);
        if (BGS_sld != null) BGS_sld.onValueChanged.AddListener(SetBGSVolume);
    }

    // 슬라이더 값이 변할 때(volume 매개변수) 자동으로 호출됩니다.
    public void SetBGMVolume(float volume)
    {
        if (BGM != null) BGM.volume = volume;
        PlayerPrefs.SetFloat("bgm", volume);
        PlayerPrefs.Save();
    }

    public void SetBGSVolume(float volume)
    {
        if (BGS != null) BGS.volume = volume;
        PlayerPrefs.SetFloat("bgs", volume);
        PlayerPrefs.Save();


    }


    public void OnLoadSound()
    {// 저장된 설정값 불러오기 (값이 없으면 기본값 1f)
        float bgmVol = PlayerPrefs.GetFloat("bgm", 1f);
        float bgsVol = PlayerPrefs.GetFloat("bgs", 1f);
        float seVol = PlayerPrefs.GetFloat("se", 1f);

        // [핵심] 1. 실제 오디오 소스의 볼륨을 저장된 값으로 변경
        if (BGM != null) BGM.volume = bgmVol;
        if (BGS != null) BGS.volume = bgsVol;
        if (SE != null) SE.volume = seVol;
        if (SE_2 != null) SE_2.volume = seVol;

        // [핵심] 2. 슬라이더 UI의 위치도 저장된 값으로 변경
        if (BGM_sld != null) BGM_sld.value = bgmVol;
        if (BGS_sld != null) BGS_sld.value = bgsVol;
        if (SE_sld != null) SE_sld.value = seVol;
    }

    public void ChangeBGM(AudioClip newClip)
    {
        if (BGM != null)
        {
            BGM.clip = newClip;
            BGM.time = 0f; // 시간을 0초로 초기화
            BGM.Play();    // 처음부터 다시 재생!
        }
    }

    public void ChangeBGS(AudioClip newClip)
    {
        if (BGS != null)
        {
            // 핵심 방어 코드: 만약 이번 씬에서 틀어야 할 음악이 이전 씬과 "똑같은" 음악이라면?
            // 굳이 처음부터 다시 재생하지 않고 그대로 이어서 틀도록 둡니다.
            if (BGS.clip == newClip)
                return;

            // 새로운 음악으로 교체하고 재생합니다.
            BGS.clip = newClip;
            BGS.Play();
        }
    }
}
