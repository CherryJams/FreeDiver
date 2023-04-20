using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource audioSource;
    private Transform audioType;

    public AudioSource GetAudioSource(string soundType, string soundName)
    {
        audioType = GameObject.FindGameObjectWithTag(soundType).transform;
        foreach (Transform child in audioType)
        {
            if(child.name == soundName)
            {
                return audioSource = child.GetComponent<AudioSource>();
            }
        }

        return null;
    }
}