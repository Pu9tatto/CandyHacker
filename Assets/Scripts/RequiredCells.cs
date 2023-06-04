using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RequiredCells : MonoBehaviour
{
    [SerializeField] private Map _map;

    private Vector2Int[] _cellIndexes;

    public List<Sprite> RequiredSprites { get; private set; } = new List<Sprite>();

    public void GenerateRequiredList(int count)
    {
        RequiredSprites.Clear();

        _cellIndexes = new Vector2Int[count];

        int randomXIndex = Random.Range(0, count);
        int randomYIndex = Random.Range(0, count);
        int index = 1;
        Vector2Int newVector = new Vector2Int(randomXIndex, randomYIndex);
        Sprite sprite;

        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                index = randomYIndex == 0 ? 1 : 0;
            }
            else if (i % 2 == index)
            {
                while (_cellIndexes.Contains(newVector))
                {
                    randomYIndex = Random.Range(0, count);
                    newVector = new Vector2Int(randomXIndex, randomYIndex);
                }
            }
            else
            {
                while (_cellIndexes.Contains(newVector))
                {
                    randomXIndex = Random.Range(0, count);
                    newVector = new Vector2Int(randomXIndex, randomYIndex);
                }
            }

            _cellIndexes[i] = newVector;
        }

        foreach (Vector2Int vector in _cellIndexes)
        {
            sprite = _map.GetCell(vector.y, vector.x).Icon;
            RequiredSprites.Add(sprite);
        }
    }
}

