using UnityEngine;

public class CamShake : MonoBehaviour
{
    [SerializeField]
    private Vector3 _maxRotation = new Vector3(25.0f, 25.0f, 25.0f);
    [SerializeField]
    private float _traumaPower = 1.0f;
    [SerializeField]
    private float _traumaDampen = 0.25f;
    [SerializeField]
    private float _shakeSpeed = 1.0f;

    private float _trauma;
    private float _shakeTime;
    private Vector3 _seed;

    public bool ReduceTrauma = false;

    public void AddTrauma(float t)
    {
        if(_trauma <= 0.0f)
        {
            _seed = GetSeed3();
            _shakeTime = 0.0f;
        }

        _trauma = Mathf.Clamp01(_trauma + t);
    }

    private void LateUpdate()
    {
        if (_trauma > 0.0f)
        {
            if (_trauma > 0.0f && ReduceTrauma)
                _trauma -= _traumaDampen * Time.deltaTime;

            float curTrauma = Mathf.Pow(Mathf.Clamp01(_trauma), _traumaPower);

            _shakeTime += Time.deltaTime * _shakeSpeed;

            Vector3 rotation = Vector3.zero;

            rotation.x = _maxRotation.x * curTrauma * (Mathf.PerlinNoise(_shakeTime + _seed.x, _seed.x * 10.0f) - 0.5f) * 2.0f;
            rotation.y = _maxRotation.y * curTrauma * (Mathf.PerlinNoise(_shakeTime + _seed.y, _seed.y * 10.0f) - 0.5f) * 2.0f;
            rotation.z = _maxRotation.z * curTrauma * (Mathf.PerlinNoise(_shakeTime + _seed.z, _seed.z * 10.0f) - 0.5f) * 2.0f;

            transform.localRotation = transform.localRotation * Quaternion.Euler(rotation);
        }

        ReduceTrauma = true;
    }

    private Vector3 GetSeed3()
    {
        return new Vector3(UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f) * 2.0f;
    }
}
