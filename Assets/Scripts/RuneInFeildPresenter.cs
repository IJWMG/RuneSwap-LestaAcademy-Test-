using UnityEngine;

[RequireComponent(typeof(PlayableFeildsController))]
public class RuneInFeildPresenter : MonoBehaviour
{
    private IRuneAndFeildModel _runeModel;
    private IRuneVew _runeVew = RuneVew.GetRuneVew();
    private void Awake() {
        _runeModel = GetComponent<PlayableFeildsController>();
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
