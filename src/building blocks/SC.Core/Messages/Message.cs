using System;

namespace SC.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        /// <summary>
        //saber quando é a aggregacao
        /// </summary>
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}