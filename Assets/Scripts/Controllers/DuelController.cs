﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class DuelController : MonoBehaviour {

    public float timeLimit = 5;
    public Image background;
    public Animator countdownAnimator, endingAnimator;

    private bool duelFinished;
    private StatisticsController stats;
    private DuelCharacterController player;
    private Player loadedPlayer;
    private EnemyCharacterController enemy;
    private Enemy loadedEnemy;
    private DuelCharacterController winner;
    private DuelTimeController timer;
    private DialogController beginningDialog, victoryDialog, defeatDialog;
    private List<SpeechText> playerDialogs, enemyDialogs;
    private ObjectSpawner[] spawners;

    void Awake() {
        GameController.Instance.SaveGame();

        LoadPlayer();
        LoadEnemy();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<DuelCharacterController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCharacterController>();
        beginningDialog = GameObject.FindGameObjectWithTag("BeginningDialog").GetComponent<DialogController>();
        victoryDialog = GameObject.FindGameObjectWithTag("VictoryDialog").GetComponent<DialogController>();
        defeatDialog = GameObject.FindGameObjectWithTag("DefeatDialog").GetComponent<DialogController>();
        stats = GameObject.FindObjectOfType<StatisticsController>();
        timer = GameObject.FindObjectOfType<DuelTimeController>();
        spawners = GameObject.FindObjectsOfType<ObjectSpawner>();

        GetDuel();
        SetEnemy(this.loadedEnemy);
        SetPlayer(this.loadedPlayer);
        EvaluateTimer();
        SetDialogs(playerDialogs, enemyDialogs);
    }

    private void LoadPlayer() {
        loadedPlayer = GameController.Instance.GetPlayer();
        Instantiate(Resources.Load<GameObject>("Characters/" + loadedPlayer.prefab));
    }

    private void LoadEnemy() {
        loadedEnemy = GameController.Instance.GetEnemy();
        Instantiate(Resources.Load<GameObject>("Characters/" + loadedEnemy.prefab));
    }

    private void GetDuel() {
        var duel = GameController.Instance.GetDuel();
        timeLimit = duel.timeLimit;
        background.sprite = Resources.Load<Sprite>("Backgrounds/" + duel.background);
        foreach (var spawner in spawners) {
            switch (spawner.type) {
                case ObjectSpawner.Type.Target:
                    spawner.minTime = duel.targetMinTime;
                    spawner.maxTime = duel.targetMaxTime;
                    break;
                case ObjectSpawner.Type.Evade:
                    spawner.minTime = duel.evadeMinTime;
                    spawner.maxTime = duel.evadeMaxTime;
                    break;
                case ObjectSpawner.Type.Powerup:
                    spawner.minTime = duel.powerupMinTime;
                    spawner.maxTime = duel.powerupMaxTime;
                    break;
            }
        }
    }

    private void SetEnemy(Enemy e) {
        if (e.characterName != null) {
            enemy.characterName = e.characterName;
        }
        enemy.damage = e.damage;
        enemy.health = e.health;
        enemy.minTimeToClick = e.minTimeToClick;
        enemy.maxTimeToClick = e.maxTimeToClick;
        enemyDialogs = e.dialogs;
    }

    private void SetPlayer(Player p) {
        player.characterName = p.characterName;
        player.damage = p.damage;
        player.health = p.health;
        playerDialogs = p.dialogs;
    }

    private bool IsNullOrEmpty(List<SpeechText> list) {
        return list == null || list.Count == 0;
    }

    private void SetDialogs(List<SpeechText> playerDialogs, List<SpeechText> enemyDialogs) {
        if (IsNullOrEmpty(playerDialogs) || IsNullOrEmpty(enemyDialogs)) {
            return;
        }

        bool playerHasNext = true, enemyHasNext = true;
        SpeechText currentDialog;

        var playerEnumerator = playerDialogs.GetEnumerator();
        var enemyEnumerator = enemyDialogs.GetEnumerator();
        playerEnumerator.MoveNext();
        enemyEnumerator.MoveNext();

        bool turn = false; //if true player starts talking
        bool playerIsStuck;
        bool enemyIsStuck;

        while (playerHasNext || enemyHasNext) {
            currentDialog = playerEnumerator.Current;
            while (turn && playerHasNext) {
                SetDialog(currentDialog);
                turn = currentDialog.sequential;
                playerHasNext = playerEnumerator.MoveNext();
                currentDialog = playerEnumerator.Current;
            }

            currentDialog = enemyEnumerator.Current;
            while (!turn && enemyHasNext) {
                SetDialog(currentDialog);
                turn = !currentDialog.sequential;
                enemyHasNext = enemyEnumerator.MoveNext();
                currentDialog = enemyEnumerator.Current;
            }

            playerIsStuck = (!turn && playerHasNext);
            enemyIsStuck = (turn && enemyHasNext);

            if ((playerIsStuck && !enemyHasNext) || (enemyIsStuck && !playerHasNext)) {
                turn = !turn;
            }
        }
    }

    private void SetDialog(SpeechText dialog) {
        switch (dialog.phase) {
            case SpeechText.Phase.Beginning:
                beginningDialog.dialogs.Add(dialog);
                break;
            case SpeechText.Phase.Victory:
                victoryDialog.dialogs.Add(dialog);
                break;
            case SpeechText.Phase.Defeat:
                defeatDialog.dialogs.Add(dialog);
                break;
        }
    }

    private void EvaluateTimer() {
        if (timeLimit <= 0) {
            //hide timer
            timer.gameObject.SetActive(false);
        }
    }

    private IEnumerator _StartDuelPhase() {
        countdownAnimator.SetTrigger("startCountdown");
        //var animation = countdownAnimator.GetCurrentAnimatorStateInfo(0);
        //yield return new WaitForSeconds(animation.length);
        yield return new WaitForSeconds(3); //workaround, for some reason its not working

        timer.StartTimer();
        foreach (var s in spawners) {
            s.enabled = true;
        }
    }

    public void StartDuelPhase() {
        StartCoroutine(_StartDuelPhase());
    }

    public void EndDuelPhase() {
        Time.timeScale = 1; //if duel end with bulletime activated
        player.revolver.gameObject.SetActive(false);

        timer.PauseTimer();
        foreach (var s in spawners) {
            Destroy(s);
        }
    }

    public void TargetExpired(TargetController target) {
        stats.targetsExpired++; //statistics
        //if the target that the enemy was aiming expired
        //he should pick a new one
        if (target == enemy.selectedTarget) {
            enemy.SelectTarget();
        }
    }

    public void EndDuelEditor() {
        StartCoroutine(EndDuel());
    }

    public IEnumerator EndDuel() {
        EndDuelPhase();

        stats.timeElapsed = timer.currentTime; //statistics
        stats.timeRemaining = timeLimit - timer.currentTime; //statistics
        winner = player.health > enemy.health ? player : enemy;

        endingAnimator.SetTrigger("ending");
        yield return new WaitForSeconds(2);

        if (winner == player) {
            victoryDialog.enabled = true;
        } else {
            defeatDialog.enabled = true;
        }

    }

    public void EndDuelScene() {
        if (duelFinished) {
            return;
        }
        duelFinished = true;
        GameController.Instance.stats = this.stats;
        GameController.Instance.EndDuel(winner);
    }

    private void RegisterShot(DuelCharacterController source, DuelCharacterController destiny) {
        var damage = ApplyDoubleDamage(source);
        if (destiny.TakeDamage(damage) <= 0) {
            StartCoroutine(EndDuel());
        }
    }

    private float ApplyDoubleDamage(DuelCharacterController source) {
        var damage = source.damage;
        if (source.hasPowerup > 0) {
            damage *= DoubleDamagePowerup.damageFactor;
            source.hasPowerup--;
        }
        return damage;
    }

    private bool CanShoot() {
        return !player.revolver.isReloading;
    }

    public void RegisterEnemyShot() {
        stats.enemyTargetsHit++; //statistics
        enemy.Fire();
        RegisterShot(source: enemy, destiny: player);
    }

    public bool RegisterPlayerShot() {
        var canShoot = CanShoot();
        if (canShoot) {
            stats.playerTargetsHit++;
            player.Fire();
            RegisterShot(source: player, destiny: enemy);
        } else {
            //play empty sound
        }

        return canShoot;
    }

    public void RegisterEnemyDoubleDamage(DoubleDamagePowerup dd) {
        stats.enemyDoubleDamageHit++; //statistics
        enemy.Fire();
        enemy.hasPowerup = dd.numberOfShots;
    }

    public bool RegisterPlayerDoubleDamage(DoubleDamagePowerup dd) {
        stats.playerDoubleDamageHit++; //statistics
        player.Fire();
        player.hasPowerup = dd.numberOfShots;
        return true; //can always hit powerups, doesnt consume ammo
    }

    public void RegisterEnemyBulletTime(BulletTimePowerup bt) {
        bt.ResetTimeScale();
        stats.enemyBulletTimeHit++; //statistics
        enemy.Fire();
        bt.SetBulletTime(bt.enemySlowFactor);
    }

    public bool RegisterPlayerBulletTime(BulletTimePowerup bt) {
        bt.ResetTimeScale();
        stats.playerBulletTimeHit++;
        player.Fire();
        bt.SetBulletTime(bt.playerSlowFactor);
        return true; //can always hit powerups, doesnt consume ammo
    }

    public void RegisterEnemyEvasion(EvasionTargetController ev) {
        ev.SetInvulnerable(enemy);
    }

    public bool RegisterPlayerEvasion(EvasionTargetController ev) {
        ev.SetInvulnerable(player);
        return true; //can always evade, doesnt consume ammo
    }

}
