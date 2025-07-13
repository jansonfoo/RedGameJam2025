using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource BGMusicMenu;
    public AudioSource BGMusicOpening;
    public AudioSource BGMusicGame;
    public AudioSource uiSfxSource;

    public void ToggleBGMusicMenu()
    {
        if (!BGMusicMenu.isPlaying)
            BGMusicMenu.Play();
        else
        {
            BGMusicMenu.Stop();
        }
    }

    public void ToggleBGMusicOpening()
    {
        if (!BGMusicOpening.isPlaying)
            BGMusicOpening.Play();
        else
        {
            BGMusicOpening.Stop();
        }
    }

    public void ToggleBGMusicGame()
    {
        if (!BGMusicGame.isPlaying)
            BGMusicGame.Play();
        else
        {
            BGMusicGame.Stop();
        }
    }

    public void PlaySoundButtonClick(AudioClip SoundClick)
    {
        uiSfxSource.PlayOneShot(SoundClick);
    }

    public void PlaySoundGameOver(AudioClip SoundGameOver)
    {
        uiSfxSource.PlayOneShot(SoundGameOver);
    }

    public void PlaySoundPlacedIncorrect(AudioClip SoundPlacedIncorrect)
    {
        uiSfxSource.PlayOneShot(SoundPlacedIncorrect);
    }

    public void PlaySoundPlacedCorrectSort(AudioClip SoundPlacedCorrectSort)
    {
        uiSfxSource.PlayOneShot(SoundPlacedCorrectSort);
    }

    public void PlaySoundPlacedCorrectLuggage(AudioClip SoundPlacedCorrectLuggage)
    {
        uiSfxSource.PlayOneShot(SoundPlacedCorrectLuggage);
    }

    public void PlaySoundPageFlip(AudioClip SoundFlip)
    {
        uiSfxSource.PlayOneShot(SoundFlip);
    }

    public void PlaySoundPageRip(AudioClip SoundRip)
    {
        uiSfxSource.PlayOneShot(SoundRip);
    }

    public void PlaySoundWinPencil(AudioClip SoundWin)
    {
        uiSfxSource.PlayOneShot(SoundWin);
    }

    public void PlaySoundWinPlane(AudioClip SoundPlane)
    {
        uiSfxSource.PlayOneShot(SoundPlane);
    }

}
