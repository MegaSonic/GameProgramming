using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NewEditModeTest {

    public GameObject enemyToTest;

	[Test]
	public void NewEditModeTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator BasicDamageTest() {
        var go = new GameObject();
        Enemy e = go.AddComponent<Enemy>();
        e.maxHealth = 100;
        e.currentHealth = 100;
        e.basePhysicalDefense = 0;
        e.bonusPhysicalDefense = 0;
        e.baseDodgeChance = 0;
        e.bonusDodgeChance = 0;

        GameObject dummy = new GameObject();
        e.Damage(dummy, DamageType.PHYSICAL, 10);

        Assert.AreEqual(e.GetCurrentHealth(), 90);

        /*
        e.Damage(dummy, DamageType.PHYSICAL, 100);

        Assert.IsNull(e);
        */

		yield return null;
	}

    [UnityTest]
    public IEnumerator EvasionTest()
    {
        var go = new GameObject();
        Enemy e = go.AddComponent<Enemy>();
        e.maxHealth = 10000;
        e.currentHealth = 10000;
        e.basePhysicalDefense = 0;
        e.bonusPhysicalDefense = 0;
        e.baseDodgeChance = 30;
        e.bonusDodgeChance = 0;

        GameObject dummy = new GameObject();

        for (int i = 0; i < 10000; i++)
        {
            e.Damage(dummy, DamageType.PHYSICAL, 1);
        }


        Assert.IsTrue(e.GetCurrentHealth() < 3300 && e.GetCurrentHealth() > 2700);
        Debug.Log(e.GetCurrentHealth());

        yield return null;
    }

    [UnityTest]
    public IEnumerator HealingTest()
    {
        var go = new GameObject();
        Enemy e = go.AddComponent<Enemy>();
        e.maxHealth = 100;
        e.currentHealth = 100;
        e.basePhysicalDefense = 0;
        e.bonusPhysicalDefense = 0;
        e.baseDodgeChance = 0;
        e.bonusDodgeChance = 0;

        GameObject dummy = new GameObject();

        e.Damage(dummy, DamageType.PHYSICAL, 50);

        e.Heal(100);

        Assert.AreEqual(e.GetCurrentHealth(), 100);
        
        yield return null;
    }
}
