using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// CoSphericalGoal
    /// </summary>
    public  class CoSphericalGoal
    {
        private CoSphericalGoal(){}


        /// <summary>
        /// Creates a CoSphericalGoal that forces a set of nodes to lie on a common spherical surface.
        /// The sphere position and radius are computed based the current node positions
        /// </summary>
        /// <param name="startPositions">The start positions of the nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="CoSphericalGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.CoSphericalGoal ByPoints(
            List<Point> startPositions,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.CoSphericalGoal>();
            goal.Initialize(startPositions.ToTriples(), weight);
            return goal;
        }
    }
}
