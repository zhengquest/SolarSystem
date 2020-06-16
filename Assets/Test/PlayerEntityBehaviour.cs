using UnityEngine;

namespace Test
{
	public class PlayerEntityBehaviour : OrbitEntityBehaviour
	{
		private readonly float m_turnAngle = 10f;

		public PlayerEntityBehaviour(Entity centreEntity, Entity satelliteEntity, float fixedTimeStep) 
			: base(centreEntity, satelliteEntity, fixedTimeStep)
		{
		}

		public override void Update(float deltaTime)
		{
			//attraction to sun
			//base.Update(deltaTime);

			if (Input.GetKey(KeyCode.A))
			{
				m_satelliteEntity.SetRotation(m_satelliteEntity.Rotation * Quaternion.AngleAxis(m_turnAngle, Vector3.forward));
			}
			else if (Input.GetKey(KeyCode.D))
			{
				m_satelliteEntity.SetRotation(m_satelliteEntity.Rotation * Quaternion.AngleAxis(-m_turnAngle, Vector3.forward));
			}
			else if (Input.GetKey(KeyCode.Space))
			{
				
			}
		}
	}
}