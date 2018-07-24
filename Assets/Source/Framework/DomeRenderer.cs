using UnityEngine;

public class DomeRenderer : MonoBehaviour {

    public float width = 1;
    public float height = 1;

    private void Update () {
        Mesh mesh = new Mesh ();

        Vector3[] vertecies = new Vector3[4];

        vertecies[0] = new Vector3 (-width, -height);
        vertecies[1] = new Vector3 (-width, height);
        vertecies[2] = new Vector3 (width, height);
        vertecies[3] = new Vector3 (width, -height);

        mesh.vertices = vertecies;

        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

        GetComponent<MeshFilter> ().mesh = mesh;
    }
}
