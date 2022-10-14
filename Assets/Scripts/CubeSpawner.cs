using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPooler _pooler;
    [SerializeField] private UIDocument _ui;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _cubeSpeed;
    [SerializeField] private float _cubeDistance;


    private float _lastSpawnTime;
    private Regex _regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
    private TextField _spawnField;
    private TextField _speedField;
    private TextField _distanceField;


    private void Start()
    {
        _spawnField = _ui.rootVisualElement.Q<TextField>("Spawn");
        _speedField = _ui.rootVisualElement.Q<TextField>("Speed");
        _distanceField = _ui.rootVisualElement.Q<TextField>("Distance");

        _spawnField.value = _spawnRate.ToString();
        _speedField.value = _cubeSpeed.ToString();
        _distanceField.value = _cubeDistance.ToString();

        _spawnField.RegisterValueChangedCallback(evt =>
        {
            if (!_regex.IsMatch(evt.newValue))
            {
                _spawnField.value = evt.previousValue;
            }
            _spawnRate = float.Parse(_spawnField.value);
        });
        _speedField.RegisterValueChangedCallback(evt =>
        {
            if (!_regex.IsMatch(evt.newValue))
            {
                _speedField.value = evt.previousValue;
            }
            _cubeSpeed = float.Parse(_speedField.value);
        });
        _distanceField.RegisterValueChangedCallback(evt =>
        {
            if (!_regex.IsMatch(evt.newValue))
            {
                _distanceField.value = evt.previousValue;
            }
            _cubeDistance = float.Parse(_distanceField.value);
        });
    }
    private void Update()
    {
        _lastSpawnTime += Time.deltaTime;
        if (_lastSpawnTime > _spawnRate)
        {
            _lastSpawnTime -= _spawnRate;
            var pooledObject = _pooler.GetPooledObject();

            if (pooledObject != null)
            {
                pooledObject.Spawn(transform.position, transform.rotation);

                if (pooledObject is IProjectile)
                {
                    IProjectile projectile = pooledObject as IProjectile;

                    projectile.SetDistance(_cubeDistance);
                    projectile.SetSpeed(_cubeSpeed);
                }
            }
        }
    }
}
