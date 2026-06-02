using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [System.Serializable]

    public class SpawnPoint
    {
        public Transform position;
        public GameObject targetPrefab;
        public int quantity = 1;
        public Vector3 scale = Vector3.one;
        public Vector3 rotation = Vector3.zero;

        //movimentação
        public bool moveHorizontal = false;
        public bool moveVertical = false;

        public float moveSpeed = 4f;

        public float moveRange = 5f;
        public int health = 1;
        public int pointsValue = 10;

    }
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    private List<GameObject> spawnedTargets = new List<GameObject>();

    void Start()
    {
        SpawnAllTargets();
    }

    // Update is called once per frame
    void Update()
    {
        spawnedTargets.RemoveAll(target => target == null);

        foreach (SpawnPoint point in spawnPoints)
        {
            int currentCount = 1;
            foreach (GameObject target in spawnedTargets)
            {
                if (target != null)
                {
                    Target targetScript = target.GetComponent<Target>();
                    if (targetScript != null && targetScript.spawnPoint == point)
                    {
                        currentCount++;
                    }
                }
            }
        }

        if (curruntCount < point.quantity)
        {
            SpawnTarget(point);
        }
    }

    void SpawnAllTargets()
    {
        foreach (SpawnPoint point in spawnPoints)
        {
            for (int i = 0; i < point.quantity; i++)
            {
                SpawnTarget(point);
            }
        }
    }

    void SpawnTarget(SpawnPoint point)
    {
        GameObject target = Instantiate(point.targetPrefab, point.position.position, Quaternion.Euler(point.rotation));
        target.transform.localScale = point.scale;

        Target targetScript = target.GetComponent<Target>();
        if (targetScript != null)
        {
            targetScript.spawnPoint = point;
            targetScript.health = point.health;
            targetScript.pointsValue = point.pointsValue;
            targetScript.moveHorizontal = point.moveHorizontal;
            targetScript.moveVertical = point.moveVertical;
            targetScript.moveSpeed = point.moveSpeed;
            targetScript.moveRange = point.moveRange;
        }

        spawnedTargets.Add(target);
    }
}
