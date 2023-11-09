using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    int wave = 0; // The Wave number
    int waveAddtionTime = 2; // How much more time will be added per new wave
    int mobsPerWave = 5; // amount of mobs per wave
    float waveDuration; // How long the wave will last
    float waveDurationTick; // The tick of the wave duration
    int setMobsToSpawn; // Set mobs to spawn per wave
    int mobsSpawned; // How many mobs have been spawned
    int startMobsToSpawn; // The amount of mobs that will spawn at the start
    int waveTimeSpilt = 10; // This number is divied by the wave duration so we can get how often mobs should spawn
    float mobSpawnIntervals; // How often mobs spawn
    float mobSpawnTick; // The tick of mobSpawnInteravls

    // All Spawn Locations
    List<Transform> SpawnLocations = new List<Transform>();
    public GameObject basicEnemy;
    Transform enemiesTransform;

    private void Awake()
    {
        enemiesTransform = GameObject.Find("Enemies").transform;
        foreach (Transform child in GameObject.Find("Spawn Locations").transform)
        {
            SpawnLocations.Add(child);
        }
    }

    // Handles variable sets / control
    void StartNewWave()
    {
        wave++;
        waveDuration = 10 + waveAddtionTime * wave;
        waveDurationTick = waveDuration;
        setMobsToSpawn = wave * mobsPerWave;
        startMobsToSpawn = (int)Mathf.Floor(setMobsToSpawn / 5F);
        mobSpawnIntervals = waveDuration / waveTimeSpilt;
        mobSpawnTick = mobSpawnIntervals;
        mobsSpawned = 0;

        //print("Started new wave " + wave);
        SpawnStartMobs();
    }

    // Calls to spawn the amount of start mobs
    void SpawnStartMobs()
    {
        //print("Called to spawn start mobs");
        SpawnMobs(startMobsToSpawn);
    }

    // Figure out how much mobs to spawn per mob interval
    void SpawnMobsDuringWave()
    {
        int mobsLeftInWaveToSpawn = setMobsToSpawn - mobsSpawned;
        int mobsToSpawn = (int)Mathf.Floor(Mathf.Clamp(mobsLeftInWaveToSpawn / waveTimeSpilt, 1, (float)mobsLeftInWaveToSpawn));
        SpawnMobs(mobsToSpawn);
    }

    // Tick mob spawn
    void TickMobSpawnTimer(float delta)
    {
        mobSpawnTick -= delta;
        if (mobSpawnTick <= 0)
        {
            SpawnMobsDuringWave();
            mobSpawnTick = mobSpawnIntervals;
        }
    }

    // Tick wave duration
    void TickWaveDuration(float delta)
    {
        waveDurationTick -= Time.deltaTime;
        if (waveDurationTick <= 0)
        {
            //print("Wave " + wave + " Complete.");
            StartNewWave();
        }
    }

    void SpawnMobs(int amount)
    {
        if (mobsSpawned >= setMobsToSpawn) { return; }

        for (int i = 0; i < amount; i++)
        {
            Vector3 newSpawnLocation = SpawnLocations[Random.Range(0, SpawnLocations.Count)].position;
            Instantiate(basicEnemy, newSpawnLocation , Quaternion.identity, enemiesTransform);
        }

        //print("Spawned a total of " + amount + " mobs (" + (mobsSpawned + 1) + "/" + setMobsToSpawn + ")");
        mobsSpawned = Mathf.Clamp(mobsSpawned + amount, 0, setMobsToSpawn);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartNewWave();
    }

    // Update is called once per frame
    void Update()
    {
        TickMobSpawnTimer(Time.deltaTime);
        TickWaveDuration(Time.deltaTime);
    }
}
