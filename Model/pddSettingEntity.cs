using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reptile
{
    public class pddSettingEntity
    {
        public string siteUrl { set; get; }
        public string detailUrl { set; get; }

        public List<CategoryOne> categoryList { get; set; }

    }

    public class CategoryOne : Category
    {
        public List<CategoryTwo> goodsCategory { get; set; }
    }

    public class CategoryTwo : Category
    {
        public List<Category> goodsCategory { get; set; }
    }
}
