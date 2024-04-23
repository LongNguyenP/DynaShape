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
        /// <param name="pressure">The pressure being applied on the triangle</param>
        /// <param name="weight">The goal's weight/impact on the solver</param>
        /// <returns name="ConstantPressureGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.ConstantPressureGoal Create(
            Point startPosition1,
            Point startPosition2,
            Point startPosition3,
            [DefaultArgument("0.1")] float pressure,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.ConstantPressureGoal(startPosition1.ToTriple(), startPosition2.ToTriple(), startPosition3.ToTriple(), pressure, weight);
        }


        /// <summary>
        /// Creates a ConstantPressureGoal which applies a force perpendicular to a triangular surface, with magnitude proportional to the surface area.
        /// </summary>
        /// <param name="mesh">A mesh to apply the ConstantPressureGoal to</param>
        /// <param name="pressure">The pressure being applied on the mesh's triangles</param>
        /// <param name="weight">The goal's weight/impact on the solver</param>
        /// <returns name="ConstantPressureGoal"></returns>
        [NodeCategory("Create")]
        public static List<DynaShape.Goals.ConstantPressureGoal> Create(
            Mesh mesh,
            [DefaultArgument("0.1")] float pressure,
            [DefaultArgument("1.0")] float weight)
        {
            List<DynaShape.Goals.ConstantPressureGoal> pressureGoals = new List<DynaShape.Goals.ConstantPressureGoal>();

            List<double> vertices = mesh.TrianglesAsNineNumbers.ToList();

            int faceCount = vertices.Count / 9;

            for (int i = 0; i < faceCount; i++)
            {
                int j = i * 9;
                pressureGoals.Add(
                    new DynaShape.Goals.ConstantPressureGoal(
                        new Triple(vertices[j + 0], vertices[j + 1], vertices[j + 2]),
                        new Triple(vertices[j + 3], vertices[j + 4], vertices[j + 5]),
                        new Triple(vertices[j + 6], vertices[j + 7], vertices[j + 8]),
                        pressure,
                        weight));
            }

            return pressureGoals;
        }


        /// <summary>
        /// Modifies the ConstantPressureGoal's parameters while the solver is running
        /// </summary>
        /// <param name="constantPressureGoal">A ConstantPressureGoal to modify with the given parameters</param>
        /// <param name="pressure">An optional new pressure to apply to the ConstantPressureGoal</param>
        /// <param name="weight">An optional new weight for the AngleGoal</param>
        /// <returns name="ConstantPressureGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.ConstantPressureGoal Change(
            DynaShape.Goals.ConstantPressureGoal constantPressureGoal,
            [DefaultArgument("-1.0")] float pressure,
            [DefaultArgument("-1.0")] float weight)
        {
            if (pressure >= 0.0) constantPressureGoal.Pressure = pressure;
            if (weight >= 0.0) constantPressureGoal.Weight = weight;
            return constantPressureGoal;
        }
    }
}
