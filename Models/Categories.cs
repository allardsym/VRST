using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRCT;

namespace VRCT
{
	class Categories
	{
		private List<Category> categoryList = new List<Category>();
		public List<Category> GetCategories()
		{
			categoryList.Add(new Category("Legs"));
			categoryList.Add(new Category("Knee"));
			categoryList.Add(new Category("Hands"));
			categoryList.Add(new Category("Spine"));
			categoryList.Add(new Category("Shoulder"));
			categoryList.Add(new Category("Balance"));
			categoryList.Add(new Category("Elbow"));
			categoryList.Add(new Category("Wrist"));
			categoryList.Add(new Category("Hand/eye coordination"));
			categoryList.Add(new Category("Cognitive"));
			categoryList.Add(new Category("Upper body"));
			categoryList.Add(new Category("Focus"));
			categoryList.Add(new Category("Hip/Knee flexion"));
			categoryList.Add(new Category("Weight shifting"));
			categoryList.Add(new Category("Thighs"));
			categoryList.Add(new Category("Lower Limb"));
			categoryList.Add(new Category("Lungs"));
			categoryList.Add(new Category("Calming effect on the body and mind"));
			return categoryList;
		}
	}
}
