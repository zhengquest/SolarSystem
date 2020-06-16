using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class World
    {
        private float m_timeStepAccumulator = 0.0f;
        private float m_fixedTimeStep = 0.016f;
        private List<Entity> m_entities;
        
        public World()
        {
            m_entities = new List<Entity>(5);
            
            Entity sun = new StarEntity();
            sun.SetPosition(new Vector2(0, 2));
            m_entities.Add(sun);

            Entity autonomousPlanet = new PlanetEntity();
            autonomousPlanet.SetPosition(new Vector2(0, 1));
            autonomousPlanet.SetVelocity(new Vector2(2f, 0));
            autonomousPlanet.SetBehaviour(
                new AutonomousEntityBehaviour(sun, autonomousPlanet, m_fixedTimeStep));
            m_entities.Add(autonomousPlanet);

            Entity unstablePlanet = new PlanetEntity();
            unstablePlanet.SetPosition(new Vector2(0, 0));
            unstablePlanet.SetVelocity(new Vector2(1f, 0));
            unstablePlanet.SetBehaviour(
                new OrbitEntityBehaviour(sun, unstablePlanet, m_fixedTimeStep));
            m_entities.Add(unstablePlanet);
        }
        
        public void Update(float deltaTime)
        {
            m_timeStepAccumulator += deltaTime;

            for (; m_timeStepAccumulator > m_fixedTimeStep; m_timeStepAccumulator -= m_fixedTimeStep)
            {
                foreach (Entity entity in m_entities)
                {
                    entity.Update(m_fixedTimeStep);
                }
            }
        }

        public void Render()
        {
            foreach (Entity entity in m_entities)
            {
                entity.Render();
            }
        }
    }
}