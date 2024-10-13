using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp.MVVM.Model
{
    public class UserSearchHistory
    {
        public Dictionary<string, int> CategorySearchCount { get; set; }
        public Dictionary<DateTime, int> DateSearchCount { get; set; }

        public UserSearchHistory()
        {
            CategorySearchCount = new Dictionary<string, int>();
            DateSearchCount = new Dictionary<DateTime, int>();
        }

        public void RecordSearch(string category, DateTime date)
        {
            if (CategorySearchCount.ContainsKey(category))
            {
                CategorySearchCount[category]++;
            }
            else
            {
                CategorySearchCount[category] = 1;
            }

            if (DateSearchCount.ContainsKey(date))
            {
                DateSearchCount[date]++;
            }
            else
            {
                DateSearchCount[date] = 1;
            }
        }

        public (string mostSearchedCategory, DateTime mostSearchedDate) GetMostSearchedCategoryAndDate()
        {
            var mostSearchedCategory = CategorySearchCount.OrderByDescending(kvp => kvp.Value).FirstOrDefault().Key;
            var mostSearchedDate = DateSearchCount.OrderByDescending(kvp => kvp.Value).FirstOrDefault().Key;

            return (mostSearchedCategory, mostSearchedDate);
        }

        public IEnumerable<string> GetTopCategories()
        {
            return CategorySearchCount.OrderByDescending(kvp => kvp.Value)
                                      .Take(3)
                                      .Select(kvp => kvp.Key);
        }

        public IEnumerable<DateTime> GetTopDates()
        {
            return DateSearchCount.OrderByDescending(kvp => kvp.Value)
                                  .Take(3)
                                  .Select(kvp => kvp.Key);
        }
    }
}
