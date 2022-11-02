using UnityEngine;

public class CellBlocker : MonoBehaviour, IPlaceable
{
    public int X {get; private set; }
    public int Y {get; private set; }
    private void Awake() {
        X = (int) transform.position.x;
        Y = (int) transform.position.y;
    }
}
