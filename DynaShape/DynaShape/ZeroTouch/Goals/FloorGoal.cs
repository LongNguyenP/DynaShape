using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// FloorGoal
    /// </summary>
    public  class FloorGoal
    {
        private FloorGoal(){}

        /// <summary>
        /// Force a set of nodes to stay above a horizontal floor plane.
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="floorHeight"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.FloorGoal FloorGoal_Create(
            List<Point> startPositions,
            [DefaultArgument("0.0")] float floorHeight,
            [DefaultArgument("1000.0")] float weight)
        {
            return new DynaShape.Goals.FloorGoal(startPositions.ToTriples(), floorHeight, weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="floorHeight"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.FloorGoal FloorGoal_Change(
            DynaShape.Goals.FloorGoal goal,
            [DefaultArgument("0.0")] float floorHeight,
            [DefaultArgument("-1.0")] float weight)
        {
            goal.FloorHeight = floorHeight;
            if (weight > 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
