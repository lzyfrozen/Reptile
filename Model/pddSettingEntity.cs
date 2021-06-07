using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reptile
{
    public class PddSettingEntity
    {
        public string siteUrl { set; get; }
        public string detailUrl { set; get; }

        public List<CategoryOne> optList { get; set; } = new List<CategoryOne>();

    }

    public class CategoryOne : Category
    {
        public List<CategoryTwo> children { get; set; } = new List<CategoryTwo>();
    }

    public class CategoryTwo : Category
    {
        public List<CategoryThree> children { get; set; } = new List<CategoryThree>();
    }

    public class CategoryThree : Category
    {
        public string[] children { get; set; } = new string[] { };
    }
}
