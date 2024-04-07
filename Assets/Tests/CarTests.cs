using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CarTests : InputTestFixture
{
    private Mouse _mouse;   
    private Inventory _inventory;
    private Weapon _crowbar;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _mouse = InputSystem.AddDevice<Mouse>();
        _inventory = TestHelpers.LoadScriptableObject<Inventory>("PlayerInventory");
        _crowbar = TestHelpers.LoadScriptableObject<Weapon>("Crowbar");
    }

    [SetUp]
    public void TestSetup()
    {
        SceneManager.LoadScene("TestingEnvironment");
        _inventory.leftWeapon = _crowbar;
    }

    [UnityTest]
    public IEnumerator Should_Not_Destroy_Car_Below_Threshold()
    {
        var player = TestHelpers.GetPlayer();
        var car = TestHelpers.InstantiatePrefab<CarExplode>("GreenCarExploding", new Vector3(0, -1, 0));
        var playerController = player.GetComponent<PlayerController>();

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        var currentDamage = _inventory.leftWeapon.damage;
        _inventory.leftWeapon.damage = (int)car.DamageThreshold - 1;
    
        Press(_mouse.leftButton);

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        while(playerController.currentState == PlayerState.attacking)
        {
            yield return new WaitForFixedUpdate();
        }
        
        Assert.IsFalse(car == null);

        _inventory.leftWeapon.damage = currentDamage;
    }

    [UnityTest]
    public IEnumerator Should_Destroy_Car_At_Threshold()
    {
        var player = TestHelpers.GetPlayer();
        var car = TestHelpers.InstantiatePrefab<CarExplode>("GreenCarExploding", new Vector3(0, -1, 0));
        var playerController = player.GetComponent<PlayerController>();

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        var currentDamage = _inventory.leftWeapon.damage;
        _inventory.leftWeapon.damage = (int)car.DamageThreshold;
    
        Press(_mouse.leftButton.);

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        while(playerController.currentState == PlayerState.attacking)
        {
            yield return new WaitForFixedUpdate();
        }
        
        Assert.IsTrue(car == null);

        _inventory.leftWeapon.damage = currentDamage;
    }
    
}