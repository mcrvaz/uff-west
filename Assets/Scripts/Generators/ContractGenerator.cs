using UnityEngine;
using System.Collections.Generic;

public class ContractGenerator : Generator<Contract> {

    private List<string> names = new List<string>(new string[] {
        "Alex", "John", "Dude", "Lebowski"
    });
    private List<string> crimes = new List<string>(new string[] {
        "Murder", "Robbery", "Murder and Robbery"
    });
    private List<string> rewards = new List<string>(new string[] {
        "$100", "$200", "$300", "$400"
    });

    private List<string> faces = new List<string>(new string[] {
        "face"
    });

    public ContractGenerator() { }

    private string GetRandomValue(List<string> list) {
        return list[Random.Range(0, list.Count)];
    }

    public Contract Generate() {
        return new Contract(
            name: GetRandomValue(names),
            crime: GetRandomValue(crimes),
            reward: GetRandomValue(rewards),
            facePrefab: GetRandomValue(faces)
        );
    }
}
