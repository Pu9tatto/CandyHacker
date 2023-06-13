using System.Runtime.InteropServices;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private TextView textView;
    [SerializeField] private int _maxPhase = 10;
    //[SerializeField] private int _startLevel;

    [DllImport("__Internal")] private static extern void SaveExtern(string date);
    [DllImport("__Internal")] private static extern void LoadExtern();
    [DllImport("__Internal")] private static extern void SetToLeaderboard(int value);

    //private const string _levelKey = "currentLevel";

    private int _level = 0;

    //private void Awake()
    //{
    //    PlayerPrefs.SetInt(_levelKey, _startLevel);
    //    PlayerPrefs.Save();
    //}

    private void Start()
    {
        textView.SetText($"{_level + 1}");
    }

    public void NextLevel()
    {
        _level++;

        textView.SetText($"{_level + 1}");

        Save();

        SetToLeaderboard(_level);
    }

    public int GetLevelPhase()
    {

        LoadExtern();

        int phase = (int)Mathf.Sqrt(_level);

        return Mathf.Min(phase, _maxPhase);
    }


    public void Save()
    {
        string jsonString = JsonUtility.ToJson(_level);
        SaveExtern(jsonString);
    }

    public void Load(string value)
    {
        _level = JsonUtility.FromJson<int>(value);
    }

    //private void SaveLevel(int level)
    //{
    //    PlayerPrefs.SetInt(_levelKey, level);
    //    PlayerPrefs.Save();
    //}

    //private void LoadLevel()
    //{
    //    if (PlayerPrefs.HasKey(_levelKey))
    //        _level = PlayerPrefs.GetInt(_levelKey);
    //    else
    //        _level = 0;
    //}

    [ContextMenu("ResetLevel")]
    private void ResetLevel()
    {
        _level = 0; 
        Save();
    }
}
