using UnityEngine;

namespace Test
{
	public class PlayerEntityBehaviour : OrbitEntityBehaviour
	{
		private readonly float m_turnAngle = 15f;
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
			//base.Update(deltaTime);

			Debug.DrawRay(m_satelliteEntity.Position, Vector3.forward * 100f, Color.blue);
			
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
			else if (Input.GetKey(KeyCode.Space))
			{
				Vector3 newVelocity = m_satelliteEntity.Velocity == Vector2.zero
					? new Vector3(0, 1f): m_satelliteEntity.Rotation * m_satelliteEntity.Velocity;
				m_satelliteEntity.SetVelocity(newVelocity);
			}
		}
	}
}