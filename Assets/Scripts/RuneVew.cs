using UnityEngine;
using DG.Tweening;

interface IRuneVew
{
    public void ChangeRuneCondition(Rune rune, EventType type);
    public void TryMoveToMouseCliked(PlayableFeild feild);
}

public class RuneVew : MonoBehaviour, IRuneVew
{
    private bool _isMoveing = false;
    private Rune _occupiedRune;
    private Vector2 _allOneScale = new Vector2(1, 1);
    public void ChangeRuneCondition(Rune rune, EventType type)
    {
        switch (type)
        {
            case EventType.OnMouseDown:
                if (_occupiedRune != null && !_isMoveing){_occupiedRune.transform.localScale = _allOneScale;}
                if (!_isMoveing)
                {
                    _occupiedRune = rune;
                }
                break;
            case EventType.OnMouseEnter:
                if (_occupiedRune == rune) { break; }
                rune.transform.localScale *= 1.1f;
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
        await _occupiedRune.transform.DOMove(feild.transform.position, 0.4f).
                                      SetEase(Ease.Linear).AsyncWaitForCompletion();
        _occupiedRune.UpdateCoordinates();
        ReleaseOccupuedRune();
        _isMoveing = false;
    }
    private void ReleaseOccupuedRune()
    {
        _occupiedRune.transform.localScale = _allOneScale;
        _occupiedRune = null;
    }
    private bool CheckForNeighbourClick(PlayableFeild feild)
    {
        if (feild.X != _occupiedRune.X && feild.Y != _occupiedRune.Y) return false;
        if (feild.X + 1 == _occupiedRune.X || feild.X - 1 == _occupiedRune.X) return true;
        if (feild.Y + 1 == _occupiedRune.Y || feild.Y - 1 == _occupiedRune.Y) return true;

        return false;
    }
}
