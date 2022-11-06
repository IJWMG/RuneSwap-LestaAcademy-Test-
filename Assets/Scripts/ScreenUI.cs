using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moveText;
    [SerializeField] private Text _audioText;
    [SerializeField] private GameObject _gameOverPanel, _menuPanel;
    public void ShowMoveCounter(int counter){
        if(_gameOverPanel is null){print("Gameoverpanel is null");}
        _moveText.text = $"Rune moves: \n{counter}";
    }
    public void OnWinDetected(){
        Debug.Log("Congratulations");
        _gameOverPanel.SetActive(true);
    }
    public void MenuPanelSwitch(){
        _menuPanel.SetActive(!_menuPanel.activeSelf);
    }
    public void ChangeAudioText(bool isMuted){
        if (isMuted){
            _audioText.text = "UNMUTE";
            return;
        }
        _audioText.text = "MUTE";

    }
}
