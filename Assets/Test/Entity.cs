using UnityEngine;

namespace Test
{
    // The generic space entity.
    public class Entity
    {
        // Position and velocity in space.
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }

        private EntityBehaviour m_behaviour;

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

        public void SetBehaviour(EntityBehaviour behaviour)
        {
            m_behaviour = behaviour;
        }
        
        public void Update(float deltaTime)
        {
            m_behaviour?.Update(this, deltaTime);

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
            }
        }
    }
}