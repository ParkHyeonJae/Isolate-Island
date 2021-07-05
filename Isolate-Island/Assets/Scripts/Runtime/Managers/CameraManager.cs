using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace IsolateIsland.Runtime.Managers
{
    using Camera = UnityEngine.Camera;
    public class CameraManager : IManagerInit
    {
        public Camera defaultCamera;

        public CinemachineBrain cinemachineBrain;

        public CinemachineVirtualCamera cinemachineVirtualCamera;
        public CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

        public void OnInit()
        {
            defaultCamera = defaultCamera ?? Camera.main;
            cinemachineBrain = cinemachineBrain ?? defaultCamera.GetComponent<CinemachineBrain>();
            cinemachineVirtualCamera = Managers.Instance.DI.Get<CinemachineVirtualCamera>();
            cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void CameraShake(float intensity, float duration)
        {
            Managers.Instance.Coroutine.StartRoutine(IntensityAdjustor());
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        }

        private IEnumerator IntensityAdjustor()
        {
            while (cinemachineBasicMultiChannelPerlin.m_AmplitudeGain <= 0)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain -= Time.deltaTime;
                yield return null;
            }
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;

        }

    }
}
