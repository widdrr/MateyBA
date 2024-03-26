using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ItemTests : InputTestFixture
{
    private Keyboard _keyboard;

    //[OneTimeSetUp] will be called only once before the tests begin
    //Use it when you need to set something up once to use in all tests.
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _keyboard = InputSystem.AddDevice<Keyboard>();
    }

    //[SetUp] will automatically be called
    //before each [UnityTest] test. Use this to write common setups.
    //If you need to yield instructions, use [UnitySetUp]
    [SetUp]
    public void TestSetup()
    {
        SceneManager.LoadScene("TestingEnvironment");
    }

    [UnityTest]
    public IEnumerator ItemTest()
    {
        var player = TestHelpers.GetPlayer();
        var inventory = TestHelpers.LoadScriptableObject<Inventory>("Inventory");
        var item = TestHelpers.InstantiatePrefab<HealthUpgrade>("Boaba", new Vector3(2,2,0));
        player.transform.position = new Vector3(2, 2, 0);
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(item == null, true);
    }
}
