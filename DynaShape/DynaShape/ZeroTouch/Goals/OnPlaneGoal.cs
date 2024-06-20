using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// OnPlaneGoal
    /// </summary>
    public  class OnPlaneGoal
    {
        private OnPlaneGoal(){}


        /// <summary>
        /// Creates an OnPlaneGoal to force a set of nodes to lie on the specified plane.
        /// This differs from the CoPlanar goal, where the target plane is computed based on the current node positions rather than being defined and fixed in advance.
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="targetPlaneOrigin">The origin of the plane to use.</param>
        /// <param name="targetPlaneNormal">The normal of the plane to use.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="OnPlaneGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.OnPlaneGoal ByStartPositionsOriginNormal(
            List<Point> startPositions,
            [DefaultArgument("Point.Origin()")] Point targetPlaneOrigin,
            [DefaultArgument("Vector.ZAxis()")] Vector targetPlaneNormal,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.OnPlaneGoal>();
            goal.Initialize(
                startPositions.ToTriples(),
                targetPlaneOrigin.ToTriple(),
                targetPlaneNormal.ToTriple(),
                weight);
            return goal;
        }

        /// <summary>
        /// Creates an OnPlaneGoal to force a set of nodes to lie on the specified plane.
        /// This differs from the CoPlanar goal, where the target plane is computed based on the current node positions rather than being defined and fixed in advance.
        /// </summary>
        /// <param name="startPositions">The start positions of the nodes.</param>
        /// <param name="targetPlane">The target plane to use for the nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="OnPlaneGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.OnPlaneGoal ByStartPositionsPlane(
            List<Point> startPositions,
            [DefaultArgument("Plane.ByOriginNormal(Point.Origin(), Vector.ZAxis())")] Plane targetPlane,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.OnPlaneGoal>();
            goal.Initialize(
                startPositions.ToTriples(),
                targetPlane.Origin.ToTriple(),
                targetPlane.Normal.ToTriple(),
                weight);
            return goal;
        }
    }
}
