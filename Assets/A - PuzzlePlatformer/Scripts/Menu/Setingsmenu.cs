using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Setingsmenu : MonoBehaviour
{
    // controls and genrates resolotuions for resolutions dropwdown
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionsIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height )
            {
                currentResolutionsIndex = i;
            }

        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionsIndex;
        resolutionDropdown.RefreshShownValue();

    }
    // makes above resoltions actully set resolution

    public void SetResolution()
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen);
    }

    // contols volume from main mixer
    public AudioMixer audioMixer;
    private int resolutionIndex;

    public void SetVolmue(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    // controls quality of game from qulity drop down
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    //controls full screen from toggle
    public void SetFullsceen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


}
