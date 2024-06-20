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
        /// <param name="startPositions">The start positions of the nodes</param>
        /// <param name="floorHeight">The floor height</param>
        /// <param name="weight">The goal's weight/impact on the solver</param>
        [NodeCategory("Create")]
        public static DynaShape.Goals.FloorGoal Create(
            List<Point> startPositions,
            [DefaultArgument("0.0")] float floorHeight,
            [DefaultArgument("1000.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.FloorGoal>();
            goal.Initialize(startPositions.ToTriples(), floorHeight, weight);
            return goal;
        }
    }
}
