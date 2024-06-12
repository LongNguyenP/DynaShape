using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// Common circular goal.
    /// </summary>
    public class CoCircularGoal
    {
        private CoCircularGoal(){}


        /// <summary>
        /// Creates a CoCircularGoal that attempts to force a set of nodes to lie on a common circular arc.
        /// </summary>
        /// <param name="startPositions">The starting points for the CoCircularGoal</param>
        /// <param name="weight">The goal's weight/impact on the solver</param>
        /// <returns name="CoCircularGoal">A newly defined CoCircularGoal</returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.CoCircularGoal Create(
            List<Point> startPositions,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.CoCircularGoal>();
            goal.Initialize(startPositions.ToTriples(), weight);
            return goal;
        }
    }
}
