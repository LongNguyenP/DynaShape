using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// FloorGoal
    /// </summary>
    public  class FloorGoal
    {
        private FloorGoal(){}

        /// <summary>
        /// Creates a FloorGoal to force a set of nodes to stay above a horizontal floor plane.
        /// </summary>
        /// <param name="startPositions">The start positions.</param>
        /// <param name="floorHeight">The floor height to maintain.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="FloorGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.FloorGoal Create(
            List<Point> startPositions,
            [DefaultArgument("0.0")] float floorHeight,
            [DefaultArgument("1000.0")] float weight)
        {
            return new DynaShape.Goals.FloorGoal(startPositions.ToTriples(), floorHeight, weight);
        }


        /// <summary>
        /// Modifies the FloorGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="floorGoal">The FloorGoal to modify.</param>
        /// <param name="floorHeight">An optional new floor height parameter of the FloorGoal.</param>
        /// <param name="weight">An optional new weight for the EqualLengthsGoal.</param>
        /// <returns name="FloorGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.FloorGoal Change(
            DynaShape.Goals.FloorGoal floorGoal,
            [DefaultArgument("0.0")] float floorHeight,
            [DefaultArgument("-1.0")] float weight)
        {
            floorGoal.FloorHeight = floorHeight;
            if (weight > 0.0) floorGoal.Weight = weight;
            return floorGoal;
        }
    }
}
