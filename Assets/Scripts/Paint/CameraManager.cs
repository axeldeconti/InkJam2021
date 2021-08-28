using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _bgCam;

    [SerializeField] private GameObject _camEffect;

    [SerializeField] private GameObject _camEffect1;

    private void Start()
    {
        StartCoroutine(TestCorout());
    }

    private IEnumerator TestCorout()
    {
        _camEffect.SetActive(false);

        yield return null;

        _camEffect.SetActive(true);
        _camEffect1.SetActive(false);

        yield return null;

        _camEffect1.SetActive(true);
    }
}
