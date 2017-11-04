using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager> {
    
    public delegate void PlayerFiredGun(Transform playerPosition, string name);
    public static PlayerFiredGun OnPlayerFiredGun;

    public delegate void EnemyReachedNode(string name, Transform transform, string path, int node);
    public static EnemyReachedNode OnEnemyReachedNode;

    public delegate void EnemySpawned(string name, Transform transform, string path);
    public static EnemySpawned OnEnemySpawned;

    public delegate void EnemyKilled(string name, string source, Transform transform);
    public static EnemyKilled OnEnemyKilled;

    public delegate void DamagedSomething(string damageDealer, string damageReceiver, DamageType type, int amount);
    public static DamagedSomething OnDamagedSomething;

    public delegate void EnemyHealed(string name, Transform transform, int amount);
    public static EnemyHealed OnEnemyHealed;

    public delegate void EnemyDodged(string name, Transform transform);
    public static EnemyDodged OnEnemyDodged;

    public delegate void EnemyDamaged(string name, string source, Transform transform, DamageType type, int amount);
    public static EnemyDamaged OnEnemyDamaged;

    public delegate void EnemyDespawned(string name, Transform transform);
    public static EnemyDespawned OnEnemyDespawned;

    public delegate void StartGameSave(ref Game game);
    public static StartGameSave OnStartGameSave;

    public delegate void LoadGameSave(Game game);
    public static LoadGameSave OnLoadGameSave;
}
