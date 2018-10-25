using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nihon
{
    public class Master_SpawnController : MonoBehaviour
    {
        public GameObject[] spawnerLocations;
        public GameObject[] Blocks;
        private bool noBlanks;

        // Use this for initialization
        void Start()
        {
            Spawner();
        }

        /*
         * Spawner randomly generates block pieces and spawns them to a spawn point
         * this will repeat for the amount of spawn points are added
         * this will also not double up on pieces, it will repeat for x-amount of spawn points 
         * but once all blocks have been used up, spawn location will be left blank
         * because of this, equal amount of pieces should be added to game manager of equal spawn locations (Unless level desires this)
         * 
         * Random generates spawnLocation and Block piece > If spawnLocation has no child object (block) it will place a block in that location.
         */
        public void Spawner()
        {
            int i = 0;
            while (i < Blocks.Length)
            {
                GameObject randomSpawnerLocation = spawnerLocations[Random.Range(0, spawnerLocations.Length)];

                if (randomSpawnerLocation.transform.childCount == 0)
                {
                    Instantiate(Blocks[i], randomSpawnerLocation.transform);
                    i++;
                }
                Debug.Log("DEBUG: LOOP COUNTER = " + i); // Can delete this later, checking for performance loop
            }
        }
    }
}