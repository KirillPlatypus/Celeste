using UnityEngine;
using Player.View;
using System;
using Cinemachine;

namespace CameraScript
{
    public sealed class ShakeCamera : MonoBehaviour
    {
        protected PlayerView _player { get { return FindObjectOfType<PlayerView>(); } }
        
        [SerializeField] private CinemachineVirtualCamera cinemachine;
        private CinemachineBasicMultiChannelPerlin cameraShake;

        private const float amplitude = 1.5f;

        private void Awake()
        {
            cameraShake = cinemachine.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }

        private void OnEnable() 
        {
            _player.OnDashingStateChange += CameraShaking;
        }
        
        private void OnDisable() 
        {
            _player.OnDashingStateChange -= CameraShaking;
        }

        private void CameraShaking(bool IsDoing)
        {
            cameraShake.m_AmplitudeGain = IsDoing ? amplitude : 0f;
        } 
    }
}