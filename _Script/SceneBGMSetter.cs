using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBGMSetter : MonoBehaviour
{
    [Header("이 씬에서 재생할 배경음악을 넣어주세요")]
    public AudioClip sceneMusic;

    [Header("이 씬에서 재생할 빗소리 넣어주세요")]
    public AudioClip RainMusic;

    void Start()
    {
        setSceneMusic();

    }

    void setSceneMusic()
    {
        // 1. 살아서 넘어온 (또는 이 씬에 있는) SoundControl을 찾습니다.
        GameObject soundObj = GameObject.Find("SoundControl");

        if (soundObj != null)
        {
            SoundHandler handler = soundObj.GetComponent<SoundHandler>();

            // 2. SoundHandler에게 "이 음악(sceneMusic)으로 바꿔 틀어줘!" 라고 명령합니다.
            if (handler != null && sceneMusic != null)
            {
                handler.ChangeBGM(sceneMusic);
            }
            if (handler != null && RainMusic != null)
            {
                handler.ChangeBGS(RainMusic);
            }
        }
    }

}
