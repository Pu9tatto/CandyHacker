using System.Collections.Generic;
using UnityEngine;

public class WinChecker : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private RequiredCells _requiredCells;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private EndGameWindowWidget _endGameWindow;

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

    private void OnCheckWin()
    {
        if (IsWin())
        {
            ShowEndGameWindow();
        }
        else if (_selectedSprites.Count == _maxSelectedSprites)
        {
            ShowEndGameWindow();
        }
    }

    private void ShowEndGameWindow()
    {
        _endGameWindow.gameObject.SetActive(true);
        _endGameWindow.SetTitle(IsWin());
    }

    private bool IsWin()
    {
        int overlayCount = 0;
        int requiredSpritesIndex = 0;

        _requiredSprites = _requiredCells.RequiredSprites;
        _selectedSprites = _map.SelectedSprites;

        for (int i = 0; i < _selectedSprites.Count; i++)
        {
            if (_selectedSprites[i] == _requiredSprites[requiredSpritesIndex])
            {
                overlayCount++;
                requiredSpritesIndex++;
            }
            else
            {
                overlayCount = 0;
                requiredSpritesIndex = 0;
            }
        }

        if (overlayCount == _requiredSprites.Count)
        {
            return true;
        }

        return false;
    }
}
