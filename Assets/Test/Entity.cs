using UnityEngine;

namespace Test
{
    // The generic space entity.
    public abstract class Entity
    {
        // Position and velocity in space.
        public Vector2 Position { get; private set; }
        public Quaternion Rotation { get; private set; } 
        public Vector2 Velocity { get; private set; }

        protected OrbitEntityBehaviour m_behaviour;

        public Entity()
        {        
        }
        
        public void SetPosition(Vector2 pos)
        {
            Position = pos;
        }
        
        public void SetRotation(Quaternion rot)
        {
            Rotation = rot;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public void SetBehaviour(OrbitEntityBehaviour behaviour)
        {
            m_behaviour = behaviour;
        }

        public virtual void Update(float deltaTime)
        {
            m_behaviour?.Update(deltaTime);

            // Do integration over time.
            Position += Velocity * deltaTime;
        }

        public virtual void Render()
        {
 
        }
    }
}