using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DuelCharacterController : MonoBehaviour {

    public float health;
    public float damage;
    public int hasPowerup; //tells how many poweruped shots are left
    public Text healthText;
    public RevolverCylinderController revolver;

    void Awake() {
        UpdateUIHealth();
    }

    private void UpdateUIHealth() {
        healthText.text = health.ToString();
    }

    public float TakeDamage(float damage) {
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
