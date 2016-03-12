using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AudioManager : MonoBehaviour
{
    public static AudioManager ins;

//    public AudioClip sfxButtonClick;
//    public AudioClip sfxMoney;

    public enum Type { Music, Audio }
    public static bool isPlayingBackgroundMusic;
    public static bool isPlayButtonSound = true;



  public AudioClip sfxLiftOpen;
    public void Awake()
    {
        if (ins == null) { ins = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); }
    }
    // over loads
    public static void CreatePlayAudioObject(Type type, AudioClip aClip)
    {
        CreatePlayAudioObject(type, aClip, 1.0f);
    }
    public static void CreatePlayAudioObject(Type type, AudioClip aClip, float vol)
    {
        CreatePlayAudioObject(type, aClip, vol, "AudioPlayObject");
    }
    public static void CreatePlayAudioObject(AudioClip aClip)
    {
        CreatePlayAudioObject(Type.Audio, aClip, 1.0f);
    }

    // creates a dynamic Audio object to position and play in the world
    public static void CreatePlayAudioObject(Type type, AudioClip aClip, float vol, string objName)
    {
        // instance a new gameobject
        GameObject apObject = new GameObject(type + "_" + objName);
        // position the object in the world
        apObject.transform.position = Vector3.zero;

        // add an AudioSource component
        apObject.AddComponent<AudioSource>();
        // return this script for use
        AudioSource apAudio = apObject.GetComponent<AudioSource>();
        // initialize some AudioSource fields
        apAudio.playOnAwake = false;
        apAudio.rolloffMode = AudioRolloffMode.Linear;

        apAudio.loop = false;
        apAudio.clip = aClip;
        apAudio.volume = vol;

        // play the clip
        apAudio.Play();
        //apAudio.mute = IsMutedAudio(type);
        // destroy this object after clip length
        Destroy(apObject, aClip.length);
    }

    //static bool IsMutedAudio(Type type)
    //{
    //  //  string key = type == Type.Music ? SettingsScript.musicKey : SettingsScript.soundKey;
    //   // return !Utilities.PlayerPrefs.GetBool(key, true);
    //}

    void Update()
    {
        //// todo use UniRx to subscribe on button input
        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (isPlayButtonSound)
        //    {
        //        if (EventSystem.current.currentSelectedGameObject != null)
        //        {
        //            var b = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        //            if (b != null)
        //            {
        //                CreatePlayAudioObject(BaseManager.GetInstance().sfxButtonClick);
        //            }

        //            var t = EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>();
        //            if (t != null)
        //            {
        //                CreatePlayAudioObject(BaseManager.GetInstance().sfxButtonClick);
        //            }
        //        }
        //    }
        //}
    }

    // fade our AudioSource object based on speed (> 0 fades volume up, < 0 fades volume out, == 0 assumes the sound is playing and just destroys it)
    public static IEnumerator FadeAudioObject(GameObject aObject, float fadeSpeed)
    {
        Animation apAnim = aObject.GetComponent<Animation>();
        AudioSource aSource = aObject.GetComponent<AudioSource>();

        // we are not a fade audio object
        if (apAnim == null)
        {
            // we simply destroy the object and return
            if (fadeSpeed <= 0)
            {
                Destroy(aObject);
            }
            // we are a psitive playing sound, so just play it
            if (fadeSpeed > 0 && aSource != null)
            {
                aSource.Play();
            }
            yield return null;
        }

        // animation clip is default to fade out (1 to 0), these will look reveresed but they are correct
        if (fadeSpeed < 0)
        {
            apAnim[apAnim.clip.name].time = apAnim[apAnim.clip.name].length;
        }
        else
        {
            apAnim[apAnim.clip.name].time = 0;
        }
        // set our speed
        apAnim[apAnim.clip.name].speed = fadeSpeed;

        // play the audio
        if (aSource.isPlaying == false)
        {
            aSource.Play();
        }
        // play the fade
        apAnim.Play();

        // yield the length of the clip
        if (fadeSpeed < 0)
        {
            while (apAnim.isPlaying) { yield return new WaitForEndOfFrame(); }
            Destroy(aObject);
        }
    }

    // over loads
    public static GameObject CreateGetFadeAudioObject(Type type, AudioClip aClip)
    {
        return CreateGetFadeAudioObject(type, aClip, true);
    }
    public static GameObject CreateGetFadeAudioObject(Type type, AudioClip aClip, bool dLoop)
    {
        return CreateGetFadeAudioObject(type, aClip, dLoop, null);
    }
    public static GameObject CreateGetFadeAudioObject(Type type, AudioClip aClip, bool dLoop, AnimationClip fadeClip)
    {
        return CreateGetFadeAudioObject(type, aClip, dLoop, fadeClip, "ReturnedAudioPlayObject");
    }

    // creates/returns a dynamic Audio object to position and plays in the world with loop
    public static GameObject CreateGetFadeAudioObject(Type type, AudioClip aClip, bool dLoop, AnimationClip fadeClip, string objName)
    {
        // instance a new gameobject
        GameObject apObject = new GameObject(type + "_" + objName);
        // position the object in the world
        apObject.transform.position = Vector3.zero;
        // add our DontDestroyOnLoad
        DontDestroyOnLoad(apObject);
        // add an AudioSource component
        apObject.AddComponent<AudioSource>();
        // return this script for use
        AudioSource apAudio = apObject.GetComponent<AudioSource>();
        // initialize some AudioSource fields
        apAudio.playOnAwake = false;
        apAudio.rolloffMode = AudioRolloffMode.Linear;

        apAudio.loop = dLoop;
        apAudio.clip = aClip;
        apAudio.volume = 1.0f; // default
       // apAudio.mute = IsMutedAudio(type);
        if (fadeClip != null)
        {
            Animation apAnim = apObject.AddComponent<Animation>();
            apAnim.AddClip(fadeClip, fadeClip.name);
            apAnim.clip = fadeClip;
            apAnim.playAutomatically = false;
        }
        // return our AudioObject
        return apObject;
    }

    // Setting
    public static void SetAudioEnable(bool enable)
    {
        if (enable)
        {
            EnableAllAudio();
        }
        else
        {
            DisableAllAudio();
        }
    }

    public static void SetMusicEnable(bool enable)
    {
        if (enable)
        {
            EnableAllMusic();
        }
        else
        {
            DisableAllMusic();
        }
    }

    public static List<float> audioVolumes;
    public static List<float> musicVolumes;

    public static void EnableAllAudio()
    {
        int cnt = 0;

        foreach (var at in GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[])
        {
            if (at.gameObject.name.StartsWith(Type.Audio.ToString() + "_"))
            {
                at.mute = false;
                cnt++;
            }
        }
    }

    public static void DisableAllAudio()
    {
        audioVolumes = new List<float>();

        foreach (var at in GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[])
        {
            if (at.gameObject.name.StartsWith(Type.Audio.ToString() + "_"))
            {
                audioVolumes.Add(at.volume);

                at.mute = true;
            }
        }
    }

    public static void EnableAllMusic()
    {
        int cnt = 0;

        foreach (var at in GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[])
        {
            if (at.gameObject.name.StartsWith(Type.Music.ToString() + "_"))
            {
                at.mute = false;
                cnt++;
            }
        }
    }

    public static void DisableAllMusic()
    {
        musicVolumes = new List<float>();

        foreach (var at in GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[])
        {
            if (at.gameObject.name.StartsWith(Type.Music.ToString() + "_"))
            {
                musicVolumes.Add(at.volume);

                at.mute = true;
            }
        }

    }
}
