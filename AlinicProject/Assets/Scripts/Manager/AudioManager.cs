using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    List<AudioClip> anyAudioClipList = new List<AudioClip>();   // 모든 오디오 클립 리소스 리스트

    private void Awake()
    {
        instance = this;
        // 모든 오디오 클립 호출
        AudioClip[] audioClip = Resources.LoadAll<AudioClip>("Charactor/Weapons/Sounds");
        foreach (AudioClip clip in audioClip)
        {
            anyAudioClipList.Add(clip);
        }

    }
    // 발사 오디오 반환함수
    public AudioClip GetFireClip(string gunName)
    {
        foreach (var clip in anyAudioClipList)
        {
            if (clip.name.Equals(gunName + "_fire", System.StringComparison.OrdinalIgnoreCase))
                return clip;
        }
        return null;
    }
    // 장전 오디오 반환함수
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
