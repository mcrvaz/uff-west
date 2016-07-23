using UnityEngine;
using System;
using System.Collections.Generic;

public class RevolverCylinderController : MonoBehaviour {

    public class SpriteNameComparer : IComparer<SpriteRenderer> {
        public int Compare(SpriteRenderer x, SpriteRenderer y) {
            return x.name.CompareTo(y.name);
        }
    }

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
        SortBullets(); //or they might not be in order
        animator = GetComponent<Animator>();
    }

    void OnMouseDown() {
        if (isReloading) {
            Reload();
        }
    }

    private void SortBullets() {
        SpriteNameComparer comparer = new SpriteNameComparer();
        Array.Sort(bullets, comparer);
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
