using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class OrbitEntityBehaviour
    {
        protected Entity m_centreOfGravity;
        protected Entity m_satelliteEntity;
        protected List<Vector2> m_orbit;
        protected float m_fixedTimeStep;

        private readonly float m_gravityForce = 10f;

        public OrbitEntityBehaviour(Entity centreEntity, Entity satelliteEntity, float fixedTimeStep)
        {
            m_centreOfGravity = centreEntity;
            m_satelliteEntity = satelliteEntity;
            m_fixedTimeStep = fixedTimeStep;

            // knowing two entities, it's possible to calculate the orbit for the satellite
            CalculateOrbit();
        }
        
        private void CalculateOrbit()
        {
            m_orbit = new List<Vector2>();

            Vector2 currentPosition = m_satelliteEntity.Position;
            Vector2 currentVelocity = m_satelliteEntity.Velocity;

            // First point in the orbit
            m_orbit.Add(currentPosition);
            
            // Keep adding point to the orbit for a full loop
            do
            {
                if (m_orbit.Count > 150)
                {
                    Debug.LogError("something prolly went wrong. break now");
                    break;
                }
                
                currentVelocity += CalculateAcceleration(m_centreOfGravity.Position, currentPosition) * m_fixedTimeStep;
                currentPosition += currentVelocity * m_fixedTimeStep;
                m_orbit.Add(currentPosition);

            } while (Vector2.Dot(currentVelocity.normalized, m_satelliteEntity.Velocity.normalized) < 0.9999f);
            
            Debug.LogError("orbit complete. points in orbit: "+m_orbit.Count);
        }

        public virtual void Update(float deltaTime)
        {
            var acc = CalculateAcceleration(m_centreOfGravity.Position, m_satelliteEntity.Position);
            // Apply acceleration toward the centre of mass.
            m_satelliteEntity.SetVelocity(m_satelliteEntity.Velocity + acc * deltaTime);
        }

        public virtual void Render()
        {
            RenderOrbit(Color.red);
        }

        protected Vector2 CalculateAcceleration(Vector2 centerPosition, Vector2 satellitePosition)
        {
            // Apply gravity towards the centre           
            Vector2 diff = centerPosition - satellitePosition;
            float distanceSquared = diff.sqrMagnitude;
            Vector2 direction = diff.normalized;
            return direction * (m_gravityForce / (distanceSquared));
        }
        
        protected void RenderOrbit(Color orbitColor)
        {
            if (m_orbit == null || m_orbit.Count < 2)
            {
                throw new Exception("orbit not valid for render");
            }
            
            for (int i = 1; i < m_orbit.Count; i++)
            {
                Debug.DrawLine(m_orbit[i - 1], m_orbit[i], orbitColor);
            }
            
            Debug.DrawLine(m_orbit[m_orbit.Count - 1], m_orbit[0], orbitColor);
        }
    }
}