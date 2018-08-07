using GApp.Core.MetaDatas.ReadConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Services.Messages;

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
            Meta_msg,
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

        EntityMetaDataConfiguratrion entityMetaData;
        public MessagesService(Type typeOfEntity)
        {
            entityMetaData = EntityMetaDataConfiguratrion.CreateConfigEntity(typeOfEntity);
        }

        #region Articles
        public string DefinitArticle()
        {
            if (this.entityMetaData.entityMetataData?.isMale == true)
            {
                if (this.entityMetaData.CultureInfo.TwoLetterISOLanguageName.ToLower() == "fr")
                {
                    return "le";
                }
                if (this.entityMetaData.CultureInfo.TwoLetterISOLanguageName.ToLower() == "en")
                {
                    return "the";
                }
                return "";
            }
            else
            {
                if (this.entityMetaData.CultureInfo.TwoLetterISOLanguageName.ToLower() == "fr")
                {
                    return "la";
                }
                if (this.entityMetaData.CultureInfo.TwoLetterISOLanguageName.ToLower() == "en")
                {
                    return "the";
                }
                return "";

            }

        }

        public string OfTheArticle()
        {
            if (this.DefinitArticle() == "le")
                return "du";
            if (this.DefinitArticle() == "la")
                return "de la";
            return "of the";

        }

        /// <summary>
        /// Fr : un une 
        /// </summary>
        /// <returns></returns>
        public string UndefindedArticle()
        {
            if (this.entityMetaData.entityMetataData?.isMale == true)
            {
                if (this.entityMetaData.CultureInfo.TwoLetterISOLanguageName.ToLower() == "fr")
                {
                    return "un";
                }
                if (this.entityMetaData.CultureInfo.TwoLetterISOLanguageName.ToLower() == "en")
                {
                    return "a";
                }
                return "";
            }
            else
            {
                if (this.entityMetaData.CultureInfo.TwoLetterISOLanguageName.ToLower() == "fr")
                {
                    return "une";
                }
                if (this.entityMetaData.CultureInfo.TwoLetterISOLanguageName.ToLower() == "en")
                {
                    return "a";
                }
                return "";

            }

        }
        public string OfUndefindedArticle()
        {

            if (this.entityMetaData.CultureInfo.TwoLetterISOLanguageName.ToLower() == "fr")
            {
                return "d'" + this.UndefindedArticle();
            }
            if (this.entityMetaData.CultureInfo.TwoLetterISOLanguageName.ToLower() == "en")
            {
                return "of ";
            }
            return "";
        }
        #endregion

        #region Add Titles to Controller_messages
        /// <summary>
        /// Add create entity title to messages 
        /// </summary>
        /// <param name="controller_messages"></param>
        public void Create(Dictionary<string, string> controller_messages)
        {
            string oneEntityName = this.OfUndefindedArticle() + " " + this.entityMetaData.entityMetataData?.SingularName?.ToLower();
            controller_messages["Create_Title"] = string.Format(msgMessagesService.CreateTitle, oneEntityName);
        }



        public void Edit(Dictionary<string, string> controller_messages)
        {
            string oneEntityName = this.OfTheArticle() + " " + this.entityMetaData.entityMetataData?.SingularName.ToLower() ;
            controller_messages["Edit_Title"] = string.Format(msgMessagesService.Edit_Title, oneEntityName).ToLower().FirstLetterToUpperCase();
        }

        public void Index(Dictionary<string, string> controller_messages)
        {
            controller_messages["Index_Title"] = string.Format(msgMessagesService.Manager_of, this.entityMetaData.entityMetataData?.PluralName)
                .ToLower()
                .FirstLetterToUpperCase();
        }

        public void Details(Dictionary<string, string> controller_messages)
        {
            //msg["Index_Title"] = string.Format(msgMessagesService.Manager_of, this.entityMetaData.entityMetataDataAttribute?.PluralName)
            //    .ToLower()
            //    .FirstLetterToUpperCase();
        }

        public void Delete(Dictionary<string, string> controller_messages)
        {
            string oneEntityName = this.OfTheArticle() + " " + this.entityMetaData.entityMetataData?.SingularName;
            controller_messages["Delete_Title"] = string.Format(msgMessagesService.Delete_Title, oneEntityName).ToLower().FirstLetterToUpperCase();
        }
        #endregion

    }
}
