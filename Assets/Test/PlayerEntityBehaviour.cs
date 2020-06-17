using UnityEngine;

namespace Test
{
	public class PlayerEntityBehaviour : OrbitEntityBehaviour
	{
		private const float m_turnAngle = 15f;
		private readonly Vector2 m_acceleration = new Vector2(0,0.2f);

		public PlayerEntityBehaviour(BaseEntity centreBaseEntity, BaseEntity satelliteBaseEntity) 
			: base(centreBaseEntity, satelliteBaseEntity)
		{
			satelliteBaseEntity.SetRotation(Quaternion.identity);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);

			float currentRotationAngleZ = MSatelliteBaseEntity.Rotation.eulerAngles.z;
			
			if (Input.GetKey(KeyCode.A))
			{
				MSatelliteBaseEntity.SetRotation(
					Quaternion.AngleAxis(currentRotationAngleZ + m_turnAngle, Vector3.forward));
			}
			else if (Input.GetKey(KeyCode.D))
			{
				MSatelliteBaseEntity.SetRotation(
					Quaternion.AngleAxis(currentRotationAngleZ - m_turnAngle, Vector3.forward));
			}

			if (Input.GetKey(KeyCode.Space))
			{
				MSatelliteBaseEntity.SetVelocity(MSatelliteBaseEntity.Velocity + (Vector2)(MSatelliteBaseEntity.Rotation * m_acceleration));
			}
		}
	}
}