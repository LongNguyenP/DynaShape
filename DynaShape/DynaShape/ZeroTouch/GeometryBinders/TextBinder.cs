using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DSCore;

namespace DynaShape.ZeroTouch.GeometryBinders;

/// <summary>
/// Wrapper class for TextBinder
/// </summary>
public class TextBinder
{
    private TextBinder(){}
    /// <summary>
    /// Create a TextBinder to preview text.
    /// </summary>
    /// <param name="position">The location of the text.</param>
    /// <param name="text">The string value of the text.</param>
    /// <param name="color">An optional color to preview the circle geometry.</param>
    /// <returns name="textBinder"></returns>
    public static DynaShape.GeometryBinders.TextBinder ByPositionText(
        Point position,
        string text,
        [DefaultArgument("null")] Color color)
    {
        return new DynaShape.GeometryBinders.TextBinder(position.ToTriple(), text, color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor);
    }
}