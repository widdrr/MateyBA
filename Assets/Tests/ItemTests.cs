using System.Collections;
using System.Xml.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ItemTests : InputTestFixture
{
    private Inventory _inventory;
    private SaveManager _saveManager;

    //[OneTimeSetUp] will be called only once before the tests begin
    //Use it when you need to set something up once to use in all tests.
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _inventory = TestHelpers.LoadScriptableObject<Inventory>("PlayerInventory");
        _saveManager = TestHelpers.LoadScriptableObject<SaveManager>("SaveManager");
    }

    //[SetUp] will automatically be called
    //before each [UnityTest]/[Test] test. Use this to write common setups.
    //If you need to yield instructions, use [UnitySetUp]
    [SetUp]
    public void TestSetup()
    {
        SceneManager.LoadScene("TestingEnvironment");
        _saveManager.state.pickups.Clear();
        _inventory.coins = 0;
    }

    [TearDown]
    //[TearDown] will automatically be called
    //after each [UnityTest]/[Test] test. Use this to write common cleaups.
    //If you need to yield instructions, use [UnitySetUp]
    public void TestTearDown()
    {
        _saveManager.state.pickups.Clear();
        _inventory.coins = 0;
    }

    [UnityTest]
    public IEnumerator Should_Pick_Up_Free_Item()
    {
        var player = TestHelpers.GetPlayer();
        var playerHealth = player.GetComponent<HealthManager>();
        var previousHealth = playerHealth.maxHealth;
        var item = TestHelpers.InstantiatePrefab<HealthUpgrade>("Boaba", new Vector3(2,2,0));
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        
        player.transform.position = new Vector3(2, 2, 0);
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        
        Assert.IsTrue(item == null);
        Assert.IsTrue(playerHealth.maxHealth > previousHealth);
    }

    [UnityTest]
    public IEnumerator Should_Purchase_Item_When_Affording()
    {
        var player = TestHelpers.GetPlayer();
        var playerHealth = player.GetComponent<HealthManager>();
        var previousHealth = playerHealth.maxHealth;
        var item = TestHelpers.InstantiatePrefab<HealthUpgrade>("BoabaCuBani", new Vector3(2, 2, 0));
        _inventory.coins = item.Price;
       
        yield return new WaitForFixedUpdate();    
        yield return new WaitForFixedUpdate();    
        
        player.transform.position = new Vector3(2, 2, 0);
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        
        Assert.IsTrue(item == null);
        Assert.IsTrue(_inventory.coins == 0);
        Assert.IsTrue(playerHealth.maxHealth > previousHealth);
    }

    [UnityTest]
    public IEnumerator Should_Not_Purchase_Item_When_Not_Affording()
    {
        var player = TestHelpers.GetPlayer();
        var playerHealth = player.GetComponent<HealthManager>();
        var previousHealth = playerHealth.maxHealth;
        var item = TestHelpers.InstantiatePrefab<HealthUpgrade>("BoabaCuBani", new Vector3(2, 2, 0));
        _inventory.coins = 0;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        
        player.transform.position = new Vector3(2, 2, 0);
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        
        Assert.IsTrue(item != null);
        Assert.IsTrue(playerHealth.maxHealth == previousHealth);
    }

    [UnityTest]
    public IEnumerator Should_Use_Reusable_Item_Multiple_Times()
    {
        int numTimes = 3;
        var player = TestHelpers.GetPlayer();
        var playerHealth = player.GetComponent<HealthManager>();
        var item = TestHelpers.InstantiatePrefab<HealthUpgrade>("BoabaReusable", new Vector3(2, 2, 0));

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        for (int i = 0; i < numTimes; i++)
        {
            var previousHealth = playerHealth.maxHealth;
            player.transform.position = new Vector3(2, 2, 0);

            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(item != null);
            Assert.IsTrue(playerHealth.maxHealth > previousHealth);

            player.transform.position = new Vector3(0, 0, 0);

            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
        }
    }

    [UnityTest]
    public IEnumerator Should_Purchase_Reusable_Item_While_Affording()
    {
        int numTimes = 3;
        var player = TestHelpers.GetPlayer();
        var playerHealth = player.GetComponent<HealthManager>();
        var item = TestHelpers.InstantiatePrefab<HealthUpgrade>("BoabaCuBaniReusable", new Vector3(2, 2, 0));
        int previousHealth;
        _inventory.coins = item.Price * numTimes;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        for (int i = 0; i < numTimes; i++)
        {
            var previousCoins = _inventory.coins;
            previousHealth = playerHealth.maxHealth;
            player.transform.position = new Vector3(2, 2, 0);

            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(item != null);
            Assert.IsTrue(playerHealth.maxHealth > previousHealth);
            Assert.IsTrue(_inventory.coins < previousCoins);

            player.transform.position = new Vector3(0, 0, 0);

            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
        }

        previousHealth = playerHealth.maxHealth;
        player.transform.position = new Vector3(2, 2, 0);

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        Assert.IsTrue(item != null);
        Assert.IsTrue(playerHealth.maxHealth == previousHealth);
        Assert.IsTrue(_inventory.coins < item.Price);

    }

    [UnityTest]
    public IEnumerator Non_Player_Sould_Not_Pick_Up_Item()
    {
        var item = TestHelpers.InstantiatePrefab<HealthUpgrade>("Boaba", new Vector3(2, 2, 0));
        var itemCollider = item.GetComponent<Collider2D>();
        var enemy = TestHelpers.InstantiatePrefab<GenericEnemyController>("Slime", new Vector3(4, 4, 0));
        var enemyCollider = enemy.GetComponent<Collider2D>();

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        enemy.transform.position = new Vector3(2, 2, 0);

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        Assert.IsTrue(item != null);
        Assert.IsTrue(enemyCollider.IsTouching(itemCollider));

    }
}
