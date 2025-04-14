using FMODUnity;
using FMOD.Studio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayOneShot(string eventPath)
    {
        RuntimeManager.PlayOneShot(eventPath);
    }

    public void PlayOneShotWithParam(string eventPath, string paramName, float paramValue)
    {
        var instance = RuntimeManager.CreateInstance(eventPath);
        instance.setParameterByName(paramName, paramValue);
        instance.start();
        instance.release();
    }
}