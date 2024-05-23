using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DSCore;

namespace DynaShape.ZeroTouch.GeometryBinders.GeometryBinders;

/// <summary>
/// Wrapper class for LineBinder
/// </summary>
public class LineBinder
{
    private LineBinder(){}

    /// <summary>
    /// Create a LinderBinder to preview line geometry.
    /// </summary>
    /// <param name="line">The line to generate a preview for.</param>
    /// <param name="color">An optional color to preview the circle geometry.</param>
    /// <returns name="lineBinder"></returns>
    public static DynaShape.GeometryBinders.LineBinder ByLine(
        Line line,
        [DefaultArgument("null")] Color color)
    {
        return new DynaShape.GeometryBinders.LineBinder(
            line.StartPoint.ToTriple(),
            line.EndPoint.ToTriple(),
            color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor);
    }
    /// <summary>
    /// Create a LinderBinder to preview line geometry.
    /// </summary>
    /// <param name="startPoint">The start point of the line.</param>
    /// <param name="endPoint">The end point of the line.</param>
    /// <param name="color">An optional color to preview the circle geometry.</param>
    /// <returns name="lineBinder"></returns>
    public static DynaShape.GeometryBinders.LineBinder ByStartPointEndPoint(
        Point startPoint,
        Point endPoint,
        [DefaultArgument("null")] Color color)
    {
        return new DynaShape.GeometryBinders.LineBinder(
            startPoint.ToTriple(),
            endPoint.ToTriple(),
            color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor);
    }
}