using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private LevelSettings _settings;

    [SerializeField] protected LevelSwitcher _levelSwitcher;

    [SerializeField] private Cell _cell;
    [SerializeField] private Transform _cellContainer;
    [SerializeField] private Map _map;
    [SerializeField] private ListCellsView _requiredCellView;
    [SerializeField] private RequiredCells _requiredCells;
    [SerializeField] private PossibleSprites _possibleSprites;
    [SerializeField] private Timer _timer;

    private int _mapCount;
    private int _requiredCellsCount;
    private int _selectedListCount;
    private int _spritesCount;
    private float _time;
    private float _cellModifiedScale;
    private Vector2 _startPoint;
    private List<Sprite> _sprites;

    public int MapCount => _mapCount;

    public int SelectedListCount => _selectedListCount;

    private void Awake()
    {
        _levelSwitcher.LoadFirstLevel();

        InitLevelSettings();

        Build();
    }

    private void Start()
    {
        InitRequiredCells();
    }

    private void Build()
    {

        _timer.StartTimer(_time);

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
        _timer.StartTimer(_time);

        _map.ClearMap();
        _requiredCellView.ClearList();

        InitLevelSettings();

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

    private void InitLevelSettings()
    {
        int phase = SetPhase();

        _spritesCount = _settings.Settings[phase].SpriteCount;
        _mapCount = _settings.Settings[phase].MapCount;
        _requiredCellsCount = _settings.Settings[phase].RequiredCellsCount;
        _selectedListCount = _settings.Settings[phase].SelectedCellsCount;
        _time = _settings.Settings[phase].Time;
    }

    private int SetPhase() => _levelSwitcher.GetLevelPhase();
}

