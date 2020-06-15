using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMain : MonoBehaviour
{
    Test.World m_world;
    
    // Start is called before the first frame update
    void Start()
    {
        m_world = new Test.World();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the world
        m_world.Update(UnityEngine.Time.deltaTime);

        // Render the world using debug rendering.
        m_world.Render();
    }
}
