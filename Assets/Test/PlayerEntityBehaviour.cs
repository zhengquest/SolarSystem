using UnityEngine;

namespace Test
{
	public class PlayerEntityBehaviour : OrbitEntityBehaviour
	{
		private const float m_turnAngle = 15f;
		private readonly Vector2 m_acceleration = new Vector2(0,0.2f);

		public PlayerEntityBehaviour(Entity centreEntity, Entity satelliteEntity) 
			: base(centreEntity, satelliteEntity)
		{
			satelliteEntity.SetRotation(Quaternion.identity);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);

			float currentRotationAngleZ = m_satelliteEntity.Rotation.eulerAngles.z;
			
			if (Input.GetKey(KeyCode.A))
			{
				m_satelliteEntity.SetRotation(
					Quaternion.AngleAxis(currentRotationAngleZ + m_turnAngle, Vector3.forward));
			}
			else if (Input.GetKey(KeyCode.D))
			{
				m_satelliteEntity.SetRotation(
					Quaternion.AngleAxis(currentRotationAngleZ - m_turnAngle, Vector3.forward));
			}

			if (Input.GetKey(KeyCode.Space))
			{
				m_satelliteEntity.SetVelocity(m_satelliteEntity.Velocity + (Vector2)(m_satelliteEntity.Rotation * m_acceleration));
			}
		}
	}
}