using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/**
 * 오디오 리소스 호출 매니저
 * 
 */
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    List<AudioClip> anyAudioClipList = new List<AudioClip>();   // 모든 오디오 클립 리소스 리스트

    private void Awake()
    {
        instance = this;
        LoadAudio();
    }

    // 모든 오디오 클립 호출
    public void LoadAudio()
    {
        AudioClip[] audioClip = Resources.LoadAll<AudioClip>("Charactor/Weapons/Sounds");
        foreach (AudioClip clip in audioClip)
        {
            anyAudioClipList.Add(clip);
        }
    }
    // 격발 사운드 리턴 함수
    public AudioClip GetFireClip(string gunName)
    {
        foreach (var clip in anyAudioClipList)
        {
            if (clip.name.Equals(gunName + "_fire", System.StringComparison.OrdinalIgnoreCase))
                return clip;
        }
        return null;
    }
    // 장전 사운드 리턴 함수
    public AudioClip GetReloadClip(string gunName)
    {
        foreach(var clip in anyAudioClipList)
        {
            if (clip.name.Equals(gunName + "_reload_mix", System.StringComparison.OrdinalIgnoreCase))
            {
                return clip;
            }
        }
        return null;
    }
    // 빈총 격발 사운드 반환 함수
    // 사운드 버그있어서 사용 x
    public AudioClip GetDryClip(string gunName)
    {
        foreach(var clip in anyAudioClipList)
        {
            if (clip.name.Equals(gunName + "_dryfire", System.StringComparison.OrdinalIgnoreCase))
            {
                return clip;
            }
        }
        return null;
    }
}
