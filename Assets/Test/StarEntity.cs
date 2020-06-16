using UnityEngine;

namespace Test
{
	public class StarEntity : Entity
	{
		public override void Update(float deltaTime)
		{
		}

		public override void Render()
		{
			float size = 0.08f;
			Vector3 pos = new Vector3(Position.x, Position.y, 0);
			Vector3 up = new Vector3(0,1,0) * size;
			Vector3 right = new Vector3(1,0,0) * size;

			for (int i = 0, angle = 0; i < 20; i++, angle+= 33)
			{
				Quaternion rot = Quaternion.AngleAxis(angle + Time.time * 100f, Vector3.forward);
				float newSize = size * i;
				Matrix4x4 matrix = Matrix4x4.TRS(pos, rot, new Vector3(newSize,newSize,newSize));
                    
				Vector3 topLeft = matrix.MultiplyPoint3x4(up - right);
				Vector3 topRight = matrix.MultiplyPoint3x4(up + right);
				Vector3 botLeft = matrix.MultiplyPoint3x4(-up - right);
				Vector3 botRight = matrix.MultiplyPoint3x4(-up + right);
                    
				Debug.DrawLine(topRight, botRight, Color.yellow);
				Debug.DrawLine(botRight, botLeft, Color.red);
				Debug.DrawLine(botLeft, topLeft, Color.yellow);
				Debug.DrawLine(topLeft, topRight, Color.red);
			}
		}
	}
}