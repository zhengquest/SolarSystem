using UnityEngine;

namespace Test
{
    public class EntityBehaviour
    {
        private Entity m_centreOfGravity;
        private float gravityForce = 10;

        public EntityBehaviour(Entity centreEntity)
        {
            m_centreOfGravity = centreEntity;
        }
        
        public void Update(Entity entity, float deltaTime)
        {
            // Apply gravity towards the cenre           
            Vector2 diff = m_centreOfGravity.Position - entity.Position;
            
            float distanceSquared = diff.sqrMagnitude;
            Vector2 direction = diff.normalized;

            Vector2 acc = direction * (gravityForce / (distanceSquared));

            // Apply acceleration toward the centre of mass.
            entity.SetVelocity( entity.Velocity + acc * deltaTime);
        }
    }
}