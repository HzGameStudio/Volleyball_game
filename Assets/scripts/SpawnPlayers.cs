using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject ballPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        Vector3 randomPos = new Vector3(Random.Range(minX, maxX), 26f, Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Vector3 cords = new Vector3(21f, 40f, 0f);
            PhotonNetwork.Instantiate(ballPrefab.name, cords, Quaternion.identity);
        }
    }
}
