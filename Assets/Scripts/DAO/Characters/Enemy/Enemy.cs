﻿using System.Xml;
using System.Xml.Serialization;

public class Enemy:Character {

    public float minTimeToClick, maxTimeToClick;

    public Enemy() { }

    public Enemy(string characterName, float damage, float minTimeToClick, float maxTimeToClick, float health = 100) {
        this.characterName = characterName;
        this.damage = damage;
        this.minTimeToClick = minTimeToClick;
        this.maxTimeToClick = maxTimeToClick;
        this.health = health;
    }
}