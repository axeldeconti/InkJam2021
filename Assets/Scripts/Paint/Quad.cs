using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Quad : MonoBehaviour
{
    private int _prepareUVID = Shader.PropertyToID("_PrepareUV");
    private int _positionID = Shader.PropertyToID("_PainterPosition");
    private int _hardnessID = Shader.PropertyToID("_Hardness");
    private int _strengthID = Shader.PropertyToID("_Strength");
    private int _radiusID = Shader.PropertyToID("_Radius");
    private int _blendOpID = Shader.PropertyToID("_BlendOp");
    private int _colorID = Shader.PropertyToID("_PainterColor");
    private int _textureID = Shader.PropertyToID("_MainTex");
    private int _posCountID = Shader.PropertyToID("_PositionsCount");
    private int _positionsID = Shader.PropertyToID("_Positions");

    private CommandBuffer _command;
    private Material _mat;
    private List<Vector4> _posArray;

    public Transform[] transforms;

    [Range(0, 1)]
    public float hardness;
    public float strength;
    public float radius;
    public Color color;

    private void Start()
    {
        _mat = GetComponent<MeshRenderer>().material;

        _posArray = new List<Vector4>();

        _command = new CommandBuffer();
        _command.name = "CommmandBuffer - " + gameObject.name;
    }

    private void Update()
    {
        _posArray.Clear();

        for (int i = 0; i < transforms.Length; i++)
        {
            _posArray.Add(transforms[i].position);
        }

        _mat.SetFloat(_prepareUVID, 0);
        _mat.SetInt(_posCountID, _posArray.Count);
        _mat.SetVectorArray(_positionsID, _posArray);
        _mat.SetFloat(_hardnessID, hardness);
        _mat.SetFloat(_strengthID, strength);
        _mat.SetFloat(_radiusID, radius);
        _mat.SetColor(_colorID, color);
    }
}