using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoSingleton<UI_Manager>
{
    private int _highlightedUpgrade;
    public bool mainMenu;
    private float timeSurvived;
    public enum UI_State
    {
        None,
        Paused,
        Upgrading,
        Options,
        Death
    }

    [SerializeField] private UI_State currentUIState;

    [Header("References")] 
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private UpgradePanel[] UI_UpgradePanels;
    [SerializeField] private RectTransform healthFill, xpFill;
    [SerializeField] private TextMeshProUGUI healthText, xpText;
    [SerializeField] private TextMeshProUGUI timeSurvivedText;
    [SerializeField] private TextMeshProUGUI upgradeDescription;
    [SerializeField] private Sprite[] backGroundSprites;
    
    private Player _player;

    [Header("options")] 
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider effectsVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Toggle screenshakeToggle;
    
    private void Start()
    {
        if(GameManager.Instance != null)
            _player = GameManager.Instance.playerObject.GetComponent<Player>();

        var MasterVolumeSaved = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        var EffectsVolumeSaved = PlayerPrefs.GetFloat("EffectsVolume", 0.5f);
        var MusicVolumeSaved = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        var ScreenshakeToggleSaved = PlayerPrefs.GetInt("ScreenshakeToggle", 1) == 1? true:false;

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(MasterVolumeSaved) * 20);
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(EffectsVolumeSaved) * 20);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(MusicVolumeSaved) * 20);

        masterVolume.value = MasterVolumeSaved;
        effectsVolume.value = EffectsVolumeSaved;
        musicVolume.value = MusicVolumeSaved;

        screenshakeToggle.isOn = ScreenshakeToggleSaved;
    }

    public void SetEffectsVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("EffectsVolume",newVolume);
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(newVolume) * 20);

    }
    
    public void SetMusicVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume",newVolume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(newVolume) * 20);
    }
    
    public void SetMasterVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("MasterVolume",newVolume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(newVolume) * 20);

    }

    public void SetToggle(bool newToggle)
    {
        PlayerPrefs.SetInt("ScreenshakeToggle",newToggle?1:0);
    }

    public void FinishOptions()
    {
        changeUIState(UI_State.Paused);
    }

    public void OpenOptions()
    {
        changeUIState(UI_State.Options);
    }

    public void Resume()
    {
        changeUIState(UI_State.None);
    }

    public void ChangeScene(int newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    private void Update()
    {
        switch (currentUIState)
        {
            case UI_State.None:
                pausePanel.SetActive(false);
                upgradePanel.SetActive(false);
                OptionsPanel.SetActive(false);
                DeathScreen.SetActive(false);
                Time.timeScale = 1f;
                break;
            case UI_State.Paused:
                pausePanel.SetActive(true);
                upgradePanel.SetActive(false);
                OptionsPanel.SetActive(false);
                DeathScreen.SetActive(false);

                Time.timeScale = 0f;
                break;
            case UI_State.Upgrading:
                pausePanel.SetActive(false);
                upgradePanel.SetActive(true);
                OptionsPanel.SetActive(false);
                DeathScreen.SetActive(false);

                Time.timeScale = 0f;
                break;
            case UI_State.Options:
                pausePanel.SetActive(false);
                upgradePanel.SetActive(false);
                OptionsPanel.SetActive(true);
                DeathScreen.SetActive(false);

                Time.timeScale = 0f;
                break;
            case UI_State.Death:
                pausePanel.SetActive(false);
                upgradePanel.SetActive(false);
                OptionsPanel.SetActive(false);
                DeathScreen.SetActive(true);

                Time.timeScale = 0f;
                break;
            default:
                Debug.LogError("This shouldn't happen, Enum out of range");
                throw new ArgumentOutOfRangeException();
        }

        if(mainMenu)
            return;;
        healthFill.sizeDelta = new Vector2(_player.GetHealthPercentage() * 500, 30);
        xpFill.sizeDelta = new Vector2(GameManager.Instance.GetXPPercentage() * 500, 30);

        healthText.text = "HP: " + Mathf.RoundToInt(_player.GetHealthPercentage() * 100) + "%";
        xpText.text = "XP: " + Mathf.RoundToInt(GameManager.Instance.GetXPPercentage() * 100) + "%";

        timeSurvived += Time.deltaTime;
        timeSurvivedText.text = "Time Survived: " + Mathf.CeilToInt(timeSurvived);

    }

    public void SetUpgrade(int ID, Sprite newImage, string newText, int newLevel, string description)
    {
        UI_UpgradePanels[ID].upgradeImage.sprite = newImage;
        UI_UpgradePanels[ID].upgradeText.text = newText;
        UI_UpgradePanels[ID].upgradeBar.fillAmount = 0.1f * newLevel - 0.05f + (newLevel == 10 ? 0.05f : 0f);
        UI_UpgradePanels[ID].upgradeBackground.sprite = backGroundSprites[newLevel - 1];
        UI_UpgradePanels[ID].description = description;
        
        
        switch (newLevel)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
        }
    }

    public void HighlightUpgrade(int _ID)
    {
        upgradeDescription.text = UI_UpgradePanels[_ID].description;
        _highlightedUpgrade = _ID;
    }

    public void SelectUpgrade()
    {
        UpgradeManager.Instance.DoUpgrade(_highlightedUpgrade);
        upgradeDescription.text = "Select Upgrade";
    }

    public void changeUIState(UI_State newState)
    {
        currentUIState = newState;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if (currentUIState != UI_State.Paused)
        {
            currentUIState = UI_State.Paused;
        }
        else
        {

            if (UpgradeManager.Instance.isUpgrading)
                currentUIState = UI_State.Upgrading;
            else
            {
                currentUIState = UI_State.None;
            }
        }
    }
}

[System.Serializable]
public struct UpgradePanel
{
    public Image upgradeImage;
    public Image upgradeBackground;
    public TextMeshProUGUI upgradeText;
    public Image upgradeBar;
    public string description;
}
