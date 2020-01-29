using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    [SerializeField]
    float _minRadius = 0;
    [SerializeField]
    float _maxRadius = 5;

    [SerializeField]
    bool _isRegularTetrahedron = false;


    private const float TetrAngle = Mathf.PI * 109.4712f / 180;
    private const float AngleBtwSegment = Mathf.PI * 2 / 3;

    void Start()
    {
        if(!_isRegularTetrahedron)
        {
            GetComponent<MeshFilter>().mesh = RandomTetrahedron(_minRadius, _maxRadius);
        }
        else
        {
            GetComponent<MeshFilter>().mesh = RegularTetrahedron(Random.Range(_minRadius,_maxRadius));
        }
        
    }


    public static Mesh Triangle(Vector3 vert0, Vector3 vert1, Vector3 vert2)
    {
        var mesh = new Mesh
        {
            vertices = new[] { vert0, vert1, vert2 },
            triangles = new[] { 0, 1, 2 }
        };

        return mesh;
    }

    public Mesh RandomTetrahedron(float minRadius, float maxRadius)
    {
        Vector3[] points = new Vector3[4];

        Vector3 startPos = gameObject.transform.position;

        points[0] = startPos;

        for (int i=1;i<=3;i++)
        {
            points[i] = new Vector3(Random.Range(_minRadius, _maxRadius),Random.Range(_minRadius, _maxRadius),
                Random.Range(_minRadius, _maxRadius)) + startPos;
        }

        CombineInstance[] combMesh = new CombineInstance[4];
        combMesh[0].mesh = Triangle(points[0], points[1], points[2]);
        combMesh[1].mesh = Triangle(points[1], points[3], points[2]);
        combMesh[2].mesh = Triangle(points[0], points[2], points[3]);
        combMesh[3].mesh = Triangle(points[0], points[3], points[1]);


        var mesh = new Mesh();
        mesh.CombineMeshes(combMesh, true, false);


        return mesh;
     
    }

    //Additionally
    public Mesh RegularTetrahedron(float radius)
    {
        float angle = 0;
        var vertex = new Vector3[4];
        vertex[0] = new Vector3(0, radius, 0);

        for (var i = 1; i <= 3; i++)
        {
            vertex[i] = new Vector3(radius * Mathf.Sin(angle) * Mathf.Sin(TetrAngle),
                                radius * Mathf.Cos(TetrAngle),
                                radius * Mathf.Cos(angle) * Mathf.Sin(TetrAngle));
            angle += AngleBtwSegment;
        }

        CombineInstance[] combMesh = new CombineInstance[4];

        combMesh[0].mesh = Triangle(vertex[0], vertex[1], vertex[2]);
        combMesh[1].mesh = Triangle(vertex[1], vertex[3], vertex[2]);
        combMesh[2].mesh = Triangle(vertex[0], vertex[2], vertex[3]);
        combMesh[3].mesh = Triangle(vertex[0], vertex[3], vertex[1]);

        var mesh = new Mesh();
        mesh.CombineMeshes(combMesh, true, false);
        return mesh;
    }




}
