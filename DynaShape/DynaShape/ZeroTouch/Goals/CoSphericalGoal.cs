using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// CoSphericalGoal
    /// </summary>
    public  class CoSphericalGoal
    {
        private CoSphericalGoal(){}

        /// <summary>
        /// Force a set of nodes to lie on a common spherical surface.
        /// The sphere position and radius are computed based the current node positions
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.CoSphericalGoal Create(
            List<Point> startPositions,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.CoSphericalGoal(startPositions.ToTriples(), weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.CoSphericalGoal Change(
            DynaShape.Goals.CoSphericalGoal goal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
