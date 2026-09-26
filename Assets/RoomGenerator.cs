using UnityEngine;
using System.Collections.Generic;
using System;

namespace System.Runtime.CompilerServices
{
    class IsExternalInit
    {
     
    }
}

public class RoomGenerator : MonoBehaviour
{
	// private int rows = 5;
	// private int cols = 8;
	private float tileSize = 10;
	private HashSet<Vector2> occupied = new();
	private GameObject referenceCorridorTile;
	private GameObject referenceCornerTile;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		GenerateGrid();
	}
	private void GenerateGrid()
	{
		referenceCorridorTile = Instantiate(Resources.Load<GameObject>("CorridorTile"));
		referenceCornerTile = Instantiate(Resources.Load<GameObject>("CornerTile"));

		Debug.Log("calling GeneratePath");
		Room origin = GeneratePath(50);
		Debug.Log(origin);
		DisplayRoom(origin);

		Destroy(referenceCorridorTile);
		Destroy(referenceCornerTile);
	}

	private void DisplayRoom(Room room)
	{
		bool isCorridor = room.Next == null || room.From == null || room.Next?.From == room.From;
		Vector2 ori = room.Next.From ?? (Vector2) room.From;
		if (!isCorridor)
		{
			var from = (Vector2) room.From;
			var to = (Vector2) room.Next?.From;
			ori = from.Equals(Vector2.right) && to.Equals(Vector2.up)
			   || from.Equals(Vector2.down) && to.Equals(Vector2.left) ? Vector2.down
			    : from.Equals(Vector2.left) && to.Equals(Vector2.down)
			   || from.Equals(Vector2.up) && to.Equals(Vector2.right) ? Vector2.up
			    : from.Equals(Vector2.right) && to.Equals(Vector2.down)
			   || from.Equals(Vector2.up) && to.Equals(Vector2.left) ? Vector2.left
				: Vector2.right;
			//ori = from.Equals(Vector2.right) && to.Equals(Vector2.down) ? Vector2.down : ori;
			// ori = from.Equals(Vector2.up) && to.Equals(Vector2.right) || from == Vector2.right && to == Vector2.up
			// 	? Vector2.up
			// 	: from == Vector2.up && to == Vector2.left || from == Vector2.left && to == Vector2.up
			// 	? Vector2.left
			// 	: from == Vector2.down && to == Vector2.left || from == Vector2.left && to == Vector2.down
			// 	? Vector2.down
			// 	: Vector2.right;
		}
		PlaceTile(room.Pos, isCorridor ? referenceCorridorTile : referenceCornerTile, ori);
		if (room.Next != null)
		{
			DisplayRoom(room.Next);
		}
	}
	private void PlaceTile(Vector2 pos, GameObject referenceTile, Vector2 ori)
	{
		GameObject tile = Instantiate(referenceTile, transform);
		tile.transform.position = pos * tileSize;
		tile.transform.localScale = new Vector2(tileSize, tileSize);
		float angle = Mathf.Atan2(ori.x, ori.y) * 180 / Mathf.PI;
		tile.transform.eulerAngles = new Vector3(0, 0, angle);
	}

	private Room GeneratePath(int length)
	{
		return NextRoom(length, 0, new Vector2(0, 0), null);
	}
	private Room? NextRoom(int depth, int minDepth, Vector2 pos, Vector2? from)
	{
		occupied.Add(pos);
		if (depth <= minDepth)
		{
			return new Room(depth, pos, from, null);
		}
		Vector2[] orientationArray = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
		var orientations = new List<Vector2>(orientationArray);
		while (orientations.Count > 0)
		{
			int i = UnityEngine.Random.Range(0, orientations.Count);
			Vector2 ori = orientations[i];
			orientations.RemoveAt(i);

			Vector2 nextPos = pos + ori;
			if (occupied.Contains(nextPos))
			{
				continue;
			}
			Room? next = NextRoom(depth - 1, minDepth, nextPos, ori);
			if (next != null)
			{
				return new Room(depth, pos, from, next);
			}
		}
		occupied.Remove(pos);
		return null;
	}

}

record Room(int Depth, Vector2 Pos, Vector2? From, Room? Next);
