using UnityEngine;
using System.Collections;

public class RevolverCylinderController : MonoBehaviour {

    [HideInInspector]
    public bool isReloading;

    private int index = 0; //index 0 should be the empty cylinder
    private SpriteRenderer[] bullets;
    private Animator animator;

    void Awake() {
        var goBullets = GameObject.FindGameObjectsWithTag("Bullet");
        bullets = new SpriteRenderer[goBullets.Length];
        for (int i = 0; i < goBullets.Length; i++) {
            bullets[i] = goBullets[i].GetComponent<SpriteRenderer>();
        }
        print(bullets.Length);
        animator = GetComponent<Animator>();
    }

    void OnMouseDown() {
        //GAMBIARRA
        if (index == bullets.Length) {
            index--;
            isReloading = true;
            animator.SetBool("blinking", isReloading);
        }
        //GAMBIARRA

        if (isReloading) {
            Reload();
        }
    }

    public void Fire() {
        //return; //skips bullets

        if (index < bullets.Length) {
            bullets[index].enabled = false;
            index++;
        } else {
            index--;
            isReloading = true;
            animator.SetBool("blinking", isReloading);
        }
    }

    public void Reload() {
        bullets[index].enabled = true;
        index--;

        //index 0 is the empty cylinder
        if (index == 0) {
            index++;
            isReloading = false;
            animator.SetBool("blinking", isReloading);
        }
    }

}
