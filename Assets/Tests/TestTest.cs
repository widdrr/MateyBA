using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class NewTestScript
{

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        SceneManager.LoadScene("TestingEnvironment");
        yield return new WaitForSeconds(2);
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().UpdateAnimationAndMove(new Vector3(1, 0, 0));
        yield return new WaitForFixedUpdate();
        Assert.AreNotEqual(player.transform.position, Vector3.zero);
        SceneManager.LoadScene("TestingEnvironment");
    }
}
