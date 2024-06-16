using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Mesh = Autodesk.Dynamo.MeshToolkit.Mesh;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// DirectionalWindGoal
    /// </summary>
    public  class DirectionalWindGoal
    {
        private DirectionalWindGoal(){}


        /// <summary>
        /// Creates a DirectionalWindGoal to simulate wind by applying a constant force on the three vertices of a triangle,
        /// scaled by the cosine of the angle between the wind vector and the triangle's normal.
        /// With this implementation, the wind has full effect when it hits the triangle head-on, and zero
        /// effect if it blows parallel to the triangle.
        /// </summary>
        /// <param name="startPosition1">A triangle's first node position.</param>
        /// <param name="startPosition2">A triangle's second node position.</param>
        /// <param name="startPosition3">A triangle's third node position.</param>
        /// <param name="windVector">A vector representing the direction of the wind.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        [NodeCategory("Create")]
        public static DynaShape.Goals.DirectionalWindGoal ByPoints(
            Point startPosition1,
            Point startPosition2,
            Point startPosition3,
            [DefaultArgument("Vector.ByCoordinates(1.0, 0, 0)")] Vector windVector,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.DirectionalWindGoal>();
            goal.Initialize(
                startPosition1.ToTriple(),
                startPosition2.ToTriple(),
                startPosition3.ToTriple(),
                windVector.ToTriple(),
                weight);
            return goal;
        }


        /// <summary>
        /// Creates a DirectionalWindGoal to simulate wind blowing along a specified direction, by applying a force on the three vertices of a triangle,
        /// The force magnitude is additionally scaled by the cosine of the angle between the wind vector and the triangle's normal.
        /// With this implementation, the wind has full effect when it hits the triangle head-on, and zero
        /// effect if it blows parallel to the triangle.
        /// </summary>
        /// <param name="mesh">A mesh comprised of triangles to apply the DirectionalWindGoal to.</param>
        /// <param name="windVector">A vector representing the direction of the wind.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        [NodeCategory("Create")]
        public static List<DynaShape.Goals.DirectionalWindGoal> ByMesh(
            Mesh mesh,
            [DefaultArgument("Vector.ByCoordinates(1.0, 0, 0)")] Vector windVector,
            [DefaultArgument("1.0")] float weight)
        {
            List<DynaShape.Goals.DirectionalWindGoal> goals = new List<DynaShape.Goals.DirectionalWindGoal>();

            List<double> vertices = mesh.TrianglesAsNineNumbers.ToList();

            int faceCount = vertices.Count / 9;

            for (int i = 0; i < faceCount; i++)
            {
                int j = i * 9;
                var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.DirectionalWindGoal>();
                goal.Initialize(
                    new Triple(vertices[j + 0], vertices[j + 1], vertices[j + 2]),
                    new Triple(vertices[j + 3], vertices[j + 4], vertices[j + 5]),
                    new Triple(vertices[j + 6], vertices[j + 7], vertices[j + 8]),
                    windVector.ToTriple(),
                    weight);
                goals.Add(goal);
            }

            return goals;
        }
    }
}
