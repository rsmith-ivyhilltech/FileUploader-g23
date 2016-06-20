using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Microsoft.Office.Interop.Excel;
using Microsoft.SharePoint.Client;
using System.Security;

namespace FileLoader.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Gets named range from an Excel Workbook
        /// </summary>
        /// <param name="activeWorkbook"></param>
        /// <returns></returns>
        public static List<Name> GetNamedRanges(this Workbook activeWorkbook)
        {
            List<Name> namedRanges = new List<Name>();
            Name name;
            for (int i = 1; i < activeWorkbook.Names.Count; i++)
            {
                name = activeWorkbook.Names.Item(i);
                namedRanges.Add(name);
            }     

            return namedRanges;
        }       

        /// <summary>
        /// Returns a value indicating whether the field value for the specified item is null or empty
        /// </summary>
        /// <param name="item"></param>
        /// <param name="DisplayName"></param>
        /// <returns></returns> 
        public static string GetListFieldInternalName(this List list, string DisplayName,LoggedInUser user)
        {
            try                
            {
                var internalName = string.Empty;
                using (var clientContext = new ClientContext(System.Configuration.ConfigurationManager.AppSettings["SiteURL"]))
                {
                    SecureString securePassWd = new SecureString();
                    foreach (var c in user.Password.ToCharArray())
                    {
                        securePassWd.AppendChar(c);
                    }
                    clientContext.Credentials = new SharePointOnlineCredentials(user.UserName, securePassWd);

                    FieldCollection listFields = list.Fields;

                    // //Lambda Expression not working, use alternative for now. Fix it
                    //clientContext.Load(listFields, fields => fields.Include(field => field.InternalName));                       
                    clientContext.ExecuteQuery();
                    
                   foreach(var f in listFields)
                    {
                        if (f.Title == DisplayName)
                            internalName=f.InternalName;                       
                    }

                }

                return internalName;
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException(string.Format("The field '{0}' could not be found in list '{1}'", DisplayName, list), ex);
            }
        }

        /// <summary>
        /// Returns a value indicating whether the field value for the specified item is null or empty
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldInternalName"></param>
        /// <returns></returns> 
        public static bool ValueIsNull(this ListItem item, string fieldInternalName)
        {
            try
            {
                return item[fieldInternalName] == null || item[fieldInternalName].ToString() == string.Empty;
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException(string.Format("The field '{0}' could not be found in list '{1}'", fieldInternalName, item.ParentList), ex);
            }
        }

        public static int GetLookupFielId(this List list, string lookupField, string lookupValue, LoggedInUser user)
        {
            try {
                     int id = 0;

                    using (var clientContext = new ClientContext(System.Configuration.ConfigurationManager.AppSettings["SiteURL"]))
                    {
                        SecureString securePassWd = new SecureString();
                        foreach (var c in user.Password.ToCharArray())
                        {
                            securePassWd.AppendChar(c);
                        }
                        clientContext.Credentials = new SharePointOnlineCredentials(user.UserName, securePassWd);

                        clientContext.Load(list, oList => oList.DefaultViewUrl);
                        CamlQuery query = CamlQuery.CreateAllItemsQuery(100);

                        Microsoft.SharePoint.Client.ListItemCollection items = list.GetItems(query);
                        clientContext.Load(items);
                        clientContext.ExecuteQuery();

                        foreach (ListItem item in items)
                        {
                            if (item[lookupField].ToString() == lookupValue)
                            { 
                                id = Convert.ToInt32(item["ID"]);
                                break;
                            }
                        }        
                 
                        //Not working, Fix it use alternative for now.
                        /*FieldLookupValue lookupColumnValue = null;
                        CamlQuery camlQueryForItem = new CamlQuery();
                        camlQueryForItem.ViewXml=
                            string.Format(@"<View>
                              <Query>
                                  <Where>
                                     <Eq>
                                         <FieldRef Name='{0}'/>
                                         <Value Type='{1}'>{2}</Value>
                                     </Eq>
                                   </Where>
                               </Query>
                             </View>", lookupField, lookupFieldType, lookupValue);

                        ListItemCollection listItems = list.GetItems(camlQueryForItem);
                        clientContext.Load(listItems, items => items.Include
                                                          (listItem => listItem["ID"],
                                                           listItem => listItem[lookupField]));
                        clientContext.ExecuteQuery();

                        if (listItems != null)
                        {
                            ListItem item = listItems[0];
                            lookupColumnValue = new FieldLookupValue();
                            lookupColumnValue.LookupId = Convert.ToInt32(item["ID"].ToString());
                        }*/                      

                    }
                    return id;  
              }       
           
         catch(Exception ex){
                  throw new ArgumentOutOfRangeException(string.Format("The item '{0}' could not be found in list '{1}'", lookupValue, list), ex);
            }
        }      

    }
}
