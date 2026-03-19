using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string SOUND_KEY = "Sound";
    
    public float MasterVolume => AudioListener.volume;
    
    public void Init()
    {
        AudioListener.volume = PlayerPrefs.GetFloat(SOUND_KEY, 1.0f);
    }

    public void SetMasterVolume(float argValue)
    {
        AudioListener.volume = argValue;
        PlayerPrefs.SetFloat(SOUND_KEY, argValue);
    }
}
