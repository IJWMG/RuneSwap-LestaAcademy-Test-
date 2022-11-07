using UnityEngine;

public class LightController : MonoBehaviour, IRuneVewObserver
{
    [SerializeField] private GameObject _glow;
    private GameObject _currentGlow;
    private RuneVew _runeVew = RuneVew.GetRuneVew();
    private void Awake()
    {
        _runeVew.OnMoveMade += OnMoveMade;
        _runeVew.OnMoveStart += OnMoveStart;
        _runeVew.OnOccupiedChange += OnOccupiedChange;
    }
    public void OnMoveMade(int counter)
    {
        return;
    }
    public void OnMoveStart()
    {
        if (_currentGlow is null) return;
        Destroy(_currentGlow);
    }
    public void OnOccupiedChange(PlayableFeild position)
    {
        if (_currentGlow != null)
        {
            Destroy(_currentGlow);
        }
        InstantiateGlow(position);
    }
    private void InstantiateGlow(IPlaceable position)
    {
        _currentGlow = Instantiate(
                    _glow, new Vector2(
                               (float)position.X, (float)position.Y),
                    Quaternion.identity);

    }
    private void OnDisable()
    {
        _runeVew.OnMoveMade -= OnMoveMade;
        _runeVew.OnMoveStart -= OnMoveStart;
        _runeVew.OnOccupiedChange -= OnOccupiedChange;

    }

}
