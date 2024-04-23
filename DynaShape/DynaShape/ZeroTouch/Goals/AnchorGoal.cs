using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// Anchor goal to simulate pinning down an object at points.
    /// </summary>
    public class AnchorGoal
    {
        private AnchorGoal(){}

        /// <summary>
        /// Creates an AnchorGoal to attempt to keep a node on a specified point
        /// By default the weight for this goal is set very high to ensure the node really "sticks" to the anchor
        /// </summary>
        /// <param name="startPosition">The starting position for this AnchorGoal</param>
        /// <param name="anchor">The resting position for this anchor goal.</param>
        /// <param name="weight">Weight controls how strict the goal is.</param>
        /// <returns name="anchorGoal">A newly defined AnchorGoal.</returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.AnchorGoal Create(
            Point startPosition,
            [DefaultArgument("null")] Point anchor,
            [DefaultArgument("1000.0")] float weight)
        {
            return new DynaShape.Goals.AnchorGoal(
                startPosition.ToTriple(),
                anchor?.ToTriple() ?? startPosition.ToTriple(),
                weight);
        }


        /// <summary>
        /// Modifies the AngleGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal">An AnchorGoal to modify with the given parameters.</param>
        /// <param name="anchor">The new anchor for the given AnchorGoal</param>
        /// <param name="weight">An optional new weight for the given AnchorGoal</param>
        /// <returns name="anchorGoal">The modified AnchorGoal</returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.AnchorGoal Change(
            DynaShape.Goals.AnchorGoal goal,
            [DefaultArgument("null")] Point anchor,
            [DefaultArgument("-1.0")] float weight)
        {
            if (anchor != null) goal.Anchor = anchor.ToTriple();
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
