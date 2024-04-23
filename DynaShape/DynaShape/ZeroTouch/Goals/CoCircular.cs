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
        /// Creates a CoCircularGoal that attempts to force a set of nodes to lie on a common circular arc
        /// </summary>
        /// <param name="startPositions">The starting points for the CoCircularGoal</param>
        /// <param name="weight">The goal's weight/impact on the solver</param>
        /// <returns name="CoCircularGoal">A newly defined CoCircularGoal</returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.CoCircularGoal Create(
            List<Point> startPositions,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.CoCircularGoal(startPositions.ToTriples(), weight);
        }


        /// <summary>
        /// Modifies the  the CoCircularGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="coCircularGoal">A CoCircular goal to modify with the given parameters</param>
        /// <param name="weight">An optional new weight for the AngleGoal</param>
        /// <returns name="CoCircularGoal">The modified CoCircular goal</returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.CoCircularGoal Change(
            DynaShape.Goals.CoCircularGoal coCircularGoal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 0.0) coCircularGoal.Weight = weight;
            return coCircularGoal;
        }
    }
}
