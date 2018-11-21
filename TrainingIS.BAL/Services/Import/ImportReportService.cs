using System;
using System.Collections.Generic;
using System.Data;
using System.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Resources;
using static GApp.BLL.Services.MessagesService;

namespace TrainingIS.BLL
{
    /// <summary>
    /// Create import rapport
    /// </summary>
    public class ImportReport
    {
        public List<object> ImportedObjects { set; get; }
        public DataTable DataTableRows_Errors { set; get; }
        public DataTable DataTableMessage { set; get; }
        public int Number_of_inserted_rows { set; get; }
        public int Number_of_updated_rows { set; get; }
        public int Number_of_inserted_erros_rows { set; get; }
        public int Number_of_updated_erros_rows { set; get; }
        public List<Message> _Messages = null;

        protected Type EntityType;

        public ImportReport(Type EntityType, DataTable dataTable)
        {
            this.EntityType = EntityType;

            // Create DataTableRows_Errors instance
            string data_table_error_name = string.Format(msg_ImportService.entity_with_errors, EntityType.getLocalName());
            this.DataTableRows_Errors = new DataTable(data_table_error_name.Truncate(30));
            foreach (DataColumn item in dataTable.Columns)
            {
                DataColumn dataColumn = new DataColumn(item.ColumnName, item.DataType);
                this.DataTableRows_Errors.Columns.Add(dataColumn);
            }

            // Init statistic operation numbers
            Number_of_inserted_rows = 0;
            Number_of_updated_rows = 0;
            Number_of_inserted_erros_rows = 0;
            Number_of_updated_erros_rows = 0;

            // Init Messages
            _Messages = new List<Message>();

            // init DataTableMessage
            string DataTableMessage_Table_Name = string.Format("{0} messages", EntityType.getLocalName());
            DataTableMessage = new DataTable(DataTableMessage_Table_Name.Truncate(30));
            DataTableMessage.Columns.Add("Message");
            DataTableMessage.Columns.Add("MessageType");

            this.ImportedObjects = new List<object>();
        }

        #region AddMessage 
        public void AddMessage(string message, MessageTypes messageType)
        {
            this.AddMessage(message, "", messageType);

        }
        public void AddMessage(string message, MessageTypes messageType, DataRow dataRow)
        {
            this.AddMessage(message, "", messageType, dataRow);

        }
        public void AddMessage(string message, string title, MessageTypes messageType)
        {
            this.AddMessage(message, title, messageType, null);

        }
        public void AddMessage( string message, string title, MessageTypes messageType, DataRow dataRowError)
        {
            this.AddMessage("", message, title, messageType, dataRowError);
        }
        public void AddMessage(string Id, string message, string title, MessageTypes messageType, DataRow dataRowError)
        {
            // ArgumentException
            if ((messageType == MessageTypes.Error
                || messageType == MessageTypes.Add_Error
                || messageType == MessageTypes.Delete_Error
                || messageType == MessageTypes.Update_Error) && dataRowError == null)
            {
                string msg_ArgumentException = "you must set the DataRow of Error";
                throw new ArgumentException(msg_ArgumentException);
            }

            Message msg = new Message(Id, message, title, messageType);

            // Add DataRow error
            if (dataRowError != null)
            {
                this.Add_DataRows_WithDataBaseErros(dataRowError);
            }
            this.UpdateStaticNumbers(messageType);
            this.AddDataMessageRow(msg);
            this._Messages.Add(msg);
        }

        public bool IsErrorsExist()
        {
            if (this._Messages != null)
            {
                var messages_errors = this._Messages.Where(m =>
                            m.MessageType == MessageTypes.Error
                               || m.MessageType == MessageTypes.Add_Error
                               || m.MessageType == MessageTypes.Delete_Error
                               || m.MessageType == MessageTypes.Update_Error
                               ).ToList();
                if (messages_errors.Count > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
           
        }

        private void UpdateStaticNumbers(MessageTypes messageType)
        {
            switch (messageType)
            {
                case MessageTypes.Info:
                    break;
                case MessageTypes.Warning:
                    break;
                case MessageTypes.Error:
                    break;
                case MessageTypes.Add_Success:
                    Number_of_inserted_rows++;
                    break;
                case MessageTypes.Update_Success:
                    Number_of_updated_rows++;
                    break;
                case MessageTypes.Add_Warning:
                    break;
                case MessageTypes.Update_Warning:
                    break;
                case MessageTypes.Delete_Warning:
                    break;
                case MessageTypes.Add_Error:
                    Number_of_inserted_erros_rows++;
                    break;
                case MessageTypes.Update_Error:
                    Number_of_updated_erros_rows++;
                    break;
                case MessageTypes.Resume_Info:
                    break;
                case MessageTypes.Resume_Warning:
                    break;
                case MessageTypes.Resume_Error:
                    break;
            }
        }

        private void UpdateStaticNumbers()
        {
            throw new NotImplementedException();
        }

        private void AddDataMessageRow(Message msg)
        {
            // Add DataMessage 
            DataRow dataRowMessage = this.DataTableMessage.NewRow();
            dataRowMessage[0] = msg.Msg;
            dataRowMessage[1] = msg.MessageType;
            this.DataTableMessage.Rows.Add(dataRowMessage);
        }
        #endregion

        #region HTML Repport
        /// <summary>
        /// Get HTML repport
        /// </summary>
        /// <returns></returns>
        public string get_HTML_Report()
        {
            string html_meta_Info = "";
            string html_Error = "";
            string html_Waring = "";
            string html_Info = "";

            string Resume_Error = "";
            string Resume_waring = "";
            string Resume_Info = "";


            string html_report = "";

           
            string MessageFormat = "<div id=\"{0}\" class=\"{1}\" role=\"alert\" ><div class=\"title\">{2}</div>{3}</div>";


            // Resume
            if (Number_of_inserted_rows > 0)
            {
                string Resume_Number_of_inserted_rows = string.Format(msg_ImportService.In_total_there_is_the_insertion_of, this.Number_of_inserted_rows, this.EntityType.getLocalPluralName());
                this.AddMessage("Number_of_inserted_rows", Resume_Number_of_inserted_rows,"", MessageTypes.Resume_Info, null);
            }

            if (Number_of_updated_rows > 0)
            {
                string Resume_Number_of_updated_rows = string.Format(msg_ImportService.In_total_there_is_the_update_of, this.Number_of_updated_rows, this.EntityType.getLocalPluralName());
                this.AddMessage("Number_of_updated_rows", Resume_Number_of_updated_rows, "", MessageTypes.Resume_Info, null);
            }

            if (Number_of_inserted_erros_rows > 0)
            {
                string Resume_Number_of_inserted_erros_rows = string.Format(msg_ImportService.In_total_there_is_error_Insertion_of, this.Number_of_inserted_erros_rows);

                this.AddMessage("Number_of_inserted_erros_rows", Resume_Number_of_inserted_erros_rows, "", MessageTypes.Resume_Error, null);
            }

            if (Number_of_updated_erros_rows > 0)
            {
                string Resume_Number_of_updated_erros_rows = string.Format(msg_ImportService.In_total_there_is_error_Update__of, this.Number_of_updated_erros_rows);
                this.AddMessage("Number_of_updated_erros_rows", Resume_Number_of_updated_erros_rows, "", MessageTypes.Resume_Error, null);
            }



            foreach (var message in this._Messages)
            {
                if (!message.MessageType.ToString().Contains("Resume"))
                {
                    if (message.MessageType.ToString().Contains("Error"))
                    {
                        html_Error += string.Format(MessageFormat, message.Id, "alert alert-danger", message.Title, message.Msg);
                    }
                    if (message.MessageType.ToString().Contains("Waring"))
                    {
                        html_Waring += string.Format(MessageFormat, message.Id, "alert alert-warning", message.Title, message.Msg);
                    }
                    if (message.MessageType.ToString().Contains("Success"))
                    {
                        html_Info += string.Format(MessageFormat, message.Id, "alert alert-success", message.Title, message.Msg);
                    }
                    if (message.MessageType.ToString().Contains("Meta_msg"))
                    {
                        html_meta_Info += string.Format(MessageFormat, message.Id, "alert alert-info", message.Title, message.Msg);
                    }
                }
                else
                {
 
                    if (message.MessageType.ToString().Contains("Error"))
                    {
                        Resume_Error += string.Format(MessageFormat, message.Id, "alert alert-danger", message.Title, message.Msg);
                    }
                    if (message.MessageType.ToString().Contains("Waring"))
                    {
                        Resume_waring += string.Format(MessageFormat, message.Id, "alert alert-warning", message.Title, message.Msg);
                    }
                    if (message.MessageType.ToString().Contains("Info"))
                    {
                        Resume_Info += string.Format(MessageFormat, message.Id, "alert alert-info", message.Title, message.Msg);
                    }
                  

                }


            }

            html_report += html_meta_Info;
            html_report += "<hr>";
            html_report += Resume_Error + html_Waring + Resume_Info;
            html_report += "<hr>";
            html_report += html_Error + html_Waring + html_Info;
            
           
            return html_report;
        }
        #endregion

        #region DataRows with Errors
        private void Add_DataRows_WithDataBaseErros(DataRow dataRow)
        {
            this.DataTableRows_Errors.ImportRow(dataRow);
        }
        #endregion


        public DataSet get_DataSet_Report()
        {
            DataSet dataSet = new DataSet();
            if (this.DataTableRows_Errors.Rows.Count > 0)
                dataSet.Tables.Add(this.DataTableRows_Errors);

            dataSet.Tables.Add(this.DataTableMessage);

            return dataSet;
        }
    }
}
