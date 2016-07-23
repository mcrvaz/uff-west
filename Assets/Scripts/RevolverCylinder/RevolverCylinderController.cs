using UnityEngine;

public class RevolverCylinderController : MonoBehaviour {

    [HideInInspector]
    public bool isReloading;

    private int index;
    private SpriteRenderer[] bullets;
    private Animator animator;

    void Awake() {
        var goBullets = GameObject.FindGameObjectsWithTag("Bullet");
        bullets = new SpriteRenderer[goBullets.Length];
        for (int i = 0; i < goBullets.Length; i++) {
            bullets[i] = goBullets[i].GetComponent<SpriteRenderer>();
        }
        animator = GetComponent<Animator>();
    }

    void OnMouseDown() {
        if (isReloading) {
            Reload();
        }
    }

    public void Fire() {
        //return; //skips revolver
        bullets[index].enabled = false;
        if (index < bullets.Length - 1) {
            index++;
        } else {
            isReloading = true;
            animator.SetBool("reloading", isReloading);
        }
    }

    public void Reload() {
        bullets[index].enabled = true;
        index--;
        if (index < 0) {
            index++;
            isReloading = false;
            animator.SetBool("reloading", isReloading);
        }
    }

}
