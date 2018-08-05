using GApp.Core.MetaDatas.ReadConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingIS.WebApp.Manager.Views.msgs;

namespace TrainingIS.WebApp.Helpers
{
    /// <summary>
    /// Generate ConfirmationMessage for EntityManager
    /// </summary>
    public class MsgViews
    {
        EntityMetaDataConfiguratrion entityMetaData;
        msgManager msgManager;
        public MsgViews(Type typeOfEntity)
        {
            entityMetaData = EntityMetaDataConfiguratrion.CreateConfigEntity(typeOfEntity);
        }

        public string DefinitArticle()
        {
            if(this.entityMetaData.entityMetataData?.isMale == true)
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

        public string OfTheArticle()
        {
            if (this.DefinitArticle() == "le")
                return "du";
            if (this.DefinitArticle() == "la")
                return "de la";
            return "of the";

        }

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

        public void Create(Dictionary<string, string> msg)
        {
            string oneEntityName = this.OfUndefindedArticle() + " " + this.entityMetaData.entityMetataData?.SingularName?.ToLower();
            msg["Create_Title"] = string.Format(msgManager.CreateTitle, oneEntityName);
        }

        

        public void Edit(Dictionary<string, string> msg)
        {
            string oneEntityName = this.OfTheArticle() + " " + this.entityMetaData.entityMetataData?.SingularName;
            msg["Edit_Title"] = string.Format(msgManager.Edit_Title, oneEntityName).ToLower().FirstLetterToUpperCase();
        }

        public void Index(Dictionary<string, string> msg)
        {
            msg["Index_Title"] = string.Format(msgManager.Manager_of, this.entityMetaData.entityMetataData?.PluralName)
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
            string oneEntityName = this.OfTheArticle() + " " + this.entityMetaData.entityMetataData?.SingularName;
            msg["Delete_Title"] = string.Format(msgManager.Delete_Title, oneEntityName).ToLower().FirstLetterToUpperCase();
        }
    }
}