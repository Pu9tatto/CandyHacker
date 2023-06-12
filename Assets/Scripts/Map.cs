using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map : MonoBehaviour
{
    [SerializeField] private Color _interactableColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private ListCellsView _selectedCellsView;

    public event UnityAction SelectedSpritesChanged;

    public List<Sprite> SelectedSprites { get; private set; } = new List<Sprite>();

    private int _startInteractiveRow = 0;
    private Cell[,] _mapCells;
    private int _mapCount;
    private bool _isRowActive;

    private void Start()
    {
        SetInteractiveRow(_startInteractiveRow);

        Subscribed();
    }

    private void OnDisable()
    {
        UnSubscribed();
    }

    public void AddCell(Cell cell) => _mapCells[cell.XIndex, cell.YIndex] = cell;

    public Cell GetCell(int Xindex, int YIndex) => _mapCells[Xindex, YIndex];

    public void Init(int mapCount)
    {
        _mapCount = mapCount;
        _mapCells = new Cell[mapCount, mapCount];
    }

    public void SetInteractiveRow(int rowNumber)
    {
        _isRowActive = true;

        for (int i = 0; i < _mapCount; i++)
        {
            for (int j = 0; j < _mapCount; j++)
            {
                _mapCells[i, j].SetInteractive(i == rowNumber);
                _mapCells[i, j].SetColor(i == rowNumber ? _interactableColor : _defaultColor);
            }
        }
    }

    public void SetInteractiveColumn(int columnNumber)
    {
        _isRowActive = false;

        for (int i = 0; i < _mapCount; i++)
        {
            for (int j = 0; j < _mapCount; j++)
            {
                _mapCells[i, j].SetInteractive(j == columnNumber);
                _mapCells[i, j].SetColor(j == columnNumber ? _interactableColor : _defaultColor);
            }
        }
    }

    public void SetNonInteractiveMap()
    {
        for (int i = 0; i < _mapCount; i++)
        {
            for (int j = 0; j < _mapCount; j++)
            {
                _mapCells[i, j].SetInteractive(false);
                _mapCells[i, j].SetColor(_defaultColor);
            }
        }
    }

    public void ClearMap()
    {
        ClearMapCell();
        ClearSelectedSprites();
    }

    public void Subscribed()
    {
        foreach (var cell in _mapCells)
        {
            cell.CellPositionClicked += OnSetInteract;
            cell.CellIconClicked += OnSelectSprite;
        }
    }

    private void OnSetInteract((int,int) position)
    {
        if(_isRowActive)
            SetInteractiveColumn(position.Item2);
        else
            SetInteractiveRow(position.Item1);
    }

    private void OnSelectSprite(Sprite sprite)
    {
        SelectedSprites.Add(sprite);

        _selectedCellsView.Render(sprite);

        SelectedSpritesChanged?.Invoke();
    }

    private void ClearMapCell()
    {
        UnSubscribed();

        foreach (var cell in _mapCells)
        {
            Destroy(cell.gameObject);
        }
    }

    private void ClearSelectedSprites()
    {
        SelectedSprites.Clear();

        _selectedCellsView.ClearList();
    }

    private void UnSubscribed()
    {
        foreach (var cell in _mapCells)
        {
            cell.CellPositionClicked -= OnSetInteract;
            cell.CellIconClicked -= OnSelectSprite;
        }
    }


}
