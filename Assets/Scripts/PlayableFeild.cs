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
    private void SetCurrentRune(Collider2D collider)
    {
        Rune rune = collider.GetComponent<Rune>();
        if (rune != null)
        {
            CurrentRune = rune;
        }
        IsOccupied = true;
    }
    public void ReleaseCurrentRune()
    {
        //print("feild is free");
        CurrentRune = null;
        IsOccupied = false;
    }
    private void OnMouseEnter()
    {
        if (CurrentRune is null) { return; }

        _controller.PresentRuneState(this, EventType.OnMouseEnter);
    }
    private void OnMouseDown()
    {
        if (!IsOccupied)
        {
            _controller.OnMouseClickInvoker(this);
        }
        else if (CurrentRune != null)
        {
            _controller.PresentRuneState(this, EventType.OnMouseDown);
        }
    }
    private void OnMouseExit()
    {
        if (CurrentRune is null) { return; }

        _controller.PresentRuneState(this, EventType.OnMouseExit);

    }
}
