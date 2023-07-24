using UnityEngine;

namespace TitleSetSystem
{
    public class AspectSet : MonoBehaviour
    {
        void Start()
        {
            float targetAspectRatio = 16f / 9f; // 目標のアスペクト比（16:9）
            float currentAspectRatio = (float)Screen.width / Screen.height;

            if (currentAspectRatio > targetAspectRatio)
            {
                // 画面が目標のアスペクト比よりも横に広い場合
                int targetWidth = Mathf.RoundToInt(Screen.height * targetAspectRatio);
                Screen.SetResolution(targetWidth, Screen.height, false);
            }
            else
            {
                // 画面が目標のアスペクト比よりも縦に長い場合
                int targetHeight = Mathf.RoundToInt(Screen.width / targetAspectRatio);
                Screen.SetResolution(Screen.width, targetHeight, false);
            }
        }
    }
}