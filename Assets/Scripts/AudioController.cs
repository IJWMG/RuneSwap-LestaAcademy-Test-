using UnityEngine;
using System;

public class AudioController : MonoBehaviour
{
    private RuneVew _runeVew ;
    private AudioSource _audio;
    private bool _isMutedByPlayer;
    private void Awake() {
        _runeVew = RuneVew.GetRuneVew();
        _isMutedByPlayer = Convert.ToBoolean(PlayerPrefs.GetString("audio", "false"));
        _runeVew.OnMoveMade += AudioStopper;
        _runeVew.OnMoveStart += AudioStarter;
        _audio = GetComponent<AudioSource>();
    }
    private void AudioStopper(int dontNeed){
        if (_isMutedByPlayer) return;
        _audio.Stop();
    }
    private void AudioStarter(){
        if (_isMutedByPlayer) return;
        _audio.Play();
    }
    public bool OnPlayerAudioMuteChange(){
        _isMutedByPlayer = !_isMutedByPlayer;
        PlayerPrefs.SetString("audio", _isMutedByPlayer.ToString());
        return _isMutedByPlayer;
    }
}
