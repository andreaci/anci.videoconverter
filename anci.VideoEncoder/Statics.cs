using System.ComponentModel;

namespace anci.VideoEncoder
{
    public static class Statics
    {
        //public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
        //{
        //    if (obj.InvokeRequired)
        //        obj.Invoke(action, new object[0]);
        //    else
        //        action();
        //}

        public delegate void InvokeIfRequiredDelegate<T>(T obj)
            where T : ISynchronizeInvoke;

        public static void InvokeIfRequired<T>(this T obj, InvokeIfRequiredDelegate<T> action)
            where T : ISynchronizeInvoke
        {
            if (obj.InvokeRequired)
                obj.Invoke(action, new object[] { obj });
            else
                action(obj);
        }
    }
}