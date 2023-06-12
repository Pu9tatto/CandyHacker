using System.Runtime.InteropServices;
using UnityEngine;

public class Language : MonoBehaviour
{
    [DllImport("__Internal")] private static extern string GetLang();
    [SerializeField] private string _currentLanguage;

    public string CurrentLanguage => _currentLanguage;

    public static Language Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);

            _currentLanguage = GetLang();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
