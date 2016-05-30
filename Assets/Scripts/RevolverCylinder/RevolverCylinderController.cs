using UnityEngine;
using System.Collections;

public class RevolverCylinderController : MonoBehaviour {

    public int index = 1; //index 0 should be the empty cylinder
    public bool isReloading;

    private SpriteRenderer[] bullets;

    void Awake() {
        bullets = GetComponentsInChildren<SpriteRenderer>();
    }

    void OnMouseDown() {
        print(index);
        //GAMBIARRA
        if (index == bullets.Length) {
            index--;
            isReloading = true;
        }
        //GAMBIARRA

        if (isReloading) {
            Reload();
        }
    }

    public void Fire() {
        if (index < bullets.Length) {
            bullets[index].enabled = false;
            index++;
        } else {
            index--;
            isReloading = true;
        }
    }

    public void Reload() {
        bullets[index].enabled = true;
        index--;

        //index 0 is the empty cylinder
        if (index == 0) {
            index++;
            isReloading = false;
        }
    }

}
