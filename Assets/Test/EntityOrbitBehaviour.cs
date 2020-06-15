using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class EntityOrbitBehaviour
    {
        private Entity m_centreOfGravity;
        private Entity m_orbitEntity;
     
        private readonly float m_gravityForce = 10f;

        public EntityOrbitBehaviour(Entity centreEntity, Entity orbitEntity)
        {
            m_centreOfGravity = centreEntity;
            m_orbitEntity = orbitEntity;
        }

        public List<Vector2> CalculateOrbit(float fixedTimeStep)
        {
            List<Vector2> orbit = new List<Vector2>();

            //Vector2 nextPosition = m_centreOfGravity.Position;
            Vector2 currentPosition = m_orbitEntity.Position;
            Vector2 currentVelocity = m_orbitEntity.Velocity;

            // First point in the orbit
            orbit.Add(currentPosition);
            
            // Keep adding point to the orbit for a full loop
            do
            {
                if (orbit.Count > 150)
                {
                    Debug.LogError("something prolly went wrong. break now");
                    break;
                }
                
                currentVelocity += CalculateAcceleration(currentPosition) * fixedTimeStep;
                currentPosition += currentVelocity * fixedTimeStep;
                orbit.Add(currentPosition);

            } while (Vector2.Dot(currentVelocity.normalized, m_orbitEntity.Velocity.normalized) < 0.999f);
            
            Debug.LogError("orbit complete");
            return orbit;
        }

        public void Update(float deltaTime)
        {
            var acc = CalculateAcceleration(m_orbitEntity.Position);

            // Apply acceleration toward the centre of mass.
            m_orbitEntity.SetVelocity(m_orbitEntity.Velocity + acc * deltaTime);
        }

        protected virtual Vector2 CalculateAcceleration(Vector2 position)
        {
            // Apply gravity towards the centre           
            Vector2 diff = m_centreOfGravity.Position - position;
            float distanceSquared = diff.sqrMagnitude;
            Vector2 direction = diff.normalized;
            return direction * (m_gravityForce / (distanceSquared));
        }
    }
}