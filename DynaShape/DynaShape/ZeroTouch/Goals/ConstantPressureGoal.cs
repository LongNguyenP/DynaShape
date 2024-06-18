using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Mesh = Autodesk.Dynamo.MeshToolkit.Mesh;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// ConstantPressureGoal class
    /// </summary>
    public class ConstantPressureGoal
    {
        private ConstantPressureGoal(){}


        /// <summary>
        /// Creates a ConstantPressureGoal which applies a force perpendicular to a triangular surface, with magnitude proportional to the surface area.
        /// </summary>
        /// <param name="startPosition1"></param>
        /// <param name="startPosition2"></param>
        /// <param name="startPosition3"></param>
        /// <param name="pressure">The pressure being applied on the triangle.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        [NodeCategory("Create")]
        public static DynaShape.Goals.ConstantPressureGoal ByPoints(
            Point startPosition1,
            Point startPosition2,
            Point startPosition3,
            [DefaultArgument("0.1")] float pressure,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.ConstantPressureGoal>();
            goal.Initialize(startPosition1.ToTriple(), startPosition2.ToTriple(), startPosition3.ToTriple(), pressure, weight);
            return goal;
        }


        /// <summary>
        /// Creates a ConstantPressureGoal which applies a force perpendicular to a triangular surface, with magnitude proportional to the surface area.
        /// </summary>
        /// <param name="mesh">A mesh to apply the ConstantPressureGoal to.</param>
        /// <param name="pressure">The pressure being applied on the mesh's triangles.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        [NodeCategory("Create")]
        public static List<DynaShape.Goals.ConstantPressureGoal> ByMesh(
            Mesh mesh,
            [DefaultArgument("0.1")] float pressure,
            [DefaultArgument("1.0")] float weight)
        {
            var goals = TracingUtils.GetObjectFromTrace<List<DynaShape.Goals.ConstantPressureGoal>>();

            List<double> vertices = mesh.TrianglesAsNineNumbers.ToList();
            int faceCount = vertices.Count / 9;

            if (goals.Count != faceCount)
            {
                goals.Clear();
                for (int i = 0; i < faceCount; i++)
                    goals.Add(new DynaShape.Goals.ConstantPressureGoal());
            }

            for (int i = 0; i < faceCount; i++)
            {
                int j = i * 9;
                goals[i].Initialize(
                    new Triple(vertices[j + 0], vertices[j + 1], vertices[j + 2]),
                    new Triple(vertices[j + 3], vertices[j + 4], vertices[j + 5]),
                    new Triple(vertices[j + 6], vertices[j + 7], vertices[j + 8]),
                    pressure,
                    weight);
            }

            return goals;
        }
    }
}
