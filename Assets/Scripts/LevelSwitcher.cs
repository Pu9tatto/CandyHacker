using System.Runtime.InteropServices;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private TextView _textView;
    [SerializeField] private int _maxPhase = 10;

    [DllImport("__Internal")] private static extern void SetToLeaderboard(int value);

    private const string _levelKey = "currentLevel";

    private int _level=0;

    private void Start()
    {
        _textView.SetText($"{_level + 1}");
    }

    public void NextLevel()
    {
        _level++;

        _textView.SetText($"{_level + 1}");

        SaveLevel(_level);

#if UNITY_WEBGL && !UNITY_EDITOR
        SetToLeaderboard(_level+1);
#endif

    }

    public void LoadFirstLevel()
    {
        LoadLevel();
    }

    public int GetLevelPhase()
    {
        int phase = (int)Mathf.Sqrt(_level);

        return Mathf.Min(phase, _maxPhase);
    }

    private void SaveLevel(int level)
    {
        AnotherArt.PlayerPrefs.SetString(_levelKey, level.ToString());
        AnotherArt.PlayerPrefs.Save();
    }

    private void LoadLevel()
    {
        if (AnotherArt.PlayerPrefs.HasKey(_levelKey))
        {
            string value = AnotherArt.PlayerPrefs.GetString(_levelKey);
            _level = int.Parse(value);
        }
        else
            _level = 0;
    }

    [ContextMenu("ResetLevel")]
    private void ResetLevel()
    {
        _level = 0;
        SaveLevel(_level);
    }
}
