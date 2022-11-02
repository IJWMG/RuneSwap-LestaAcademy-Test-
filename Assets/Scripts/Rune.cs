using UnityEngine;

interface IPlaceable
{
    public int X { get; }
    public int Y { get; }

}
public class Rune : MonoBehaviour, IPlaceable
{
    public int X { get; private set; }
    public int Y { get; private set; }
    private RuneType _type;
    private bool _isTypeSet = false;

    public RuneType Type
    {
        get
        {
            return _type;
        }
        set
        {
            if (!_isTypeSet)
            {
                _type = value;
                _isTypeSet = true;
            }
            else
            {
                print("Type already set");
                return;
            }
        }
    }
    private void Awake()
    {
        UpdateCoordinates();
    }
    public void UpdateCoordinates(){
        X = (int)transform.position.x;
        Y = (int)transform.position.y;
    }
}
