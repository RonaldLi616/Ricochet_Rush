using UnityEngine;
using System.Collections;

public class ClayPot : MonoBehaviour
{
    // Basket
    #region 
    [Header("Basket")]
    [SerializeField] private Basket basket;
    #endregion

    // Obstacle Spawner
    #region 
    [Header("Obstacle Spawner")]
    [SerializeField] private GameObject obstacleSpawner;
    private GameObject GetObstacleSpawner()
    {
        if (obstacleSpawner == null) { Debug.Log("Missing Obstacle Spawner Reference!"); }
        return obstacleSpawner;
    }

    [SerializeField]private bool spawnerAvailable = true;
    public void SetSpawnerAvailable(bool isAvailable) { spawnerAvailable = isAvailable; }
    public bool IsSpawnerAvailable() { return spawnerAvailable; }
    #endregion

    // Obstacle
    #region 
    [Header("Obstacle Prefab")]
    [SerializeField] private GameObject[] obstacles = new GameObject[0];
    private GameObject GetObstacle()
    {
        GameObject obstacle = obstacles[0];
        if (obstacle == null) { Debug.Log("Missing Obstacle Prefab Reference!"); }
        return obstacle;
    }
    #endregion

    // Method
    #region 
    // Instantiate Obstacle
    #region 
    private void InstantiateObstacle()
    {
        if (!spawnerAvailable) { return; }
        spawnerAvailable = false;
        GameObject go = Instantiate(GetObstacle(), GetObstacleSpawner().transform);
        go.transform.localPosition = new Vector3(-0.2f, 1.5f, 0f);
        go.GetComponent<Plant>().SetClayPot(this);
        SpawnFromPot();
    }
    #endregion

    #endregion

    // Animation
    #region 
    // Spawn From Pot
    #region 
    private void SpawnFromPot()
    { 
        Vector3 finalScale = Vector3.zero;
        // Hash Table
        #region 
        Hashtable scaleUpObjectHash = new Hashtable();
        scaleUpObjectHash.Add("name", "ClayPot_" + this.gameObject.GetInstanceID() + "_ScaleFrom");
        scaleUpObjectHash.Add("scale", finalScale);
        scaleUpObjectHash.Add("easetype", iTween.EaseType.easeOutSine);
        scaleUpObjectHash.Add("time", 1f);
        scaleUpObjectHash.Add("onstart", "OnStartMoveFromAnimation_ClayPotObstacle");
        scaleUpObjectHash.Add("onstarttarget", this.gameObject);

        #endregion

        iTween.ScaleFrom(GetObstacleSpawner(), scaleUpObjectHash);
    }
    #endregion

    private void OnStartMoveFromAnimation_ClayPotObstacle()
    { 
        Vector3 startPosition = Vector3.zero;
        // Hash Table
        #region 
        Hashtable moveFromObjectHash = new Hashtable();
        moveFromObjectHash.Add("name", "ClayPot_" + this.gameObject.GetInstanceID() + "_MoveFrom");
        moveFromObjectHash.Add("position", transform.TransformPoint(startPosition));
        moveFromObjectHash.Add("space", Space.Self);
        moveFromObjectHash.Add("easetype", iTween.EaseType.easeOutSine);
        moveFromObjectHash.Add("time", 1f);

        #endregion

        iTween.MoveFrom(GetObstacleSpawner(), moveFromObjectHash);
    }

    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            InstantiateObstacle();
        }
    }
}
