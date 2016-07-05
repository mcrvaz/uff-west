public class Statistics {

    public int playerTargetsHit;
    public int enemyTargetsHit;
    public int playerDoubleDamageHit;
    public int enemyDoubleDamageHit;
    public int playerBulletTimeHit;
    public int enemyBulletTimeHit;
    public int targetsExpired;
    public int playerShots;
    public int enemyShots;
    public float timeElapsed;
    public float timeRemaining;
    public float playerTimeBetweenShots;
    public float enemyTimeBetweenShots;
    public float playerShotsPerSecond;
    public float enemyShotsPerSecond;

    public Statistics() { }

    public Statistics(
        int playerTargetsHit, int enemyTargetsHit,
        int playerDoubleDamageHit, int enemyDoubleDamageHit,
        int playerBulletTimeHit, int enemyBulletTimeHit,
        int targetsExpired, int playerShots, int enemyShots,
        float timeElapsed, float timeRemaining,
        float playerTimeBetweenShots, float enemyTimeBetweenShots,
        float playerShotsPerSecond, float enemyShotsPerSecond
    ) {
        this.playerTargetsHit = playerTargetsHit;
        this.enemyTargetsHit = enemyTargetsHit;
        this.playerDoubleDamageHit = playerDoubleDamageHit;
        this.enemyDoubleDamageHit = enemyDoubleDamageHit;
        this.playerBulletTimeHit = playerBulletTimeHit;
        this.enemyBulletTimeHit = enemyBulletTimeHit;
        this.targetsExpired = targetsExpired;
        this.playerShots = playerShots;
        this.enemyShots = enemyShots;
        this.timeElapsed = timeElapsed;
        this.timeRemaining = timeRemaining;
        this.playerTimeBetweenShots = playerTimeBetweenShots;
        this.enemyTimeBetweenShots = enemyTimeBetweenShots;
        this.playerShotsPerSecond = playerShotsPerSecond;
        this.enemyShotsPerSecond = enemyShotsPerSecond;
    }
}
