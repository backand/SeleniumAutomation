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
        [TestMethod, Timeout(360000)]
        public void NewObjectAddDeleteFields()
        {
            var modelName = "itai";
            var page = ApplicationsPage.LeftMenu.FetchPage<NewObjectPage>(modelName);
            KeyValuePair<string, FieldType>[] fields =
            {
                new KeyValuePair<string, FieldType>("StringField", FieldType.String),
                new KeyValuePair<string, FieldType>("BooleanField", FieldType.Boolean),
                new KeyValuePair<string, FieldType>("DatetimeField", FieldType.DateTime)
            };
            var modelPage = page.SubmitObject(modelName, fields);
            modelPage = modelPage.ValidateAndUpdate();
            ValidateRow(modelPage, modelName, true, fields);

            // Delete field
            var field = fields.First();
            var rectangle = modelPage.GetModelRectangle(modelName);
            var fieldRow = rectangle.GetFieldRow(field.Key, field.Value);
            fieldRow.Edit().Delete();
            modelPage = modelPage.ValidateAndUpdate();
            ValidateRow(modelPage, modelName, false, field);
        }

        [TestMethod, Timeout(360000)]
        public void NewField()
        {
            var modelPage = ApplicationsPage.LeftMenu.FetchPage<ModelPage>();
            const string fieldName = "itaiName";
            const string modelName = "users";
            var field = new KeyValuePair<string, FieldType>(fieldName, FieldType.String);
            modelPage.GetModelRectangle(modelName).New().Name(field.Key).Type(field.Value).Add().Close();
            modelPage = modelPage.ValidateAndUpdate();
            ValidateRow(modelPage, modelName, true, field);
        }

        [TestMethod, Timeout(360000)]
        public void DeleteField()
        {
            var modelName = "users";
            var fieldName = "email";
            var field = new KeyValuePair<string, FieldType>(fieldName, FieldType.String);
            var modelPage = ApplicationsPage.LeftMenu.FetchPage<ModelPage>();
            var rectangle = modelPage.GetModelRectangle(modelName);
            var fieldRow = rectangle.GetFieldRow(field.Key, field.Value);
            fieldRow.Edit().Delete();

            modelPage = modelPage.ValidateAndUpdate();
            ValidateRow(modelPage, modelName, false, field);
        }

        private static void ValidateRow(ModelPage modelPage, string modelName, bool exist,
            params KeyValuePair<string, FieldType>[] fields)
        {
            var rectangle = modelPage.GetModelRectangle(modelName);
            foreach (var field in fields)
            {
                var fieldRow = rectangle.GetFieldRow(field.Key, field.Value);
                Assert.AreEqual(exist, fieldRow != null);
            }
        }
    }
}