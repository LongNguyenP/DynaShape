using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// CoLinear goal.
    /// </summary>
    public class CoLinearGoal
    {
        private CoLinearGoal(){}


        /// <summary>
        /// Creates a CoLinearGoal that attempts to force a set of nodes to lie on a common line.
        /// The line position and orientation are computed based on the current node positions.
        /// This is different from the OnLine goal, where the target line is fixed and defined in advance.
        /// </summary>
        /// <param name="startPositions">The starting points for the CoLinearGoal</param>
        /// <param name="weight">The goal's weight/impact on the solver</param>
        [NodeCategory("Create")]
        public static DynaShape.Goals.CoLinearGoal ByPoints(
            List<Point> startPositions,
            [DefaultArgument("1000.0")] float weight)
          {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.CoLinearGoal>();
            goal.Initialize(startPositions.ToTriples(), weight);
            return goal;
        }
    }
}
