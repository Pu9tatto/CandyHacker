using TMPro;
using UnityEngine;

public class EndGameWindowWidget : MonoBehaviour
{
    [SerializeField] private TMP_Text _winTitle;
    [SerializeField] private TMP_Text _loseTitle;

    public void SetTitle(bool isWin)
    {
        if (isWin)
        {
            _winTitle.gameObject.SetActive(true);
            _loseTitle.gameObject.SetActive(false);
        }
        else
        {
            _winTitle.gameObject.SetActive(false);
            _loseTitle.gameObject.SetActive(true);
        }
    }
}
