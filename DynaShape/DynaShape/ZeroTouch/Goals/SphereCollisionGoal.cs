using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// SphereCollisionGoal
    /// </summary>
    public  class SphereCollisionGoal
    {
        private SphereCollisionGoal(){}


        /// <summary>
        /// Creates a SphereCollisionGoal that maintains a minimum distance between the nodes.
        /// </summary>
        /// <param name="centers">The centers of the nodes.</param>
        /// <param name="radii">The radii of the spheres.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="SphereCollisionGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.SphereCollisionGoal ByPointsRadii(
            List<Point> centers,
            List<float> radii,
            [DefaultArgument("1.0")] float weight)
        {
            if (centers.Count != radii.Count)
                throw new Exception("Error: centers count is not equal to radii count");

            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.SphereCollisionGoal>();
            goal.Initialize(centers.ToTriples(), radii, weight);
            return goal;
        }
    }
}
