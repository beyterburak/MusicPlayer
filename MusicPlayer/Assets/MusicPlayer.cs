using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private Text songNameText;
    [SerializeField] private Text durationText;
    [SerializeField] private Text lengthText;
    [SerializeField] private Text playPauseText;
    [SerializeField] private Slider durationSlider;

    [SerializeField] List<AudioClip> musicList = new List<AudioClip>();
    int currentSong = 0; 

    private AudioSource musicSource;

    int lengthMinute;
    int lengthSeconds;

    int durationMinute;
    int durationSeconds;
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        PlayMusic();
    }

    void Update()
    {
        if (musicSource.time == musicList[currentSong].length)
        {
            NextMusic();
        }
        CalculateDuration();
        CalculateSlider();
    }

    void CalculateSlider()
    {
        durationSlider.value = musicSource.time;
    }

    void CalculateDuration()
    {
        durationMinute = (int)musicSource.time / 60;
        durationSeconds = (int)musicSource.time % 60;

        if (durationSeconds < 10)
        {
            durationText.text = durationMinute.ToString() + ":0" + durationSeconds.ToString();
        }
        else
        {
            durationText.text = durationMinute.ToString() + ":" + durationSeconds.ToString();
        }
    }

    void CalculateLength()
    {
        lengthMinute = (int)musicList[currentSong].length / 60;
        lengthSeconds = (int)musicList[currentSong].length % 60;
        if (lengthSeconds < 10)
        {
            lengthText.text = lengthMinute.ToString() + ":0" + lengthSeconds.ToString();
        }
        else
        {
            lengthText.text = lengthMinute.ToString() + ":" + lengthSeconds.ToString();
        }
        durationSlider.maxValue = (int)musicList[currentSong].length;
    }

    void UpdateMusicText()
    {
        songNameText.text = musicList[currentSong].name;
    }

    public void PlayPause()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            playPauseText.text = "l>";
        }
        else
        {
            musicSource.Play();
            playPauseText.text = "ll"; 
        }
    }

    public void PlayMusic()
    {
        musicSource.clip = musicList[currentSong];
        musicSource.Play();
        UpdateMusicText();
        CalculateLength();
    }
    
    public void PreviousMusic()
    {
        if (currentSong == 0)
        { 
            currentSong = musicList.Count - 1; 
        }
        else
        {
            currentSong--;
        }
        PlayMusic();
    }

    public void NextMusic()
    {
        if (currentSong == musicList.Count - 1)
        {
            currentSong = 0;
        }
        else
        {
            currentSong++;
        }
        PlayMusic();
    }
}
