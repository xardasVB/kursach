using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Helpers
{
    public static class PagingHelper
    {
        public static string PageLinks(this HtmlHelper html,
            int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            if (totalPages > 13)
            {
                for (int i = 1; i <= (currentPage <= 8 ? 11 : 3); i++)
                    result.AppendLine(CreatePageLink(i, currentPage, pageUrl));

                if (currentPage > 8 && currentPage < totalPages - 5)
                {
                    result.AppendLine(CreateTripleDot());
                    for (int i = currentPage - 3; i <= currentPage + 3; i++)
                        result.AppendLine(CreatePageLink(i, currentPage, pageUrl));
                }
                else if (currentPage >= totalPages - 5)
                {
                    result.AppendLine(CreateTripleDot());
                    for (int i = totalPages - 8; i <= totalPages; i++)
                        result.AppendLine(CreatePageLink(i, currentPage, pageUrl));
                }

                if (currentPage < totalPages - 5)
                {
                    result.AppendLine(CreateTripleDot());
                    result.AppendLine(CreatePageLink(totalPages, currentPage, pageUrl));
                }
            }
            else
                for (int i = 1; i <= totalPages; i++)
                    result.AppendLine(CreatePageLink(i, currentPage, pageUrl));


            TagBuilder tagDiv = new TagBuilder("div");
            TagBuilder tagUl = new TagBuilder("ul");

            tagUl.InnerHtml = result.ToString();
            tagUl.AddCssClass("pagination");
            tagDiv.InnerHtml = tagUl.ToString();

            return tagDiv.ToString();
        }

        public static string CreatePageLink(int pageNumber, int currentPage, Func<int, string> pageUrl)
        {
            TagBuilder tagLi = new TagBuilder("li");
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(pageNumber));
            tag.InnerHtml = pageNumber.ToString();
            if (pageNumber == currentPage)
                tagLi.AddCssClass("active");
            tagLi.InnerHtml = tag.ToString();
            return tagLi.ToString();
        }

        public static string CreateTripleDot()
        {
            TagBuilder tagLi = new TagBuilder("li");
            tagLi.InnerHtml = "<span>...</span>";
            return tagLi.ToString();
        }
    }
}