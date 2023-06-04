using System.Collections.Generic;
using UnityEngine;

public class PossibleSprites : MonoBehaviour
{
    [SerializeField] private List<Sprite> _possibleCellSprites;

    public List<Sprite> GetSprites(int count)
    {
        count = Mathf.Min(count, _possibleCellSprites.Count);
        return _possibleCellSprites.GetRange(0, count);
    }
}
