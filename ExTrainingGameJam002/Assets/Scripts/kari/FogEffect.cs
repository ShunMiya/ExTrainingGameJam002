using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FogEffect : MonoBehaviour
{
    public Texture2D fogMaskTexture;
    private Material fogMaterial;

    void Start()
    {
        // �J������Quad��\�����邽�߂̃}�e���A�����쐬
        Shader shader = Shader.Find("Unlit/Transparent");
        fogMaterial = new Material(shader);
        fogMaterial.mainTexture = fogMaskTexture;
    }

    void OnPostRender()
    {
        // Quad��\��
        GL.PushMatrix();
        GL.LoadOrtho();
        fogMaterial.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.TexCoord2(0, 0);
        GL.Vertex3(0, 0, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(0, 1, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(1, 1, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(1, 0, 0);
        GL.End();
        GL.PopMatrix();
    }
}