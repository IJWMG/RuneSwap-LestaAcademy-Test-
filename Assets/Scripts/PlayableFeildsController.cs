using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Collections.Generic;

interface IRuneAndFeildModel
{
    public event UnityAction<PlayableFeild, EventType> OnConditionChange;
    public event UnityAction<PlayableFeild> OnMouseClick;
}
// TODO: сделать возможность выбора поля от 5х5 до 11х11 (?)
public class PlayableFeildsController : MonoBehaviour, IRuneAndFeildModel
{
    [SerializeField] private PlayableFeild _cellPrefab;
    public PlayableFeild[] ActiveFeilds { get; private set; }
    public PlayableFeild[] FeildsForRunes { get; private set; }
    public event UnityAction<PlayableFeild, EventType> OnConditionChange;
    public event UnityAction<PlayableFeild> OnMouseClick;
    private Vector2[] _coordinatesOfFeilds;
    public const int FEILD_SIZE = 5;
    private void Awake()
    {
        InitializeAllFeild();
        GenerateFeildForRunes();
        WinChecker.SetFeildsToCheck(FeildsForRunes);
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
    private void GenerateFeildForRunes()
    {
        int[] collumns = CalculateCollumnsForRunes();
        var freeFeilds = (from f in ActiveFeilds
                          where (collumns.Contains(f.X))
                          select f).ToArray();
        FeildsForRunes = freeFeilds;
    }
    private int[] CalculateCollumnsForRunes()
    {
        int[] collumns = new int[(FEILD_SIZE / 2) + 1];
        int i = FEILD_SIZE / 2, j = 0;
        while (i >= 0)
        {
            if (i == 0)
            {
                collumns[j] = i;
                break;
            }
            collumns[j] = -i;
            j++;
            collumns[j] = i;
            j++;
            i -= 2;
        }
        return collumns;
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
    public void OnMouseClickInvoker(PlayableFeild feild)
    {
        OnMouseClick?.Invoke(feild);
    }
    public void PresentRuneState(PlayableFeild feild, EventType eventType)
    {
        OnConditionChange?.Invoke(feild, eventType);
    }
}
