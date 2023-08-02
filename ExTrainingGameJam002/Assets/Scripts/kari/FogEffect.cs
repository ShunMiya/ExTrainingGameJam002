using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FogEffect : MonoBehaviour
{
    public Texture2D fogMaskTexture;
    private Material fogMaterial;

    void Start()
    {
        // カメラにQuadを表示するためのマテリアルを作成
        Shader shader = Shader.Find("Unlit/Transparent");
        fogMaterial = new Material(shader);
        fogMaterial.mainTexture = fogMaskTexture;
    }

    void OnPostRender()
    {
        // Quadを表示
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