using UnityEngine;

public class DoubleDamagePowerup : TargetController {

    public int numberOfShots;

    void OnMouseDown() {
        //TO DO
        duelController.RegisterPlayerDoubleDamage();
        DestroySelf();
    }

    public new void OnEnemyMouseDown() {
        //TO DO
        duelController.RegisterEnemyDoubleDamage();
        DestroySelf();
    }

}
