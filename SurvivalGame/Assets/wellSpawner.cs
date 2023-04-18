using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class wellSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawns;
    Transform currentSpawn;

    public Transform well;

    // Start is called before the first frame update
    void Start()
    {

        currentSpawn = spawns[Random.Range(1, 6)];
        Instantiate(well, currentSpawn.position, currentSpawn.rotation);
    }
}
