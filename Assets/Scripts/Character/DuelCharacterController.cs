using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DuelCharacterController : MonoBehaviour {

    public float health;
    public float damage;

    public Text healthText;

    void Awake() {
        UpdateUIHealth();
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

    public void UpdateUIHealth() {
        healthText.text = health.ToString();
    }

}
