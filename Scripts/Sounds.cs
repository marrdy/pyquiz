using UnityEngine.Audio;
using UnityEngine;
[System.Serializable]
public class Sounds
{
    public enum SoundType
    {
        Music,
        SFX
    }
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
    public SoundType type;
    public bool loop;
}
