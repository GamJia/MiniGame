using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] GameObject pipe; // pipe 프리팹을 지정

    void Start()
    {
        StartCoroutine(SpawnPipeCoroutine());
    }

    IEnumerator SpawnPipeCoroutine()
    {
        while (true)
        {
            // 파이프 생성 위치 설정 (x: 4, y: -1~3 사이 랜덤, z: 0)
            Vector3 spawnPosition = new Vector3(4, Random.Range(0, 2f), 0);

            // 파이프 생성 및 3초 후 자동 삭제
            GameObject newPipe = Instantiate(pipe, spawnPosition, Quaternion.identity);
            Destroy(newPipe, 5f); // 3초 후 파이프 삭제

            // 1초 대기
            yield return new WaitForSeconds(2.5f);
        }
    }
}
