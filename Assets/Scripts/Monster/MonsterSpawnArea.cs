namespace Knight
{
    public class MonsterSpawnArea
    {
        public readonly int xMin;
        public readonly int xMax;
        public readonly int yPos;

        public MonsterSpawnArea(int min, int max, int y)
        {
            xMin = min;
            xMax = max;
            yPos = y;
        }
    }
}