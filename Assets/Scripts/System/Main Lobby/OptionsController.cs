using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    public AudioMixer mainMixer;
    private GameObject _graphics;
    private GameObject _sound;
    private GameObject _controlls;
    private int _menuState = 0;
    private int _qualityIndex = 0;
    private string[] _qualityNames;
    private Resolution[] _resolutions;
    private List<string> _resNames = new List<string>();
    private int _currentResolutionIndex = 0;

    void Awake()
    {
        _graphics = GameObject.Find("Graphics");
        _sound = GameObject.Find("Sound");
        _controlls = GameObject.Find("Controlls");
        _qualityNames = QualitySettings.names;
    }

    void Start()
    {
        _graphics.SetActive(true);
        _sound.SetActive(false);
        _controlls.SetActive(false);
        QualitySettings.SetQualityLevel(_qualityIndex);
        _resolutions = Screen.resolutions;

        for(int i = 0; i < _resolutions.Length; i++)
        {
            string res = _resolutions[i].width + "X" + _resolutions[i].height;
            _resNames.Add(res);

            if(_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                _currentResolutionIndex = i;
            }
        }
    }

    void Update()
    {
        float _masterVolume;
        float _musicVolume;
        float _effectsVolume;
        mainMixer.GetFloat("MasterVolume", out _masterVolume);
        mainMixer.GetFloat("MusicVolume", out _musicVolume);
        mainMixer.GetFloat("EffectsVolume", out _effectsVolume);
        _sound.transform.Find("MasterVolumeSlider").Find("Persents").GetComponent<Text>().text = ((int)(100f + _masterVolume * 1.25f)).ToString();
        _sound.transform.Find("MusicVolumeSlider").Find("Persents").GetComponent<Text>().text = ((int)(100f + _musicVolume * 1.25f)).ToString();
        _sound.transform.Find("EffectsVolumeSlider").Find("Persents").GetComponent<Text>().text = ((int)(100f + _effectsVolume * 1.25f)).ToString();
        _graphics.transform.Find("PresetsChoose").Find("ChosedText").GetComponent<Text>().text = _qualityNames[_qualityIndex];
        _graphics.transform.Find("ResolutionChoose").Find("ChosedText").GetComponent<Text>().text = _resNames[_currentResolutionIndex];
        if (_qualityIndex == 0)
        {
            _graphics.transform.Find("PresetsChoose").Find("LeftButton").GetComponent<Button>().interactable = false;
            _graphics.transform.Find("PresetsChoose").Find("RightButton").GetComponent<Button>().interactable = true;
        } 
        else if (_qualityIndex == _qualityNames.Length - 1)
        {
            _graphics.transform.Find("PresetsChoose").Find("LeftButton").GetComponent<Button>().interactable = true;
            _graphics.transform.Find("PresetsChoose").Find("RightButton").GetComponent<Button>().interactable = false;
        } 
        else
        {
            _graphics.transform.Find("PresetsChoose").Find("LeftButton").GetComponent<Button>().interactable = true;
            _graphics.transform.Find("PresetsChoose").Find("RightButton").GetComponent<Button>().interactable = true;
        }

        if (_currentResolutionIndex == 0)
        {
            _graphics.transform.Find("ResolutionChoose").Find("LeftButton").GetComponent<Button>().interactable = false;
            _graphics.transform.Find("ResolutionChoose").Find("RightButton").GetComponent<Button>().interactable = true;
        }
        else if (_currentResolutionIndex == _resNames.Count - 1)
        {
            _graphics.transform.Find("ResolutionChoose").Find("LeftButton").GetComponent<Button>().interactable = true;
            _graphics.transform.Find("ResolutionChoose").Find("RightButton").GetComponent<Button>().interactable = false;
        }
        else
        {
            _graphics.transform.Find("ResolutionChoose").Find("LeftButton").GetComponent<Button>().interactable = true;
            _graphics.transform.Find("ResolutionChoose").Find("RightButton").GetComponent<Button>().interactable = true;
        }
    }

    public void Graphic()
    {
        if(_menuState != 0)
        {
            _controlls.SetActive(false);
            _sound.SetActive(false);
            _graphics.SetActive(true);
            _menuState = 0;
        }
        else
        {
            return;
        }
    }

    public void Sound()
    {
        if (_menuState != 1)
        {
            _controlls.SetActive(false);
            _graphics.SetActive(false);
            _sound.SetActive(true);
            _menuState = 1;
        }
        else
        {
            return;
        }
    }

    public void Controlls()
    {
        if (_menuState != 2)
        {
            _sound.SetActive(false);
            _graphics.SetActive(false);
            _controlls.SetActive(true);
            _menuState = 2;
        }
        else
        {
            return;
        }
    }

    public void SetMasterVolume(float value)
    {
        mainMixer.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        mainMixer.SetFloat("MusicVolume", value);
    }

    public void SetEffectVolume(float value)
    {
        mainMixer.SetFloat("EffectsVolume", value);
    }

    public void ResolutionChange(bool side)
    {
        if(side)
        {
            _currentResolutionIndex += 1;
            Screen.SetResolution(_resolutions[_currentResolutionIndex].width, _resolutions[_currentResolutionIndex].height, Screen.fullScreen);
        }
        else
        {
            _currentResolutionIndex -= 1;
            Screen.SetResolution(_resolutions[_currentResolutionIndex].width, _resolutions[_currentResolutionIndex].height, Screen.fullScreen);
        }
    }

    public void PresetChange(bool side)
    {
        if (side)
        {
            _qualityIndex += 1;
            QualitySettings.IncreaseLevel(false);
        }
        else
        {
            _qualityIndex -= 1;
            QualitySettings.DecreaseLevel(false);
        }
    }

    public void SetFullscreen(bool toggle)
    {
        Screen.fullScreen = toggle;
    }

    public void SetVSync(bool toggle)
    {
        if(toggle)
        {
            QualitySettings.vSyncCount = 2;
        } 
        else
        {
            QualitySettings.vSyncCount = 0;
        }

    }
}
