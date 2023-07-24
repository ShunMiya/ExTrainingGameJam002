using UnityEngine;

namespace TitleSetSystem
{
    public class AspectSet : MonoBehaviour
    {
        void Start()
        {
            float targetAspectRatio = 16f / 9f; // �ڕW�̃A�X�y�N�g��i16:9�j
            float currentAspectRatio = (float)Screen.width / Screen.height;

            if (currentAspectRatio > targetAspectRatio)
            {
                // ��ʂ��ڕW�̃A�X�y�N�g��������ɍL���ꍇ
                int targetWidth = Mathf.RoundToInt(Screen.height * targetAspectRatio);
                Screen.SetResolution(targetWidth, Screen.height, false);
            }
            else
            {
                // ��ʂ��ڕW�̃A�X�y�N�g������c�ɒ����ꍇ
                int targetHeight = Mathf.RoundToInt(Screen.width / targetAspectRatio);
                Screen.SetResolution(Screen.width, targetHeight, false);
            }
        }
    }
}