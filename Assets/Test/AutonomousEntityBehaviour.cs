using UnityEngine;

namespace Test
{
	public class AutonomousEntityBehaviour : OrbitEntityBehaviour
	{
		private int m_orbitIndex;

		public AutonomousEntityBehaviour(Entity centreEntity, Entity satelliteEntity, float fixedTimeStep) 
			: base(centreEntity, satelliteEntity, fixedTimeStep)
		{
			m_orbitIndex = 0;
		}

		public override void Update(float deltaTime)
		{
			var acc = CalculateAcceleration(m_centreOfGravity.Position, m_satelliteEntity.Position);
			var deltaAcc = acc * deltaTime;
			var adjustment = CalculateAdjustment();
			var adjustedAcc =  (adjustment.magnitude > 0.1f
				? adjustment
				: adjustment + m_satelliteEntity.Velocity + deltaAcc);
			
			// Apply acceleration toward the centre of mass.
			m_satelliteEntity.SetVelocity(adjustedAcc);

			Vector2 direction = m_satelliteEntity.Velocity.normalized;
			float size = 0.2f;
			Vector3 dirEnd = new Vector3(m_satelliteEntity.Position.x + direction.x*size, 
				m_satelliteEntity.Position.y + direction.y*size, 0);
			Debug.DrawLine(m_satelliteEntity.Position, dirEnd, Color.white);
			
			
			m_orbitIndex = ++m_orbitIndex % m_orbit.Count;
		}

		private Vector2 CalculateAdjustment()
		{
//			Debug.LogError($"orbit index {m_timeStep} value {m_orbit[m_timeStep]}");
			Debug.DrawLine(m_orbit[m_orbitIndex], m_satelliteEntity.Position, Color.red);
			var adjustmentVector = m_orbit[m_orbitIndex] - m_satelliteEntity.Position;

			if (adjustmentVector.magnitude > 0.25f)
			{
				//Debug.Break();
			}

			return adjustmentVector * 30f;

			//return adjustmentVector.normalized * ScaleAdjustmentMagnitude(adjustmentVector.magnitude, 5f);
		}

		public override void Render()
		{
			if (m_renderOrbit)
			{
				RenderOrbit(Color.blue);
			}		
		}

		private float ScaleAdjustmentMagnitude(float x, float scalar)
		{
			// Debug.LogError("MAG ADJ: "+(1 - Mathf.Pow(1 - magnitude, 5))*scalar);
			// return (1 - Mathf.Pow(1 - magnitude, 5)) * scalar;
			var scaled = (1 - Mathf.Cos((x * Mathf.PI) / 2) )* scalar;
			Debug.LogError(scaled);
			return scaled;
		}
	}
}