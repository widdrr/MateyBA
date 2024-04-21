using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class LLMTests : InputTestFixture
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
    public IEnumerator PlayerCanPickUpFreeItem()
    {
        var player = TestHelpers.GetPlayer();
        var boaba = TestHelpers.InstantiatePrefab<HealthUpgrade>("Boaba", player.transform.position + Vector3.right);

        yield return null;

        Assert.AreEqual(boaba, null);
    }

    [UnityTest]
    public IEnumerator PlayerCanPurchaseItemsWhenHeAffordIt()
    {
        var player = TestHelpers.GetPlayer();
        var inventory = TestHelpers.LoadScriptableObject<Inventory>("PlayerInventory");
        inventory.coins = 100;

        var boabaCuBani = TestHelpers.InstantiatePrefab<HealthUpgrade>("BoabaCuBani", player.transform.position + Vector3.right);

        yield return null;

        Assert.AreEqual(boabaCuBani, null);
    }

    [UnityTest]
    public IEnumerator PlayerCantPurchaseItemsWhenHeCantAffordIt()
    {
        var player = TestHelpers.GetPlayer();
        var inventory = TestHelpers.LoadScriptableObject<Inventory>("PlayerInventory");
        inventory.coins = 0;

        var boabaCuBani = TestHelpers.InstantiatePrefab<HealthUpgrade>("BoabaCuBani", player.transform.position + Vector3.right);

        yield return null;

        Assert.AreNotEqual(boabaCuBani, null);
    }

    [UnityTest]
    public IEnumerator PlayerCanUseReusableItemsMultipleTimes()
    {
        var player = TestHelpers.GetPlayer();
        var boabaReusable = TestHelpers.InstantiatePrefab<HealthUpgrade>("BoabaReusable", player.transform.position + Vector3.right);

        yield return null;

        Assert.AreEqual(boabaReusable, null);

        yield return null;

        Assert.AreEqual(boabaReusable, null);
    }

    [UnityTest]
    public IEnumerator PlayerCanPurchaseReusableItemWhileAffording()
    {
        var player = TestHelpers.GetPlayer();
        var inventory = TestHelpers.LoadScriptableObject<Inventory>("PlayerInventory");
        inventory.coins = 100;

        var boabaCuBaniReusable = TestHelpers.InstantiatePrefab<HealthUpgrade>("BoabaCuBaniReusable", player.transform.position + Vector3.right);

        yield return null;

        Assert.AreEqual(boabaCuBaniReusable, null);
    }

    [UnityTest]
    public IEnumerator NPCCannotPickUpItems()
    {
        var slime = TestHelpers.InstantiatePrefab<SlimeController>("Slime", Vector3.zero);
        var boaba = TestHelpers.InstantiatePrefab<HealthUpgrade>("Boaba", slime.transform.position + Vector3.right);

        yield return null;

        Assert.AreNotEqual(boaba, null);
    }
}