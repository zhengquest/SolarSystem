using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    // The generic space entity.
    public class Entity
    {
        // Position and velocity in space.
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }

        private EntityOrbitBehaviour _mOrbitBehaviour;

        private List<Vector2> m_orbit;

        public Entity()
        {        
        }
        
        public void SetPosition(Vector2 pos)
        {
            Position = pos;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public void SetBehaviour(EntityOrbitBehaviour orbitBehaviour)
        {
            _mOrbitBehaviour = orbitBehaviour;
        }

        public void SetOrbit(IEnumerable<Vector2> orbit)
        {
            m_orbit = new List<Vector2>(orbit);
        }

        public void Update(float deltaTime)
        {
            _mOrbitBehaviour?.Update(deltaTime);

            // Do integration over time.
            Position += Velocity * deltaTime;
        }

        public void Render()
        {
            Vector3 pos = new Vector3(Position.x, Position.y, 0);
            
            // We assume this is stationary body (planet) if its velocity is too low.
            if (Velocity.magnitude < 0.01f)
            {
                float size = 0.1f;
                Vector3 up = new Vector3(0,1,0)*size;
                Vector3 right = new Vector3(1,0,0)*size;
                
                Debug.DrawLine(pos + up + right, pos - up + right, Color.yellow);
                Debug.DrawLine(pos - up + right, pos - up - right, Color.yellow);
                Debug.DrawLine(pos - up - right, pos + up - right, Color.yellow);
                Debug.DrawLine(pos + up - right, pos + up + right, Color.yellow);
            }
            else
            {
                Vector2 direction = Velocity.normalized;
                float size = 0.2f;
                Vector3 dirEnd = new Vector3(Position.x + direction.x*size, Position.y + direction.y*size, 0);
                Debug.DrawLine(pos, dirEnd, Color.white);
                
                RenderOrbit();
            }
        }
        
        private void RenderOrbit()
        {
            if (m_orbit.Count < 2)
            {
                throw new Exception("orbit vertices too few");
            }
            
            for (int i = 1; i < m_orbit.Count; i++)
            {
                Debug.DrawLine(m_orbit[i - 1], m_orbit[i], Color.blue);
            }
            
            Debug.DrawLine(m_orbit[m_orbit.Count - 1], m_orbit[0], Color.blue);
        }
    }
}