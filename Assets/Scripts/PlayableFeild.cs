using UnityEngine;

public class PlayableFeild : MonoBehaviour, IPlaceable
{
    public bool IsOccupied { get; private set; } = false;
    public Rune CurrentRune { get; private set; }

    public int X { get; private set; }
    public int Y { get; private set; }
    private PlayableFeildsController _controller;
    private void Awake()
    {
        _controller = this.transform.parent.GetComponent<PlayableFeildsController>();
        X = (int)transform.position.x;
        Y = (int)transform.position.y;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IPlaceable>() is null)
        {
            return;
        }
        SetCurrentRune(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<IPlaceable>() is null)
        {
            return;
        }
        ReleaseCurrentRune();
    }

    private void SetCurrentRune(Collider2D collider)
    {
        Rune rune = collider.GetComponent<Rune>();
        if(rune != null){
            CurrentRune = rune;
        }
        IsOccupied = true;
    }
    private void ReleaseCurrentRune()
    {
        CurrentRune = null;
        IsOccupied = false;
        print("Rune released from " + transform.position);
    }
    private void OnMouseEnter()
    {
        if(CurrentRune is null) {return;}

        _controller.PresentRuneState(CurrentRune, EventType.OnMouseEnter);
    }
    private void OnMouseDown() {
        if(!IsOccupied) {
            _controller.OnMouseClickInvoker(this);
        }
        else if (CurrentRune != null){
            _controller.PresentRuneState(CurrentRune, EventType.OnMouseDown);
        }
    }
    private void OnMouseExit() {
        if(CurrentRune is null) {return;}

        _controller.PresentRuneState(CurrentRune, EventType.OnMouseExit);

    }
}
