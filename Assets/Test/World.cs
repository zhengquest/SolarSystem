using UnityEngine;

namespace Test
{
    public class World
    {
        private Entity m_entity;
        private Entity m_planet;

        private float m_timeStepAccumulator = 0.0f;
        private float m_fixedTimeStep = 0.016f;

        public World()
        {
            m_planet = new Entity();
            m_planet.SetPosition(new Vector2(0, 2));
            
            m_entity = new Entity();
            m_entity.SetPosition(new Vector2(0, 1));
            m_entity.SetVelocity(new Vector2(2f, 0));
            m_entity.SetBehaviour(new AutonomousEntityBehaviour(m_planet, m_entity, m_fixedTimeStep));
        }
        
        public void Update(float deltaTime)
        {
            m_timeStepAccumulator += deltaTime;

            for (; m_timeStepAccumulator > m_fixedTimeStep; m_timeStepAccumulator -= m_fixedTimeStep)
            {
                m_planet.Update(m_fixedTimeStep);
                m_entity.Update(m_fixedTimeStep);
            }
        }

        public void Render()
        {
            m_planet.Render();
            m_entity.Render();
        }
    }
}