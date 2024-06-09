using Autodesk.DesignScript.Runtime;
using DynamoServices;
using DynaShape.Goals;

namespace DynaShape;

[IsVisibleInDynamoLibrary(false)]
internal class TracingUtils
{
    private const string TraceID = "{0459D869-0C72-447F-96D8-08A7FB92214B}-REVIT";
    private static Dictionary<Guid, object> traceableObjectDictionary = new Dictionary<Guid, object>();

    public static T GetObjectFromTrace<T>() where T : new()
    {
        T item;
        Guid traceId = TracingUtils.GetObjectIdFromTrace();

        if (traceId.Equals(Guid.Empty))
        {
            item = new T();
            TracingUtils.RegisterTraceableObjectForId(Guid.NewGuid(), item);
        }
        else
        {
            item = (T)TracingUtils.GetTracedObjectById(traceId);

            if (item == null)
            {
                item = new T();
                TracingUtils.RegisterTraceableObjectForId(traceId, item);
            }
        }

        return item;
    }

    private static Guid GetObjectIdFromTrace()
    {
        string id = TraceUtils.GetTraceData(TraceID);
        return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
    }

    private static object GetTracedObjectById(Guid id)
    {
        object item;
        traceableObjectDictionary.TryGetValue(id, out item);
        return item;
    }

    private static void RegisterTraceableObjectForId(Guid id, object objectToTrace)
    {
        if (traceableObjectDictionary.ContainsKey(id))
        {
            traceableObjectDictionary[id] = objectToTrace;
        }
        else
        {
            traceableObjectDictionary.Add(id, objectToTrace);
            TraceUtils.SetTraceData(TraceID, id.ToString());
        }
    }
}