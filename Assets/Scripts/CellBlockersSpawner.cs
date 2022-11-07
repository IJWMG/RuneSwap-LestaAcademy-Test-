using UnityEngine;
using System.Linq;


public class CellBlockersSpawner : MonoBehaviour
{
    [SerializeField] private PlayableFeildsController _playableFeilds;
    [SerializeField] private CellBlocker _blockerPrefab;
    private void Start()
    {
        SpawnBlockers();
    }
    private void SpawnBlockers()
    {
        var freeFeilds = (from f in _playableFeilds.ActiveFeilds
                          where !(_playableFeilds.FeildsForRunes.Contains(f)) && f.Y % 2 == 0
                          select f);
        foreach (var feild in freeFeilds)
        {
            Instantiate(_blockerPrefab, feild.transform.position, Quaternion.identity, this.transform);
        }
    }
}
