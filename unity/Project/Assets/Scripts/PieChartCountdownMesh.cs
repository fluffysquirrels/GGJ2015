using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class PieChartCountdownMesh : MonoBehaviour {

    public float Value;
    public float DeltaPerSecond;
    private MeshFilter meshFilter;
    private Mesh mesh;

    void Start() {
        this.meshFilter = GetComponent<MeshFilter> ();
        this.mesh = meshFilter.mesh;
        mesh.Clear ();
        mesh.MarkDynamic ();
    }

	void Update () {
        mesh.Clear ();
        SetCountdownMesh (mesh);

        Value += DeltaPerSecond * Time.deltaTime;
        Value %= 1;
	}

    private void SetCountdownMesh(Mesh mesh) {
        float normalisedValue = Value - Mathf.Floor(Value); // Between 0 and 1.
        float circleAngle = 2 * Mathf.PI * normalisedValue;

        // Point on the unit circle where the angle intersects.
        var circlePoint = new Vector3 (Mathf.Sin (circleAngle), Mathf.Cos (circleAngle), 0);

        // This scale factor will take the larger of the x and y co-ordinates to 1, so onto a square around the origin.
        float scaleFactor = 1 / Mathf.Max(Mathf.Abs(circlePoint.x), Mathf.Abs(circlePoint.y));
        var squarePoint = circlePoint * scaleFactor;

        float sideLength = 1;
        float halfLength = sideLength / 2;

        mesh.vertices = new [] {
            halfLength * new Vector3( 0, 0, 0),    // 0
            halfLength * new Vector3( 1, 1, 0),    // 1
            halfLength * new Vector3( 1,-1, 0),    // 2
            halfLength * new Vector3(-1,-1, 0),    // 3
            halfLength * new Vector3(-1, 1, 0),    // 4
            halfLength * new Vector3( 0, 1, 0),    // 5
            halfLength * squarePoint,              // 6
        };

        var triangles = new List<int> ();
        if (normalisedValue < (float) 1 / 8) {
            triangles.AddRange (new []{ 0, 6, 1 });
            triangles.AddRange (new []{ 0, 1, 2 });
            triangles.AddRange (new []{ 0, 2, 3 });
            triangles.AddRange (new []{ 0, 3, 4 });
            triangles.AddRange (new []{ 0, 4, 5 });
        } else if (normalisedValue < (float) 3 / 8) {
            triangles.AddRange (new []{ 0, 6, 2 });
            triangles.AddRange (new []{ 0, 2, 3 });
            triangles.AddRange (new []{ 0, 3, 4 });
            triangles.AddRange (new []{ 0, 4, 5 });
        } else if (normalisedValue < (float) 5 / 8) {
            triangles.AddRange (new []{ 0, 6, 3 });
            triangles.AddRange (new []{ 0, 3, 4 });
            triangles.AddRange (new []{ 0, 4, 5 });
        } else if (normalisedValue < (float) 7 / 8) {
            triangles.AddRange (new []{ 0, 6, 4 });
            triangles.AddRange (new []{ 0, 4, 5 });
        } else {
            triangles.AddRange (new []{ 0, 6, 5 });
        }

        mesh.triangles = MirrorIndices (triangles.ToArray ());
    }

    private static int[] MirrorIndices(int[] indices) {
        var withMirrored = new int[indices.Length * 2];

        indices.CopyTo (withMirrored, 0);

        for (int tri = 0; tri < indices.Length / 3; ++tri) {
            withMirrored [indices.Length + 3 * tri + 0] = indices [3 * tri + 0];
            withMirrored [indices.Length + 3 * tri + 1] = indices [3 * tri + 2]; // Swapped
            withMirrored [indices.Length + 3 * tri + 2] = indices [3 * tri + 1]; // Swapped
        }

        return withMirrored;
    }

    private static void SetSillyTetrahedronMesh(Mesh mesh) {
        mesh.vertices = new Vector3[] {
            new Vector3( 1, 1, 1),
            new Vector3( 1, 1,-1),
            new Vector3(-1, 1, 1),
            new Vector3( 1,-1, 1),
        };

        mesh.triangles = new int[] {
            0, 1, 2,
            1, 3, 2,
            2, 3, 0,
            0, 3, 1,
        };
    }
}
