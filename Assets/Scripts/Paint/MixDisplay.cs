using UnityEngine;

public class MixDisplay : MonoBehaviour
{
    [SerializeField] private Material _mat;

    [SerializeField] private Transform _spawner;

    [SerializeField] private Transform _spawner1;

    private void Update()
    {
        _mat.SetVector("_Pos", _spawner.position);
        _mat.SetVector("_Pos1", _spawner1.position);
    }
}