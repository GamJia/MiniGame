using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBird;

namespace FlappyBird
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] GameObject pipe; // pipe Prefab을 지정
        [SerializeField] GameObject ground; // ground Prefab을 지정

        private Coroutine pipeCoroutine = null;
        private Coroutine groundCoroutine = null;

        void Start()
        {
            if (groundCoroutine == null)
            {
                groundCoroutine = StartCoroutine(SpawnGroundCoroutine());
            }
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if(pipeCoroutine==null)
                {
                    pipeCoroutine = StartCoroutine(SpawnPipeCoroutine());
                }
                
            }
        }


        IEnumerator SpawnPipeCoroutine()
        {
            while (true)
            {
                Vector3 spawnPosition = new Vector3(4, Random.Range(0, 2f), 0);

                GameObject newPipe = Instantiate(pipe, spawnPosition, Quaternion.identity, transform);
                Destroy(newPipe, 5f);

                yield return new WaitForSeconds(2.5f);
            }
        }

        IEnumerator SpawnGroundCoroutine()
        {
            while (true)
            {
                Vector3 spawnPosition = new Vector3(0, -4, 0);

                GameObject newGround = Instantiate(ground, spawnPosition, Quaternion.identity, transform);
                Destroy(newGround, 5f);

                yield return new WaitForSeconds(3f);
            }
        }
    }

}
