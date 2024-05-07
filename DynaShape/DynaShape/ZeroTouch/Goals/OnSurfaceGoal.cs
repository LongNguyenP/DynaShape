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
    /// OnSurfaceGoal
    /// </summary>
    public  class OnSurfaceGoal
    {
        private OnSurfaceGoal(){}

        /// <summary>
        /// Creates an OnSurfaceGoal to force a set of nodes to lie on the specified surface.
        /// </summary>
        /// <param name="startPositions">The start positions of the nodes.</param>
        /// <param name="targetSurface">The target surface to force the nodes to lie on.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="OnSurfaceGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.OnSurfaceGoal Create(
            List<Point> startPositions,
            Surface targetSurface,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.OnSurfaceGoal(startPositions.ToTriples(), targetSurface, weight);
        }


        /// <summary>
        /// Modifies the OnSurfaceGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="onSurfaceGoal">The OnSurfaceGoal to modify.</param>
        /// <param name="targetSurface">An optional new target surface to force the nodes to lie on.</param>
        /// <param name="weight">An optional new weight for the OnSurfaceGoal.</param>
        /// <returns name="OnSurfaceGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.OnSurfaceGoal Change(
            DynaShape.Goals.OnSurfaceGoal onSurfaceGoal,
            [DefaultArgument("null")] Surface targetSurface,
            float weight)
        {
            if (targetSurface != null) onSurfaceGoal.TargetSurface = targetSurface;
            if (weight >= 0.0) onSurfaceGoal.Weight = weight;
            return onSurfaceGoal;
        }
    }
}
