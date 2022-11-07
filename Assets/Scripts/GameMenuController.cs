using UnityEngine;
using UnityEngine.Events;

public class GameMenuController : MonoBehaviour
{
    public event UnityAction OnEscapeButtonDown, OnAudioButtonDown;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapeButtonDown?.Invoke();
            print("escape is down");
        }
    }
    public void OnExitDown()
    {
        Application.Quit();
    }
    public void OnMenuClose()
    {
        OnEscapeButtonDown?.Invoke();
    }
    public void OnAudioDown()
    {
        OnAudioButtonDown?.Invoke();
    }
}
