using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Common.Utils
{
    public class ClassCodeGenerator
    {
        /// <summary>
        /// Create next class code
        /// </summary>
        /// <param name="same_level_max_class_code">Max child class code of same parent class sub child classes.</param>
        /// <param name="level_depth">child class level depth</param>
        /// <returns></returns>
        public static string CreateNextClassCode(string same_level_max_class_code, int child_code_level_depth)
        {

            if ((same_level_max_class_code.Length / Global.class_code_length_every_level) != child_code_level_depth)
            {
                same_level_max_class_code = same_level_max_class_code.Substring(0, Global.class_code_length_every_level * child_code_level_depth);
            }

            string parent_code = same_level_max_class_code.Substring(0, (child_code_level_depth - 1) * 8);
            string max_child_code = same_level_max_class_code.Substring((child_code_level_depth - 1) * 8);
            return parent_code + FillNumberToLevelLength(Convert.ToInt32(max_child_code) + 1);
        }

        /// <summary>
        /// Create next class code
        /// </summary>
        /// <param name="same_level_max_class_code">Max child class code of same parent class sub child classes.</param>
        /// <param name="level_depth">child class level depth</param>
        /// <returns></returns>
        public static string UpdateClassCode(string currentCode, int order)
        {

            var level = currentCode.Length / Global.class_code_length_every_level;

            var code = currentCode.Substring(0, Global.class_code_length_every_level * (level - 1));

            return code + FillNumberToLevelLength(order);
        }
        /// <summary>
        /// Create first child class code, format is {parent_class_code}0000001
        /// </summary>
        /// <param name="parent_class_code">parent class code</param>
        /// <returns></returns>
        public static string CreateFirstChildClassCode(string parent_class_code)
        {
            return parent_class_code + FillNumberToLevelLength(1);
        }

        public static string CreateZeroChildClassCode(string parent_class_code)
        {
            return parent_class_code + FillNumberToLevelLength(0);
        }

        public static string CreateRootClassCode(long root_class_id)
        {
            return FillNumberToLevelLength(root_class_id);
        }

        /// <summary>
        /// Create class list item name with pre symbol
        /// </summary>
        /// <param name="item_text">class name</param>
        /// <param name="level_depth">level depth</param>
        /// <returns></returns>
        public static string CreateClassListItemText(string item_text, int level_depth)
        {
            return CreateClassListItemPreSymbol(level_depth) + item_text;
        }

        /// <summary>
        /// Fill number string to level length by using charactor 0
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string FillNumberToLevelLength(long number)
        {
            string num = Math.Abs(number).ToString();
            if (num.Length > Global.class_code_length_every_level)
            {
                return num;
            }
            else
            {
                var loop = Global.class_code_length_every_level - num.Length;
                for (int i = 0; i < loop; i++)
                {
                    num = "0" + num;
                }
            }
            return num;
        }

        /// <summary>
        /// Create additional pre symbol for class item
        /// </summary>
        /// <param name="level_depth">level depth</param>
        /// <returns></returns>
        private static string CreateClassListItemPreSymbol(int level_depth)
        {
            string Symbol = "|-";
            for (int i = 1; i < level_depth; i++)
            {
                Symbol += "----";
            }
            return Symbol;
        }
    }
}
