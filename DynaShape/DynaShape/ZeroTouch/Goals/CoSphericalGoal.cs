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
        public static DynaShape.Goals.CoSphericalGoal Create(
            List<Point> startPositions,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.CoSphericalGoal(startPositions.ToTriples(), weight);
        }


        /// <summary>
        /// Adjust the CoSphericalGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="coSphericalGoal">A CoSphericalGoal to modify.</param>
        /// <param name="weight">An optional new weight for the CoSphericalGoal.</param>
        /// <returns name ="CoSphericalGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.CoSphericalGoal Change(
            DynaShape.Goals.CoSphericalGoal coSphericalGoal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 0.0) coSphericalGoal.Weight = weight;
            return coSphericalGoal;
        }
    }
}
