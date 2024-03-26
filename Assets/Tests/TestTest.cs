using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class NewTestScript : InputTestFixture
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        SceneManager.LoadScene("TestingEnvironment");
        yield return new WaitForSeconds(2);
        var player = GameObject.FindGameObjectWithTag("Player");
        Press(keyboard.wKey);
        yield return new WaitForFixedUpdate();
        Assert.AreNotEqual(player.transform.position, Vector3.zero);
        SceneManager.LoadScene("TestingEnvironment");
    }
}
