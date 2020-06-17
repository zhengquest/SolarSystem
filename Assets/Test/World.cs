using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class World
    {
        private const float m_fixedTimeStep = 0.016f;
        private float m_timeStepAccumulator = 0.0f;
        private List<BaseEntity> m_entities;
        
        public World()
        {
            Time.timeScale = 0.5f;
            m_entities = new List<BaseEntity>(7);
            
            StarBaseEntity sun = new StarBaseEntity();
            sun.SetPosition(new Vector2(0, 2));
            m_entities.Add(sun);

            PlanetBaseEntity autonomousPlanetBase = new PlanetBaseEntity();
            autonomousPlanetBase.SetPosition(new Vector2(0, 1));
            autonomousPlanetBase.SetVelocity(new Vector2(2, 0));
            autonomousPlanetBase.SetRenderParameter(0.05f, Color.green, 3);
            AutonomousEntityBehaviour autonomousBehaviour = new AutonomousEntityBehaviour(sun, autonomousPlanetBase);
            autonomousBehaviour.CalculateOrbit(m_fixedTimeStep, true);
            autonomousPlanetBase.SetBehaviour(autonomousBehaviour);
            m_entities.Add(autonomousPlanetBase);

            PlanetBaseEntity autonomousPlanet1 = new PlanetBaseEntity();
            autonomousPlanet1.SetPosition(new Vector2(0, 0.5f));
            autonomousPlanet1.SetVelocity(new Vector2(2, 0));
            autonomousPlanet1.SetRenderParameter(0.05f, Color.yellow, 5);
            AutonomousEntityBehaviour autonomousBehaviour1 = new AutonomousEntityBehaviour(sun, autonomousPlanet1);
            autonomousBehaviour1.CalculateOrbit(m_fixedTimeStep, true);
            autonomousPlanet1.SetBehaviour(autonomousBehaviour1);
            m_entities.Add(autonomousPlanet1);
            
            PlanetBaseEntity autonomousPlanet2 = new PlanetBaseEntity();
            autonomousPlanet2.SetPosition(new Vector2(0, 0));
            autonomousPlanet2.SetVelocity(new Vector2(2, 0));
            autonomousPlanet2.SetRenderParameter(0.05f, Color.magenta, 7);
            AutonomousEntityBehaviour autonomousBehaviour2 = new AutonomousEntityBehaviour(sun, autonomousPlanet2);
            autonomousBehaviour2.CalculateOrbit(m_fixedTimeStep, true);
            autonomousPlanet2.SetBehaviour(autonomousBehaviour2);
            m_entities.Add(autonomousPlanet2);
            
            PlanetBaseEntity unstablePlanetBase = new PlanetBaseEntity();
            unstablePlanetBase.SetPosition(new Vector2(0, 0));
            unstablePlanetBase.SetVelocity(new Vector2(1, 0));
            unstablePlanetBase.SetRenderParameter(0.05f, Color.white, 10);
            OrbitEntityBehaviour orbitBehaviour = new OrbitEntityBehaviour(sun, unstablePlanetBase);
            orbitBehaviour.CalculateOrbit(m_fixedTimeStep, true);
            unstablePlanetBase.SetBehaviour(orbitBehaviour);
            m_entities.Add(unstablePlanetBase);
            
            PlanetBaseEntity unstablePlanet1 = new PlanetBaseEntity();
            unstablePlanet1.SetPosition(new Vector2(1, 3));
            unstablePlanet1.SetVelocity(new Vector2(-1f, 1));
            unstablePlanet1.SetRenderParameter(0.05f, Color.white, 10);
            OrbitEntityBehaviour orbitBehaviour1 = new OrbitEntityBehaviour(sun, unstablePlanet1);
            orbitBehaviour1.CalculateOrbit(m_fixedTimeStep, true);
            unstablePlanet1.SetBehaviour(orbitBehaviour1);
            m_entities.Add(unstablePlanet1);
            
            PlanetBaseEntity unstablePlanet2 = new PlanetBaseEntity();
            unstablePlanet2.SetPosition(new Vector2(-1, 3));
            unstablePlanet2.SetVelocity(new Vector2(-1f, -1));
            unstablePlanet2.SetRenderParameter(0.05f, Color.white, 10);
            OrbitEntityBehaviour orbitBehaviour2 = new OrbitEntityBehaviour(sun, unstablePlanet2);
            orbitBehaviour2.CalculateOrbit(m_fixedTimeStep, true);
            unstablePlanet2.SetBehaviour(orbitBehaviour2);
            m_entities.Add(unstablePlanet2);
            
            PlayerBaseEntity playerBase = new PlayerBaseEntity();
            playerBase.SetPosition(new Vector2(0, -1));
            playerBase.SetVelocity(new Vector2(0, 0.01f));
            playerBase.SetBehaviour(new PlayerEntityBehaviour(sun, playerBase));
            m_entities.Add(playerBase);
        }
        
        public void Update(float deltaTime)
        {
            m_timeStepAccumulator += deltaTime;

            for (; m_timeStepAccumulator > m_fixedTimeStep; m_timeStepAccumulator -= m_fixedTimeStep)
            {
                foreach (BaseEntity entity in m_entities)
                {
                    entity.Update(m_fixedTimeStep);
                }
            }
        }

        public void Render()
        {
            foreach (BaseEntity entity in m_entities)
            {
                entity.Render();
            }
        }
    }
}