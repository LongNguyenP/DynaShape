using Autodesk.DesignScript.Runtime;
using Autodesk.Dynamo.MeshToolkit;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// ConstantVolumePressureGoal
    /// </summary>
    public class ConstantVolumePressureGoal
    {
        private ConstantVolumePressureGoal(){}


        /// <summary>
        /// Creates a ConstantVolumePressureGoal that simulates pressure trapped inside a closed volume. The pressure decreases as the volume expands (Boyle's law).
        /// </summary>
        /// <param name="mesh">A closed mesh to apply the ConstantVolumePressureGoal to.</param>
        /// <param name="volumePressureConstant">The constant that is the product of the pressure and volume. Meaning, the pressure will automatically decrease as the mesh volume increases (and vice versa).</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        [NodeCategory("Create")]
        public static DynaShape.Goals.ConstantVolumePressureGoal ByMesh(
            Mesh mesh,
            [DefaultArgument("0.0")] float volumePressureConstant,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.ConstantVolumePressureGoal>();
            goal.Initialize(mesh, volumePressureConstant, weight);
            return goal;
        }
    }
}
