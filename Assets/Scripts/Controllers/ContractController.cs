using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContractController : MonoBehaviour {

    public Text enemyName, reward, crime;
    public Image enemyFace;

    void Awake() {
        GetContract();
    }

    private void GetContract() {
        var contract = GameController.Instance.GetContract();
        enemyName.text = contract.name;
        reward.text = contract.reward;
        crime.text = contract.crime;
    }

}
