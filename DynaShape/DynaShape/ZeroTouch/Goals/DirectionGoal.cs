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
    /// DirectionGoal
    /// </summary>
    public  class DirectionGoal
    {
        private DirectionGoal(){}

        /// <summary>
        /// Creates a DirectionGoal to force a line connecting two nodes to be parallel to the specified direction.
        /// </summary>
        /// <param name="start">The start point of the line.</param>
        /// <param name="end">The end point of the line.</param>
        /// <param name="targetDirection">The direction to remain parallel to.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="DirectionGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.DirectionGoal Create(
            Point start,
            Point end,
            [DefaultArgument("null")] Vector targetDirection,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.DirectionGoal(
                start.ToTriple(),
                end.ToTriple(),
                targetDirection?.ToTriple() ?? (end.ToTriple() - start.ToTriple()).Normalise(),
                weight);
        }


        /// <summary>
        /// Adjust the DirectionGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="directionGoal">The DirectionGoal to modify.</param>
        /// <param name="targetDirection">The direction to remain parallel to.</param>
        /// <param name="weight">An optional new weight for the DirectionGoal.</param>
        /// <returns name="DirectionGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.DirectionGoal Change(
            DynaShape.Goals.DirectionGoal directionGoal,
            [DefaultArgument("null")] Vector targetDirection,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetDirection != null) directionGoal.TargetDirection = targetDirection.ToTriple();
            if (weight >= 0.0) directionGoal.Weight = weight;
            return directionGoal;
        }
    }
}
