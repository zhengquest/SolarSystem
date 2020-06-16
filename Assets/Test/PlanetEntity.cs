using UnityEngine;

namespace Test
{
	public class PlanetEntity : Entity
	{
		private float m_renderSize;
		private Color m_renderColor;
		
		public void SetRenderParameter(float size, Color color)
		{
			m_renderSize = size;
			m_renderColor = color;
		}
		
		public override void Render()
		{
			float size = 0.05f;
			Vector3 pos = new Vector3(Position.x, Position.y, 0);
			Vector3 nextPoint = pos;
			Vector3 startPoint = pos + new Vector3(Mathf.Cos(0), Mathf.Sin(0), 0) * size;
            
			for(int i = 1; i < 91; i++)
			{
				nextPoint.x = Mathf.Cos((i*4)*Mathf.Deg2Rad) * size;
				nextPoint.y = Mathf.Sin((i*4)*Mathf.Deg2Rad) * size;
				nextPoint += pos;
                    
				Debug.DrawLine(startPoint, nextPoint, Color.yellow);
                    
				startPoint = nextPoint;
			}
            
			m_behaviour?.Render();
		}
	}
}