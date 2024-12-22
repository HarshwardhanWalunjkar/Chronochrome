using UnityEngine.Audio;
using System;
using UnityEngine;
using TMPro;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] PlayerSounds;
    public Sound[] BackgroundSounds;
    private string[] bgNames;
    public static AudioManager Instance; 
    private bool bgStreaming = false;
    private int index = 0;
    public static Sound bgSound;
    private Animator audioUI;
    public TextMeshProUGUI audioname;
    private bool mute = false;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach(Sound s in PlayerSounds) { 
            s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach (Sound s in BackgroundSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
       bgNames = new string[] {"Beggin", "Honey(Are you coming?)", "Baby Said", "SUPERMODEL", "GOSSIP"};
       audioUI = GetComponentInChildren<Animator>();
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(PlayerSounds, sound => sound.name == name);
        s.source.Play();
    }
    public void PlayBg(string name)
    {
        Debug.Log("Setting track in variable bgSound and playing");
        bgSound = Array.Find(BackgroundSounds, sound => sound.name == name);
        bgSound.source.Play();
    }
    public void Stop(string name) {
        Sound s = Array.Find(PlayerSounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void MuteBg()
    {
        mute = !mute;
        if (mute)
        {
            Debug.Log("set vol to mute");
            bgSound.source.mute = true;
        }
        else
        {
            Debug.Log("set vol to unmute");
            bgSound.source.mute = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!bgStreaming)
        {
            Debug.Log("Playing Track");
            PlayBg(bgNames[index]);
            index = (index + 1) % 5;
            bgStreaming = true;
        }
        if (!bgSound.source.isPlaying)
        {
            audioname.text = "Maneskin -- " + bgNames[index];
            
            Debug.Log("Changing Bg Music");
            bgStreaming = false;
            audioUI.SetTrigger("AudioChange");
        }
    }
}
