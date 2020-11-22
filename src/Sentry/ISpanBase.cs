using ContribSentry.Enums;
using System;
using System.Collections.Generic;

namespace ContribSentry.Interface
{
    public class Dumb : ISpanBase
    {
        public string Description => throw new NotImplementedException();

        public string Op => throw new NotImplementedException();

        public string SpanId => throw new NotImplementedException();

        public string ParentSpanId => throw new NotImplementedException();

        public DateTimeOffset? StartTimestamp => throw new NotImplementedException();

        public DateTimeOffset? Timestamp => throw new NotImplementedException();

        public string TraceId => throw new NotImplementedException();

        public bool Error => throw new NotImplementedException();

        public string Status => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Finish()
        {
            throw new NotImplementedException();
        }

        public void Finish(int? httpStatus)
        {
            throw new NotImplementedException();
        }

        public void GetParentSpans(List<ISpanBase> spans)
        {
            throw new NotImplementedException();
        }

        public ISpanBase StartChild(string url, ESpanRequest requestType)
        {
            throw new NotImplementedException();
        }

        public ISpanBase StartChild(string description, string op)
        {
            throw new NotImplementedException();
        }
    }
    public static class Xunxo
    {
        public static Func<string,string,ISpanBase> Start = (a,b)=>new Dumb();
        public static Func<string, ESpanRequest, ISpanBase> StartReq = (a, b) => new Dumb();
    }
    public interface ISpanBase : IDisposable
    {
        string Description { get; }
        string Op { get; }
        string SpanId { get; }
        string ParentSpanId { get; }
        DateTimeOffset? StartTimestamp { get; }
        DateTimeOffset? Timestamp { get; }
        string TraceId { get; }
        bool Error { get; }
        string Status { get; }


        void Finish();
        void Finish(int? httpStatus);
        void GetParentSpans(List<ISpanBase> spans);
        ISpanBase StartChild(string url, ESpanRequest requestType);
        ISpanBase StartChild(string description, string op = null);
    }
}
