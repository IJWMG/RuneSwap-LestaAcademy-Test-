using UnityEngine;
using TMPro;

public class ScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moveText;
    [SerializeField] private GameObject _gameOverPanel;
    public void ShowMoveCounter(int counter){
        if(_gameOverPanel is null){print("Gameoverpanel is null");}
        _moveText.text = $"Rune moves: \n{counter}";
    }
    public void OnWinDetected(){
        Debug.Log("Congratulations");
        _gameOverPanel.SetActive(true);
    }
}
