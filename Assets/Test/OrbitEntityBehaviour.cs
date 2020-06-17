using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// This class contains the provided default entity behaviour logic
    /// I added a function to calculate a projected orbit based on initial position and velocity of the entity
    /// as well as a render function to draw it out
    /// </summary>
    public class OrbitEntityBehaviour : BaseEntityBehaviour
    {
        protected BaseEntity MCentreBaseOfGravity;
        protected BaseEntity MSatelliteBaseEntity;
        protected List<Vector2> m_orbit;
        protected bool m_renderOrbit;
        
        private const int m_maxPointsInOrbit = 500;
        private const float m_gravityForce = 10f;
        private const float m_tolerance = 0.9999f;

        public OrbitEntityBehaviour(BaseEntity centreBaseEntity, BaseEntity satelliteBaseEntity)
        {
            MCentreBaseOfGravity = centreBaseEntity;
            MSatelliteBaseEntity = satelliteBaseEntity;
        }
        
        public void CalculateOrbit(float fixedTimeStep, bool renderOrbit)
        {
            m_renderOrbit = renderOrbit;
            m_orbit = new List<Vector2>();

            Vector2 currentPosition = MSatelliteBaseEntity.Position;
            Vector2 currentVelocity = MSatelliteBaseEntity.Velocity;

            // can't calculate orbit without initial velocity
            if (MSatelliteBaseEntity.Velocity == Vector2.zero)
            {
                return;
            }
            
            // add the first point in the orbit
            m_orbit.Add(currentPosition);
            
            // keep adding points to the orbit until a full loop is achieved
            do
            {
                if (m_orbit.Count > m_maxPointsInOrbit)
                {
                    break;
                }
                
                currentVelocity += CalculateAcceleration(MCentreBaseOfGravity.Position, currentPosition) * fixedTimeStep;
                currentPosition += currentVelocity * fixedTimeStep;
                m_orbit.Add(currentPosition);

            } while (Vector2.Dot(currentVelocity.normalized, MSatelliteBaseEntity.Velocity.normalized) < m_tolerance);
        }

        public override void Update(float deltaTime)
        {
            var acc = CalculateAcceleration(MCentreBaseOfGravity.Position, MSatelliteBaseEntity.Position);
            // Apply acceleration toward the centre of mass.
            MSatelliteBaseEntity.SetVelocity(MSatelliteBaseEntity.Velocity + acc * deltaTime);
        }

        public override void Render()
        {
            if (m_renderOrbit)
            {
                RenderOrbit(Color.HSVToRGB(0,0.65f,1f));
            }
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