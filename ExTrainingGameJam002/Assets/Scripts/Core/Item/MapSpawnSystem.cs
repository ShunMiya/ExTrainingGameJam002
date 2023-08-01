using System.Collections.Generic;
using UnityEngine;

namespace StageSystem.Map
{
    public class MapSpawnSystem : MonoBehaviour
    {
        [SerializeField] private MapFactory mapFactory;
        [SerializeField] private List<Transform> mapList;

        public string Path;

        private void Start()
        {
            // Path��"Posion"�̏ꍇ�AmapList����3�����Ƀ}�b�v�𐶐�����
            if (Path == "Poision")
            {
                if (mapList.Count > 0)
                {
                    // �����_���ȏ����Ń}�b�v���X�g���V���b�t��
                    ShuffleMapList();

                    // �ő�3�̃}�b�v�𐶐�
                    int maxMaps = Mathf.Min(3, mapList.Count);
                    for (int i = 0; i < maxMaps; i++)
                    {
                        Transform selectedMap = mapList[i];
                        mapFactory.MapCreate(selectedMap.position, transform.parent, Path);
                    }
                }
            }
            // ����ȊO�̏ꍇ�́AmapList����łȂ��ꍇ�Ƀ����_���Ɉ�̗v�f��I������mapFactory.MapCreate�ɓn��
            else if (mapList.Count > 0)
            {
                int randomIndex = Random.Range(0, mapList.Count);
                Transform selectedMap = mapList[randomIndex];
                mapFactory.MapCreate(selectedMap.position, transform.parent, Path);
            }

            Destroy(gameObject);
        }

        // �}�b�v���X�g�������_���ɃV���b�t�����郁�\�b�h
        private void ShuffleMapList()
        {
            for (int i = 0; i < mapList.Count - 1; i++)
            {
                int randomIndex = Random.Range(i, mapList.Count);
                Transform temp = mapList[i];
                mapList[i] = mapList[randomIndex];
                mapList[randomIndex] = temp;
            }
        }
    }
}