using UnityEngine;
using System.Collections.Generic;

public class ContractGenerator : Generator<Contract> {

    private List<string> names = new List<string>(new string[] {
        "Butch Cassidy", "Sundance Kid", "\"Shotgun\" John Collins",
        "Bob Dalton", "Wyatt Earp", "Robert Newton Ford",
        "Billy the Kid", "Wild Bill", "Calamity Jane",
        "Blondie", "Papaco", "John Galt", "Andrew Ryan",
        "Geralt", "Tom Bombadil", "L'Etranger",
        "Roland Deschain", "Molly Millions", "Dmitri Karamazov",
        "Rodion Raskolnikov", "Paul Atreides", "Snake Plissken",
        "Hari Seldon", "Alex DeLarge", "Joe",
        "\"Dirty\" Harry", "Josey Wales", "Philo Beddoe",
        "El Indio", "Django", "Jesse James",
        "John Wayne", "Clinton Eastwood", "Trinity",
        "Ramón Rojo", "Douglas Mortimer", "Angel Eyes",
        "Tuco", "Sergio Leone", "Ennio Morricone",
        "Jed Cooper", "Harmonica", "\"Commander\" Shepard",
        "Adam Jensen","Tiber Septim","Gordon Freeman",
        "Niko Bellic", "Jack of Blades", "Heisenberg",
        "Shodan", "Joe \"Blondie\" Monco", "Mifune"
    });
    private List<string> crimes = new List<string>(new string[] {
        "Murder", "Robbery", "Vandalism",
        "Rape", "Genocide", "War Crimes",
        "Army Deserter", "Aggression", "Arson",
        "Burglary", "Treason", "Kidnapping"
    });
    private List<string> rewards = new List<string>(new string[] {
        "$100", "$200", "$300", "$400", "$500", "$1000"
    });

    private List<string> faces = new List<string>(new string[] {
        ContractConstants.BOSS_FACE, ContractConstants.BAD_DEAD_FACE,
        ContractConstants.ENEMY_FACE, ContractConstants.MASKED_BANDIT_FACE,
        ContractConstants.OLD_GUY_FACE, ContractConstants.PADRE_FACE
    });

    public ContractGenerator() { }

    private string GetRandomValue(List<string> list) {
        return list[Random.Range(0, list.Count)];
    }

    public Contract Reset() {
        return Generate();
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
