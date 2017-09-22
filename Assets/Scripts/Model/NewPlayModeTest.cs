using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NewPlayModeTest {

	[Test]
	public void NewPlayModeTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator PlaymodeDeathTest() {
        var go = new GameObject();
        Enemy e = go.AddComponent<Enemy>();
        e.maxHealth = 100;
        e.currentHealth = 100;
        e.basePhysicalDefense = 0;
        e.bonusPhysicalDefense = 0;
        e.baseDodgeChance = 0;
        e.bonusDodgeChance = 0;

        GameObject dummy = new GameObject();

        e.Damage(dummy, DamageType.PHYSICAL, 110);

        yield return new WaitForFixedUpdate();

        Assert.IsFalse(e);

        yield return null;
        
	}

    [UnityTest]
    public IEnumerator PlayerHealthTest()
    {
        var go = new GameObject();
        PlayerHealth health = go.AddComponent<PlayerHealth>();
        health.EnemyDespawned("test", go.transform);
        health.EnemyDespawned("test", go.transform);
        health.EnemyDespawned("test", go.transform);
        health.EnemyDespawned("test", go.transform);
        health.EnemyDespawned("test", go.transform);

        Assert.AreEqual(health.currentHealth, health.maxHealth - health.damageOnPassedEnemy * 5);

        yield return null;
    }

    [UnityTest]
    public IEnumerator BulletMotionTest()
    {
        var go = new GameObject();
        Bullet bullet = go.AddComponent<Bullet>();
        go.AddComponent<Rigidbody>();
        bullet.SetTimeTilDeath(10);


        yield return new WaitForFixedUpdate();

        Vector3 position = go.transform.position;

        bullet.Shoot(Vector3.forward, go, DamageType.PHYSICAL, 5, 20, 2);

        yield return new WaitForFixedUpdate();

        Vector3 newPosition = go.transform.position;

        Assert.AreNotEqual(position, newPosition);

        yield return null;
    }
}
