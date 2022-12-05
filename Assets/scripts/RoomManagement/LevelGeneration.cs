using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] public GameObject Startroom; // Startroom prefab
    [SerializeField] public GameObject[] rooms; // array of all possible rooms
    [SerializeField] public GameObject Exit; // The exit of a level


    [SerializeField] private GameObject player; // The player

    private readonly int maxRoomsPerLevel = 10;
    private int spawnedRooms = 0;
    private Vector3 previousRoomExit;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {

        // Spawn the room where the player starts at the start of a level
        GameObject spawnRoom = Instantiate(Startroom, new Vector3(7.75f, 20f, 0), Quaternion.identity);
        player.transform.position = spawnRoom.transform.Find("PlayerSpawn").transform.position;
        previousRoomExit = spawnRoom.transform.Find("Exit").transform.position;
        // Debug.Log(new Vector3(24, 20f, 0) - new Vector3(7.75f, 20f, 0));
        for (float i = 0; i < maxRoomsPerLevel; i++)
        {
            // Spawn a random room
            GameObject room = Instantiate(rooms[Random.Range(0, rooms.Length)], previousRoomExit, Quaternion.identity);
            pos = previousRoomExit + (room.transform.position - room.transform.Find("Entrance").transform.position);
            room.transform.position = pos;
            previousRoomExit = room.transform.Find("Exit").transform.position;
            spawnedRooms++;
        }
        // Spawn the exit of the level
        GameObject exit = Instantiate(Exit, previousRoomExit, Quaternion.identity);
        pos = previousRoomExit + (exit.transform.position - exit.transform.Find("Entrance").transform.position);
        exit.transform.position = pos;
    }

}
// 