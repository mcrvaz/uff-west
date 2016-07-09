public class Statistics {

    public int playerTargetsHit;
    public int playerDoubleDamageHit;
    public int playerBulletTimeHit;
    public int playerShots;
    public int enemyShots;
    public float playerTimeBetweenShots;
    public float enemyTimeBetweenShots;
    public float playerShotsPerSecond;
    public float enemyShotsPerSecond;

    public Statistics() { }

    public Statistics(
        int playerTargetsHit, int playerDoubleDamageHit,
        int playerBulletTimeHit, int playerShots,
        float playerTimeBetweenShots, float playerShotsPerSecond
    ) {
        this.playerTargetsHit = playerTargetsHit;
        this.playerDoubleDamageHit = playerDoubleDamageHit;
        this.playerBulletTimeHit = playerBulletTimeHit;
        this.playerShots = playerShots;
        this.playerTimeBetweenShots = playerTimeBetweenShots;
        this.playerShotsPerSecond = playerShotsPerSecond;
    }

}
