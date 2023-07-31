using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageSystem.Map
{
    public class MapFactory : MonoBehaviour
    {
        public void MapCreate(Vector2 position, Transform parent, string Path)
        {
            string prefabPath = Path;

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (prefab == null) Debug.Log("null");
            Instantiate(prefab, position, Quaternion.identity, parent);
        }
    }
}