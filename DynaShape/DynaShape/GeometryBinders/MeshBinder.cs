using System.Collections.Generic;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using Autodesk.Dynamo.MeshToolkit;
using HelixToolkit.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using Mesh = Autodesk.Dynamo.MeshToolkit.Mesh;
using Point = Autodesk.DesignScript.Geometry.Point;


namespace DynaShape.GeometryBinders
{
    [IsVisibleInDynamoLibrary(false)]
    public class MeshBinder : GeometryBinder
    {
        private IndexGroup[] faces;
        private List<int> faceIndices;
        private IntCollection meshIndices;


        public MeshBinder()
        {
        }


        public MeshBinder(Mesh mesh, Color4 color)
        {
            Initialize(mesh, color);
        }


        public MeshBinder(Mesh mesh)
            : this(mesh, DynaShapeDisplay.DefaultMeshFaceColor)
        {
        }


        public void Initialize(Mesh mesh, Color4 color)
        {
            StartingPositions = mesh.Vertices().ToTriples().ToArray();
            Color = color;

            faceIndices = mesh.VertexIndicesByTri();
            int faceCount = faceIndices.Count / 3;

            faces = new IndexGroup[faceCount];

            for (int i = 0; i < faceCount; i++)
                faces[i] = IndexGroup.ByIndices(
                    (uint)faceIndices[i * 3],
                    (uint)faceIndices[i * 3 + 1],
                    (uint)faceIndices[i * 3 + 2]);

            meshIndices = new IntCollection();

            foreach (IndexGroup face in faces)
            {
                meshIndices.Add((int)face.A);
                meshIndices.Add((int)face.B);
                meshIndices.Add((int)face.C);
            }
        }


        public override List<object> CreateGeometryObjects(List<Node> allNodes)
        {
            List<Point> vertices = new List<Point>(NodeCount);
            for (int i = 0; i < NodeCount; i++)
                vertices.Add(allNodes[NodeIndices[i]].Position.ToPoint());

            return new List<object> { Mesh.ByVerticesAndIndices(vertices, faceIndices) };

        }


        public override void CreateDisplayedGeometries(DynaShapeDisplay display, List<Node> allNodes)
        {
            //======================================================================
            // Compute vertex normals by averaging normals of surrounding faces
            //======================================================================

            Triple[] vertexNormals = new Triple[NodeCount];

            foreach (IndexGroup face in faces)
            {
                Triple A = allNodes[NodeIndices[face.A]].Position;
                Triple B = allNodes[NodeIndices[face.B]].Position;
                Triple C = allNodes[NodeIndices[face.C]].Position;

                Triple n = (B - A).Cross(C - A).Normalise();

                vertexNormals[face.A] += n;
                vertexNormals[face.B] += n;
                vertexNormals[face.C] += n;

                if (face.D == uint.MaxValue) continue;

                Triple D = allNodes[NodeIndices[face.D]].Position;

                n = (C - A).Cross(D - A).Normalise();

                vertexNormals[face.A] += n;
                vertexNormals[face.C] += n;
                vertexNormals[face.D] += n;
            }

            for (int i = 0; i < NodeCount; i++) vertexNormals[i] = vertexNormals[i].Normalise();


            //===============================================================
            // Render mesh triangles, using vertex normals computed above
            //===============================================================

            MeshGeometry3D meshGeometry = new MeshGeometry3D()
            {
                Positions = new Vector3Collection(),
                Normals = new Vector3Collection(),
                Indices = this.meshIndices,
            };

            for (int i = 0; i < NodeCount; i++)
            {
                meshGeometry.Positions.Add(allNodes[NodeIndices[i]].Position.ToVector3());
                meshGeometry.Normals.Add(vertexNormals[i].ToVector3());
            }

            display.AddMeshModel(
                new MeshGeometryModel3D
                {
                    Geometry = meshGeometry,
                    Material = new PhongMaterial { DiffuseColor = Color },
                });
        }
    }
}
