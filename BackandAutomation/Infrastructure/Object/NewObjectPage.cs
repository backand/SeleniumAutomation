using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Dialogs;
using Infrastructure.Base;

namespace Infrastructure.Object
{
    [BackandPageType(LeftMenuOption.Objects, LeftMenuOption.DynamicObject)]
    public class NewObjectPage : BackandApplicationsBasePage
    {
        public NewObjectPage(DriverUser driver) : base(driver)
        {
        }

        public ModelPage SubmitObject(string objectName, List<KeyValuePair<string, FieldType>> fields)
        {
            Func<AddFieldDialog, KeyValuePair<string, FieldType>, AddFieldDialog> submitField =
                (dialog, pair) => dialog.Name(pair.Key).Type(pair.Value).Add();
            
            AddFieldDialog addFieldDialog = new NewObjectDialog(Driver).SetName(objectName).AddObject();
            fields.Aggregate(addFieldDialog, (current, pair) => submitField(current, pair)).Close();

            return new ModelPage(this);
        }
    }
}