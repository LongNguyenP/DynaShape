using Autodesk.DesignScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// Constant directional offset goal
    /// </summary>
    public class ConstantGoal
    {
        private ConstantGoal(){}

        /// <summary>
        /// Creates a ConstantGoal that applies a constant directional offset to the specified nodes. This goal is useful for simulating gravity.
        /// </summary>
        /// <param name="startPositions">The starting points for the ConstantGoal</param>
        /// <param name="constant">The constant vector to apply to the given nodes</param>
        /// <param name="weight">The goal's weight/impact on the solver</param>
        /// <returns name="ConstantGoal">A newly defined ConstantGoal</returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.ConstantGoal Create(
            List<Point> startPositions,
            [DefaultArgument("Vector.ByCoordinates(0, 0, -0.1)")] Vector constant,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.ConstantGoal(startPositions.ToTriples(), constant.ToTriple(), weight);
        }

        /// <summary>
        /// Modifies the ConstantGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="constantGoal">A ConstantGoal goal to modify with the given parameters</param>
        /// <param name="constant">The new constant vector to apply to the given nodes</param>
        /// <param name="weight">An optional new weight for the ConstantGoal</param>
        /// <returns name="ConstantGoal">The modified ConstantGoal</returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.ConstantGoal Change(
            DynaShape.Goals.ConstantGoal constantGoal,
            [DefaultArgument("null")] Vector constant,
            [DefaultArgument("-1.0")] float weight)
        {
            if (constant != null) constantGoal.Move = constant.ToTriple();
            if (weight >= 0.0) constantGoal.Weight = weight;
            return constantGoal;
        }
    }
}
