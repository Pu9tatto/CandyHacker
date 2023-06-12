using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "LevelSettings", order = 51)]
public class LevelSettings : ScriptableObject
{
    [SerializeField] private Setting[] _settings;

    public IReadOnlyList<Setting> Settings => _settings;
}

[Serializable]
public class Setting
{
    [SerializeField] private int _minSpritesCount;
    [SerializeField] private int _maxSpritesCount;
    [SerializeField] private int _minMapCount;
    [SerializeField] private int _maxMapCount;
    [SerializeField] private int _minRequiredCellsCount;
    [SerializeField] private int _maxRequiredCellsCount;
    [SerializeField] private int _minSelectedCellsCount;
    [SerializeField] private int _maxSelectedCellsCount;
    [SerializeField] private float _time;

    public int SpriteCount => UnityEngine.Random.Range(_minSpritesCount, _maxSpritesCount);
    public int MapCount => UnityEngine.Random.Range(_minMapCount, _maxMapCount);
    public int RequiredCellsCount => UnityEngine.Random.Range(_minRequiredCellsCount, _maxRequiredCellsCount);
    public int SelectedCellsCount => UnityEngine.Random.Range(_minSelectedCellsCount, _maxSelectedCellsCount);
    public float Time => _time;
}