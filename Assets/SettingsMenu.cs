using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    private float oldVolume, oldResolution, currentVolume, currentResolution;
    private bool oldFullscreen, currentFullscreen;


    private float defaultVolume = 0.5f, defaultResolution;
    private bool defaultFullscreen = true;
    private void Start()
    {
        LoadSettings();
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        foreach (Resolution resolution in resolutions) 
        {
            options.Add(resolution.width + " x " + resolution.height);
        }
        resolutionDropdown.AddOptions(options);
    }
    public void SetVolume(float volume)
    {
        currentVolume = volume;
        audioMixer.SetFloat("volume", volume);
        //UnityEngine.Debug.Log($"Volume set to {volume}");
    }

    public void SetWindowMode(int mode)
    {

        Screen.fullScreenMode = (FullScreenMode)mode;
        //UnityEngine.Debug.Log($"Window mode set to {mode}");
    }
    public void SetFullscreen(bool isFullscreen)
    {
        currentFullscreen = isFullscreen;
        //oldFullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    public void SaveSettings()
    {
        Debug.Log("Saving settings");
        PlayerPrefs.SetFloat("volume", currentVolume);
        PlayerPrefs.SetFloat("fullscreen", currentFullscreen ? 1 : 0);

        PlayerPrefs.Save();
        Debug.Log("Settings saved");

    }
    public void LoadSettings()
    {
        Debug.Log("Loading settings");
        currentVolume = PlayerPrefs.GetFloat("volume", defaultVolume);
        audioMixer.SetFloat("volume", currentVolume);
        int hi = defaultFullscreen ? 1 : 0;
        //currentResolution = PlayerPrefs.GetFloat("resolution", defaultResolution);
        currentFullscreen = PlayerPrefs.GetFloat("fullscreen", defaultFullscreen ? 1 : 0) == 1;
        Debug.Log("Settings loaded");

    }
}
