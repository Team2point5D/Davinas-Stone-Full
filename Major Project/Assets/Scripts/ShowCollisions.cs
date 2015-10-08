using UnityEngine;
//using UnityEditor;

using System.Collections.Generic;

public class ShowCollisions : MonoBehaviour
{

    // The Collider itself
    private List<MeshCollider> l_colliders;
    // array of collider points
    private Vector2[] points;
    // the transform position of the collider
    private Vector3 _t;

    void OnDrawGizmos() {
        l_colliders = new List<MeshCollider>();

        GameObject[] goObjs = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in goObjs)
        {
            if (obj.GetComponent<MeshCollider>() != null)
            {
                l_colliders.Add(obj.GetComponent<MeshCollider>());
            }
        }



        Gizmos.color = Color.blue;
        // for every point (except for the last one), draw line to the next point
        //for (int i = 0; i < points.Length - 1; i++)
        //{
        //    Gizmos.DrawLine(new Vector3(points[i].x + _t.x, points[i].y + _t.y), new Vector3(points[i + 1].x + _t.x, points[i + 1].y + _t.y));
        //}

        foreach (MeshCollider mesh in l_colliders)
        {
            Graphics.DrawMeshNow(mesh.sharedMesh, mesh.gameObject.transform.position, mesh.gameObject.transform.rotation);
        }
    }

}
