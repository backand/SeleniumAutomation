using System.Collections.Generic;
using System.Linq;
using Core.Dialogs;
using Infrastructure.Object;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Attributes;
using Tests.Base;

namespace Tests
{
    [CreateApp]
    [TestClass]
    public class NewObjectTests : BackandTestClassBase
    {
        [TestMethod]
        public void NewObjectAddDeleteFields()
        {
            string modelName = "itai";
            NewObjectPage page = ApplicationsPage.LeftMenu.FetchPage<NewObjectPage>(modelName);
            KeyValuePair<string, FieldType>[] fields =
            {
                new KeyValuePair<string, FieldType>("StringField", FieldType.String),
                new KeyValuePair<string, FieldType>("BooleanField", FieldType.Boolean),
                new KeyValuePair<string, FieldType>("DatetimeField", FieldType.DateTime)
            };
            ModelPage modelPage = page.SubmitObject(modelName, fields);
            modelPage = modelPage.ValidateAndUpdate();
            ValidateRow(modelPage, modelName, true, fields);

            // Delete field
            KeyValuePair<string, FieldType> field = fields.First();
            ModelRectangle rectangle = modelPage.GetModelRectangle(modelName);
            FieldRow fieldRow = rectangle.GetFieldRow(field.Key, field.Value);
            fieldRow.Edit().Delete();
            modelPage = modelPage.ValidateAndUpdate();
            ValidateRow(modelPage, modelName, false, field);
        }

        [TestMethod]
        public void NewField()
        {
            ModelPage modelPage = ApplicationsPage.LeftMenu.FetchPage<ModelPage>();
            const string fieldName = "itaiName";
            const string modelName = "users";
            KeyValuePair<string, FieldType> field = new KeyValuePair<string, FieldType>(fieldName, FieldType.String);
            modelPage.GetModelRectangle(modelName).New().Name(field.Key).Type(field.Value).Add().Close();
            modelPage = modelPage.ValidateAndUpdate();
            ValidateRow(modelPage, modelName, true, field);
        }

        [TestMethod]
        public void DeleteField()
        {
            string modelName = "users";
            string fieldName = "email";
            KeyValuePair<string, FieldType> field = new KeyValuePair<string, FieldType>(fieldName, FieldType.String);
            ModelPage modelPage = ApplicationsPage.LeftMenu.FetchPage<ModelPage>();
            ModelRectangle rectangle = modelPage.GetModelRectangle(modelName);
            FieldRow fieldRow = rectangle.GetFieldRow(field.Key, field.Value);
            fieldRow.Edit().Delete();

            modelPage = modelPage.ValidateAndUpdate();
            ValidateRow(modelPage, modelName, false, field);
        }

        private static void ValidateRow(ModelPage modelPage, string modelName, bool exist,
            params KeyValuePair<string, FieldType>[] fields)
        {
            ModelRectangle rectangle = modelPage.GetModelRectangle(modelName);
            foreach (KeyValuePair<string, FieldType> field in fields)
            {
                FieldRow fieldRow = rectangle.GetFieldRow(field.Key, field.Value);
                Assert.AreEqual(exist, fieldRow != null);
            }
        }
    }
}