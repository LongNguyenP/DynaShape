using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// CoPlanarGoal
    /// </summary>
    public class CoPlanarGoal
    {
        private CoPlanarGoal(){}


        /// <summary>
        /// Create a CoPlanarGoal that forces a set of nodes to lie on a common plane.
        /// The plane position and orientation are computed based on the current node positions.
        /// This is different from the OnPlane goal, where the target plane is fixed and defined in advance.
        /// </summary>
        /// <param name="startPositions">The start positions of the nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        [NodeCategory("Create")]
        public static DynaShape.Goals.CoPlanarGoal ByPoints(
            List<Point> startPositions,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.CoPlanarGoal>();
            goal.Initialize(startPositions.ToTriples(), weight);
            return goal;
        }
    }
}
