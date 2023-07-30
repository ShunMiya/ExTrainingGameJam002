using System.Collections.Generic;
using UnityEngine;

namespace StageSystem.Map
{
    public class MapSpawnSystem : MonoBehaviour
    {
        [SerializeField] private MapFactory mapFactory;
        [SerializeField] private List<Transform> mapList;

        private void Start()
        {
            // mapList‚ª‹ó‚Å‚È‚¢ê‡Aƒ‰ƒ“ƒ_ƒ€‚Éˆê‚Â‚Ì—v‘f‚ð‘I‘ð‚µ‚ÄmapFactory.MapCreate‚É“n‚·
            if (mapList.Count > 0)
            {
                int randomIndex = Random.Range(0, mapList.Count);
                Transform selectedMap = mapList[randomIndex];
                mapFactory.MapCreate(selectedMap.position, transform.parent);
            }

            Destroy(gameObject);
        }
    }
}