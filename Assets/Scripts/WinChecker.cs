using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinChecker : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private RequiredCells _requiredCells;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private EndGameWindowWidget _endGameWindow;
    [SerializeField] private Button _nextLevelButton;

    public event UnityAction EndGame;

    private int _maxSelectedSprites;
    private List<Sprite> _requiredSprites;
    private List<Sprite> _selectedSprites;

    private void Start()
    {
        _maxSelectedSprites = _levelBuilder.SelectedListCount;

        _map.SelectedSpritesChanged += OnCheckWin;
    }

    private void OnDisable()
    {
        _map.SelectedSpritesChanged -= OnCheckWin;
    }

    public void ShowLoseWindow()
    {
        ShowEndGameWindow(false);
    }

    private void OnCheckWin()
    {
        if (IsWin()) 
            ShowEndGameWindow(true);
        else if(_selectedSprites.Count == _maxSelectedSprites)
            ShowEndGameWindow(false);
    }

    private void ShowEndGameWindow(bool isWin)
    {
        _map.SetNonInteractiveMap();

        _endGameWindow.gameObject.SetActive(true);
        _endGameWindow.SetEndGameWindow(isWin);
        _nextLevelButton.gameObject.SetActive(isWin);

        EndGame?.Invoke();
    }

    private bool IsWin()
    {
        _requiredSprites = _requiredCells.RequiredSprites;
        _selectedSprites = _map.SelectedSprites;

        int requiredSpritesCount = _requiredSprites.Count;
        int selectedSpritesCount = _selectedSprites.Count;

        for (int i = 0; i <= selectedSpritesCount - requiredSpritesCount; i++)
        {
            int j;

            for (j = 0; j < requiredSpritesCount; j++)
            {
                if (_selectedSprites[i + j] != _requiredSprites[j])
                    break;
            }

            if (j == requiredSpritesCount)
                return true;
        }

        return false;
    }
}
