using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Statistics {

    public int playerTargetsHit;
    public int playerDoubleDamageHit;
    public int playerBulletTimeHit;
    public int playerShots;
    public float playerTimeBetweenShots;
    public float playerShotsPerSecond;

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
