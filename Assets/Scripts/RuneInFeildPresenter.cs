using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayableFeildsController))]
[RequireComponent(typeof(RuneVew))]
public class RuneInFeildPresenter : MonoBehaviour
{
    private IRuneAndFeildModel _runeModel;
    private IRuneVew _runeVew;
    private void Awake() {
        _runeModel = GetComponent<PlayableFeildsController>();
        _runeVew = GetComponent<RuneVew>();
        _runeModel.OnConditionChange += ChangeRuneCondition;
        _runeModel.OnMouseClick += MoveToMouseCliked;
    }
    public void ChangeRuneCondition (Rune rune, EventType type){
        _runeVew.ChangeRuneCondition(rune, type);
    }
    public void MoveToMouseCliked(PlayableFeild feild){
        _runeVew.TryMoveToMouseCliked(feild);
    }

}
