using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// CoPlanarGoal
    /// </summary>
    public class CoPlanarGoal
    {
        private CoPlanarGoal(){}

        /// <summary>
        /// Force a set of nodes to lie on a common plane.
        /// The plane position and orientation are computed based on the current node positions.
        /// This is different from the OnPlane goal, where the target plane is fixed and defined in advance.
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.CoPlanarGoal Create(
            List<Point> startPositions,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.CoPlanarGoal(startPositions.ToTriples(), weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.CoPlanarGoal Change(
            DynaShape.Goals.CoPlanarGoal goal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
