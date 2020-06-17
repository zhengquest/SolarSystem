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
            m_entities = new List<Entity>(7);
            
            StarEntity sun = new StarEntity();
            sun.SetPosition(new Vector2(0, 2));
            m_entities.Add(sun);

            PlanetEntity autonomousPlanet = new PlanetEntity();
            autonomousPlanet.SetPosition(new Vector2(0, 1));
            autonomousPlanet.SetVelocity(new Vector2(2, 0));
            autonomousPlanet.SetRenderParameter(0.05f, Color.green, 3);
            AutonomousEntityBehaviour autonomousBehaviour = new AutonomousEntityBehaviour(sun, autonomousPlanet, m_fixedTimeStep);
            autonomousBehaviour.CalculateOrbit(true);
            autonomousPlanet.SetBehaviour(autonomousBehaviour);
            m_entities.Add(autonomousPlanet);

            PlanetEntity autonomousPlanet1 = new PlanetEntity();
            autonomousPlanet1.SetPosition(new Vector2(0, 0.5f));
            autonomousPlanet1.SetVelocity(new Vector2(2, 0));
            autonomousPlanet1.SetRenderParameter(0.05f, Color.yellow, 5);
            AutonomousEntityBehaviour autonomousBehaviour1 = new AutonomousEntityBehaviour(sun, autonomousPlanet1, m_fixedTimeStep);
            autonomousBehaviour1.CalculateOrbit(true);
            autonomousPlanet1.SetBehaviour(autonomousBehaviour1);
            m_entities.Add(autonomousPlanet1);
            
            PlanetEntity autonomousPlanet2 = new PlanetEntity();
            autonomousPlanet2.SetPosition(new Vector2(0, 0));
            autonomousPlanet2.SetVelocity(new Vector2(2, 0));
            autonomousPlanet2.SetRenderParameter(0.05f, Color.magenta, 7);
            AutonomousEntityBehaviour autonomousBehaviour2 = new AutonomousEntityBehaviour(sun, autonomousPlanet2, m_fixedTimeStep);
            autonomousBehaviour2.CalculateOrbit(true);
            autonomousPlanet2.SetBehaviour(autonomousBehaviour2);
            m_entities.Add(autonomousPlanet2);
            
            PlanetEntity unstablePlanet = new PlanetEntity();
            unstablePlanet.SetPosition(new Vector2(0, 0));
            unstablePlanet.SetVelocity(new Vector2(1, 0));
            unstablePlanet.SetRenderParameter(0.05f, Color.white, 10);
            OrbitEntityBehaviour orbitBehaviour = new OrbitEntityBehaviour(sun, unstablePlanet, m_fixedTimeStep);
            orbitBehaviour.CalculateOrbit(true);
            unstablePlanet.SetBehaviour(orbitBehaviour);
            m_entities.Add(unstablePlanet);
            
            PlanetEntity unstablePlanet1 = new PlanetEntity();
            unstablePlanet1.SetPosition(new Vector2(1, 3));
            unstablePlanet1.SetVelocity(new Vector2(-1f, 1));
            unstablePlanet1.SetRenderParameter(0.05f, Color.white, 10);
            OrbitEntityBehaviour orbitBehaviour1 = new OrbitEntityBehaviour(sun, unstablePlanet1, m_fixedTimeStep);
            orbitBehaviour1.CalculateOrbit(true);
            unstablePlanet1.SetBehaviour(orbitBehaviour1);
            m_entities.Add(unstablePlanet1);
            
            PlanetEntity unstablePlanet2 = new PlanetEntity();
            unstablePlanet2.SetPosition(new Vector2(-1, 3));
            unstablePlanet2.SetVelocity(new Vector2(-1f, -1));
            unstablePlanet2.SetRenderParameter(0.05f, Color.white, 10);
            OrbitEntityBehaviour orbitBehaviour2 = new OrbitEntityBehaviour(sun, unstablePlanet2, m_fixedTimeStep);
            orbitBehaviour2.CalculateOrbit(true);
            unstablePlanet2.SetBehaviour(orbitBehaviour2);
            m_entities.Add(unstablePlanet2);
            
            PlayerEntity player = new PlayerEntity();
            player.SetPosition(new Vector2(0, -1));
            player.SetVelocity(new Vector2(0, 0.1f));
            player.SetBehaviour(new PlayerEntityBehaviour(sun, player, m_fixedTimeStep));
            m_entities.Add(player);
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