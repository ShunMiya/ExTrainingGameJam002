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
            // Pathが"Posion"の場合、mapListから3か所にマップを生成する
            if (Path == "Poision")
            {
                if (mapList.Count > 0)
                {
                    // ランダムな順序でマップリストをシャッフル
                    ShuffleMapList();

                    // 最大3つのマップを生成
                    int maxMaps = Mathf.Min(3, mapList.Count);
                    for (int i = 0; i < maxMaps; i++)
                    {
                        Transform selectedMap = mapList[i];
                        mapFactory.MapCreate(selectedMap.position, transform.parent, Path);
                    }
                }
            }
            // それ以外の場合は、mapListが空でない場合にランダムに一つの要素を選択してmapFactory.MapCreateに渡す
            else if (mapList.Count > 0)
            {
                int randomIndex = Random.Range(0, mapList.Count);
                Transform selectedMap = mapList[randomIndex];
                mapFactory.MapCreate(selectedMap.position, transform.parent, Path);
            }

            Destroy(gameObject);
        }

        // マップリストをランダムにシャッフルするメソッド
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