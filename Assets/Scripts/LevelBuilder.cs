using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private int _spritesCount;
    [SerializeField] private int _mapCount;
    [SerializeField] private int _requiredCellsCount;
    [SerializeField] private int _selectedListCount;
    [SerializeField] private Cell _cell;
    [SerializeField] private Transform _cellContainer;
    [SerializeField] private Map _map;
    [SerializeField] private ListCellsView _requiredCellView;
    [SerializeField] private RequiredCells _requiredCells;
    [SerializeField] private PossibleSprites _possibleSprites;

    private float _cellModifiedScale;
    private Vector2 _startPoint;
    private List<Sprite> _sprites;

    public int MapCount => _mapCount;

    public int SelectedListCount => _selectedListCount;

    private void Awake()
    {
        Build();
    }

    private void Start()
    {
        InitRequiredCells();
    }

    private void Build()
    {
        _map.Init(_mapCount);

        Vector2 position;
        int startPointX = -1;
        int startPointY = 1;
        float startPointMultiplier = 0.5f;

        _cellModifiedScale = 1f / Mathf.Max(_mapCount, _mapCount);

        _startPoint = new Vector2(startPointX + _cellModifiedScale, startPointY - _cellModifiedScale) * startPointMultiplier;

        for (int i = 0; i < _mapCount; i++)
        {
            for (int j = 0; j < _mapCount; j++)
            {
                position = new Vector2(_startPoint.x + i * _cellModifiedScale, _startPoint.y - j * _cellModifiedScale);

                _map.AddCell(InstantiateCell(position, j, i));
            }
        }
    }

    public void ReBuild()
    {
        _map.ClearMap();
        _requiredCellView.ClearList();

        Build();

        InitRequiredCells();

        _map.Subscribed();
        _map.SetInteractiveRow(0);
    }

    private Cell InstantiateCell(Vector3 position, int xIndex, int yIndex)
    {
        _sprites = _possibleSprites.GetSprites(_spritesCount);

        Cell cell = Instantiate(_cell, _cellContainer);

        cell.transform.localScale *= _cellModifiedScale;
        cell.transform.localPosition = position;

        cell.SetIcon(_sprites[Random.Range(0, _sprites.Count)]);
        cell.SetIndex(xIndex, yIndex);

        return cell;
    }

    private void InitRequiredCells()
    {
        _requiredCells.GenerateRequiredList(_requiredCellsCount);
        _requiredCellView.Render(_requiredCells.RequiredSprites);
    }
}

