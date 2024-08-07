﻿using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DynaShape.GeometryBinders;
using DynaShape.Goals;
using SharpDX;
using Mesh = Autodesk.Dynamo.MeshToolkit.Mesh;
using Point = Autodesk.DesignScript.Geometry.Point;

namespace DynaShape.ZeroTouch;


public static class TestingUtilities
{
    /// <summary>
    /// This is a quick test setup for shape matching goals. Here we create a 3D grid of cubes, and apply a shape matching goal on each cube to force them maintain their intial unit-cube shape
    /// </summary>
    /// <param name="X">Number of nodes along the X axis, minimum is 2</param>
    /// <param name="Y">Number of nodes along the Y axis, minimum is 2</param>
    /// <param name="Z">Number of nodes along the Z axis, minimum is 2</param>
    /// <param name="allowScaling"></param>
    /// <returns>The ShapeMatching goals, Anchor goals, and polyline binders</returns>
    [MultiReturn("goals", "geometryBinders", "anchorGoals", "points")]
    public static Dictionary<string, object> WonkyCubes(int X = 21, int Y = 21, int Z = 21, bool allowScaling = false)
    {
        Triple[,,] points = new Triple[X, Y, Z];
        List<Point> pointsLinear = new List<Point>();
        for (int i = 0; i < X; i++)
        for (int j = 0; j < Y; j++)
        for (int k = 0; k < Z; k++)
        {
            points[i, j, k] = new Triple(i, j, k + 5f);
            pointsLinear.Add(Point.ByCoordinates(i, j, k + 5f));
        }

        List<Goal> anchorGoals = new List<Goal>();

        anchorGoals.Add(new AnchorGoal(points[0, 0, Z - 1]));
        anchorGoals.Add(new AnchorGoal(points[X - 1, 0, Z - 1]));
        anchorGoals.Add(new AnchorGoal(points[X - 1, Y - 1, Z - 1]));
        anchorGoals.Add(new AnchorGoal(points[0, Y - 1, Z - 1]));

        List<Goal> goals = new List<Goal>();
        //goals.AddRange(anchorGoals);

        List<GeometryBinder> geometryBinders = new List<GeometryBinder>();

        List<Triple> vertices = new List<Triple>();

        vertices.Clear();
        for (int i = 0; i < X; i++)
        for (int j = 0; j < Y; j++)
            vertices.Add(points[i, i % 2 == 0 ? j : Y - 1 - j, 0]);
        geometryBinders.Add(new PolylineBinder(vertices));

        vertices.Clear();
        for (int j = 0; j < X; j++)
        for (int i = 0; i < Y; i++)
            vertices.Add(points[j % 2 == 0 ? i : X - 1 - i, j, 0]);
        geometryBinders.Add(new PolylineBinder(vertices));

        vertices.Clear();
        for (int i = 0; i < X; i++)
        for (int j = 0; j < Y; j++)
            vertices.Add(points[i, i % 2 == 0 ? j : Y - 1 - j, Z - 1]);
        geometryBinders.Add(new PolylineBinder(vertices));

        vertices.Clear();
        for (int j = 0; j < X; j++)
        for (int i = 0; i < Y; i++)
            vertices.Add(points[j % 2 == 0 ? i : X - 1 - i, j, Z - 1]);
        geometryBinders.Add(new PolylineBinder(vertices));

        vertices.Clear();
        for (int j = 0; j < Y; j++)
        for (int k = 0; k < Z; k++)
            vertices.Add(points[0, j, j % 2 == 0 ? k : Z - 1 - k]);
        geometryBinders.Add(new PolylineBinder(vertices));

        vertices.Clear();
        for (int k = 0; k < Z; k++)
        for (int j = 0; j < Y; j++)
            vertices.Add(points[0, k % 2 == 0 ? j : Y - 1 - j, k]);
        geometryBinders.Add(new PolylineBinder(vertices));

        vertices.Clear();
        for (int j = 0; j < Y; j++)
        for (int k = 0; k < Z; k++)
            vertices.Add(points[X - 1, j, j % 2 == 0 ? k : Z - 1 - k]);
        geometryBinders.Add(new PolylineBinder(vertices));

        vertices.Clear();
        for (int k = 0; k < Z; k++)
        for (int j = 0; j < Y; j++)
            vertices.Add(points[X - 1, k % 2 == 0 ? j : Y - 1 - j, k]);
        geometryBinders.Add(new PolylineBinder(vertices));

        vertices.Clear();
        for (int k = 0; k < Z; k++)
        for (int i = 0; i < Y; i++)
            vertices.Add(points[k % 2 == 0 ? i : X - 1 - i, 0, k]);
        geometryBinders.Add(new PolylineBinder(vertices));

        vertices.Clear();
        for (int i = 0; i < Y; i++)
        for (int k = 0; k < Z; k++)
            vertices.Add(points[i, 0, i % 2 == 0 ? k : Z - 1 - k]);
        geometryBinders.Add(new PolylineBinder(vertices));

        vertices.Clear();
        for (int k = 0; k < Z; k++)
        for (int i = 0; i < Y; i++)
            vertices.Add(points[k % 2 == 0 ? i : X - 1 - i, Y - 1, k]);
        geometryBinders.Add(new PolylineBinder(vertices));

        vertices.Clear();
        for (int i = 0; i < Y; i++)
        for (int k = 0; k < Z; k++)
            vertices.Add(points[i, Y - 1, i % 2 == 0 ? k : Z - 1 - k]);
        geometryBinders.Add(new PolylineBinder(vertices));

        for (int i = 0; i < X - 1; i++)
        for (int j = 0; j < Y - 1; j++)
        for (int k = 0; k < Z - 1; k++)
        {
            var voxelPoints = new List<Triple>()
            {
                points[i + 0, j + 0, k + 0],
                points[i + 1, j + 0, k + 0],
                points[i + 1, j + 1, k + 0],
                points[i + 0, j + 1, k + 0],
                points[i + 0, j + 0, k + 1],
                points[i + 1, j + 0, k + 1],
                points[i + 1, j + 1, k + 1],
                points[i + 0, j + 1, k + 1],
            };
            goals.Add(new ShapeMatchingGoal(voxelPoints, voxelPoints, allowScaling));
        }

        //goals.Add(new FloorGoal(pointsLinear));
        //goals.Add(new ConstantGoal(pointsLinear, Triple.BasisZ * -0.1f));

        return new Dictionary<string, object>
        {
            {"goals", goals},
            {"geometryBinders", geometryBinders},
            {"anchorGoals", anchorGoals},
            {"points", pointsLinear}
        };
    }


    [MultiReturn("shapeMatchingGoals", "lengthGoals", "meshBinders", "lineBinders", "onSurfaceGoal")]
    public static Dictionary<string, object> AuxeticRotatingSquares(int xCount = 5, int yCount = 5, double thickness = 0.0, Surface targetSurface = null)
    {
        List<Goal> shapeMatchingGoals = new List<Goal>();
        List<GeometryBinder> meshBinders = new List<GeometryBinder>();
        List<GeometryBinder> lineBinders = new List<GeometryBinder>();

        List<Point> vertices = new List<Point>();
        List<Triple> bottomVertices = new List<Triple>();
        List<int> indices = new List<int>();

        double offset = 0.001;


        List<Triple> targetShape = new List<Triple>()
        {
            new Triple(0, 0, 0),
            new Triple(1, 0, 0),
            new Triple(1, 1, 0),
            new Triple(0, 1, 0),
        };

        if (thickness > 0.0)
            for (int i = 0; i < 4; i++)
                targetShape.Add(targetShape[i] + thickness * Triple.BasisZ);

        for (int i = 0; i < xCount; i++)
        for (int j = 0; j < yCount; j++)
        {
            double k = 0.0;
            List<Triple> tileVertices = new List<Triple>();

            if ((i + j) % 2 == 0)
                tileVertices = new List<Triple>()
                {
                    new Triple(i, j + offset, k),
                    new Triple(i + 1f - offset, j, k),
                    new Triple(i + 1f, j + 1f - offset, k),
                    new Triple(i + offset, j + 1f, k),
                };
            else
            {
                tileVertices = new List<Triple>()
                {
                    new Triple(i + offset, j, k),
                    new Triple(i + 1f, j + offset, k),
                    new Triple(i + 1f - offset, j + 1f, k),
                    new Triple(i, j + 1f - offset, k),
                };
            }

            bottomVertices.AddRange(tileVertices);

            if (thickness > 0.0)
                for (int ii = 0; ii < 4; ii++) tileVertices.Add(tileVertices[ii] + thickness * Triple.BasisZ);

            shapeMatchingGoals.Add(new ShapeMatchingGoal(tileVertices, targetShape));


            vertices.Add(tileVertices[0].ToPoint());
            vertices.Add(tileVertices[1].ToPoint());
            vertices.Add(tileVertices[2].ToPoint());
            vertices.Add(tileVertices[3].ToPoint());

            int n = vertices.Count - 4;

            indices.AddRange(new [] {n, n + 1, n + 2, n, n + 2, n + 3});
        }

        List<Goal> lengthGoals = new List<Goal>();

        for (int i = 1; i < xCount; i++)
        for (int j = 0; j <= yCount; j++)
            if ((i + j) % 2 == 1)
            {
                lengthGoals.Add(new LengthGoal(
                    new Triple(i - offset, j, 0),
                    new Triple(i + offset, j, 0)));
                lineBinders.Add(new LineBinder(
                    new Triple(i - offset, j, 0),
                    new Triple(i + offset, j, 0)));
            }

        for (int i = 0; i <= xCount; i++)
        for (int j = 1; j < yCount; j++)
            if ((i + j) % 2 == 0)
            {
                lengthGoals.Add(new LengthGoal(
                    new Triple(i, j - offset, 0),
                    new Triple(i, j + offset, 0)));
                lineBinders.Add(new LineBinder(
                    new Triple(i, j - offset, 0),
                    new Triple(i, j + offset, 0)));
            }

        meshBinders.Add(new MeshBinder(Mesh.ByVerticesAndIndices(vertices, indices), new Color(.3f, .6f, .8f, 1f)));

        return new Dictionary<string, object>
        {
            {"shapeMatchingGoals", shapeMatchingGoals},
            {"lengthGoals", lengthGoals},
            {"onSurfaceGoal", targetSurface == null ? null : new OnSurfaceGoal(bottomVertices, targetSurface, 1f)},
            {"meshBinders", meshBinders},
            {"lineBinders", lineBinders},
        };
    }



    [MultiReturn("shapeMatchingGoals", "lengthGoals", "meshBinders", "lineBinders")]
    public static Dictionary<string, object> AuxeticRotatingSquares(int xCount = 5, int yCount = 5, double thickness = 0.0)
    {
        List<Goal> shapeMatchingGoals = new List<Goal>();
        List<GeometryBinder> meshBinders = new List<GeometryBinder>();
        List<GeometryBinder> lineBinders = new List<GeometryBinder>();

        List<Point> vertices = new List<Point>();
        List<int> indices = new List<int>();

        double offset = 0.001;


        List<Triple> targetShape = new List<Triple>()
        {
            new Triple(0, 0, 0),
            new Triple(1, 0, 0),
            new Triple(1, 1, 0),
            new Triple(0, 1, 0),
        };

        if (thickness > 0.0)
            for (int i = 0; i < 4; i++)
                targetShape.Add(targetShape[i] + thickness * Triple.BasisZ);

        for (int i = 0; i < xCount; i++)
        for (int j = 0; j < yCount; j++)
        {
            double k = 0.0;
            List<Triple> tileVertices = new List<Triple>();

            if ((i + j) % 2 == 0)
                tileVertices = new List<Triple>()
                {
                    new Triple(i, j + offset, k),
                    new Triple(i + 1f - offset, j, k),
                    new Triple(i + 1f, j + 1f - offset, k),
                    new Triple(i + offset, j + 1f, k),
                };
            else
            {
                tileVertices = new List<Triple>()
                {
                    new Triple(i + offset, j, k),
                    new Triple(i + 1f, j + offset, k),
                    new Triple(i + 1f - offset, j + 1f, k),
                    new Triple(i, j + 1f - offset, k),
                };
            }


            if (thickness > 0.0)
                for (int ii = 0; ii < 4; ii++) tileVertices.Add(tileVertices[ii] + thickness * Triple.BasisZ);

            shapeMatchingGoals.Add(new ShapeMatchingGoal(tileVertices, targetShape));

            vertices.Add(tileVertices[0].ToPoint());
            vertices.Add(tileVertices[1].ToPoint());
            vertices.Add(tileVertices[2].ToPoint());
            vertices.Add(tileVertices[3].ToPoint());

            int n = vertices.Count - 4;

            indices.AddRange(new [] {n, n + 1, n + 2, n, n + 2, n + 3});
        }

        List<Goal> lengthGoals = new List<Goal>();

        for (int i = 1; i < xCount; i++)
        for (int j = 0; j <= yCount; j++)
            if ((i + j) % 2 == 1)
            {
                lengthGoals.Add(new LengthGoal(
                    new Triple(i - offset, j, 0),
                    new Triple(i + offset, j, 0)));
                lineBinders.Add(new LineBinder(
                    new Triple(i - offset, j, 0),
                    new Triple(i + offset, j, 0)));
            }

        for (int i = 0; i <= xCount; i++)
        for (int j = 1; j < yCount; j++)
            if ((i + j) % 2 == 0)
            {
                lengthGoals.Add(new LengthGoal(
                    new Triple(i, j - offset, 0),
                    new Triple(i, j + offset, 0)));
                lineBinders.Add(new LineBinder(
                    new Triple(i, j - offset, 0),
                    new Triple(i, j + offset, 0)));
            }

        meshBinders.Add(new MeshBinder(Mesh.ByVerticesAndIndices(vertices, indices), new Color(.3f, .6f, .8f, 1f)));

        return new Dictionary<string, object>
        {
            {"shapeMatchingGoals", shapeMatchingGoals},
            {"lengthGoals", lengthGoals},
            {"meshBinders", meshBinders},
            {"lineBinders", lineBinders},
        };
    }



    //[MultiReturn("shapeMatchingGoals", "lengthGoals", "meshBinders", "lineBinders")]
    //public static Dictionary<string, object> AuxeticRotatingCubes(int xCount = 5, int yCount = 5, int zCount = 5)
    //{
    //    List<ShapeMatchingGoal> shapeMatchingGoals = new List<ShapeMatchingGoal>();
    //    List<LineBinder> lineBinders = new List<LineBinder>();

    //    List<Point> meshVertices = new List<Point>();
    //    List<int> meshVertexIndices = new List<int>();

    //    float offset = 0.1f;

    //    for (int i = 0; i < xCount; i++)
    //    for (int j = 0; j < yCount; j++)
    //    {
    //        float k = 0f;

    //        List<Triple> triples = new List<Triple>();

    //        if ((i + j) % 2 == 0)
    //            triples = new List<Triple>()
    //            {
    //                new Triple(i              , j      + offset, k),
    //                new Triple(i + 1f - offset, j              , k),
    //                new Triple(i + 1f         , j + 1f - offset, k),
    //                new Triple(i      + offset, j + 1f         , k),
    //                new Triple(i              , j      + offset, k + 1),
    //                new Triple(i + 1f - offset, j              , k + 1),
    //                new Triple(i + 1f         , j + 1f - offset, k + 1),
    //                new Triple(i      + offset, j + 1f         , k + 1),
    //            };
    //        else
    //        {
    //            triples = new List<Triple>()
    //            {
    //                new Triple(i + offset, j, k),
    //                new Triple(i + 1f, j + offset, k),
    //                new Triple(i + 1f - offset, j + 1f, k),
    //                new Triple(i, j + 1f - offset, k),
    //                new Triple(i + offset, j, k + 1),
    //                new Triple(i + 1f, j + offset, k + 1),
    //                new Triple(i + 1f - offset, j + 1f, k + 1),
    //                new Triple(i, j + 1f - offset, k + 1),
    //            };
    //        }

    //        ProcessCube(triples[0], triples[1], triples[2], triples[3], triples[4], triples[5], triples[6], triples[7], shapeMatchingGoals, lineBinders, meshVertices, meshVertexIndices);
    //    }

    //    return new Dictionary<string, object>
    //    {
    //        {"shapeMatchingGoals", shapeMatchingGoals},
    //        {"lengthGoals", null},
    //        //{"onSurfaceGoal", surface == null ? null : new OnSurfaceGoal(allTriples, surface, 1f)},
    //        {"onSurfaceGoal", null},
    //        {"meshBinders", new List<MeshBinder> { new MeshBinder(Mesh.ByVerticesAndIndices(meshVertices, meshVertexIndices), new Color(.3f, .6f, .8f, 1f)) }},
    //        {"lineBinders", lineBinders},
    //    };
    //}


    [MultiReturn("shapeMatchingGoals", "mergeGoals", "meshBinders", "lineBinders")]
    public static Dictionary<string, object> AuxeticRotatingTriangles(int xCount = 5, int yCount = 5, double thickness = 0.0)
    {
        List<Goal> shapeMatchingGoals = new List<Goal>();
        List<Goal> mergeGoals = new List<Goal>();
        List<GeometryBinder> meshBinders = new List<GeometryBinder>();
        List<GeometryBinder> lineBinders = new List<GeometryBinder>();

        List<Point> vertices = new List<Point>();
        List<int> indices = new List<int>();

        double offset = 0.001;

        double cos30 = Math.Cos(Math.PI / 6.0);

        for (int j = 0; j < yCount; j++)
        for (int i = 0; i < xCount; i++)
        {
            double xOffset = 2 * i + 1;
            double yOffset = 2 * j * cos30 + cos30;

            Triple M = new Triple(xOffset, yOffset, 0.0);

            List<Triple> v = new List<Triple>();

            for (int k = 0; k < 6; k++)
                v.Add(new Triple(xOffset + Math.Cos(k * Math.PI / 3.0), yOffset + Math.Sin(k * Math.PI / 3.0), 0.0));

            for (int k = 0; k < 5; k++)
                shapeMatchingGoals.Add(ProcessTriangle(M, v[k], v[k + 1], k % 2 == 0, offset,  thickness, vertices, indices));

            shapeMatchingGoals.Add(ProcessTriangle(M, v[5], v[0], false, offset, thickness, vertices, indices));

            M = new Triple(xOffset + 1, yOffset, 0.0);

            if (i < xCount - 1)
            {
                shapeMatchingGoals.Add(ProcessTriangle(
                    M,
                    new Triple(xOffset + 1.5, yOffset + cos30, 0.0),
                    new Triple(xOffset + 0.5, yOffset + cos30, 0.0),
                    false, offset, thickness, vertices, indices));

                shapeMatchingGoals.Add(ProcessTriangle(
                    M,
                    new Triple(xOffset + 0.5, yOffset - cos30, 0.0),
                    new Triple(xOffset + 1.5, yOffset - cos30, 0.0),
                    true, offset, thickness, vertices, indices));
            }
        }

        meshBinders.Add(new MeshBinder(Mesh.ByVerticesAndIndices(vertices, indices), new Color(.3f, .6f, .8f, 1f)));

        //=======================================================================================
        // Line Binders
        //=======================================================================================

        for (int j = 0; j < yCount; j++)
        for (int i = 0; i < xCount * 2; i++)
        {
            double xOffset = i;
            double yOffset = 2 * j * cos30 + cos30;

            Triple M = new Triple(xOffset, yOffset, 0.0);

            List<Triple> v = new List<Triple>();

            for (int k = 1; k < 6; k+=2)
                v.Add(new Triple(xOffset + offset * Math.Cos(k * Math.PI / 3.0), yOffset + offset * Math.Sin(k * Math.PI / 3.0), 0.0));

            lineBinders.Add(new LineBinder(v[0], v[1]));
            lineBinders.Add(new LineBinder(v[1], v[2]));
            lineBinders.Add(new LineBinder(v[2], v[0]));

            mergeGoals.Add(new MergeGoal(v, 0.01f));
        }

        for (int j = 0; j <= yCount; j++)
        for (int i = 0; i < xCount * 2; i++)
        {
            double xOffset = i + 0.5;
            double yOffset = 2 * j * cos30;

            Triple M = new Triple(xOffset, yOffset, 0.0);

            List<Triple> v = new List<Triple>();

            for (int k = 1; k < 6; k += 2)
                v.Add(new Triple(xOffset + offset * Math.Cos(k * Math.PI / 3.0), yOffset + offset * Math.Sin(k * Math.PI / 3.0), 0.0));

            lineBinders.Add(new LineBinder(v[0], v[1]));
            lineBinders.Add(new LineBinder(v[1], v[2]));
            lineBinders.Add(new LineBinder(v[2], v[0]));

            mergeGoals.Add(new MergeGoal(v, 0.1f));
        }



        return new Dictionary<string, object>
        {
            {"shapeMatchingGoals", shapeMatchingGoals},
            {"mergeGoals", mergeGoals},
            {"meshBinders", meshBinders},
            {"lineBinders", lineBinders},
        };
    }


    private static ShapeMatchingGoal ProcessQuad(Triple A, Triple B, Triple C, Triple D, List<Point> vertices, List<int> indices)
    {
        List<Triple> triples = new List<Triple> { A, B, C, D };
        vertices.Add(A.ToPoint());
        vertices.Add(B.ToPoint());
        vertices.Add(C.ToPoint());
        vertices.Add(D.ToPoint());
        int n = vertices.Count - 4;
        indices.AddRange(new[] { n, n + 1, n + 2, n, n + 2, n + 3 });
        return new ShapeMatchingGoal(triples, triples);
    }

    private static ShapeMatchingGoal ProcessTriangle(Triple A, Triple B, Triple C, bool complementary, double offset, double thickness, List<Point> vertices, List<int> indices)
    {
        Triple AB = B - A;
        Triple BC = C - B;
        Triple CA = A - C;

        Triple M, N, Q;

        if (!complementary)
        {
            M = A + offset * AB / AB.Length;
            N = B + offset * BC / BC.Length;
            Q = C + offset * CA / CA.Length;
        }
        else
        {
            M = B - offset * AB / AB.Length;
            N = C - offset * BC / BC.Length;
            Q = A - offset * CA / CA.Length;
        }

        List<Triple> triples = new List<Triple> { M, N, Q };
        vertices.Add(M.ToPoint());
        vertices.Add(N.ToPoint());
        vertices.Add(Q.ToPoint());
        int n = vertices.Count - 3;
        indices.AddRange(new[] { n, n + 1, n + 2 });

        for (int i = 0; i < 3; i++)
            triples.Add(triples[i] + new Triple(0, 0, thickness));

        return new ShapeMatchingGoal(triples, triples);
    }
}