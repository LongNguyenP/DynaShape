using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// MergeGoal
    /// </summary>
    public  class MergeGoal
    {
        private MergeGoal(){}


        /// <summary>
        /// Creates a MergeGoal to pull a set of nodes to their center of a given mass.
        /// This is useful to force the nodes to have coincident positions.
        /// By default the weight value is set very high to ensure that the nodes really merge together at one position.
        /// </summary>
        /// <param name="startPositions">The starting positions of the nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="MergeGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.MergeGoal ByPoints(
            List<Point> startPositions,
            [DefaultArgument("1000.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.MergeGoal>();
            goal.Initialize(startPositions.ToTriples(), weight);
            return goal;
        }
    }
}
