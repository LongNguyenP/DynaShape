using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// 
    /// </summary>
    public  class CoLinearGoal
    {
        private CoLinearGoal(){}

        /// <summary>
        /// Creates a CoLinearGoal that attempts to force a set of nodes to lie on a common line.
        /// The line position and orientation are computed based on the current node positions.
        /// This is different from the OnLine goal, where the target line is fixed and defined in advance.
        /// </summary>
        /// <param name="startPositions">The starting points for the CoLinearGoal</param>
        /// <param name="weight">The goal's weight/impact on the solver</param>
        /// <returns></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.CoLinearGoal Create(
            List<Point> startPositions,
            [DefaultArgument("1000.0")] float weight)
        {
            return new DynaShape.Goals.CoLinearGoal(startPositions.ToTriples(), weight);
        }


        /// <summary>
        /// Modifies the CoLinearGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="coLinearGoal">A CoLinearGoal to modify with the given parameters</param>
        /// <param name="weight">An optional new weight for the AngleGoal</param>
        /// <returns name="CoLinearGoal">The modified CoLinearGoal goal</returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.CoLinearGoal Change(
            DynaShape.Goals.CoLinearGoal coLinearGoal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 0.0) coLinearGoal.Weight = weight;
            return coLinearGoal;
        }
    }
}
