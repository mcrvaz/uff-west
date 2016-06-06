using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DuelCharacterController : MonoBehaviour {

    public float health;
    public float damage;
    public bool invulnerable;
    public Text healthText;
    public RevolverCylinderController revolver;
    [HideInInspector]
    public int hasPowerup; //tells how many empowered shots are left

    void Awake() {
        UpdateUIHealth();
    }

    private void UpdateUIHealth() {
        healthText.text = health.ToString();
    }

    public float TakeDamage(float damage) {
        if (invulnerable) {
            return health;
        }
        health -= damage;
        UpdateUIHealth();
        return health;
    }

    public float RecoverHealth(float health) {
        this.health += health;
        UpdateUIHealth();
        return this.health;
    }

}
