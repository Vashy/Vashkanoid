using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource destroyedAudioSource;
    [SerializeField] private AudioSource blockDestroyedAudioSource;
    [SerializeField] private AudioSource blockHitAudioSource;
    [SerializeField] private AudioSource shipHitAudioSource;

    private void Start()
    {
        DeadTrigger.OnDeadTriggered += PlayDestroyedAudio;
        Block.OnBlockDestroyed += PlayBlockDestroyedAudio;
        SpaceShip.OnShipHit += PlayShipHitAudio;
        Block.OnBlockHit += PlayBlockHitAudio;
    }

    private void OnDestroy()
    {
        DeadTrigger.OnDeadTriggered -= PlayDestroyedAudio;
        Block.OnBlockDestroyed -= PlayBlockDestroyedAudio;
        Block.OnBlockHit -= PlayBlockHitAudio;
        SpaceShip.OnShipHit -= PlayShipHitAudio;
    }

    private void PlayBlockDestroyedAudio(float resiliencePoints)
    {
        blockDestroyedAudioSource.Play();
    }

    private void PlayDestroyedAudio()
    {
        destroyedAudioSource.Play();
    }

    private void PlayBlockHitAudio()
    {
        blockHitAudioSource.Play();
    }

    private void PlayShipHitAudio()
    {
        shipHitAudioSource.Play();
    }
}
