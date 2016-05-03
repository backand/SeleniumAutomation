using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace Core
{
    public class OrCondition : By
    {
        public OrCondition(params By[] findBys)
        {
            FindBys = findBys;
        }

        public By[] FindBys { get; set; }

        public override IWebElement FindElement(ISearchContext context)
        {
            foreach (var findBy in FindBys)
            {
                IWebElement element;
                if (context.TryFindElement(findBy, out element))
                    return element;
            }
            return null;
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
            => new ReadOnlyCollection<IWebElement>(FindBys.SelectMany(context.FindElements).ToList());
    }
}