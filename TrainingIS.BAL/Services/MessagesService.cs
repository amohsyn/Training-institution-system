using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.BLL
{
    /// <summary>
    /// Messges Manager
    /// </summary>
    public class MessagesService
    {
        #region Classes
        public enum MessageTypes
        {
            Info,
            Warning,
            Error,
            Add_Success,
            Update_Success,
            Delete_Success,
            Add_Warning,
            Update_Warning,
            Delete_Warning,
            Add_Error,
            Update_Error,
            Delete_Error,
            Resume_Info,
            Resume_Warning,
            Resume_Error,
        }

        public class Message
        {
            public string Title { set; get; }
            public string Msg { set; get; }
            public MessageTypes MessageType { set; get; }

            #region Constructors
            public Message(string Message)
            {
                this.Msg = Message;
            }
            public Message(string Message, MessageTypes MessageType) : this(Message)
            {
                this.MessageType = MessageType;
            }
            public Message(string Message, string Title) : this(Message)
            {
                this.Title = Title;
            }
            public Message(string message, string title, MessageTypes MessageType)
                : this(message, title)
            {
                this.MessageType = MessageType;
            }
            #endregion
        }
        #endregion
    }
}
