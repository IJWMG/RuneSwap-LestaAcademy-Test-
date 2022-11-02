using UnityEngine;
using UnityEngine.Events;

interface IRuneAndFeildModel{
    public event UnityAction<Rune, EventType> OnConditionChange;
    public event UnityAction<PlayableFeild> OnMouseClick;
}

public class PlayableFeildsController : MonoBehaviour, IRuneAndFeildModel
{
    [SerializeField] private PlayableFeild _cellPrefab;
    public PlayableFeild[] ActiveFeilds {get; private set; }
    public event UnityAction<Rune, EventType> OnConditionChange;
    public event UnityAction<PlayableFeild> OnMouseClick;
    private Vector2[] _coordinatesOfFeilds;
    public const int FEILD_SIZE = 5;
    private void Awake()
    {
        InitializeAllFeild();
    }
    private void InitializeAllFeild()
    {
        FillCoordinates();
        ActiveFeilds = new PlayableFeild[_coordinatesOfFeilds.Length];
        for (int i = 0; i < _coordinatesOfFeilds.Length; i++)
        {
            GameObject feild = Instantiate(_cellPrefab.gameObject, _coordinatesOfFeilds[i], Quaternion.identity, this.transform);
            ActiveFeilds[i] = feild.GetComponent<PlayableFeild>();
        }
    }
    private void FillCoordinates()
    {
        _coordinatesOfFeilds = new Vector2[FEILD_SIZE * FEILD_SIZE];
        for (int x = 0 - FEILD_SIZE / 2, i = 0; i < _coordinatesOfFeilds.Length; x++)
        {
            for (int y = 0 - FEILD_SIZE / 2; y <= FEILD_SIZE / 2; y++, i++)
            {
                _coordinatesOfFeilds[i] = new Vector2(x, y);
            }
        }
    }
    public void OnMouseClickInvoker(PlayableFeild feild){
        OnMouseClick?.Invoke(feild);
    }
    public void PresentRuneState(Rune rune, EventType eventType){
        OnConditionChange?.Invoke(rune, eventType);
    }
}
