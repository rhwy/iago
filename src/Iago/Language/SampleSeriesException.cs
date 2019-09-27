using System;

namespace Iago.Language
{
    [Serializable]
    public class SampleSeriesException : Exception
    {
        private static string GetMessageFromNumber(int number, Exception inner=null)
        {
            return $"Sample #{number} :"+
                   (inner!=null?inner.Message:String.Empty);
        }

        public SampleSeriesException() { }
        public SampleSeriesException(int sampleNumber) : base(GetMessageFromNumber(sampleNumber)) { }
        public SampleSeriesException(int sampleNumber, Exception inner) : base(GetMessageFromNumber(sampleNumber,inner), inner) { }
        
        protected SampleSeriesException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
        public override string ToString()
        {
            if(base.InnerException==null)
                return base.ToString();
            return string.Format(
                $"{Message}{InnerException}");
        }
    }
}