using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public List<GameObject> prefabList;
    [SerializeField] private int maxObjectCount;
    private Dictionary<string, List<GameObject>> objects;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        objects = new Dictionary<string, List<GameObject>>();
        CreateCloneObjects();
    }
    private void CreateCloneObjects()
    {
        foreach(GameObject go in prefabList)
        {
            GameObject cloneParent = Instantiate(new GameObject(go.tag));
            cloneParent.transform.parent = transform;
            List<GameObject> cloneList = new List<GameObject>();
            for(int i = 0; i<maxObjectCount;i++)
            {
                GameObject instantiatedGO = Instantiate(go);
                instantiatedGO.transform.parent = cloneParent.transform;
                instantiatedGO.SetActive(false);
                cloneList.Add(instantiatedGO);
            }
            objects.Add(go.tag, cloneList);
        }
    }
    public GameObject GetNextAvailableGameObject(string tag)
    {
        if(objects.ContainsKey(tag))
        {
            List<GameObject> taggedGoList = objects[tag];
            foreach (GameObject go in taggedGoList)
            {
                if (!go.activeSelf)
                {
                    go.SetActive(true);
                    return go;
                }
            }
        }
        return null;
    }
}
