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

		public AutonomousEntityBehaviour(Entity centreEntity, Entity satelliteEntity) 
			: base(centreEntity, satelliteEntity)
		{
			m_currentOrbitIndex = 0;
		}

		public override void Update(float deltaTime)
		{
			Vector2 acc = CalculateAcceleration(m_centreOfGravity.Position, m_satelliteEntity.Position);
			Vector2 deltaAcc = acc * deltaTime;
			Vector2 adjustment = (m_orbit[m_currentOrbitIndex] - m_satelliteEntity.Position) * m_adjustmentStrength;
			Vector2 adjustedAcc =  adjustment.magnitude > 0.1f ? adjustment 
				: adjustment + m_satelliteEntity.Velocity + deltaAcc;
			
			m_satelliteEntity.SetVelocity(adjustedAcc);
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