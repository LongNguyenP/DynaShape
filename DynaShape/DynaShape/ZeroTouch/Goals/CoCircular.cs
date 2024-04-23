using Autodesk.DesignScript.Runtime;
using DynaShape.Goals;
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
    /// Common circular goal.
    /// </summary>
    public class CoCircularGoal
    {
        private CoCircularGoal(){}

        /// <summary>
        /// Force a set of nodes to lie on a common circular arc
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.CoCircularGoal CoCircularGoal_Create(
            List<Point> startPositions,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.CoCircularGoal(startPositions.ToTriples(), weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.CoCircularGoal CoCircularGoal_Change(
            DynaShape.Goals.CoCircularGoal goal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
