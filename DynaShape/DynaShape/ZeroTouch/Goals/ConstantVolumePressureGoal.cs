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
        /// <returns name="ConstantVolumePressureGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.ConstantVolumePressureGoal Create(
            Mesh mesh,
            [DefaultArgument("0.0")] float volumePressureConstant,
            [DefaultArgument("1.0")] float weight)
        {
            return new
            DynaShape.Goals.ConstantVolumePressureGoal(mesh, volumePressureConstant, weight);
        }


        /// <summary>
        /// Modifies the ConstantVolumePressureGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="constantVolumePressureGoal">A ConstantVolumePressureGoal to modify.</param>
        /// <param name="volumePressureConstant">An optional new constant for the ConstantVolumePressureGoal.</param>
        /// <param name="weight">An optional new weight for the ConstantVolumePressureGoal.</param>
        /// <returns name="ConstantVolumePressureGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.ConstantVolumePressureGoal Change(
            DynaShape.Goals.ConstantVolumePressureGoal constantVolumePressureGoal,
            [DefaultArgument("-1.0")] float volumePressureConstant,
            [DefaultArgument("-1.0")] float weight)
        {
            if (volumePressureConstant >= 0.0) constantVolumePressureGoal.VolumePressureConstant = volumePressureConstant;
            if (weight >= 0.0) constantVolumePressureGoal.Weight = weight;
            return constantVolumePressureGoal;
        }
    }
}
