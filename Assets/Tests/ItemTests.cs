using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ItemTests : InputTestFixture
{
    private Keyboard _keyboard;
    private Inventory _inventory;
    private SaveManager _saveManager;

    //[OneTimeSetUp] will be called only once before the tests begin
    //Use it when you need to set something up once to use in all tests.
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Debug.Log("setup");
        _keyboard = InputSystem.AddDevice<Keyboard>();
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
        _inventory.coins = item.GetComponent<Pickup>().Price;
       
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
}
