using UnityEngine;

public class UIPresenter : MonoBehaviour
{
    private IRuneVew _runeVew = RuneVew.GetRuneVew();
    private WinChecker _winChecker = WinChecker.GetWinChecker();
    [SerializeField] private ScreenUI _screenUI;
    [SerializeField] private GameMenuController _menuController;
    [SerializeField] private AudioController _audio;
    private  void Awake() {
        _menuController.OnAudioButtonDown += AudioTrigger;
        _menuController.OnEscapeButtonDown += OpenMenu;
        _winChecker.OnWinDetected += InitializeWin;
        _runeVew.OnMoveMade += OnMoveMade;
    }
    private void OnMoveMade(int counter){
        _screenUI.ShowMoveCounter(counter);
    }
    private void InitializeWin(){
        _screenUI.OnWinDetected();
        _menuController.OnAudioButtonDown -= AudioTrigger;
        _winChecker.OnWinDetected -= InitializeWin;
        _runeVew.OnMoveMade -= OnMoveMade;

    }
    private void OnDisable() {
        _menuController.OnEscapeButtonDown -= OpenMenu;

    }
    private void OpenMenu(){
        _screenUI.MenuPanelSwitch();
    }
    private void AudioTrigger(){
        
        _screenUI.ChangeAudioText(_audio.OnPlayerAudioMuteChange());
    }
}
