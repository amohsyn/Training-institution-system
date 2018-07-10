using GApp.Core.MetaDatas.ReadConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingIS.WebApp.Helpers.msgs;
using GApp.Core.Extentions;

namespace TrainingIS.WebApp.Helpers
{
    /// <summary>
    /// Generate ConfirmationMessage for EntityManager
    /// </summary>
    public class MsgHelper
    {
        EntityMetaDataConfiguratrion entityMetaData;
        msgManager msgManager;
        public MsgHelper(Type typeOfEntity)
        {
            entityMetaData = EntityMetaDataConfiguratrion.CreateConfigEntity(typeOfEntity);
        }

        private string DefinitArticle()
        {
            if(this.entityMetaData.entityMetataDataAttribute?.isMale == true)
            {
                if(this.entityMetaData.CultureInfo.TwoLetterISOLanguageName.ToLower() == "fr")
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

       private string OfTheArticle()
        {
            if (this.DefinitArticle() == "le")
                return "du";
            if (this.DefinitArticle() == "la")
                return "de la";
            return "of the";

        }

        private string UndefindedArticle()
        {
            if (this.entityMetaData.entityMetataDataAttribute?.isMale == true)
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

        public void Create(Dictionary<string, string> msg)
        {
            string oneEntityName = this.UndefindedArticle() + " " + this.entityMetaData.entityMetataDataAttribute?.SingularName;
            msg["Create_Title"] = string.Format(msgManager.CreateTitle, oneEntityName);
        }

        public void Edit(Dictionary<string, string> msg)
        {
            string oneEntityName = this.OfTheArticle() + " " + this.entityMetaData.entityMetataDataAttribute?.SingularName;
            msg["Edit_Title"] = string.Format(msgManager.Edit_Title, oneEntityName).ToLower().FirstLetterToUpperCase();
        }

        public void Index(Dictionary<string, string> msg)
        {
            msg["Index_Title"] = string.Format(msgManager.Manager_of, this.entityMetaData.entityMetataDataAttribute?.PluralName)
                .ToLower()
                .FirstLetterToUpperCase();
        }

        public void Details(Dictionary<string, string> msg)
        {
            //msg["Index_Title"] = string.Format(msgManager.Manager_of, this.entityMetaData.entityMetataDataAttribute?.PluralName)
            //    .ToLower()
            //    .FirstLetterToUpperCase();
        }

        public void Delete(Dictionary<string, string> msg)
        {
            string oneEntityName = this.OfTheArticle() + " " + this.entityMetaData.entityMetataDataAttribute?.SingularName;
            msg["Delete_Title"] = string.Format(msgManager.Delete_Title, oneEntityName).ToLower().FirstLetterToUpperCase();
        }
    }
}