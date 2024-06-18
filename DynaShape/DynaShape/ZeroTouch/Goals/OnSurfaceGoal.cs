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
        public static DynaShape.Goals.OnSurfaceGoal ByPoints(
            List<Point> startPositions,
            Surface targetSurface,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.OnSurfaceGoal>();
            goal.Initialize(startPositions.ToTriples(), targetSurface, weight);
            return goal;
        }
    }
}
