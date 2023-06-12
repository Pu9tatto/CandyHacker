using UnityEngine;

public class LeveSwitcher : MonoBehaviour
{
    [SerializeField] private TextView textView;
    [SerializeField] private int _maxPhase = 10;

    private const string _levelKey = "currentLevel";

    private int _level;

    private void Start()
    {
        textView.SetText($"Level: {_level + 1}");
    }

    public void NextLevel()
    {
        _level++;

        textView.SetText($"Level: {_level + 1}");

        SaveLevel(_level);
    }

    public int GetLevelPhase()
    {
        LoadLevel();

        int phase = (int)Mathf.Sqrt(_level);

        return Mathf.Min(phase, _maxPhase);
    }

    private void SaveLevel(int level)
    {
        PlayerPrefs.SetInt(_levelKey, level);
        PlayerPrefs.Save();
    }

    private void LoadLevel()
    {
        if (PlayerPrefs.HasKey(_levelKey))
            _level = PlayerPrefs.GetInt(_levelKey);
        else
            _level = 0;
    }

    [ContextMenu("ResetLevel")]
    private void ResetLevel()
    {
        SaveLevel(0);
    }
}
