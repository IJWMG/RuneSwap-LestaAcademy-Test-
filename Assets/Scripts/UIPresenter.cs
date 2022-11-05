using UnityEngine;

public class UIPresenter : MonoBehaviour
{
    private IRuneVew _runeVew = RuneVew.GetRuneVew();
    private WinChecker _winChecker = WinChecker.GetWinChecker();
    [SerializeField] private ScreenUI _screenUI;
    private  void Awake() {
        _winChecker.OnWinDetected += InitializeWin;
        _runeVew.OnMoveMade += OnMoveMade;
    }
    private void OnMoveMade(int counter){
        _screenUI.ShowMoveCounter(counter);
    }
    private void InitializeWin(){
        _screenUI.OnWinDetected();
        _winChecker.OnWinDetected -= InitializeWin;
        _runeVew.OnMoveMade -= OnMoveMade;

    }
}
