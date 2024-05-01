using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SaveTests : InputTestFixture
{
    private Inventory _inventory;
    private SaveManager _saveManager;
    private SaveManager.GameState _previousState;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _inventory = TestHelpers.LoadScriptableObject<Inventory>("PlayerInventory");
        _saveManager = TestHelpers.LoadScriptableObject<SaveManager>("SaveManager");
        _previousState = _saveManager.state;
        _saveManager.Clear();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    { 
        _saveManager.state = _previousState;
        _saveManager.Save();
    }
  

    [SetUp]
    public void TestSetup()
    {
        SceneManager.LoadScene("TestingEnvironment");
    }

    [UnityTest]
    public IEnumerator Should_Save_And_Load_State()
    {
        var player = TestHelpers.GetPlayer();
        Vector3 savePosition = new(Random.Range(-6f, 6f), Random.Range(-4f, 4f), 0f);
        var savePoint = TestHelpers.InstantiatePrefab<SavePoint>("SavePoint", savePosition);
        savePosition += new Vector3(0f, 0.5f, 0f);
        var coins = Mathf.FloorToInt(Random.Range(0, 100));
        var potions = Mathf.FloorToInt(Random.Range(0, 25));
        _inventory.coins = coins;
        _inventory.potions = potions;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        player.transform.position = savePosition;

        yield return new WaitForSeconds(0.5f);

        Vector3 storedPosition = _saveManager.state.playerPosition;

        Assert.LessOrEqual((storedPosition - savePosition).sqrMagnitude, 0.001f);
        Assert.AreEqual(_saveManager.state.coins, coins);
        Assert.AreEqual(_saveManager.state.potions, potions);

        player.transform.position = new Vector3(0f, 0f, 0f);
        _inventory.coins = 0;
        _inventory.potions = 0;

        SceneManager.LoadScene("TestingEnvironment");
        _saveManager.Load();

        yield return new WaitForSeconds(0.5f);

        player = TestHelpers.GetPlayer();
        Assert.LessOrEqual((player.transform.position - savePosition).sqrMagnitude, 0.001f);
        Assert.AreEqual(_inventory.coins, coins);
        Assert.AreEqual(_inventory.potions, potions);
    }
}