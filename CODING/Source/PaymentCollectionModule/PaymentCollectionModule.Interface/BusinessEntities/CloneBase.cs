using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ProtoBuf;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    //[DataContract]
    //[ProtoInclude(2, typeof(Invoice))]
    //[ProtoInclude(3, typeof(Bill))]    
    //[ProtoInclude(4, typeof(PaymentMethod))] 
    [Serializable]
    public class CloneBase<T> : ICloneable
    {
        public CloneBase() { }

        private string m_status;


        /// <summary>
        /// status information
        /// </summary>

       // [DataMember(Order = 1)]
        public string Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return this.Clone();    // simply delegate to our type-safe 
        }

        /// <summary>
        /// Returns a deep copy of the object
        /// </summary>
        /// <returns></returns>        
        //public T Clone()
        //{
        //    T clone = default(T);
        //        BinaryFormatter bf = new BinaryFormatter();
        //        MemoryStream memStream = new MemoryStream();
        //        bf.Serialize(memStream, this);
        //        memStream.Flush();
        //        memStream.Position = 0;
        //        clone = ((T)bf.Deserialize(memStream));   //this is the copy             

        //    return clone;
        //}

        public T Clone()
        {
            T clone = default(T);
            MemoryStream ptMs = new MemoryStream();
            Serializer.Serialize<T>(ptMs, (T)Activator.CreateInstance(typeof(T)));
            ptMs.Flush();
            ptMs.Position = 0;
            clone = Serializer.Deserialize<T>(ptMs);

            return clone;
        }
    }
}
