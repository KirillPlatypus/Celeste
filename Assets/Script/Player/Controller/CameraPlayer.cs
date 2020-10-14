namespace Controller
{
    public sealed class CameraPlayer : PlayerElement
    {
        private const float amplitude = 1.5f;

        private void Start()
        {
            aplication.cameraShake = aplication.cinemachine.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
        private void Update()
        {
            aplication.cameraShake.m_AmplitudeGain = aplication.playerModel.Dash ? amplitude : 0f;
        }
    }
}