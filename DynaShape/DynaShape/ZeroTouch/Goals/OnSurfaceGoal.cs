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
    /// OnSurfaceGoal
    /// </summary>
    public  class OnSurfaceGoal
    {
        private OnSurfaceGoal(){}

        /// <summary>
        /// Force a set of nodes to lie on the specified surface.
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="targetSurface"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.OnSurfaceGoal OnSurfaceGoal_Create(
            List<Point> startPositions,
            Surface targetSurface,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.OnSurfaceGoal(startPositions.ToTriples(), targetSurface, weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="targetSurface"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.OnSurfaceGoal OnSurfaceGoal_Change(
            DynaShape.Goals.OnSurfaceGoal goal,
            [DefaultArgument("null")] Surface targetSurface,
            float weight)
        {
            if (targetSurface != null) goal.TargetSurface = targetSurface;
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
