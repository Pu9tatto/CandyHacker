using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    public Sprite Icon { get; private set; }
    public bool IsInteractive { get; private set; }
    public int XIndex { get; private set; }
    public int YIndex { get; private set; }

    public event UnityAction<(int,int)> CellPositionClicked;
    public event UnityAction<Sprite> CellIconClicked;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetInteractive(bool isInteractive) => IsInteractive = isInteractive;

    public void SetColor(Color color) => _renderer.color = color;

    public void SetIcon(Sprite sprite)
    {
        Icon = sprite;
        _renderer.sprite = sprite;
    }

    public void SetIndex(int indexX, int indexY)
    {
        XIndex = indexX;
        YIndex = indexY;
    }

    private void OnMouseUp()
    {
        if (IsInteractive)
        {
            CellPositionClicked?.Invoke((XIndex, YIndex));
            CellIconClicked?.Invoke(Icon);

            gameObject.SetActive(false);
        }
    }
}
