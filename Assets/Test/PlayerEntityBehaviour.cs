using UnityEngine;

namespace Test
{
	public class PlayerEntityBehaviour : OrbitEntityBehaviour
	{
		private readonly float m_turnAngle = 15f;
		private readonly Vector2 m_acceleration = new Vector2(0,0.2f);

		private Vector3 m_rotateAxis;
		private new PlayerEntity m_satelliteEntity;
		
		public PlayerEntityBehaviour(Entity centreEntity, PlayerEntity satelliteEntity, float fixedTimeStep) 
			: base(centreEntity, satelliteEntity, fixedTimeStep)
		{
			m_satelliteEntity = satelliteEntity;
			satelliteEntity.SetRotation(Quaternion.identity);
		}

		public override void Update(float deltaTime)
		{
			//attraction to sun
			base.Update(deltaTime);

			if (Input.GetKey(KeyCode.A))
			{
				m_satelliteEntity.SetRotation(
					m_satelliteEntity.Rotation * Quaternion.AngleAxis(m_turnAngle, Vector3.forward));
			}
			else if (Input.GetKey(KeyCode.D))
			{
				m_satelliteEntity.SetRotation(
					m_satelliteEntity.Rotation * Quaternion.AngleAxis(m_turnAngle * -1f, Vector3.forward));
			}
			else
			{
				if (Input.GetKey(KeyCode.Space))
				{
					m_satelliteEntity.SetVelocity(m_satelliteEntity.Velocity + (Vector2)(m_satelliteEntity.Rotation * m_acceleration));

					Debug.DrawRay(m_satelliteEntity.Position, m_satelliteEntity.Velocity * 1f, Color.red);
					Debug.DrawRay(m_satelliteEntity.Position, m_satelliteEntity.Rotation * Vector2.up, Color.grey);
				}
			}
		}
	}
}