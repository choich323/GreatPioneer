using System;
using UnityEngine;

public enum Language
{
    English = 0,
    Korean,
}

public class LanguageManager : MonoBehaviour
{
    private const string LANG_KEY = "Language";
    
    private Language _currentLanguage = Language.English;
    
    public Language CurrentLanguage => _currentLanguage;
    
    public void Init()
    {
        LoadLanguage();
    }

    void LoadLanguage()
    {
        _currentLanguage = (Language)PlayerPrefs.GetInt(LANG_KEY, 0);
    }
    
    public void SetLanguage(int argLanguageIndex)
    {
        var prevLang = _currentLanguage;
        PlayerPrefs.SetInt(LANG_KEY, argLanguageIndex);
        PlayerPrefs.Save();
        _currentLanguage = (Language)argLanguageIndex;
        Debug.Log($"Language Changed. {prevLang} -> {_currentLanguage}");
    }
}
