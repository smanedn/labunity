using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float posX;
    public float posY;
    public float posZ;
    public string sceneName;

    public PlayerData(Vector3 position, string scene)
    {
        posX = position.x;
        posY = position.y;
        posZ = position.z;
        sceneName = scene;
    }
}
