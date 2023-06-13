using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameWindowWidget : MonoBehaviour
{
    [SerializeField] private TMP_Text _winTitle;
    [SerializeField] private TMP_Text _loseTitle;
    [SerializeField] private Color _winColor;
    [SerializeField] private Color _loseColor;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetEndGameWindow(bool isWin)
    {
        _winTitle.gameObject.SetActive(isWin);
        _loseTitle.gameObject.SetActive(!isWin);

        _image.color = isWin ? _winColor : _loseColor;
    }
}
