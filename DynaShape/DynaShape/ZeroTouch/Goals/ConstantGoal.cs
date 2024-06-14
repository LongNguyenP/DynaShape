using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// Constant directional offset goal
    /// </summary>
    public class ConstantGoal
    {
        private ConstantGoal(){}


        /// <summary>
        /// Creates a ConstantGoal that applies a constant directional offset to the specified nodes. This goal is useful for simulating gravity.
        /// </summary>
        /// <param name="startPositions">The starting points for the ConstantGoal.</param>
        /// <param name="constant">The constant vector to apply to the given nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="ConstantGoal">A newly defined ConstantGoal.</returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.ConstantGoal ByPoints(
            List<Point> startPositions,
            [DefaultArgument("Vector.ByCoordinates(0, 0, -0.1)")] Vector constant,
            [DefaultArgument("1.0")] float weight)
        {
            DynaShape.Goals.ConstantGoal goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.ConstantGoal>();
            goal.Initialize(
                startPositions.ToTriples(),
                constant.ToTriple(),
                weight);
            return goal;
        }
    }
}
