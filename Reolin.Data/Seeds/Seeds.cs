using Reolin.Data.Domain;
using System.Linq;

namespace Reolin.Data.Seeds
{
    public static class Seeds
    {
        #region InitalJobCategoryItems
        private static string[] InitialJobCategoryItems = new string[]
            {
                "automotive & machinary",
                "home & office",
                "computer & Electronics",
                "food & beverage",
                "Media, Entertainment, Art",
                "Medical & health",
                "Legal & Financial",
                "clothing & beauty",
                "Travel & Transportation",
                "Pets & Livestock",
                "Kids",
                "Sport & Hobbies",
                "Nonprofit",
                "Agricultural & Gardening",
                "Industial",
                "Advertising",
                "Constraction",
                "Cermonie & wedding",
                "Barganing & Dealing",
                "Educational",
                "Hypermarkets & Shoppingmal"
        };
        #endregion


        internal static void AddDefaultNetworks(this DataContext source)
        {
            var oldNetworks = source.SocialNetworks;
            foreach(var item in oldNetworks)
            {
                source.SocialNetworks.Remove(item);
            }
            source.SaveChanges();

            SocialNetwork[] networks = new SocialNetwork[]
            {
                new SocialNetwork() { Name =  "Facebook", IconPath = "/files/facebook.png" },
                new SocialNetwork() { Name =  "gmail", IconPath = "/files/gmail.png" },
                new SocialNetwork() { Name =  "telegram", IconPath = "/files/telegrm.png" }
            };

            foreach (var item in networks)
            {
                if (!source.SocialNetworks.Any(s => s.Name == item.Name))
                {
                    source.SocialNetworks.Add(item);
                }
            }

            source.SaveChanges();
        }

        internal static void AddDefaultJobCategories(this DataContext source)
        {
            foreach (var item in InitialJobCategoryItems)
            {
                if (!source.JobCategories.Any(jc => jc.Name == item))
                {
                    source.JobCategories.Add(new JobCategory()
                    {
                        Name = item
                    });
                }
            }

            source.SaveChanges();
        }

        internal static void AddDefaultJobSubCategoies(this DataContext source)
        {
            string[] items = new[] { "Sale", "Manufacture", "Services" };
            foreach (var cat in items)
            {
                if (!source.JobCategories.Any(j => j.Name == cat && j.IsSubCategory))
                {
                    source.JobCategories.Add(new JobCategory()
                    {
                        Name = cat,
                        IsSubCategory = true
                    });
                }
            }

            source.SaveChanges();
        }
    }
}
