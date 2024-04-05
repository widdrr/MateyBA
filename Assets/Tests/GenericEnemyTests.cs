using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GenericEnemyTests : InputTestFixture
{
    static string[] enemies = {
        "Rat",
        "Dog",
        "Boschetar",
        "Slime",
        "Bat",
        "Boss",
        "Dragon"
    };
    [SetUp]
    public void TestSetup()
    {
        SceneManager.LoadScene("TestingEnvironment");    
    }

    [UnityTest]
    public IEnumerator Should_Not_Collide_With_Another_Enemy([ValueSource("enemies")] string name1, [ValueSource("enemies")] string name2)
    {
        var enemy1 = TestHelpers.InstantiatePrefab<GenericEnemyController>(name1, new Vector3(4, 4, 0));
        var enemy2 = TestHelpers.InstantiatePrefab<GenericEnemyController>(name2, new Vector3(2, 2, 0));
        var enemy1Col = enemy1.GetComponent<Collider2D>() ? enemy1.GetComponent<Collider2D>() : enemy1.GetComponentInChildren<Collider2D>();
        var enemy2Col = enemy2.GetComponent<Collider2D>() ? enemy2.GetComponent<Collider2D>() : enemy2.GetComponentInChildren<Collider2D>();
        var circle = new GameObject("Circle");
        circle.gameObject.layer = 6;
        circle.AddComponent<CircleCollider2D>().radius = 1.0f;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        circle.transform.position = new Vector3(2f, 2f, 0);
        enemy1.transform.position = new Vector3(2f, 2f, 0);

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        Assert.IsNotNull(enemy1Col);
        Assert.IsNotNull(enemy2Col);
        Assert.IsTrue(!enemy1Col.IsTouching(enemy2Col));
        Assert.IsTrue(enemy1Col.IsTouching(circle.GetComponent<CircleCollider2D>()));
        Assert.IsTrue(enemy2Col.IsTouching(circle.GetComponent<CircleCollider2D>()));
    }

    [UnityTest]
    public IEnumerator Should_Not_Be_Damaged_By_Player([ValueSource("enemies")] string name1)
    {
        var enemy = TestHelpers.InstantiatePrefab<GenericEnemyController>(name1, new Vector3(4, 4, 0));
        var enemyCol = enemy.GetComponent<Collider2D>() ? enemy.GetComponent<Collider2D>() : enemy.GetComponentInChildren<Collider2D>();
        var player = TestHelpers.GetPlayer();

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        var enemyHealth = enemy.GetComponent<HealthManager>() ? enemy.GetComponent<HealthManager>() : enemy.GetComponentInChildren<HealthManager>();
        var previousHealth = enemyHealth.CurrentHealth;
        player.transform.position = new Vector3(4, 4, 0);

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(enemyHealth.CurrentHealth, previousHealth);
        Assert.IsTrue(enemyCol.IsTouching(player.GetComponent<Collider2D>()));
    }
}
