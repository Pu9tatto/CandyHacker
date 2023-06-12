using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private WinChecker _winChecker;
    [SerializeField] private SliderValueChanger _slider;

    private float _fullTime;

    private void OnEnable()
    {
        _winChecker.EndGame += OnEndGame;
    }

    private void OnDisable()
    {
        _winChecker.EndGame -= OnEndGame;
    }

    public void StartTimer(float time)
    {
        _fullTime = time;

        StartCoroutine(TurnTimer(_fullTime));
    }

    public void RestartTimer()
    {
        StopAllCoroutines();

        StartCoroutine(TurnTimer(_fullTime));
    }

    private IEnumerator TurnTimer(float time)
    {
        while(time>0)
        {
            time-= Time.deltaTime;

            _slider.ChangeSliderValue(time/_fullTime);

            yield return null;
        }

        _winChecker.ShowLoseWindow();
    }

    private void OnEndGame()
    {
        StopAllCoroutines();
    }
}
