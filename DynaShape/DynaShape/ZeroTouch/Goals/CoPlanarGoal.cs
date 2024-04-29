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
        /// Creates a CoPlanarGoal that forces a set of nodes to lie on a common plane.
        /// The plane position and orientation are computed based on the current node positions.
        /// This is different from the OnPlane goal, where the target plane is fixed and defined in advance.
        /// </summary>
        /// <param name="startPositions">The start positions of the nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="CoPlanarGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.CoPlanarGoal Create(
            List<Point> startPositions,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.CoPlanarGoal(startPositions.ToTriples(), weight);
        }


        /// <summary>
        /// Adjust the CoPlanarGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="coPlanarGoal">A CoPlanarGoal to modify.</param>
        /// <param name="weight"></param>
        /// <returns name="CoPlanarGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.CoPlanarGoal Change(
            DynaShape.Goals.CoPlanarGoal coPlanarGoal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 0.0) coPlanarGoal.Weight = weight;
            return coPlanarGoal;
        }
    }
}
