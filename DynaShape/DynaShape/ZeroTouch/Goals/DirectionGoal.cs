using Autodesk.DesignScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Geometry;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// DirectionGoal
    /// </summary>
    public  class DirectionGoal
    {
        private DirectionGoal(){}

        /// <summary>
        /// Force the line connecting two nodes to be parallel to the specified direction.
        /// </summary>
        /// <param name="startPosition1"></param>
        /// <param name="startPosition2"></param>
        /// <param name="targetDirection"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.DirectionGoal DirectionGoal_Create(
            Point startPosition1,
            Point startPosition2,
            [DefaultArgument("null")] Vector targetDirection,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.DirectionGoal(
                startPosition1.ToTriple(),
                startPosition2.ToTriple(),
                targetDirection?.ToTriple() ?? (startPosition2.ToTriple() - startPosition1.ToTriple()).Normalise(),
                weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="targetDirection"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.DirectionGoal DirectionGoal_Change(
            DynaShape.Goals.DirectionGoal goal,
            [DefaultArgument("null")] Vector targetDirection,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetDirection != null) goal.TargetDirection = targetDirection.ToTriple();
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
