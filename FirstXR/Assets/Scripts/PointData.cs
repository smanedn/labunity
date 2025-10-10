[System.Serializable]
public class PointData
{
    public int currentCoins;
    public float points;

    public PointData(int coins, float pts)
    {
        currentCoins = coins;
        points = pts;
    }
}
