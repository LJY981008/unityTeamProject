using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/**
 * ����� ���ҽ� ȣ�� �Ŵ���
 * 
 */
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    List<AudioClip> anyAudioClipList = new List<AudioClip>();   // ��� ����� Ŭ�� ���ҽ� ����Ʈ

    private void Awake()
    {
        instance = this;
        LoadAudio();
    }

    // ��� ����� Ŭ�� ȣ��
    public void LoadAudio()
    {
        AudioClip[] audioClip = Resources.LoadAll<AudioClip>("Charactor/Weapons/Sounds");
        foreach (AudioClip clip in audioClip)
        {
            anyAudioClipList.Add(clip);
        }
    }
    // �ݹ� ���� ���� �Լ�
    public AudioClip GetFireClip(string gunName)
    {
        foreach (var clip in anyAudioClipList)
        {
            if (clip.name.Equals(gunName + "_fire", System.StringComparison.OrdinalIgnoreCase))
                return clip;
        }
        return null;
    }
    // ���� ���� ���� �Լ�
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
    // ���� �ݹ� ���� ��ȯ �Լ�
    // ���� �����־ ��� x
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
