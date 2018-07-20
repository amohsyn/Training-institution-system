using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TrainingIS.BLL.MessagesService;

namespace TrainingIS.BLL
{
    /// <summary>
    /// Create import rapport
    /// </summary>
    public class ImportReportService
    {

        protected List<Message> _Messages = null;

        public ImportReportService()
        {
            _Messages = new List<Message>();
        }

        #region AddMessage 
        public void AddMessage(string message)
        {
            Message msg = new Message(message);
            this._Messages.Add(msg);
        }
        public void AddMessage(string message,string title)
        {
            Message msg = new Message(message, title);
            this._Messages.Add(msg);
        }
        public void AddMessage(string message, MessageTypes messageType)
        {
            Message msg = new Message(message, messageType);
            this._Messages.Add(msg);
        }
        public void AddMessage(string message, string title,MessageTypes messageType)
        {
            Message msg = new Message(message, title, messageType);
            this._Messages.Add(msg);
        }


        #endregion

        /// <summary>
        /// Get HTML repport
        /// </summary>
        /// <returns></returns>
        public string getReport()
        {
            string html_Error = "";
            string html_Waring = "";
            string html_Info = "";

            string Resume_Error = "";
            string Resume_waring = "";
            string Resume_Info = "";

            string html_report = "";

            string MessageFormat = "<div class=\"{0}\" role=\"alert\" ><div class=\"title\">{1}</div>{2}</div>";


            foreach (var message in this._Messages)
            {
                if (!message.MessageType.ToString().Contains("Resume"))
                {
                    if (message.MessageType.ToString().Contains("Error")) {
                        html_Error += string.Format(MessageFormat, "alert alert-danger", message.Title, message.Msg);
                    }
                    if (message.MessageType.ToString().Contains("Waring"))
                    {
                        html_Waring += string.Format(MessageFormat, "alert alert-warning", message.Title, message.Msg);
                    }
                    if (message.MessageType.ToString().Contains("Success"))
                    {
                        html_Info += string.Format(MessageFormat, "alert alert-success", message.Title, message.Msg);
                    }
                }
                else
                {
                    if (message.MessageType.ToString().Contains("Error"))
                    {
                        Resume_Error += string.Format(MessageFormat, "alert alert-danger", message.Title, message.Msg);
                    }
                    if (message.MessageType.ToString().Contains("Waring"))
                    {
                        Resume_waring += string.Format(MessageFormat, "alert alert-warning", message.Title, message.Msg);
                    }
                    if (message.MessageType.ToString().Contains("Info"))
                    {
                        Resume_Info += string.Format(MessageFormat, "alert alert-info", message.Title, message.Msg);
                    }

                }

                
            }


            html_report = html_Error + html_Waring + html_Info;
            html_report += "<hr>";
            html_report += Resume_Error + html_Waring + Resume_Info;
            return html_report;


             
        }
    }
}
