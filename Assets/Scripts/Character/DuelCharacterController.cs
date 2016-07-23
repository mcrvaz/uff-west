using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DuelCharacterController : MonoBehaviour {

    public string characterName;
    public float health;
    public float damage;
    public bool invulnerable;
    [HideInInspector]
    public int hasPowerup; //how many empowered shots are left
    [HideInInspector]
    public RevolverCylinderController revolver;

    protected Image healthBar;
    protected Animator animator;

    void Awake() {
        healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<Image>();
        revolver = GameObject.FindGameObjectWithTag("RevolverCylinder").GetComponent<RevolverCylinderController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start() {
        UpdateUIHealth();
    }

    private void UpdateUIHealth() {
        healthBar.fillAmount = health / 100;
    }

    public void Fire() {
        if (revolver != null) {
            revolver.Fire();
        }
        animator.SetTrigger("shooting");
    }

    public float TakeDamage(float damage) {
        if (invulnerable) {
            return health;
        }
        health -= damage;
        animator.SetTrigger("hit");
        UpdateUIHealth();
        return health;
    }

    public float RecoverHealth(float health) {
        this.health += health;
        UpdateUIHealth();
        return this.health;
    }

}
