using UnityEngine;
using System;

public class AudioController : MonoBehaviour, IRuneVewObserver
{
    [SerializeField] AudioSource _audioForClick;
    private RuneVew _runeVew;
    private AudioSource _audioForMove;
    private bool _isMutedByPlayer;
    private void Awake()
    {
        _runeVew = RuneVew.GetRuneVew();
        _isMutedByPlayer = Convert.ToBoolean(PlayerPrefs.GetString("audio", "false"));
        _runeVew.OnMoveMade += OnMoveMade;
        _runeVew.OnMoveStart += AudioStarter;
        _runeVew.OnOccupiedChange += OnOccupiedChange;
        _audioForMove = GetComponent<AudioSource>();
    }
    public void OnMoveMade(int counter) => AudioStopper();
    public void OnMoveStart() => AudioStarter();
    private void AudioStopper()
    {
        if (_isMutedByPlayer) return;
        _audioForMove.Stop();
    }
    private void AudioStarter()
    {
        if (_isMutedByPlayer) return;
        _audioForMove.Play();
    }
    public bool OnPlayerAudioMuteChange()
    {
        _isMutedByPlayer = !_isMutedByPlayer;
        PlayerPrefs.SetString("audio", _isMutedByPlayer.ToString());
        return _isMutedByPlayer;
    }
    private void OnDisable()
    {
        _runeVew.OnMoveMade -= OnMoveMade;
        _runeVew.OnMoveStart -= AudioStarter;
        _runeVew.OnOccupiedChange -= OnOccupiedChange;

    }
    public void OnOccupiedChange(PlayableFeild feild)
    {
        if (_isMutedByPlayer) return;
        _audioForClick.Play();
    }
}
