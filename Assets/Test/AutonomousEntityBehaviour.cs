using UnityEngine;

namespace Test
{
	/// <summary>
	/// This class implements the autonomous behaviour described in the task
	/// The behaviour will adjust satellite's acceleration based on how much the current position
	/// has deviated from projected position calculated in the base behaviour
	/// </summary>
	public class AutonomousEntityBehaviour : OrbitEntityBehaviour
	{
		private int m_currentOrbitIndex;
		private const float m_adjustmentStrength = 30f;

		public AutonomousEntityBehaviour(BaseEntity centreBaseEntity, BaseEntity satelliteBaseEntity) 
			: base(centreBaseEntity, satelliteBaseEntity)
		{
			m_currentOrbitIndex = 0;
		}

		public override void Update(float deltaTime)
		{
			Vector2 acc = CalculateAcceleration(MCentreBaseOfGravity.Position, MSatelliteBaseEntity.Position);
			Vector2 deltaAcc = acc * deltaTime;
			Vector2 adjustment = (m_orbit[m_currentOrbitIndex] - MSatelliteBaseEntity.Position) * m_adjustmentStrength;
			Vector2 adjustedAcc =  adjustment.magnitude > 0.1f ? adjustment 
				: adjustment + MSatelliteBaseEntity.Velocity + deltaAcc;
			
			MSatelliteBaseEntity.SetVelocity(adjustedAcc);
			m_currentOrbitIndex = ++m_currentOrbitIndex % m_orbit.Count;
		}

		public override void Render()
		{
			if (m_renderOrbit)
			{
				RenderOrbit(Color.HSVToRGB(0.7f,0.65f,1f));
			}		
		}
	}
}