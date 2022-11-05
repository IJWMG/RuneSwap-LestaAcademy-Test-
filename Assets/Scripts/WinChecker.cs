using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Collections.Generic;

public class WinChecker
{
    public event UnityAction OnWinDetected;
    private RuneVew _runeVew = RuneVew.GetRuneVew();
    private static WinChecker _instace;
    //private static Dictionary<PlayableFeild[], bool> _feildsToCheck = new Dictionary<PlayableFeild[], bool>();
    private static List<PlayableFeild[]> _feildsToCheck;
    private WinChecker()
    {
        _runeVew.OnMoveMade += CheckForWin;
    }
    public static WinChecker GetWinChecker()
    {
        if (_instace is null)
        {
            _instace = new WinChecker();
        }
        return _instace;
    }
    public static void SetFeildsToCheck(PlayableFeild[] feildsToCheck)
    {
        DevideArrausOfFeilds(feildsToCheck);
    }
    private static void DevideArrausOfFeilds(PlayableFeild[] arrayToDevide)
    {
        _feildsToCheck = new List<PlayableFeild[]>();
        int feildSize = PlayableFeildsController.FEILD_SIZE;
        for (int i = 0; i < arrayToDevide.Length; i += feildSize)
        {
            var devided = arrayToDevide.Skip(i).Take(feildSize).ToArray();
            _feildsToCheck.Add(devided);
        }
    }
    private void CheckForWin(int dontNeed)
    {
        Debug.Log("Checking");
        for (int i = 0; i < _feildsToCheck.Count; i++)
        {
            foreach (var feild in _feildsToCheck[i])
            {
                if (feild.CurrentRune is null)
                {
                    //Debug.Log("One of the feilds is empty");
                    return;
                }
                if (feild.CurrentRune.Type != (RuneType)i)
                {
                    // Debug.Log("At least of the runes is not in place");
                    return;
                }
            }
        }
        OnWinDetected?.Invoke();
        Reload();
        Debug.Log("Check sucsess, win");
    }
    private void Reload()
    {
        _runeVew.OnMoveMade -= CheckForWin;
        _instace = null;
        _runeVew.Reset();
        Debug.Log("reloaded, ");
    }
}
