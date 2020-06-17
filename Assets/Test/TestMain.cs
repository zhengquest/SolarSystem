using UnityEngine;

namespace Test
{
    public class TestMain : MonoBehaviour
    {
        private World m_world;
    
        // Start is called before the first frame update
        private void Start()
        {
            m_world = new World();
        }

        // Update is called once per frame
        private void Update()
        {
            // Update the world
            m_world.Update(Time.deltaTime);

            // Render the world using debug rendering.
            m_world.Render();
        }
    }
}
