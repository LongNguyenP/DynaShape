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
        public static DynaShape.Goals.MergeGoal Create(
            List<Point> startPositions,
            [DefaultArgument("1000.0")] float weight)
        {
            return new DynaShape.Goals.MergeGoal(startPositions.ToTriples(), weight);
        }


        /// <summary>
        /// Modifies the MergeGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="mergeGoal">The MergeGoal to modify.</param>
        /// <param name="weight">An optional new weight for the MergeGoal.</param>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.MergeGoal Change(
            DynaShape.Goals.MergeGoal mergeGoal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 0.0) mergeGoal.Weight = weight;
            return mergeGoal;
        }
    }
}
