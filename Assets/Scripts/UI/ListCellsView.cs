using System.Collections.Generic;
using UnityEngine;

public class ListCellsView : MonoBehaviour
{
    [SerializeField] private CellView _tamplate;
    [SerializeField] private Transform _container;

    private CellView _newTamplate;

    public void Render(List<Sprite> sprites)
    {
        foreach (var sprite in sprites)
        {
            _newTamplate = Instantiate(_tamplate, _container);

            _newTamplate.SetImage(sprite);
        }
    }

    public void Render(Sprite sprite)
    {
        _newTamplate = Instantiate(_tamplate, _container);

        _newTamplate.SetImage(sprite);
    }

    public void ClearList()
    {
       var cellViews = GetComponentsInChildren<CellView>();

        foreach (var cellView in cellViews)
        {
            Destroy(cellView.gameObject);
        }
    }
}
