using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


interface IRuneVew
{
    public event UnityAction<PlayableFeild> OnOccupiedChange;
    public event UnityAction<int> OnMoveMade;
    public event UnityAction OnMoveStart;
    public void ChangeRuneCondition(PlayableFeild feildWithRune, EventType type);
    public void TryMoveToMouseCliked(PlayableFeild feild);
}

public class RuneVew : IRuneVew
{
    private int _moveCounter;
    private bool _isMoveing = false, _isUIActive = false;
    private PlayableFeild _feildUnderOccupiedRune;
    private Rune _occupiedRune;
    private Vector2 _allOneScale = new Vector2(1, 1), _increasedScale = new Vector2(1.1f, 1.1f);
    public event UnityAction<int> OnMoveMade;
    public event UnityAction<PlayableFeild> OnOccupiedChange;

    public event UnityAction OnMoveStart;
    private float _animationDuration = 0.4f;
    private static RuneVew _instace;
    private RuneVew() { }
    public static RuneVew GetRuneVew()
    {
        if (_instace is null)
        {
            _instace = new RuneVew();
        }
        return _instace;
    }
    public void ChangeRuneCondition(PlayableFeild feildWithRune, EventType type)
    {
        var rune = feildWithRune.CurrentRune;
        if (_isUIActive) { return; }
        switch (type)
        {
            case EventType.OnMouseDown:
                if (_occupiedRune != null && !_isMoveing) { _occupiedRune.transform.localScale = _allOneScale; }
                if (!_isMoveing)
                {
                    _feildUnderOccupiedRune = feildWithRune;
                    _occupiedRune = rune;
                    _occupiedRune.transform.localScale = _increasedScale;
                    OnOccupiedChange?.Invoke(_feildUnderOccupiedRune);
                }
                break;
            case EventType.OnMouseEnter:
                if (_occupiedRune == rune) { break; }
                rune.transform.localScale = _increasedScale;
                break;
            case EventType.OnMouseExit:
                if (_occupiedRune == rune) { break; }
                rune.transform.localScale = _allOneScale;
                break;
        }
    }
    public void TryMoveToMouseCliked(PlayableFeild feild)
    {
        if (feild.IsOccupied || _occupiedRune is null) { return; }

        if (CheckForNeighbourClick(feild))
        {
            _isMoveing = true;
            MoveRuneAsync(feild);
        }
    }
    private async void MoveRuneAsync(PlayableFeild feild)
    {
        _moveCounter++;
        OnMoveStart?.Invoke();
        _feildUnderOccupiedRune.ReleaseCurrentRune();
        await _occupiedRune.transform.DOMove(feild.transform.position, _animationDuration).
                                      SetEase(Ease.Linear).AsyncWaitForCompletion();
        _occupiedRune.UpdateCoordinates();
        ReleaseOccupuedRune();
        //Debug.Log("move made");
        OnMoveMade?.Invoke(_moveCounter);
        _isMoveing = false;
    }
    private void ReleaseOccupuedRune()
    {
        _occupiedRune.transform.localScale = _allOneScale;
        _occupiedRune = null;
        _feildUnderOccupiedRune = null;
    }
    private bool CheckForNeighbourClick(PlayableFeild feild)
    {
        if (feild.X != _occupiedRune.X && feild.Y != _occupiedRune.Y) return false;
        if (feild.X + 1 == _occupiedRune.X || feild.X - 1 == _occupiedRune.X) return true;
        if (feild.Y + 1 == _occupiedRune.Y || feild.Y - 1 == _occupiedRune.Y) return true;

        return false;
    }
    public void Reset()
    {
        ActivateUI();
        //Debug.Log("occupied rune is " + _occupiedRune);
    }
    public void NewGame()
    {
        _moveCounter = 0;
        DisableUI();
    }
    public void ActivateUI() => _isUIActive = true;
    public void DisableUI() => _isUIActive = false;
}
