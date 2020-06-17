using UnityEngine;

namespace Test
{
	public class PlanetBaseEntity : BaseEntity
	{
		private float m_renderSize;
		private Color m_renderColor;
		private int m_division;

		public void SetRenderParameter(float size, Color color, int division)
		{
			m_renderSize = size;
			m_renderColor = color;
			m_division = division;
		}
		
		public override void Render()
		{
			float size = m_renderSize;
			Vector3 pos = new Vector3(Position.x, Position.y, 0);
			Vector3 nextPoint = pos;
			Vector3 startPoint = pos + new Vector3(Mathf.Cos(0), Mathf.Sin(0), 0) * size;
            
			// draw a circle first
			for(int i = 1; i < 91; i++)
			{
				nextPoint.x = Mathf.Cos((i * 4) * Mathf.Deg2Rad) * size;
				nextPoint.y = Mathf.Sin((i * 4) * Mathf.Deg2Rad) * size;
				nextPoint += pos;
				Debug.DrawLine(startPoint, nextPoint, m_renderColor);
				startPoint = nextPoint;
			}

			// divide the circle by specified parameter and connect center with divided points
			float angle = 2 * Mathf.PI / m_division;

			for (int i = 0; i < m_division; i++)
			{
				Matrix4x4 matrix = new Matrix4x4(
					new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
					new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
					new Vector4(0, 0, 1, 0),
					new Vector4(0, 0, 0, 1));
				
				var point = pos + matrix.MultiplyPoint3x4(new Vector3(0, size, 0));
				Debug.DrawLine(pos, point, m_renderColor);
			}
			
			base.Render();
		}
	}
}