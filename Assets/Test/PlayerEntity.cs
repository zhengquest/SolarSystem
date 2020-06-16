using UnityEngine;

namespace Test
{
	public class PlayerEntity : Entity
	{
		public override void Render()
		{
			float size = 0.1f;
			Vector3 pos = new Vector3(Position.x, Position.y, 0);
			Vector3[] vertices = 
			{
				new Vector3(0, 1, 0),
				new Vector3(-0.3f, -0.5f, 0), 
				new Vector3(0.3f, -0.5f, 0) 
			};

			Matrix4x4 matrix = Matrix4x4.TRS(pos, Rotation, new Vector3(size,size,size));

			for (int i = 0; i < vertices.Length; i++)
			{
				int nextIndex = i + 1 >= vertices.Length ? 0 : i + 1;
				Debug.DrawLine(matrix.MultiplyPoint3x4(vertices[i]), 
					matrix.MultiplyPoint3x4(vertices[nextIndex]), Color.cyan);
			}
		}
	}
}