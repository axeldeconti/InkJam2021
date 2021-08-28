using UnityEngine;

[ExecuteInEditMode]
public class MetaballCameraEffect : MonoBehaviour
{
    static Material _material = null;

    /// <summary>
    /// Blur iterations - larger number means more blur
    /// </summary>
    [SerializeField] private int _iterations = 3;

    /// <summary>
    /// Blur spread for each iteration. Lower values
    /// give better looking blur, but require more iterations to
    /// get large blurs. Value is usually between 0.5 and 1.0
    /// </summary>
    [SerializeField] private float _blurSpread = 0.6f;

    /// <summary>
    /// The blur iteration shader.
    /// Basically it just takes 4 texture samples and averages them.
    /// By applying it repeatedly and spreading out sample locations
    /// we get a Gaussian blur approximation.
    /// </summary>
    [SerializeField] private Shader _blurShader = null;

    [SerializeField] private Material _cutOutMaterial = null;

    [SerializeField] private Camera _bgCamera = null;

    private RenderTexture _bgTargetTexture;

    private RenderTexture _buffer;

    private RenderTexture _buffer2;

    public bool test = false;

    protected void Start()
    {
        //Disable if the shader can't run on the users graphics card
        if (!_blurShader || !Material.shader.isSupported)
        {
            enabled = false;
            return;
        }

        //Disable if the shader can't run on the users graphics card
        if (!_cutOutMaterial.shader.isSupported)
        {
            enabled = false;
            return;
        }
    }

    /// <summary>
    /// Performs one blur iteration
    /// </summary>
    public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
    {
        float off = 0.5f + iteration * _blurSpread;

        Graphics.BlitMultiTap(source, dest, Material,
                               new Vector2(-off, -off),
                               new Vector2(-off, off),
                               new Vector2(off, off),
                               new Vector2(off, -off)
        );
    }

    /// <summary>
    /// Downsamples the texture to a quarter resolution
    /// </summary>
    private void DownSample4x(RenderTexture source, RenderTexture dest)
    {
        float off = 1.0f;

        Graphics.BlitMultiTap(source, dest, Material,
                               new Vector2(-off, -off),
                               new Vector2(-off, off),
                               new Vector2(off, off),
                               new Vector2(off, -off)
        );
    }

    /// <summary>
    /// Called by the camera to apply the image effect
    /// </summary>
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        int rtW = source.width / 4;
        int rtH = source.height / 4;

        _buffer = RenderTexture.GetTemporary(rtW, rtH, 0);

        //Copy source to the 4x4 smaller texture
        DownSample4x(source, _buffer);

        //Blur the small texture
        for (int i = 0; i < _iterations; i++)
        {
            _buffer2 = RenderTexture.GetTemporary(rtW, rtH, 0);
            FourTapCone(_buffer, _buffer2, i);
            RenderTexture.ReleaseTemporary(_buffer);
            _buffer = _buffer2;
        }

        //Background
        Graphics.Blit(_bgTargetTexture, destination);

        //Water
        Graphics.Blit(_buffer, destination, _cutOutMaterial);

        RenderTexture.ReleaseTemporary(_buffer);
    }

    private void OnEnable()
    {
        if (Screen.width > 0 && Screen.height > 0)
        {
            _bgTargetTexture = new RenderTexture(Screen.width, Screen.height, 16);
            _bgCamera.targetTexture = _bgTargetTexture;
        }
    }

    protected void OnDisable()
    {
        if (_material)
        {
            DestroyImmediate(_material);
        }
    }

    protected Material Material
    {
        get
        {
            if (_material == null)
            {
                _material = new Material(_blurShader);
                _material.hideFlags = HideFlags.DontSave;
            }

            return _material;
        }
    }
}