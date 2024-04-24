using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// MergeGoal
    /// </summary>
    public  class MergeGoal
    {
        private MergeGoal(){}

        /// <summary>
        /// Pull a set of nodes to their center of mass.
        /// This is useful to force the nodes to have coincident positions.
        /// By default the weight value is set very high to ensure that the nodes really merge together at one position.
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.MergeGoal MergeGoal_Create(
            List<Point> startPositions,
            [DefaultArgument("1000.0")] float weight)
        {
            return new DynaShape.Goals.MergeGoal(startPositions.ToTriples(), weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.MergeGoal MergeGoal_Change(
            DynaShape.Goals.MergeGoal goal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
